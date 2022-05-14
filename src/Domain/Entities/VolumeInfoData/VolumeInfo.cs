namespace CleanBooks.Domain.Entities.VolumeInfoData;

public class VolumeInfo
{
    public Guid Id { get; init; }
    public string Title { get; init; } //searchable
    public string Publisher { get; init; } //searchable
    public string Subtitle { get; init; }
    public DateTime PublishedDate { get; init; }
    public int? PageCount { get; init; }
    public string MaturityRating { get; init; }
    public bool? AllowAnonLogging { get; init; }
    public string ContentVersion { get; init; }
    public string Language { get; init; }
    public string PreviewLink { get; init; }
    public string InfoLink { get; init; }
    public string CanonicalVolumeLink { get; init; }
    public string Description { get; init; }
    public double? AverageRating { get; init; }
    public bool? ComicsContent { get; init; }
    public string MainCategory { get; init; }
    public int? SamplePageCount { get; init; }
    public int? PrintedPageCount { get; init; }
    public int? RatingsCount { get; init; }
    public PrintType PrintType { get; init; }
    public ImageLinks ImageLinks { get; init; }
    public ReadingModes ReadingModes { get; init; }
    public PanelizationSummary PanelizationSummary { get; init; }
    public Dimentions Dimentions { get; init; }
    public IList<string> Authors { get; init; } = new List<string>();
    public IList<string> Categories { get; init; } = new List<string>();
    public List<IndustryIdentifier> IndustryIdentifiers { get; init; } = new List<IndustryIdentifier>(); //searchable

    protected VolumeInfo() { }

    public VolumeInfo(Guid id,
        string title,
        string? subtitle,
        string publisher,
        string publishedDate,
        int pageCount,
        PrintType printType,
        string maturityRating,
        bool allowAnonLogging,
        string contentVersion,
        string language,
        string previewLink,
        string infoLink,
        string canonicalVolumeLink,
        string description,
        double? averageRating,
        int? ratingsCount,
        bool? comicsContent,
        string mainCategory,
        int? samplePageCount,
        int? printedPageCount,
        List<string> authors,
        List<string> categories,
        ReadingModes readingModes,
        PanelizationSummary panelizationSummary,
        ImageLinks imageLinks,
        Dimentions dimentions)
    {
        Id = id;
        Title = title;
        Publisher = publisher;
        Subtitle = subtitle;
        ComicsContent = comicsContent;
        MainCategory = mainCategory;
        SamplePageCount = samplePageCount;
        PrintedPageCount = printedPageCount;
        PublishedDate = DateTime.Parse(publishedDate);
        PageCount = pageCount;
        PrintType = printType;
        MaturityRating = maturityRating;
        AllowAnonLogging = allowAnonLogging;
        ContentVersion = contentVersion;
        Language = language;
        PreviewLink = previewLink;
        InfoLink = infoLink;
        CanonicalVolumeLink = canonicalVolumeLink;
        Description = description;
        AverageRating = averageRating;
        RatingsCount = ratingsCount;
        Authors = authors;
        Categories = categories;
        ImageLinks = imageLinks;
        ReadingModes = readingModes;
        PanelizationSummary = panelizationSummary;
        Dimentions = dimentions;
        ComicsContent = comicsContent;
        MainCategory = mainCategory;
        SamplePageCount = samplePageCount;
        PrintedPageCount = printedPageCount;
    }

    public void AddIndustryIdentifier(string type, string identifier)
    {
        IndustryIdentifiers.Add(new IndustryIdentifier(Guid.NewGuid(), type, identifier));
    }

    public void AddIndustryIdentifiers(List<(string type, string identifier)> industryIdentifiers)
    {
        foreach ((string type, string identifier) industryIdentifier in industryIdentifiers)
        {
            AddIndustryIdentifier(industryIdentifier.type, industryIdentifier.identifier);
        }
    }
}