using FastEndpoints;

namespace HtmxTodo.WebApi.Features.Todo
{
    public class UpdateTodo : Endpoint<UpdateTodoCmd, TodoResponse>
    {
        public TodoDbContext Context { get; }

        public UpdateTodo(TodoDbContext context)
        {
            Context = context;
        }

        public override void Configure()
        {
            Put("api/todo");
            AllowFormData(urlEncoded: true);
            Description(x => x.Accepts<UpdateTodoCmd>());
            AllowAnonymous();
        }

        public override async Task<TodoResponse> ExecuteAsync(UpdateTodoCmd req, CancellationToken ct)
        {
            Todo todo = await Context.Todos.FindAsync(req.id, ct);

            todo.Task = req.task;

            Context.Todos.Update(todo);

            await Context.SaveChangesAsync();

            return new TodoResponse(todo.Id, todo.Task, todo.Completed);
        }
    }

    public record UpdateTodoCmd(int id,string task,bool completed);
}
