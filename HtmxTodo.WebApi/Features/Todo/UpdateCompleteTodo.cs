using FastEndpoints;

namespace HtmxTodo.WebApi.Features.Todo
{
    public class UpdateCompleteTodo : Endpoint<UpdateCompleteTodoCmd, TodoResponse>
    {
        public TodoDbContext Context { get; }

        public UpdateCompleteTodo(TodoDbContext context)
        {
            Context = context;
        }

        public override void Configure()
        {
            Put("api/todo/complete/{id}");
            AllowAnonymous();
        }

        public override async Task<TodoResponse> ExecuteAsync(UpdateCompleteTodoCmd req, CancellationToken ct)
        {
            Console.WriteLine(req.completed);

            Todo todo = await Context.Todos.FindAsync(req.id, ct);

            todo.Completed = req.completed;

            Context.Todos.Update(todo);

            await Context.SaveChangesAsync();

            return new TodoResponse(todo.Id, todo.Task, todo.Completed);
        }
    }

    public record UpdateCompleteTodoCmd(int id,string task,bool completed);
}
