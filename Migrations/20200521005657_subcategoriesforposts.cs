using Microsoft.EntityFrameworkCore.Migrations;

namespace CardPortfolio.Migrations
{
    public partial class subcategoriesforposts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7361bbb6-5de8-455a-a639-2a08ef16f481");

            migrationBuilder.AddColumn<int>(
                name: "BlogPostSubCategory",
                table: "BlogPosts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b067b4f1-7379-42f8-a197-2a1f4a302a5c", "ad3a7cb5-b23b-4a75-9ee7-ceff61bc04ae", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b067b4f1-7379-42f8-a197-2a1f4a302a5c");

            migrationBuilder.DropColumn(
                name: "BlogPostSubCategory",
                table: "BlogPosts");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7361bbb6-5de8-455a-a639-2a08ef16f481", "f52790cd-19e4-46c0-a122-0cc4e0c42aa3", "Admin", "ADMIN" });
        }
    }
}
