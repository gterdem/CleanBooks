using CleanBooks.Application.Common.Mappings;
using CleanBooks.Domain.Entities;

namespace CleanBooks.Application.Books.Queries.GetBooks;

public class BookDto : IMapFrom<Book>
{
    public Guid Id { get; init; }
    public string ETag { get; init; }
    public string GApiVolumeId { get; init; }
    public string Kind { get; init; }
    public string SelfLink { get; init; }

    public VolumeInfoDto VolumeInfo { get; set; }
    //TODO: Others
}