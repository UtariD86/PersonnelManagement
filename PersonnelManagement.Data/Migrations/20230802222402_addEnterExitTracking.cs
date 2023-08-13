using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonnelManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class addEnterExitTracking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Enter",
                table: "Shifts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Exit",
                table: "Shifts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "4062f315-a582-4572-b96a-dd0a8f21e3df");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "d4043329-4dbe-42ef-acb1-bf4037ea267d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f289d3ed-bab9-4772-a623-1fa6b781c868", "AQAAAAIAAYagAAAAEKuxppr6/zju5CrJIiXFW05b7cqtu3X6j+paezJ973RrBpkMoD+7+yvZJTa6WXg+Pg==", "ec1c9d5d-d1a0-4737-9ff7-f4e58c32dc1d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e14bcbf8-3d29-4815-8b88-f8fe893e5141", "AQAAAAIAAYagAAAAEAcvlojQy9JtoODLMlJ9fODqL6lkhAuvl9y4CVRTT4kusNlhlKwbQ9JyH5T8rl2JuA==", "c5449cb6-f920-4256-b9ff-634f78cb51c2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enter",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "Exit",
                table: "Shifts");

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
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d6466e01-473e-4872-9ce0-8ae5a642be58", "AQAAAAIAAYagAAAAELMUi42iVkfV3T8xcBJ3ZmDVjrQTVAOGavL1MTtHkrxGA7o+Ww1OsvO8wXG3v2jYyg==", "792a6ea4-b0f0-4124-aa96-3b80e4365f96" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "00e3f06f-66f9-4135-b26f-4e9714371114", "AQAAAAIAAYagAAAAEG91hbJc37CNX+fOFcz2NRJTFlKwPLq6bOzb0ljCefIayDt1+9nu/sxRUfRiWph29g==", "c9802a61-615b-40cf-871d-064a884164db" });
        }
    }
}
