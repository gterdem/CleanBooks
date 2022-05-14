using CleanBooks.Application.Books.Queries.GetBooks;
using FluentAssertions;
using NUnit.Framework;

namespace CleanBooks.Application.IntegrationTests.Books;

public class GetBooksQuery_Tests : TestBase
{
    [Test]
    public async Task ShouldHaveValidQueryString()
    {
        GetBooksQuery query = new GetBooksQuery() {Q = "star wars+intitle:starwars+inauthor:lucas" };
        var queryString = query.Query.GetSearchQuery();

        var strResult = query.ToString();
        queryString.Should().Contain("intitle");
    }

    [Test]
    public async Task QueryTermParsingTest()
    {
        string qStr1 = "intitle:starwars+inauthor:lucas";
        string qStr2 = "star wars+intitle:star wars+inauthor:lucas";
        GetBooksQuery query1 = new GetBooksQuery() {Q = qStr1 };
        var res = query1.Query.GetSearchQuery();
        query1.Query.GetSearchQuery().Should().Be("q=intitle:starwars+inauthor:lucas");
        query1.Query.QValue.Should().Be(string.Empty);
        query1.Query.InTitle.Should().Be("starwars");
        
        GetBooksQuery query2 = new GetBooksQuery() {Q = qStr2 };
        query2.Query.GetSearchQuery().Should().Be("q=star wars+intitle:star wars+inauthor:lucas");
        query2.Query.QValue.Should().Be("star wars");
        query2.Query.InTitle.Should().Be("star wars");
        
    }
}