using System.ComponentModel.DataAnnotations;
using System.Text;
using CleanBooks.Application.Common.Interfaces;
using CleanBooks.Application.Common.Mappings;
using Google.Apis.Books.v1;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanBooks.Application.Books.Queries.GetBooks;

public class GetBooksQuery : IRequest<VolumesDto>
{
    public string Q { get; set; }
    public string? VolumeId { get; set; }
    public string? LangRestrict { get; set; }
    public string? Filter { get; set; }
    public int? StartIndex { get; set; } = 0;
    [Range(0, 40)] public int? MaxResults { get; set; } = 10;
    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.Append($"q={Q}");
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

    public GetBooksQueryHandler(ILogger<GetBooksQueryHandler> logger, IApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<VolumesDto> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        //TODO: Check db based on query request

        VolumesDto volumes = await GetBooksFromGoogleServiceAsync(request, cancellationToken);
        return volumes;
    }

    private async Task<VolumesDto> GetBooksFromGoogleServiceAsync(GetBooksQuery request,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Requesting volume list from Google BookService with query:{request.ToString()}");
        BooksService service = new BooksService();
        var listRequest = service.Volumes.List(request.ToString());
        var result = await listRequest.ExecuteAsync(cancellationToken);
        return BookMapper.MapGoogleVolumesDataToVolumeDto(result);
    }
}