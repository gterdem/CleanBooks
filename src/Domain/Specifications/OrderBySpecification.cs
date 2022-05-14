using Ardalis.Specification;
using CleanBooks.Domain.Entities;

namespace CleanBooks.Domain.Specifications;

public class OrderBySpecification : Specification<Book>
{
    public OrderBySpecification(OrderByType type = OrderByType.relevance)
    {
        if (type == OrderByType.newest)
        {
            Query.OrderByDescending(t => t.VolumeInfo.PublishedDate);
        }
    }
}