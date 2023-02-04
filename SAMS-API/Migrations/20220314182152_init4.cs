using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class init4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScienceClubUser");

            migrationBuilder.CreateTable(
                name: "UserScienceClubs",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ScienceClubID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserScienceClubs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserScienceClubs_ScienceClubs_ScienceClubID",
                        column: x => x.ScienceClubID,
                        principalTable: "ScienceClubs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserScienceClubs_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserScienceClubs_ScienceClubID",
                table: "UserScienceClubs",
                column: "ScienceClubID");

            migrationBuilder.CreateIndex(
                name: "IX_UserScienceClubs_UserID",
                table: "UserScienceClubs",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserScienceClubs");

            migrationBuilder.CreateTable(
                name: "ScienceClubUser",
                columns: table => new
                {
                    ClubsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScienceClubUser", x => new { x.ClubsID, x.UsersID });
                    table.ForeignKey(
                        name: "FK_ScienceClubUser_ScienceClubs_ClubsID",
                        column: x => x.ClubsID,
                        principalTable: "ScienceClubs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScienceClubUser_Users_UsersID",
                        column: x => x.UsersID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScienceClubUser_UsersID",
                table: "ScienceClubUser",
                column: "UsersID");
        }
    }
}
