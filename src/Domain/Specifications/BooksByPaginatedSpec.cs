using Ardalis.Specification;
using CleanBooks.Domain.Entities;

namespace CleanBooks.Domain.Specifications;

public class BooksByPaginatedSpec : Specification<Book>
{
    public BooksByPaginatedSpec(int skip = 0, int take = 10)
    {
        Query
            .Skip(skip)
            .Take(take);
    }
}