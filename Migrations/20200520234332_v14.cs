using Microsoft.EntityFrameworkCore.Migrations;

namespace CardPortfolio.Migrations
{
    public partial class v14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a1605e2b-588d-483a-b952-7508c975a152");

            migrationBuilder.RenameColumn(
                name: "url",
                table: "CreditCards",
                newName: "Url");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7361bbb6-5de8-455a-a639-2a08ef16f481", "f52790cd-19e4-46c0-a122-0cc4e0c42aa3", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7361bbb6-5de8-455a-a639-2a08ef16f481");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "CreditCards",
                newName: "url");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a1605e2b-588d-483a-b952-7508c975a152", "f0fb45f2-81a6-437b-a457-a3c86c90e9aa", "Admin", "ADMIN" });
        }
    }
}
