using FastEndpoints;
using Flurl.Http;
using HtmxTodo.WebApp.Features.Todo.CreateTodo;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HtmxTodo.WebApp.Features.Todo.UpdateTodo
{
    public class GetEditTodoEndpoint : EndpointWithoutRequest<Results<RazorComponentResult<GetEditTodo>,IResult>>
    {
        private readonly IConfiguration _configuration;

        public GetEditTodoEndpoint(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public override void Configure()
        {
            Get("/todo/edit/{id}");
            
            AllowAnonymous();
        }

        public override async Task<Results<RazorComponentResult<GetEditTodo>, IResult>> ExecuteAsync(CancellationToken ct)
        {

            int id = Route<int>("id");


            string url = $"{_configuration["ApiUrl"]}/api/todo/edit/{id}";

            TodoResponse todoRes = await
                url.GetJsonAsync<TodoResponse>();

            HttpContext.Response.Headers["HX-Trigger-After-Swap"] = "otherRowClicked";

            return new RazorComponentResult<GetEditTodo>(new { Model = todoRes });

        }
    }

    //public record GetEditTodoCmd(int id);
    public record TodoResponse(int id,string task,bool completed);
}
