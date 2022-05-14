namespace CleanBooks.Domain.Entities.VolumeInfoData;

public class ReadingModes : ValueObject
{
    public bool? Text { get; init; }
    public bool? Image { get; init; }

    protected ReadingModes()
    {
        
    }

    public ReadingModes(bool? text, bool? image)
    {
        Text = text;
        Image = image;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Text;
        yield return Image;
    }
}