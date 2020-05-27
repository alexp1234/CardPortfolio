using Microsoft.EntityFrameworkCore.Migrations;

namespace CardPortfolio.Migrations
{
    public partial class RemoveSubCat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2ad3ea48-9076-4434-972b-6576028c63cd");

            migrationBuilder.DropColumn(
                name: "BlogPostSubCategory",
                table: "BlogPosts");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ca6a4d9d-5ef6-4bd6-a759-28e5f78560a4", "109fc975-345c-40be-bad0-178008f666ec", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ca6a4d9d-5ef6-4bd6-a759-28e5f78560a4");

            migrationBuilder.AddColumn<int>(
                name: "BlogPostSubCategory",
                table: "BlogPosts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2ad3ea48-9076-4434-972b-6576028c63cd", "b3512beb-34cf-4b0b-86b0-7835f09096a4", "Admin", "ADMIN" });
        }
    }
}
