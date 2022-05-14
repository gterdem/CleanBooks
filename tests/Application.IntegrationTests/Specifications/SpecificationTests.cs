using CleanBooks.Domain.Specifications;
using NUnit.Framework;

namespace CleanBooks.Application.IntegrationTests.Specifications;

public class SpecificationTests : TestBase
{
    [Test]
    public Task BooksByPaginatedSpecificationShouldSkipAndReturn()
    {
        BooksByPaginatedSpec spec = new BooksByPaginatedSpec();
        return Task.CompletedTask;
    }
}