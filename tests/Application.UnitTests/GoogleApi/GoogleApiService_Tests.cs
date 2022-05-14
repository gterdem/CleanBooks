using FluentAssertions;
using Google.Apis.Books.v1;
using NUnit.Framework;

namespace CleanBooks.Application.UnitTests.GoogleApi;

public class GoogleApiService_Tests
{
    [Test]
    public async Task Should_Get_Volumes_From_Google_BookService()
    {
        // https://www.googleapis.com/books/v1/volumes?q=flowers+inauthor:keyes
        var query = "flowers+inauthor:keyes";
        
        // "kind": "books#volume",
        // "id": "8Pr_kLFxciYC",
        // "etag": "sTRwxRGco44",
        // "selfLink": "https://www.googleapis.com/books/v1/volumes/8Pr_kLFxciYC",
        // "volumeInfo": {
        //     "title": "Flowers For Algernon",
        //     "subtitle": "A Modern Literary Classic",
        //     "authors": [
        //     "Daniel Keyes"
        //         ],
        
        BooksService service = new BooksService();
        var listRequest = service.Volumes.List(query);

        var result = await listRequest.ExecuteAsync(new CancellationToken());
        result.Items.Count.Should().Be(10);
        result.Items.Should().Contain(q => q.VolumeInfo.Title == "Flowers For Algernon");
        var firstItem = result.Items.First();
        firstItem.Should().NotBeNull();
        firstItem.Id.Should().Be("8Pr_kLFxciYC");
        firstItem.VolumeInfo.Authors.Should().Contain("Daniel Keyes");
    }
}