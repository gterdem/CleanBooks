using CleanBooks.Application.TodoLists.Queries.ExportTodos;

namespace CleanBooks.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
