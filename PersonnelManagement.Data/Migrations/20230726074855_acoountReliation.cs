using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonnelManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class acoountReliation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeUser");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "2d050da9-5326-4bbc-aa46-382ce351972b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "3d2796c1-a81b-4095-a211-f8d170057e29");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "EmployeeId", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d6466e01-473e-4872-9ce0-8ae5a642be58", null, "AQAAAAIAAYagAAAAELMUi42iVkfV3T8xcBJ3ZmDVjrQTVAOGavL1MTtHkrxGA7o+Ww1OsvO8wXG3v2jYyg==", "792a6ea4-b0f0-4124-aa96-3b80e4365f96" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "EmployeeId", "PasswordHash", "SecurityStamp" },
                values: new object[] { "00e3f06f-66f9-4135-b26f-4e9714371114", null, "AQAAAAIAAYagAAAAEG91hbJc37CNX+fOFcz2NRJTFlKwPLq6bOzb0ljCefIayDt1+9nu/sxRUfRiWph29g==", "c9802a61-615b-40cf-871d-064a884164db" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "EmployeeUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedByName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeUser", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "a6a7753c-6565-4eed-8b36-e39170eea00f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "610a0200-e48c-465f-b983-9946ec54a3fb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "35902472-ad46-46bc-bcbc-60f56da0eb4c", "AQAAAAIAAYagAAAAEHKzUDHbBeqRDuMafONbHwMG9WYU+BIZUVhPqHUOO4YfLAq1bP5cMByBhn0GbJq5NQ==", "9a3a3f6b-6f82-49cb-8e03-164a17bf6efe" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b20067e1-763b-440c-9209-af91c6610cd2", "AQAAAAIAAYagAAAAECHt0NiS0wy8ZiDvfahFfKgPwhwHoBk0s1Y/O5HEdSrmWW1CtU3px8zz2k3xt8hssQ==", "fc971c03-adc6-4009-92c7-63d6c8e659cd" });
        }
    }
}
