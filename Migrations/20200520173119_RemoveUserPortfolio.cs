using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CardPortfolio.Migrations
{
    public partial class RemoveUserPortfolio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCards");

            migrationBuilder.DropTable(
                name: "UserLinesOfCredit");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "71ac88b6-a0e1-4346-bd8b-7faefbdba25f", "4f260c19-3ee3-4561-8294-bf477f455fdd", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "71ac88b6-a0e1-4346-bd8b-7faefbdba25f");

            migrationBuilder.CreateTable(
                name: "UserCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountOpenDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AnnualFee = table.Column<double>(type: "float", nullable: true),
                    BalanceTransferAPR = table.Column<double>(type: "float", nullable: true),
                    CashAdvanceAPR = table.Column<double>(type: "float", nullable: true),
                    CurrentCreditLine = table.Column<double>(type: "float", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstitutionId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchaseAPR = table.Column<double>(type: "float", nullable: true),
                    StartingCreditLine = table.Column<double>(type: "float", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLinesOfCredit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    APR = table.Column<double>(type: "float", nullable: false),
                    AccountOpenDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreditLine = table.Column<double>(type: "float", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstitutionId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLinesOfCredit", x => x.Id);
                });
        }
    }
}
