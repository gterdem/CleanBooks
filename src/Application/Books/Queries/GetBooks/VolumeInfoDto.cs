using CleanBooks.Application.Common.Mappings;
using CleanBooks.Domain.Entities.VolumeInfoData;
using CleanBooks.Domain.Enums;
using Google.Apis.Books.v1.Data;

namespace CleanBooks.Application.Books.Queries.GetBooks;

public class VolumeInfoDto : IMapFrom<VolumeInfo>
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public string Title { get; set; }
    public string Publisher { get; set; }
    public string Subtitle { get; set; }
    public string PublishedDate { get; set; }
    public int? PageCount { get; set; }
    public string MaturityRating { get; set; }
    public bool? AllowAnonLogging { get; set; }
    public string ContentVersion { get; set; }
    public string Language { get; set; }
    public string PreviewLink { get; set; }
    public string InfoLink { get; set; }
    public string CanonicalVolumeLink { get; set; }
    public string Description { get; set; }
    public double? AverageRating { get; set; }
    public bool? ComicsContent { get; set; }
    public string MainCategory { get; set; }
    public int? SamplePageCount { get; set; }
    public int? PrintedPageCount { get; set; }
    public int? RatingsCount { get; set; }
    public string PrintType { get; set; }
    public ImageLinks ImageLinks { get; set; }
    public ReadingModes ReadingModes { get; set; }
    public PanelizationSummary PanelizationSummary { get; set; }
    public Dimentions Dimentions { get; set; }
    public IList<string> Authors { get; set; } = new List<string>();
    public IList<string> Categories { get; set; } = new List<string>();
    public List<IndustryIdentifierDto> IndustryIdentifiers { get; set; } = new List<IndustryIdentifierDto>();
}