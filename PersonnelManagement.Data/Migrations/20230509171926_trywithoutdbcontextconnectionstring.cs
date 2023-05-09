using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonnelManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class trywithoutdbcontextconnectionstring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "980e00b7-7003-4300-a8dd-fe5a0ebf2255");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "739c8fce-3b88-422b-a48e-f161aa3762e9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "14ab229f-bcaf-4245-a974-8bdbe7f0853c", "AQAAAAIAAYagAAAAEDtjjbEIHghV/h+F/DZmUTWKWxY4vzfEMkbkuwbWe+DQE61xhz8hzfEtlYHaljDiiQ==", "5a49cfff-c81b-4f44-9357-3990569c9798" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d54fa7a2-c3b5-48e7-8e3d-91740831558a", "AQAAAAIAAYagAAAAEEXqIuYl+8bbpqmvgdHBkfla8WA0U3b09X4PXIED5KeHM74NysK3NNxUuIOUnk4XlQ==", "0d787d30-7cab-4db9-90b0-366cf5a22b9d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "dcfd084e-0a6b-409d-b946-14a39225d872");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "ea4961d8-1875-4e1d-9f6f-d7bd6c93d072");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8b0c1d09-8cda-44d7-a36e-03c4924ef2da", "AQAAAAIAAYagAAAAEE9iwDTWy0P4FpyMlHRyago2IrHjZx9jWmuy5GiAts3eKqN0Y4ocKUry4GKI9gpa6Q==", "18007b7c-1601-4d8a-a6cb-987a40d32b52" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "234922cb-5833-42e3-879f-1f6fd1d6d1a9", "AQAAAAIAAYagAAAAEIM2WgRrvJdMJ+WFmLt2c3jcjGPnyTgZOnlY5tZ9PI0pJg6SBQD5vQ8d8sjwOraX2A==", "f2b5a06d-c0a4-4cea-8551-e567fae5be95" });
        }
    }
}
