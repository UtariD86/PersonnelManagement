using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonnelManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class dropPositionId1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Positions_PositionId1",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_PositionId1",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PositionId1",
                table: "Employees");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PositionId1",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PositionId1",
                table: "Employees",
                column: "PositionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Positions_PositionId1",
                table: "Employees",
                column: "PositionId1",
                principalTable: "Positions",
                principalColumn: "Id");
        }
    }
}
