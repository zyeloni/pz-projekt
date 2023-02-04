using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class init5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserScienceClubs_ScienceClubs_ScienceClubID",
                table: "UserScienceClubs");

            migrationBuilder.DropForeignKey(
                name: "FK_UserScienceClubs_Users_UserID",
                table: "UserScienceClubs");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserID",
                table: "UserScienceClubs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ScienceClubID",
                table: "UserScienceClubs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserScienceClubs_ScienceClubs_ScienceClubID",
                table: "UserScienceClubs",
                column: "ScienceClubID",
                principalTable: "ScienceClubs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserScienceClubs_Users_UserID",
                table: "UserScienceClubs",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserScienceClubs_ScienceClubs_ScienceClubID",
                table: "UserScienceClubs");

            migrationBuilder.DropForeignKey(
                name: "FK_UserScienceClubs_Users_UserID",
                table: "UserScienceClubs");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserID",
                table: "UserScienceClubs",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ScienceClubID",
                table: "UserScienceClubs",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_UserScienceClubs_ScienceClubs_ScienceClubID",
                table: "UserScienceClubs",
                column: "ScienceClubID",
                principalTable: "ScienceClubs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserScienceClubs_Users_UserID",
                table: "UserScienceClubs",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
