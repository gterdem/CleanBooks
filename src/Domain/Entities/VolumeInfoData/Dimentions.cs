namespace CleanBooks.Domain.Entities.VolumeInfoData;

public class Dimentions : ValueObject
{
    public string Height { get; init; }
    public string Thickness { get; init; }
    public string Width { get; init; }

    protected Dimentions()
    {
    }

    public Dimentions(string height, string thickness, string width)
    {
        Height = height;
        Thickness = thickness;
        Width = width;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Height;
        yield return Thickness;
        yield return Width;
    }
}