namespace CSharpApp.Core.Interfaces;

public interface ITodoPostService
{
    Task<TodoRecord?> GetTodoById(int id);
    Task<ReadOnlyCollection<TodoRecord>> GetAllTodos();
    Task<String> deleteTodoById(int id);
    Task<TodoRecord> postTodo(TodoRecord todo);

}