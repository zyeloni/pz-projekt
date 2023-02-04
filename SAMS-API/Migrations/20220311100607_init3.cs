using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "registered",
                table: "Users",
                newName: "Registered");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Registered",
                table: "Users",
                newName: "registered");
        }
    }
}
