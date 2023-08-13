using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonnelManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class tryingToPictureAndEmployeeUser2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "47f7829f-1c6a-42c5-a0bf-185fbe168d8a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "7ae6eee1-b03b-4a0b-86ba-d02db8491fcc");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "177d47ca-535d-46ce-97a9-b2a8e40e1447", "AQAAAAIAAYagAAAAENhYm0JvekPc6ykrTVe2qG/SUaplkdgMwI4gz8rlIibSjVe2TevSPK7sVLuofIuYPA==", "3db9b0a2-ee76-4909-821b-e935d825ce10" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ae86ff21-6344-432a-811d-954a1e25985f", "AQAAAAIAAYagAAAAEOCQaIkgrogzLUTjsQIUV7UiklIugPce4bHCksAVfrFg1tOLzDQgR/VApQGzbqNr1w==", "c2f20a5f-b42c-4146-8d4f-c5fcad15fc9f" });
        }
    }
}
