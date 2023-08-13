using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonnelManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class TriggerTry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
            string triggerScript = @"
                CREATE TRIGGER trg_DepartmentDelete
                ON Departments
                AFTER UPDATE
                AS
                BEGIN
                    IF UPDATE(IsDeleted)
                    BEGIN
                        DECLARE @DepartmentId INT

                        SELECT @DepartmentId = INSERTED.Id
                        FROM INSERTED
                        WHERE INSERTED.IsDeleted = 1

                        UPDATE Positions
                        SET IsDeleted = 1
                        WHERE DepartmentId = @DepartmentId

                        UPDATE Employees
                        SET IsDeleted = 1
                        WHERE DepartmentId = @DepartmentId
                    END
                END";

            migrationBuilder.Sql(triggerScript);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.Sql("DROP TRIGGER trg_DepartmentDelete");
        }
    }
}
