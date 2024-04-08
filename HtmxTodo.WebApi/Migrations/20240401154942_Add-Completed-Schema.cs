using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HtmxTodo.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddCompletedSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Completed",
                table: "Todos",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completed",
                table: "Todos");
        }
    }
}
