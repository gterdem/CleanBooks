using CleanBooks.Application.Common.Mappings;
using CleanBooks.Domain.Entities;

namespace CleanBooks.Application.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; set; }

    public bool Done { get; set; }
}
