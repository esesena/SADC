using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SADC.Persistence.Migrations
{
    public partial class EmployeesFarms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeFarm");

            migrationBuilder.CreateTable(
                name: "EmployeesFarms",
                columns: table => new
                {
                    FarmId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeesFarms", x => new { x.FarmId, x.EmployeeId });
                    table.ForeignKey(
                        name: "FK_EmployeesFarms_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeesFarms_Farms_FarmId",
                        column: x => x.FarmId,
                        principalTable: "Farms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeesFarms_EmployeeId",
                table: "EmployeesFarms",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeesFarms");

            migrationBuilder.CreateTable(
                name: "EmployeeFarm",
                columns: table => new
                {
                    EmployeesId = table.Column<int>(type: "int", nullable: false),
                    FarmsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeFarm", x => new { x.EmployeesId, x.FarmsId });
                    table.ForeignKey(
                        name: "FK_EmployeeFarm_Employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeFarm_Farms_FarmsId",
                        column: x => x.FarmsId,
                        principalTable: "Farms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeFarm_FarmsId",
                table: "EmployeeFarm",
                column: "FarmsId");
        }
    }
}
