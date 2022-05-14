namespace CleanBooks.Domain.Entities.VolumeInfoData;

public class IndustryIdentifier
{
    public Guid Id { get; init; }
    public Guid VolumeInfoId { get; }
    public string Type { get; init; }
    public string Identifier { get; init; }

    private IndustryIdentifier()
    {
    }

    public IndustryIdentifier(Guid id, string type="", string identifier="")
    {
        Id = id;
        Type = type;
        Identifier = identifier;
    }
}