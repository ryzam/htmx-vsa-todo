using HtmxTodo.WebApi.Features.Todo;
using Microsoft.EntityFrameworkCore;

namespace HtmxTodo.WebApi
{
    public class TodoDbContext: DbContext
    {
        private readonly string? _connectionString;
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {

        }
       
        public DbSet<Todo>  Todos { get; set; }
    }
}
