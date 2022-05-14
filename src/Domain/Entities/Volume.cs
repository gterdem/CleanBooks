using CleanBooks.Domain.Entities.VolumeInfoData;

namespace CleanBooks.Domain.Entities;

public class Volume
{
    public Guid Id { get; init; }
    public string ETag { get; init; }
    public string GApiVolumeId { get; init; }
    public string Kind { get; init; }
    public string SelfLink { get; init; }

    public VolumeInfo VolumeInfo { get; private set; }

    protected Volume() { }

    public Volume(Guid id, string eTag, string gApiVolumeId, string kind, string selfLink, VolumeInfo volumeInfo)
    {
        Id = id;
        ETag = eTag;
        GApiVolumeId = gApiVolumeId;
        Kind = kind;
        SelfLink = selfLink;
        VolumeInfo = volumeInfo;
    }

    public void AddVolumeInfo(
        Guid id,
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
        Dimentions dimentions
    )
    {
        VolumeInfo = new VolumeInfo(
            id: Guid.NewGuid(),
            title: title,
            subtitle: subtitle,
            publisher: publisher,
            description: description,
            publishedDate: publishedDate,
            pageCount: pageCount,
            printType: printType,
            maturityRating: maturityRating,
            allowAnonLogging: allowAnonLogging,
            contentVersion: contentVersion,
            language: language,
            previewLink: previewLink,
            infoLink: infoLink,
            canonicalVolumeLink: canonicalVolumeLink,
            averageRating: averageRating,
            ratingsCount: ratingsCount,
            comicsContent: comicsContent,
            mainCategory: mainCategory,
            samplePageCount: samplePageCount,
            printedPageCount: printedPageCount,
            authors: authors,
            categories: categories,
            readingModes: readingModes,
            panelizationSummary: panelizationSummary,
            imageLinks: imageLinks,
            dimentions: dimentions
        );
    }
}