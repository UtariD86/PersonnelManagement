using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonnelManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixTry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "915cf3d3-215a-4152-bedf-8a63c130b2ae");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "6de1c525-fa5f-4901-9b1e-dd30a4985a36");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "89c3a447-34b8-402b-b45f-bb255d003961", "AQAAAAIAAYagAAAAEEzlY+gmkxCyOgLJpvYknpus2j+Cb6iCn4Csgr8FERG7mMTnBqMhbzAl5+zX+GALiQ==", "90e07917-674d-407a-b4bd-89d1518e7dc2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9b3c9e3f-47b5-424f-a25a-0ed303b3f10f", "AQAAAAIAAYagAAAAECz05lJEWLPBylkQMNcFoNQ1e/5/zdlVOHfvNiIAJVJvZ/IJZYVKOYV+oqMoxzDORQ==", "e0c783fb-bfc2-4810-9599-3001b61dbe6c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
