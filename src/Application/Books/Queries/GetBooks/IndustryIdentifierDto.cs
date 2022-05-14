using CleanBooks.Application.Common.Mappings;
using CleanBooks.Domain.Entities.VolumeInfoData;

namespace CleanBooks.Application.Books.Queries.GetBooks;

public class IndustryIdentifierDto : IMapFrom<IndustryIdentifier>
{
    public Guid Id { get; set; }
    public Guid VolumeInfoId { get; set; }
    public string Type { get; set; }
    public string Identifier { get; set; }
}