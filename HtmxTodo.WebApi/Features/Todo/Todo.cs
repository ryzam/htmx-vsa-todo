using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HtmxTodo.WebApi.Features.Todo
{
    public class Todo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public string? Task { get; set; }

        public bool Completed { get; set; } = false;
    }

    public record TodoResponse(int id, string task,bool completed);
}
