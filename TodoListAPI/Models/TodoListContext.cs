using Microsoft.EntityFrameworkCore;

namespace TodoListAPI.Models
{
    public class TodoListContext : DbContext
    {
        public TodoListContext(DbContextOptions<TodoListContext> options) : base(options)
        {

        }

        public DbSet<TodoList> TodoList { get; set; }
    }
}
