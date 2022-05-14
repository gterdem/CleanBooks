using CleanBooks.Application.Books.Queries.GetBooks;
using CleanBooks.Domain.Entities.VolumeInfoData;
using Google.Apis.Books.v1.Data;

namespace CleanBooks.Application.Common.Mappings;

public static class BookMapper
{
    public static VolumesDto MapGoogleVolumesDataToVolumeDto(Volumes volumes)
    {
        // Something wrong with query
        if (volumes.TotalItems == 0)
        {
            return null;
        }
        return new VolumesDto()
        {
            Kind = volumes.Kind,
            ETag = volumes.ETag,
            TotalItems = volumes.TotalItems,
            Books = MapItemsToBooks(volumes.Items)
        };
    }

    private static IList<BookDto> MapItemsToBooks(IList<Volume> volumesItems)
    {
        List<BookDto> bookDtos = new();
        foreach (Volume volume in volumesItems)
        {
            bookDtos.Add(MapGoogleVolumeToBookDto(volume));
        }

        return bookDtos;
    }

    public static BookDto MapGoogleVolumeToBookDto(Volume volume)
    {
        return new BookDto()
        {
            Kind = volume.Kind,
            ETag = volume.ETag,
            SelfLink = volume.SelfLink,
            GApiVolumeId = volume.Id,
            VolumeInfo = MapGoogleVolumeInfoDataToDto(volume.VolumeInfo)
        };
    }

    private static VolumeInfoDto MapGoogleVolumeInfoDataToDto(Volume.VolumeInfoData volumeInfoData)
    {
        var dto = new VolumeInfoDto()
        {
            Authors = volumeInfoData.Authors,
            Categories = volumeInfoData.Categories,
            Description = volumeInfoData.Description,
            Language = volumeInfoData.Language,
            Publisher = volumeInfoData.Publisher,
            Subtitle = volumeInfoData.Subtitle,
            Title = volumeInfoData.Subtitle,
            AverageRating = volumeInfoData.AverageRating,
            ComicsContent = volumeInfoData.ComicsContent,
            ContentVersion = volumeInfoData.ContentVersion,
            InfoLink = volumeInfoData.InfoLink,
            MainCategory = volumeInfoData.MainCategory,
            MaturityRating = volumeInfoData.MaturityRating,
            PageCount = volumeInfoData.PageCount,
            PreviewLink = volumeInfoData.PreviewLink,
            PrintType = volumeInfoData.PrintType,
            PublishedDate = volumeInfoData.PublishedDate,
            RatingsCount = volumeInfoData.RatingsCount,
            AllowAnonLogging = volumeInfoData.AllowAnonLogging,
            CanonicalVolumeLink = volumeInfoData.CanonicalVolumeLink,
            PrintedPageCount = volumeInfoData.PrintedPageCount,
            SamplePageCount = volumeInfoData.SamplePageCount,
            Dimentions = MapDimentionsData(volumeInfoData.Dimensions),
            ImageLinks = MapImageLinksData(volumeInfoData.ImageLinks),
            PanelizationSummary = MapPanelizationSummaryData(volumeInfoData.PanelizationSummary),
            ReadingModes = MapReadingModesData(volumeInfoData.ReadingModes)
        };
        if (volumeInfoData.IndustryIdentifiers == null) return dto;
        foreach (var ii in volumeInfoData.IndustryIdentifiers)
        {
            dto.IndustryIdentifiers.Add(MapIndustryIdentifier(ii));
        }

        return dto;
    }

    private static ReadingModes MapReadingModesData(Volume.VolumeInfoData.ReadingModesData readingModes)
    {
        if (readingModes == null) return null;
        
        return new ReadingModes(readingModes.Text, readingModes.Image);
    }

    private static PanelizationSummary MapPanelizationSummaryData(
        Volume.VolumeInfoData.PanelizationSummaryData panelizationSummary)
    {
        if (panelizationSummary == null) return null;
        
        return new PanelizationSummary(panelizationSummary.EpubBubbleVersion, panelizationSummary.ImageBubbleVersion,
            panelizationSummary.ContainsEpubBubbles, panelizationSummary.ContainsImageBubbles);
    }

    private static ImageLinks MapImageLinksData(Volume.VolumeInfoData.ImageLinksData imageLinks)
    {
        if (imageLinks == null) return null;
        
        return new ImageLinks(imageLinks.ExtraLarge, imageLinks.Large, imageLinks.Medium, imageLinks.Small,
            imageLinks.SmallThumbnail, imageLinks.Thumbnail);
    }

    private static IndustryIdentifierDto MapIndustryIdentifier(Volume.VolumeInfoData.IndustryIdentifiersData ii)
    {
        return new IndustryIdentifierDto() {Identifier = ii.Identifier, Type = ii.Type};
    }

    private static Dimentions MapDimentionsData(Volume.VolumeInfoData.DimensionsData dimensions)
    {
        if (dimensions == null) return null;
        return new Dimentions(dimensions.Height, dimensions.Thickness, dimensions.Width);
    }
}