using Microsoft.EntityFrameworkCore.Migrations;

namespace CardPortfolio.Migrations
{
    public partial class linkURL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "386c25b0-c1fb-4258-93a0-b8f0820f1b7e");

            migrationBuilder.AddColumn<string>(
                name: "LinkURL",
                table: "UnsecuredPersonalLoans",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkUrl",
                table: "UnsecuredLinesOfCredit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkURL",
                table: "SecuredPersonalLoans",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkUrl",
                table: "SecuredLinesOfCredit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkURL",
                table: "Mortgages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkURL",
                table: "HomeEquityLoans",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkUrl",
                table: "HomeEquityLinesOfCredit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkURL",
                table: "AutoLoans",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ae17e69a-a97e-4196-8791-5e44a6d8a6c4", "9046dc22-4fee-488c-bbad-5221bcdba40c", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ae17e69a-a97e-4196-8791-5e44a6d8a6c4");

            migrationBuilder.DropColumn(
                name: "LinkURL",
                table: "UnsecuredPersonalLoans");

            migrationBuilder.DropColumn(
                name: "LinkUrl",
                table: "UnsecuredLinesOfCredit");

            migrationBuilder.DropColumn(
                name: "LinkURL",
                table: "SecuredPersonalLoans");

            migrationBuilder.DropColumn(
                name: "LinkUrl",
                table: "SecuredLinesOfCredit");

            migrationBuilder.DropColumn(
                name: "LinkURL",
                table: "Mortgages");

            migrationBuilder.DropColumn(
                name: "LinkURL",
                table: "HomeEquityLoans");

            migrationBuilder.DropColumn(
                name: "LinkUrl",
                table: "HomeEquityLinesOfCredit");

            migrationBuilder.DropColumn(
                name: "LinkURL",
                table: "AutoLoans");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "386c25b0-c1fb-4258-93a0-b8f0820f1b7e", "c44dfdcd-da6f-4a2b-84cc-7954edd08c0f", "Admin", "ADMIN" });
        }
    }
}
