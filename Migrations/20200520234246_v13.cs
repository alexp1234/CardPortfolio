using Microsoft.EntityFrameworkCore.Migrations;

namespace CardPortfolio.Migrations
{
    public partial class v13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "befd4e91-4d82-4662-ba2e-1ee4231b5864");

            migrationBuilder.AddColumn<string>(
                name: "url",
                table: "CreditCards",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a1605e2b-588d-483a-b952-7508c975a152", "f0fb45f2-81a6-437b-a457-a3c86c90e9aa", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a1605e2b-588d-483a-b952-7508c975a152");

            migrationBuilder.DropColumn(
                name: "url",
                table: "CreditCards");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "befd4e91-4d82-4662-ba2e-1ee4231b5864", "1236baa7-6d96-4dba-a949-952f2c2f30d3", "Admin", "ADMIN" });
        }
    }
}
