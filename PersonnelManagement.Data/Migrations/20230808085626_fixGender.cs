using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonnelManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixGender : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "GenderId",
                table: "Employees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "75a8e309-62d6-4af1-a88a-35d1b7fc421e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "7632f14b-9253-4e7c-bc6c-22bdde09d799");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "589f1dee-ed97-4329-8fc0-cf9b43bf11b0", "AQAAAAIAAYagAAAAEPJZX7841VNtUqLdRC4KyzFaAuCXfBOfivjla4qh9b/j+/y2sOj+iEeo7IF8Al39pg==", "5680c088-d7ef-4298-baa2-89719e0d85aa" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b4a007d6-61fc-493d-86f5-e3088d58e3de", "AQAAAAIAAYagAAAAEBdKhRFEC92lTFSjNOvBAyQZtDF4665xdf6HrkvXI5Wdar0NGUWy3vCmlHPbqdaD0g==", "2653da7a-e4c6-4460-9742-3889c8786880" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "GenderId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
    }
}
