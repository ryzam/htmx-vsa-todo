using FastEndpoints;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HtmxTodo.WebApi.Features.Todo
{
    public class CreateTodo: Endpoint<CreateTodoCmd,TodoResponse>
    {
        public TodoDbContext Context { get; }

        public CreateTodo(TodoDbContext context)
        {
            Context = context;
        }

        public override void Configure()
        {
            Post("api/todo");
            AllowAnonymous();
        }

        public override async Task<TodoResponse> ExecuteAsync(CreateTodoCmd req, CancellationToken ct)
        {
            Todo todo = new Todo { Task = req.task };

            Context.Todos.Add(todo);

            await Context.SaveChangesAsync();

            return new TodoResponse(todo.Id,todo.Task,todo.Completed);
        }
    }

    public record CreateTodoCmd(string task);

    
   
}
