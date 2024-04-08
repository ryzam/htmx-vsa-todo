using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace HtmxTodo.WebApi.Features.Todo
{
    public class GetTodo:EndpointWithoutRequest<List<TodoResponse>>
    {
        public TodoDbContext Context { get; }

        public GetTodo(TodoDbContext context)
        {
            Context = context;
        }

        public override void Configure()
        {
            Get("/api/todo");
            AllowAnonymous();
        }

        public override async Task<List<TodoResponse>> ExecuteAsync(CancellationToken ct)
        {
            List<Todo>? todoRes = await Context?.Todos.OrderByDescending(c=>c.Id)?.ToListAsync(ct);

            return todoRes.Select(s => new TodoResponse(s.Id, s.Task, s.Completed)).ToList();
        }
    }
}
