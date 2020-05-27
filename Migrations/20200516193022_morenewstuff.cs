using Microsoft.EntityFrameworkCore.Migrations;

namespace CardPortfolio.Migrations
{
    public partial class morenewstuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "CollateralToLocRatio",
                table: "SecuredLinesOfCredit",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.CreateTable(
                name: "Mortgages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LowApr = table.Column<double>(nullable: true),
                    HighApr = table.Column<double>(nullable: true),
                    InstitutionId = table.Column<int>(nullable: true),
                    MinimumAmount = table.Column<double>(nullable: true),
                    MaximumAmount = table.Column<double>(nullable: true),
                    MinimumTermInMonths = table.Column<int>(nullable: true),
                    MaximumTermInMonths = table.Column<int>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    HasFees = table.Column<bool>(nullable: false),
                    OriginationFee = table.Column<double>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    LoanToValue = table.Column<double>(nullable: true),
                    MortgageType = table.Column<int>(nullable: false),
                    DownPaymentPercentage = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mortgages", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mortgages");

            migrationBuilder.AlterColumn<double>(
                name: "CollateralToLocRatio",
                table: "SecuredLinesOfCredit",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}
