namespace CleanBooks.Domain.Entities.VolumeInfoData;

public class PanelizationSummary: ValueObject
{
    public bool? ContainsEpubBubbles { get; init; }
    public bool? ContainsImageBubbles { get; init; }
    public string EpubBubbleVersion { get; init; }
    public string ImageBubbleVersion { get; init; }

    public PanelizationSummary()
    {
    }

    public PanelizationSummary(string epubBubbleVersion, string imageBubbleVersion, bool? containsEpubBubbles,
        bool? containsImageBubbles)
    {
        ContainsEpubBubbles = containsEpubBubbles;
        ContainsImageBubbles = containsImageBubbles;
        EpubBubbleVersion = epubBubbleVersion;
        ImageBubbleVersion = imageBubbleVersion;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ContainsEpubBubbles;
        yield return ContainsImageBubbles;
        yield return EpubBubbleVersion;
        yield return ImageBubbleVersion;
    }
}