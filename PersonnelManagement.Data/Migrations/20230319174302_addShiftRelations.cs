using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonnelManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class addShiftRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Shifts_EmployeeId",
                table: "Shifts",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_ShiftTypeId",
                table: "Shifts",
                column: "ShiftTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shifts_Employees_EmployeeId",
                table: "Shifts",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shifts_ShiftTypes_ShiftTypeId",
                table: "Shifts",
                column: "ShiftTypeId",
                principalTable: "ShiftTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shifts_Employees_EmployeeId",
                table: "Shifts");

            migrationBuilder.DropForeignKey(
                name: "FK_Shifts_ShiftTypes_ShiftTypeId",
                table: "Shifts");

            migrationBuilder.DropIndex(
                name: "IX_Shifts_EmployeeId",
                table: "Shifts");

            migrationBuilder.DropIndex(
                name: "IX_Shifts_ShiftTypeId",
                table: "Shifts");
        }
    }
}
