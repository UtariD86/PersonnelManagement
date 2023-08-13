using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonnelManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class tryingToPictureAndEmployeeUser3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "40f02c6f-eaaf-4bc5-8dbc-f37389a923fe");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "7c646b83-1932-4812-9298-b95421bab2f1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2aa038f0-dc84-430a-aa5b-d978b3cd23a9", "AQAAAAIAAYagAAAAEJMHpyw0F5iPuvUGg9gXHKf3pNUNMzacJydhe3Ir2VBbsSwz+ibUJII2Yx2WjEeWHw==", "6c70c02b-e975-4440-ba30-e42b375d4342" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8e29218f-46b0-4655-bf5f-ce78ce99c0aa", "AQAAAAIAAYagAAAAEFWPbD+upUuRznZDdSZ4K4iPlkiOW/mv3qAjLuOn8RubXGCIQlapNYmfFyyvY/p6sg==", "663f2362-ccdc-4cd1-879e-1588cda75b17" });
        }
    }
}
