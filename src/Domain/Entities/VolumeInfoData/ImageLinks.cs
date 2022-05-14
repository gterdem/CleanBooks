namespace CleanBooks.Domain.Entities.VolumeInfoData;

public class ImageLinks : ValueObject
{
    public string ExtraLarge { get; init; }
    public string Large { get; init; }
    public string Medium { get; init; }
    public string Small { get; init; }
    public string SmallThumbnail { get; init; }
    public string Thumbnail { get; init; }
    
    protected ImageLinks()
    {
    }

    public ImageLinks(string extraLarge, string large, string medium, string small, string smallThumbnail, string thumbnail)
    {
        ExtraLarge = extraLarge;
        Large = large;
        Medium = medium;
        Small = small;
        SmallThumbnail = smallThumbnail;
        Thumbnail = thumbnail;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return SmallThumbnail;
        yield return Thumbnail;
        yield return Small;
        yield return Medium;
        yield return Large;
        yield return ExtraLarge;
    }
}