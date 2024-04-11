using static TodoListAPI.Models.TodoList;

namespace TodoListAPI.Models
{
    public interface ITodoListRepository
    {
        Task<IEnumerable<TodoList>> GetAllAsync();
        Task<TodoList> GetById(int id);
        Task<TodoList> GetByTitleAsync(string title);
        Task<TodoList> GetByDateAsync(DateTime date);
        Task<IEnumerable<TodoList>> GetByStatusAsync(_Status status);
        Task<TodoList> AddNewTodo(TodoList newTodo);
        Task<TodoList> UpdateTodo(TodoList todo);
        Task DeleteTodo(int id);


    }
}