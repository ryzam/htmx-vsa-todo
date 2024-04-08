using FastEndpoints;
using Flurl.Http;
using HtmxTodo.WebApp.Features.Todo.GetTodo;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HtmxTodo.WebApp.Features.Todo.UpdateTodo
{
    public class UpdateTodoEndpoint : Endpoint<UpdateTodoCmd,RazorComponentResult<UpdateTodo>>
    {
        private readonly IConfiguration _configuration;

        public UpdateTodoEndpoint(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public override void Configure()
        {
            Put("/todo/edit/{id}");
            AllowFormData(urlEncoded: true);
            Description(x => x.Accepts<UpdateTodoCmd>());
            AllowAnonymous();
        }

        public override async Task<RazorComponentResult<UpdateTodo>> ExecuteAsync(UpdateTodoCmd req,CancellationToken ct)
        {
            string url = $"{_configuration["ApiUrl"]}/api/todo";

            TodoResponse todoRes = await
                url.PutJsonAsync(req).Result.GetJsonAsync<TodoResponse>();

            return await Task.FromResult(new RazorComponentResult<UpdateTodo>(new { Model = todoRes }));
        }
    }

    public record UpdateTodoCmd(int id,string task,bool completed);
}
