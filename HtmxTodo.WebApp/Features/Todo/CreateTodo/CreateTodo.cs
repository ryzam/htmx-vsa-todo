using FastEndpoints;
using Flurl.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HtmxTodo.WebApp.Features.Todo.CreateTodo
{
    public class CreateTodoEndpoint: Endpoint<CreateTodoCmd,RazorComponentResult<CreateTodo>>
    {
        private readonly IConfiguration _configuration;
        public CreateTodoEndpoint(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public override void Configure()
        {
            Post("/todo");
            AllowFormData(urlEncoded:true);
            AllowAnonymous();
        }

        public override async Task<RazorComponentResult<CreateTodo>> ExecuteAsync(CreateTodoCmd req, CancellationToken ct)
        {

            string? url = $"{_configuration["ApiUrl"]}/api/todo";

            TodoResponse res = await url.PostJsonAsync(req).Result.GetJsonAsync<TodoResponse>();

            return await Task.FromResult(new RazorComponentResult<CreateTodo>(new { Model = new TodoResponse(res.id,res.task)}));
        }
    }

    public record CreateTodoCmd(string task);

    public record TodoResponse(int id,string task);
}
