using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SADC.Persistence.Migrations
{
    public partial class PlotChangeToField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlantingField");

            migrationBuilder.CreateTable(
                name: "FieldPlanting",
                columns: table => new
                {
                    FieldId = table.Column<int>(type: "int", nullable: false),
                    PlantingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldPlanting", x => new { x.FieldId, x.PlantingId });
                    table.ForeignKey(
                        name: "FK_FieldPlanting_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FieldPlanting_Plantings_PlantingId",
                        column: x => x.PlantingId,
                        principalTable: "Plantings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_FieldPlanting_PlantingId",
                table: "FieldPlanting",
                column: "PlantingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FieldPlanting");

            migrationBuilder.CreateTable(
                name: "PlantingField",
                columns: table => new
                {
                    PlantingId = table.Column<int>(type: "int", nullable: false),
                    FieldId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantingField", x => new { x.PlantingId, x.FieldId });
                    table.ForeignKey(
                        name: "FK_PlantingField_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlantingField_Plantings_PlantingId",
                        column: x => x.PlantingId,
                        principalTable: "Plantings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PlantingField_FieldId",
                table: "PlantingField",
                column: "FieldId");
        }
    }
}
