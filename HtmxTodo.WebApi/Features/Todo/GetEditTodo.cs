using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace HtmxTodo.WebApi.Features.Todo
{
    public class GetEditTodo : EndpointWithoutRequest<TodoResponse>
    {
        public TodoDbContext Context { get; }

        public GetEditTodo(TodoDbContext context)
        {
            Context = context;
        }

        public override void Configure()
        {
            Get("/api/todo/edit/{id}");
            AllowAnonymous();
        }

        public override async Task<TodoResponse> ExecuteAsync(CancellationToken ct)
        {
            int? id = Route<int>("id");

            var todoRes = await Context.Todos.FindAsync(id, ct);

            return new TodoResponse(todoRes.Id, todoRes.Task, todoRes.Completed);
        }
    }
}
