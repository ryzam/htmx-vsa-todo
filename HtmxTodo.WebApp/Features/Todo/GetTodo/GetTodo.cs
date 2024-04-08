using FastEndpoints;
using Flurl.Http;
using HtmxTodo.WebApp.Features.Todo.CreateTodo;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;

namespace HtmxTodo.WebApp.Features.Todo.GetTodo
{
    public class GetTodoEndpoint : EndpointWithoutRequest<RazorComponentResult<GetTodo>>
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public GetTodoEndpoint(ILogger<GetTodoEndpoint> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;

        }
        public override void Configure()
        {
            Get("/todo");
            
            AllowAnonymous();
        }

        public override async Task<RazorComponentResult<GetTodo>> ExecuteAsync(CancellationToken ct)
        {
            _logger.LogInformation("GetTodo");

            string url = $"{_configuration["ApiUrl"]}/api/todo";

            List<TodoResponse> todoRes = await 
                url.GetJsonAsync<List<TodoResponse>>();

            return await Task.FromResult(new RazorComponentResult<GetTodo>(new { Model = todoRes }));
        }
    }

    public record TodoResponse(int id,string task,bool completed);
}
