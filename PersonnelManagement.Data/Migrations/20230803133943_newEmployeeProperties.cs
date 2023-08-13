using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonnelManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class newEmployeeProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Employees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BloodType",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndOfWork",
                table: "Employees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EndReason",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "GrossSalary",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityNumber",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "InsuranceStart",
                table: "Employees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InsuranceType",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NetSalary",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartOfWork",
                table: "Employees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "a6ebd240-201d-4214-a2b0-38aba8905360");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "0a35dccb-7c94-4247-99d7-f920871abfc6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8c686850-35a4-427d-9ef5-07ba77452865", "AQAAAAIAAYagAAAAEPknfCHtxRiswZ71BsNB5kiAtuOlFC7ZHiddNtSWCY9Ts58qw1KMJBj3wBI1IcOxAw==", "f32b3c7a-b984-4e59-90f2-776244b4a555" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d8b74a5b-1fe6-48ec-bf64-858d3628e58a", "AQAAAAIAAYagAAAAEFP8jCUuuEFwO9wGp0OOM9sQeuTS+rnzDPV5hrpvtnQbD6Wi1mmyOzWSeaeOISENrw==", "accec213-717c-445e-859b-1979485bf4a7" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adress",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "BloodType",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EndOfWork",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EndReason",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "GrossSalary",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IdentityNumber",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "InsuranceStart",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "InsuranceType",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "NetSalary",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "StartOfWork",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Employees");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
    }
}
