using Microsoft.EntityFrameworkCore.Migrations;

namespace CardPortfolio.Migrations
{
    public partial class addedname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3505a316-6fa7-43db-8090-16309ee38bc7");

            migrationBuilder.AlterColumn<int>(
                name: "InstitutionId",
                table: "SavingsAccounts",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "SavingsAccounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SavingsAccounts",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InstitutionId",
                table: "MoneyMarketAccounts",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "MoneyMarketAccounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "MoneyMarketAccounts",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InstitutionId",
                table: "CheckingAccounts",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "CheckingAccounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CheckingAccounts",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InstitutionId",
                table: "CertificateAccounts",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "CertificateAccounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CertificateAccounts",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2ad3ea48-9076-4434-972b-6576028c63cd", "b3512beb-34cf-4b0b-86b0-7835f09096a4", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2ad3ea48-9076-4434-972b-6576028c63cd");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "SavingsAccounts");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "SavingsAccounts");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "MoneyMarketAccounts");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "MoneyMarketAccounts");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "CheckingAccounts");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CheckingAccounts");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "CertificateAccounts");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CertificateAccounts");

            migrationBuilder.AlterColumn<int>(
                name: "InstitutionId",
                table: "SavingsAccounts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InstitutionId",
                table: "MoneyMarketAccounts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InstitutionId",
                table: "CheckingAccounts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InstitutionId",
                table: "CertificateAccounts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3505a316-6fa7-43db-8090-16309ee38bc7", "cfbb6693-be29-47ca-85e4-9ac395bd36ee", "Admin", "ADMIN" });
        }
    }
}
