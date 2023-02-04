using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class init6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "StudentAids",
                newName: "Subject");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "StudentAids",
                newName: "Comment");

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "StudentAids",
                type: "int",
                nullable: false,
                defaultValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "StudentAids");

            migrationBuilder.RenameColumn(
                name: "Subject",
                table: "StudentAids",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "StudentAids",
                newName: "Name");
        }
    }
}
