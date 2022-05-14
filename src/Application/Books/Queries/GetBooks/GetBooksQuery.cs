using System.ComponentModel.DataAnnotations;
using System.Text;
using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using CleanBooks.Application.Common;
using CleanBooks.Application.Common.Interfaces;
using CleanBooks.Application.Common.Mappings;
using CleanBooks.Domain.Entities;
using CleanBooks.Domain.Entities.VolumeInfoData;
using CleanBooks.Domain.Enums;
using CleanBooks.Domain.Specifications;
using Google.Apis.Books.v1;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanBooks.Application.Books.Queries.GetBooks;

public class GetBooksQuery : IRequest<VolumesDto>
{
    public string Q { get; set; }
    public string? VolumeId { get; set; }
    public string? LangRestrict { get; set; }
    public string? Filter { get; set; }
    public OrderByType OrderBy { get; set; } = OrderByType.relevance;
    public int? StartIndex { get; set; } = 0;
    [Range(0, 40)] public int? MaxResults { get; set; } = 10;

    protected SearchQuery Query
    {
        get
        {
            return new SearchQuery(Q);
        }
    }

    public class SearchQuery
    {
        private readonly string _searchTerms;
        public string QValue { get; }
        public string InTitle { get; }
        public string InAuthor { get; }
        public string InPublisher { get; }
        public string Subject { get; }

        public SearchQuery(string searchTerms)
        {
            _searchTerms = searchTerms;
            QValue = QueryUtil.GetStringBetweenCharacters(_searchTerms, '=', '+');
            {
                if (QValue.Contains(':'))
                {
                    QValue = String.Empty;
                }
            }
            var termCount = _searchTerms.Count(q => q == ':');

            if (_searchTerms.Contains("intitle:"))
            {
                if (termCount == 1) //q=intitle:starwars
                {
                    InTitle = _searchTerms.Split("intitle:")[1];
                }
                else if
                    (termCount > 1 &&
                     QValue == string
                         .Empty) //q=intitle:star wars+inauthor:lucas || q=star wars+intitle:star wars+inauthor:lucas
                {
                    try
                    {
                        InTitle = QueryUtil.GetStringBetweenStrings(_searchTerms, "intitle:", "+");
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        InTitle = QueryUtil.GetStringBetweenStrings(_searchTerms, "+", "intitle:");
                    }
                }
            }

            if (_searchTerms.Contains("inauthor:"))
            {
                if (termCount == 1) //q=intitle:starwars
                {
                    InAuthor = _searchTerms.Split("inauthor:")[1];
                }
                else if
                    (termCount > 1 &&
                     QValue == string
                         .Empty) //q=intitle:star wars+inauthor:lucas || q=star wars+intitle:star wars+inauthor:lucas
                {
                    //TODO: needs better approach
                    try
                    {
                        InAuthor = QueryUtil.GetStringBetweenStrings(_searchTerms, "inauthor:", "+");
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        InAuthor = QueryUtil.GetStringBetweenStrings(_searchTerms, "+", "inauthor:");
                    }
                }
            }

            if (searchTerms.Contains("inpublisher:"))
            {
                if (termCount == 1)
                {
                    InPublisher = _searchTerms.Split("inpublisher:")[1];
                }
                else if (termCount > 1 && QValue == string.Empty)
                {
                    InPublisher = QueryUtil.GetStringBetweenStrings(_searchTerms, "inpublisher:", "+");
                }
            }

            if (searchTerms.Contains("subject:"))
            {
                if (termCount == 1)
                {
                    Subject = _searchTerms.Split("subject:")[1];
                }
                else if (termCount > 1 && QValue == string.Empty)
                {
                    Subject = QueryUtil.GetStringBetweenStrings(_searchTerms, "subject:", "+");
                }
            }
        }

        public string GetSearchQuery()
        {
            return $"q={_searchTerms}";
        }
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.Append(Query.GetSearchQuery());

        if (VolumeId != null)
        {
            builder.Append($"&volumeId={VolumeId}");
        }

        return builder.ToString();
    }
}

public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, VolumesDto>
{
    private readonly ILogger<GetBooksQueryHandler> _logger;
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetBooksQueryHandler(ILogger<GetBooksQueryHandler> logger, IApplicationDbContext dbContext, IMapper mapper)
    {
        _logger = logger;
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<VolumesDto> Handle(GetBooksQuery request, CancellationToken cancellationToken = default)
    {
        //TODO: Check db based on query request
        var query = request.ToString();

        VolumesDto volumes = await GetBooksFromDatabaseAsync(request, cancellationToken);

        if (volumes == null)
        {
            volumes = await GetBooksFromGoogleServiceAsync(request, cancellationToken);
            _logger.LogInformation("Request completed from Google BookService with query:{0} ...", request.ToString());
            var newlyAddedBooks = await InsertToLocalDataStoreAsync(volumes, cancellationToken);
            var dto = new VolumesDto()
            {
                Books = _mapper.Map<IList<BookDto>>(newlyAddedBooks),
                Kind = volumes.Kind,
                ETag = volumes.ETag,
                TotalItems = volumes.TotalItems //TODO: db query result
            };
            _logger.LogInformation("Returning Book Dto list...");
            return dto;
        }

        return volumes;
    }

    private async Task<VolumesDto> GetBooksFromDatabaseAsync(GetBooksQuery request, CancellationToken cancellationToken)
    {
        var result = await _dbContext.Books
            .AsNoTracking()
            .WithSpecification(new OrderBySpecification(request.OrderBy))
            .WithSpecification(new BooksByPaginatedSpec(request.StartIndex.Value, request.MaxResults.Value))
            .ProjectToListAsync<BookDto>(_mapper.ConfigurationProvider);

        return new VolumesDto() {Books = result, TotalItems = (await _dbContext.Books.CountAsync(cancellationToken)), Kind = "books#volume"};
    }

    private async Task<List<Book>> InsertToLocalDataStoreAsync(VolumesDto volumes, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Inserting {0} books to local datastore...", volumes.Books.Count);
        //TODO: Check if the book exist - do it performant

        List<Book> bookEntityList = new List<Book>();
        foreach (BookDto bookDto in volumes.Books)
        {
            bookEntityList.Add(CreateNewBookFromBookDto(bookDto));
        }

        await _dbContext.Books.AddRangeAsync(bookEntityList, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Inserting books to local datastore completed...");
        return bookEntityList;
    }

    private Book CreateNewBookFromBookDto(BookDto bookDto)
    {
        //Create new Book
        VolumeInfo volumeInfo = CreateVolumeInfoFromDto(bookDto.VolumeInfo);

        return new Book(
            id: Guid.NewGuid(), eTag: bookDto.ETag, gApiVolumeId: bookDto.GApiVolumeId, kind: bookDto.Kind,
            selfLink: bookDto.SelfLink,
            volumeInfo: volumeInfo
        );
    }

    private VolumeInfo CreateVolumeInfoFromDto(VolumeInfoDto vinfo)
    {
        PrintType printType;
        int pageCount = vinfo.PageCount ?? 0;
        bool allowAnonLogging = vinfo.AllowAnonLogging ?? false;
        Enum.TryParse(vinfo.PrintType, out printType);

        return new VolumeInfo(id: Guid.NewGuid(),
            title: vinfo.Title, subtitle: vinfo.Subtitle, publisher: vinfo.Publisher,
            publishedDate: vinfo.PublishedDate,
            pageCount: pageCount, printType: printType, maturityRating: vinfo.MaturityRating,
            allowAnonLogging: allowAnonLogging,
            contentVersion: vinfo.ContentVersion, language: vinfo.Language, previewLink: vinfo.PreviewLink,
            infoLink: vinfo.InfoLink,
            canonicalVolumeLink: vinfo.CanonicalVolumeLink, description: vinfo.Description,
            averageRating: vinfo.AverageRating,
            ratingsCount: vinfo.RatingsCount, comicsContent: vinfo.ComicsContent, mainCategory: vinfo.MainCategory,
            samplePageCount: vinfo.SamplePageCount,
            printedPageCount: vinfo.PrintedPageCount,
            authors: vinfo.Authors.ToList(), categories: vinfo.Categories.ToList(),
            readingModes: vinfo.ReadingModes, panelizationSummary: vinfo.PanelizationSummary,
            imageLinks: vinfo.ImageLinks, dimentions: vinfo.Dimentions
        );
    }

    private async Task<VolumesDto> GetBooksFromGoogleServiceAsync(GetBooksQuery request,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Requesting volume list from Google BookService with query:{0}", request.ToString());
        BooksService service = new BooksService();
        var listRequest = service.Volumes.List(request.ToString());
        var result = await listRequest.ExecuteAsync(cancellationToken);
        return BookMapper.MapGoogleVolumesDataToVolumeDto(result);
    }
}