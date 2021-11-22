using Microsoft.EntityFrameworkCore.Migrations;

namespace TricksAPI.Migrations
{
    public partial class added_userid_to_tricks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Tricks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Tricks_UserId",
                table: "Tricks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tricks_AspNetUsers_UserId",
                table: "Tricks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tricks_AspNetUsers_UserId",
                table: "Tricks");

            migrationBuilder.DropIndex(
                name: "IX_Tricks_UserId",
                table: "Tricks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tricks");
        }
    }
}
