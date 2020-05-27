using Microsoft.EntityFrameworkCore.Migrations;

namespace CardPortfolio.Migrations
{
    public partial class UpdatedALittle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "CashBack");

            migrationBuilder.AddColumn<int>(
                name: "SignUpBonusCategory",
                table: "CreditCards",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SignUpBonusCategory",
                table: "CreditCards");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "CashBack",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
