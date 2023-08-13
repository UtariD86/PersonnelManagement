using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonnelManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class TriggerDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "524002fc-16b0-43bb-a910-6520b900d099");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "ac3d9548-e240-4be2-b482-1dd7ba806455");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4671012b-21a1-4bb8-9575-f4d0ba1b08df", "AQAAAAIAAYagAAAAELgvDaMSSZCQI3iNr52p1OWNVBn4suarSSWpBGIk+RpoUGx90SAaFejWHVotvxaxzQ==", "6741e6ce-e5db-48d2-b12c-1beb9251bebf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "14b92765-481e-4904-bb86-0aad6f01e067", "AQAAAAIAAYagAAAAEEdScId+4gEkYMOpDIkShK/3kEuGVTMb30/2qykb1rMeVmlwJPBVvjQ9KV3Lu2QsUA==", "95938b8d-7ea3-413c-a633-137019d3a410" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "320bb773-7616-46c9-ac04-6458e9e56315");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "92c56267-15d4-44b1-ade2-e741168ba3fd");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4897e637-dab4-448e-85ab-6bca5f4a1587", "AQAAAAIAAYagAAAAEJ60uiuN6NuYmkPn+HBQWR/FkwA906BgKV6bNzMVgAH2McToK+3vxGA+iHFOlD+A5Q==", "7da1d22f-fcf5-4ba7-987e-7ab1ef1ba376" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bf0af962-1c2c-4b23-9487-b54e3bc47606", "AQAAAAIAAYagAAAAEPaO2lCHridGGYy9zqnEj08eSG4XUYepOXC9QCn4s6jbleDKaWkFObl6bo+OS837xw==", "784e2796-dadd-46fc-84f6-0355ff666e85" });
        }
    }
}
