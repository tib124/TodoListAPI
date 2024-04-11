
using Microsoft.EntityFrameworkCore;

namespace TodoListAPI.Models
{
    public class TodoListReposiory : ITodoListRepository
    {
        private readonly TodoListContext _context;

        public TodoListReposiory(TodoListContext context)
        {
            _context = context;
        }

        public async Task<TodoList> AddNewTodo(TodoList newTodo)
        {
            var todoAdd = await _context.TodoList.AddAsync(newTodo);
            await _context.SaveChangesAsync();

            return todoAdd.Entity;
        }

        public async Task DeleteTodo(int id)
        {
           var todoToDelete = await GetById(id);

            if(todoToDelete != null)
            {
                _context.TodoList.Remove(todoToDelete);
                await _context.SaveChangesAsync();

            }
        }


        public async Task<IEnumerable<TodoList>> GetAllAsync() => await _context.TodoList.ToListAsync();



        public async Task<TodoList> GetByDateAsync(DateTime date) => await _context.TodoList.FirstOrDefaultAsync(d => d.CreatedAt.Date == date);


        public async Task<TodoList> GetById(int id) => await _context.TodoList.FindAsync(id);


        public async Task<IEnumerable<TodoList>> GetByStatusAsync(TodoList._Status status) => await _context.TodoList.Where(s => s.Status == status).ToListAsync();


        public async Task<TodoList> GetByTitleAsync(string title) => await _context.TodoList.FirstOrDefaultAsync(t => t.Title == title);

        public async Task<TodoList> UpdateTodo(TodoList todo)
        {
            var todoToUpdate = await _context.TodoList.FirstOrDefaultAsync(t => t.Id == todo.Id);

            if(todoToUpdate != null)
            {
                todoToUpdate.Title = todo.Title;
                todoToUpdate.Description = todo.Description;
                todoToUpdate.Status = todo.Status;
                todoToUpdate.LastUpdate = DateTime.Now;
                await _context.SaveChangesAsync();

            }

            return todoToUpdate;

        }
    }
}
