using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonnelManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class addScheduleShiftRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ScheduleId",
                table: "ScheduleShifts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShiftId",
                table: "ScheduleShifts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleShifts_ScheduleId",
                table: "ScheduleShifts",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleShifts_ShiftId",
                table: "ScheduleShifts",
                column: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleShifts_Schedules_ScheduleId",
                table: "ScheduleShifts",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleShifts_Shifts_ShiftId",
                table: "ScheduleShifts",
                column: "ShiftId",
                principalTable: "Shifts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleShifts_Schedules_ScheduleId",
                table: "ScheduleShifts");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleShifts_Shifts_ShiftId",
                table: "ScheduleShifts");

            migrationBuilder.DropIndex(
                name: "IX_ScheduleShifts_ScheduleId",
                table: "ScheduleShifts");

            migrationBuilder.DropIndex(
                name: "IX_ScheduleShifts_ShiftId",
                table: "ScheduleShifts");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "ScheduleShifts");

            migrationBuilder.DropColumn(
                name: "ShiftId",
                table: "ScheduleShifts");
        }
    }
}
