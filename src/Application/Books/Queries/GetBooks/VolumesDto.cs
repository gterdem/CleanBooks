﻿namespace CleanBooks.Application.Books.Queries.GetBooks;

public class VolumesDto
{
    public string Kind { get; set; }
    public int? TotalItems { get; set; }
    public string ETag { get; set; }
    public IList<BookDto> Books { get; set; }
}