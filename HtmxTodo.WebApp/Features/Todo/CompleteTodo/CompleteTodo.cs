using FastEndpoints;
using Flurl.Http;
using HtmxTodo.WebApp.Features.Todo.GetTodo;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;

namespace HtmxTodo.WebApp.Features.Todo.CompleteTodo
{
    public class CompleteTodoEndpoint : Endpoint<CompleteTodoCmd, Results<RazorComponentResult<CompleteTodo>,BadRequest<string>>>
    {
        private readonly IConfiguration _configuration;
        public CompleteTodoEndpoint(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override void Configure()
        {
            Put("/todo/complete/{id}");
            Description(x => x.Accepts<CompleteTodoCmd>());
            AllowFormData(urlEncoded: true);
            AllowAnonymous();
        }

        public override async Task<Results<RazorComponentResult<CompleteTodo>,BadRequest<string>>> ExecuteAsync(CompleteTodoCmd? req, CancellationToken ct)
        {
            int id = Route<int>("id");

            string urlGet = $"{_configuration["ApiUrl"]}/api/todo/edit/{id}";

            var todoGet = await urlGet.GetJsonAsync<TodoResponse>();

            req.completed = todoGet.completed == true ? false : true;

            string url = $"{_configuration["ApiUrl"]}/api/todo/complete/{id}";

            var todoRes = await
                url
                .WithHeader("ContentType", "application/json")
                .PutJsonAsync(req);

            if (todoRes.StatusCode == 200)
            {
                var result = await todoRes.GetJsonAsync<TodoResponse>();

                return await Task.FromResult(new RazorComponentResult<CompleteTodo>(new { Model = result }));
            }
            else
            {
                return TypedResults.BadRequest($"Unable to update todo task id {id}");
            }
        }
    }

    public record CompleteTodoCmd(int id, bool completed)
    {
        public bool completed { get; set; } = false;
    }
    public record TodoResponse(int id, string task, bool completed);
}
