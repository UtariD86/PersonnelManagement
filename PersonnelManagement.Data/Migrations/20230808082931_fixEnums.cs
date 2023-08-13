using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonnelManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixEnums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InsuranceType",
                table: "Employees",
                newName: "InsuranceTypeId");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Employees",
                newName: "GenderId");

            migrationBuilder.RenameColumn(
                name: "BloodType",
                table: "Employees",
                newName: "BloodTypeId");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "af15f1d6-e09a-4535-b187-e40c191a4af6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "05a11716-3199-4a42-af3c-bde15b43bb00");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c04bd930-166d-4c37-95c2-83c8463b92c6", "AQAAAAIAAYagAAAAENGILb24lB8Aciuq+FLNyzTGoF9QHNOHmSO60zqmPmfNAIFGGLxmXMq7VAe/pNM8qw==", "9918358b-f5c2-4968-81f8-f4263f45ab19" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "edcb5945-bef5-4cb8-b2b2-f155e850e0a1", "AQAAAAIAAYagAAAAEDgocIbOyBkosVBQ61XlZSgronpq5MQi+HzJRTO6uywvfkPx0hzuVIVpaMpyMLYfHg==", "e499a67b-2f7a-4003-b802-da5053a640e3" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InsuranceTypeId",
                table: "Employees",
                newName: "InsuranceType");

            migrationBuilder.RenameColumn(
                name: "GenderId",
                table: "Employees",
                newName: "Gender");

            migrationBuilder.RenameColumn(
                name: "BloodTypeId",
                table: "Employees",
                newName: "BloodType");

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
    }
}
