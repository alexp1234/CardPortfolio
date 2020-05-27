using Microsoft.EntityFrameworkCore.Migrations;

namespace CardPortfolio.Migrations
{
    public partial class DepositAccounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b067b4f1-7379-42f8-a197-2a1f4a302a5c");

            migrationBuilder.CreateTable(
                name: "CertificateAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InterestRate = table.Column<double>(nullable: false),
                    InstitutionId = table.Column<int>(nullable: false),
                    HasMinimumToOpenAccount = table.Column<bool>(nullable: false),
                    MinimumToOpenAccount = table.Column<double>(nullable: true),
                    HasMonthlyFee = table.Column<bool>(nullable: false),
                    MonthlyFee = table.Column<double>(nullable: false),
                    BalanceToAvoidFee = table.Column<double>(nullable: true),
                    HasMinimumAmountForApr = table.Column<bool>(nullable: false),
                    MinimumAmountForApr = table.Column<double>(nullable: true),
                    HasMaximumAmountForApr = table.Column<bool>(nullable: false),
                    MaximumAmountForApr = table.Column<double>(nullable: true),
                    AprIfAmountNotMet = table.Column<double>(nullable: true),
                    TermInMonths = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificateAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CheckingAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InterestRate = table.Column<double>(nullable: false),
                    InstitutionId = table.Column<int>(nullable: false),
                    HasMinimumToOpenAccount = table.Column<bool>(nullable: false),
                    MinimumToOpenAccount = table.Column<double>(nullable: true),
                    HasMonthlyFee = table.Column<bool>(nullable: false),
                    MonthlyFee = table.Column<double>(nullable: false),
                    BalanceToAvoidFee = table.Column<double>(nullable: true),
                    HasMinimumAmountForApr = table.Column<bool>(nullable: false),
                    MinimumAmountForApr = table.Column<double>(nullable: true),
                    HasMaximumAmountForApr = table.Column<bool>(nullable: false),
                    MaximumAmountForApr = table.Column<double>(nullable: true),
                    AprIfAmountNotMet = table.Column<double>(nullable: true),
                    HasDirectDepositRequirementForAPR = table.Column<bool>(nullable: false),
                    DirectDepositRequirementsForAPR = table.Column<double>(nullable: true),
                    InterestRateIfDDRequirementsNotMet = table.Column<double>(nullable: true),
                    DirectDepositToAvoidMonthlyFee = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckingAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MoneyMarketAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InterestRate = table.Column<double>(nullable: false),
                    InstitutionId = table.Column<int>(nullable: false),
                    HasMinimumToOpenAccount = table.Column<bool>(nullable: false),
                    MinimumToOpenAccount = table.Column<double>(nullable: true),
                    HasMonthlyFee = table.Column<bool>(nullable: false),
                    MonthlyFee = table.Column<double>(nullable: false),
                    BalanceToAvoidFee = table.Column<double>(nullable: true),
                    HasMinimumAmountForApr = table.Column<bool>(nullable: false),
                    MinimumAmountForApr = table.Column<double>(nullable: true),
                    HasMaximumAmountForApr = table.Column<bool>(nullable: false),
                    MaximumAmountForApr = table.Column<double>(nullable: true),
                    AprIfAmountNotMet = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyMarketAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SavingsAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InterestRate = table.Column<double>(nullable: false),
                    InstitutionId = table.Column<int>(nullable: false),
                    HasMinimumToOpenAccount = table.Column<bool>(nullable: false),
                    MinimumToOpenAccount = table.Column<double>(nullable: true),
                    HasMonthlyFee = table.Column<bool>(nullable: false),
                    MonthlyFee = table.Column<double>(nullable: false),
                    BalanceToAvoidFee = table.Column<double>(nullable: true),
                    HasMinimumAmountForApr = table.Column<bool>(nullable: false),
                    MinimumAmountForApr = table.Column<double>(nullable: true),
                    HasMaximumAmountForApr = table.Column<bool>(nullable: false),
                    MaximumAmountForApr = table.Column<double>(nullable: true),
                    AprIfAmountNotMet = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavingsAccounts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3505a316-6fa7-43db-8090-16309ee38bc7", "cfbb6693-be29-47ca-85e4-9ac395bd36ee", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CertificateAccounts");

            migrationBuilder.DropTable(
                name: "CheckingAccounts");

            migrationBuilder.DropTable(
                name: "MoneyMarketAccounts");

            migrationBuilder.DropTable(
                name: "SavingsAccounts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3505a316-6fa7-43db-8090-16309ee38bc7");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b067b4f1-7379-42f8-a197-2a1f4a302a5c", "ad3a7cb5-b23b-4a75-9ee7-ceff61bc04ae", "Admin", "ADMIN" });
        }
    }
}
