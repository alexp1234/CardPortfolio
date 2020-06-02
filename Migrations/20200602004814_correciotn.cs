using Microsoft.EntityFrameworkCore.Migrations;

namespace CardPortfolio.Migrations
{
    public partial class correciotn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ae17e69a-a97e-4196-8791-5e44a6d8a6c4");

            migrationBuilder.RenameColumn(
                name: "LinkUrl",
                table: "UnsecuredLinesOfCredit",
                newName: "LinkURL");

            migrationBuilder.RenameColumn(
                name: "LinkUrl",
                table: "SecuredLinesOfCredit",
                newName: "LinkURL");

            migrationBuilder.RenameColumn(
                name: "LinkUrl",
                table: "HomeEquityLinesOfCredit",
                newName: "LinkURL");

            migrationBuilder.AddColumn<string>(
                name: "LinkURL",
                table: "SavingsAccounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkURL",
                table: "MoneyMarketAccounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkURL",
                table: "CheckingAccounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkURL",
                table: "CertificateAccounts",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "de1299c3-d820-4396-8014-5a7e8d75a359", "4d4743ea-69bb-475b-b2fa-69e0ee7f4e2b", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "de1299c3-d820-4396-8014-5a7e8d75a359");

            migrationBuilder.DropColumn(
                name: "LinkURL",
                table: "SavingsAccounts");

            migrationBuilder.DropColumn(
                name: "LinkURL",
                table: "MoneyMarketAccounts");

            migrationBuilder.DropColumn(
                name: "LinkURL",
                table: "CheckingAccounts");

            migrationBuilder.DropColumn(
                name: "LinkURL",
                table: "CertificateAccounts");

            migrationBuilder.RenameColumn(
                name: "LinkURL",
                table: "UnsecuredLinesOfCredit",
                newName: "LinkUrl");

            migrationBuilder.RenameColumn(
                name: "LinkURL",
                table: "SecuredLinesOfCredit",
                newName: "LinkUrl");

            migrationBuilder.RenameColumn(
                name: "LinkURL",
                table: "HomeEquityLinesOfCredit",
                newName: "LinkUrl");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ae17e69a-a97e-4196-8791-5e44a6d8a6c4", "9046dc22-4fee-488c-bbad-5221bcdba40c", "Admin", "ADMIN" });
        }
    }
}
