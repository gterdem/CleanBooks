using CleanBooks.Application.Books.Queries.GetBooks;
using Microsoft.AspNetCore.Mvc;

namespace CleanBooks.WebUI.Controllers;

public class BooksController: ApiControllerBase
{
    [HttpGet]
    public async Task<VolumesDto> Get([FromQuery] GetBooksQuery query)
    {
        return await Mediator.Send(query);
    }
}