using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SADC.Persistence.Migrations
{
    public partial class RemoveUserInFarm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Farms_AspNetUsers_UserId",
                table: "Farms");

            migrationBuilder.DropIndex(
                name: "IX_Farms_UserId",
                table: "Farms");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Farms");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Farms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Farms_UserId",
                table: "Farms",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Farms_AspNetUsers_UserId",
                table: "Farms",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
