using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SADC.Persistence.Migrations
{
    public partial class AddFarmInPlanting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cultivation",
                table: "Plantings",
                newName: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_Plantings_FarmId",
                table: "Plantings",
                column: "FarmId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plantings_Farms_FarmId",
                table: "Plantings",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plantings_Farms_FarmId",
                table: "Plantings");

            migrationBuilder.DropIndex(
                name: "IX_Plantings_FarmId",
                table: "Plantings");

            migrationBuilder.RenameColumn(
                name: "FarmId",
                table: "Plantings",
                newName: "Cultivation");
        }
    }
}
