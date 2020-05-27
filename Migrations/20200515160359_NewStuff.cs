using Microsoft.EntityFrameworkCore.Migrations;

namespace CardPortfolio.Migrations
{
    public partial class NewStuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewCarHighAPR",
                table: "AutoLoans");

            migrationBuilder.DropColumn(
                name: "NewCarLowAPR",
                table: "AutoLoans");

            migrationBuilder.DropColumn(
                name: "UsedCarHighAPR",
                table: "AutoLoans");

            migrationBuilder.DropColumn(
                name: "UsedCarLowAPR",
                table: "AutoLoans");

            migrationBuilder.AlterColumn<double>(
                name: "LowApr",
                table: "UnsecuredPersonalLoans",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<bool>(
                name: "ArePaymentsInterestOnly",
                table: "UnsecuredLinesOfCredit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "MinimumPayment",
                table: "UnsecuredLinesOfCredit",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "LowApr",
                table: "SecuredPersonalLoans",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<bool>(
                name: "ArePaymentsInterestOnly",
                table: "SecuredLinesOfCredit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "MinimumPayment",
                table: "SecuredLinesOfCredit",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "LowApr",
                table: "HomeEquityLoans",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<bool>(
                name: "ArePaymentsInterestOnly",
                table: "HomeEquityLinesOfCredit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "MinimumPayment",
                table: "HomeEquityLinesOfCredit",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "LowApr",
                table: "AutoLoans",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArePaymentsInterestOnly",
                table: "UnsecuredLinesOfCredit");

            migrationBuilder.DropColumn(
                name: "MinimumPayment",
                table: "UnsecuredLinesOfCredit");

            migrationBuilder.DropColumn(
                name: "ArePaymentsInterestOnly",
                table: "SecuredLinesOfCredit");

            migrationBuilder.DropColumn(
                name: "MinimumPayment",
                table: "SecuredLinesOfCredit");

            migrationBuilder.DropColumn(
                name: "ArePaymentsInterestOnly",
                table: "HomeEquityLinesOfCredit");

            migrationBuilder.DropColumn(
                name: "MinimumPayment",
                table: "HomeEquityLinesOfCredit");

            migrationBuilder.AlterColumn<double>(
                name: "LowApr",
                table: "UnsecuredPersonalLoans",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "LowApr",
                table: "SecuredPersonalLoans",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "LowApr",
                table: "HomeEquityLoans",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "LowApr",
                table: "AutoLoans",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "NewCarHighAPR",
                table: "AutoLoans",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "NewCarLowAPR",
                table: "AutoLoans",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "UsedCarHighAPR",
                table: "AutoLoans",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "UsedCarLowAPR",
                table: "AutoLoans",
                type: "float",
                nullable: true);
        }
    }
}
