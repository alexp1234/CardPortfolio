using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CardPortfolio.Migrations
{
    public partial class NewModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashBack_CreditCards_CreditCardId",
                table: "CashBack");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CashBack",
                table: "CashBack");

            migrationBuilder.RenameTable(
                name: "CashBack",
                newName: "CashBacks");

            migrationBuilder.RenameIndex(
                name: "IX_CashBack_CreditCardId",
                table: "CashBacks",
                newName: "IX_CashBacks_CreditCardId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CashBacks",
                table: "CashBacks",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AutoLoans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LowApr = table.Column<double>(nullable: false),
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
                    NewCarLowAPR = table.Column<double>(nullable: true),
                    NewCarHighAPR = table.Column<double>(nullable: true),
                    UsedCarLowAPR = table.Column<double>(nullable: true),
                    UsedCarHighAPR = table.Column<double>(nullable: true),
                    AutoLoanCategory = table.Column<int>(nullable: false),
                    DownPaymentPercentage = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoLoans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogPosts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Abstract = table.Column<string>(nullable: true),
                    BodyText = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    IsPublished = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HomeEquityLinesOfCredit",
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
                    HasOriginationFee = table.Column<bool>(nullable: false),
                    OriginationFee = table.Column<double>(nullable: true),
                    HasAnnualFee = table.Column<bool>(nullable: false),
                    AnnualFee = table.Column<double>(nullable: true),
                    HasAdvanceFee = table.Column<bool>(nullable: false),
                    AdvanceFee = table.Column<double>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    LTV = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeEquityLinesOfCredit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HomeEquityLoans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LowApr = table.Column<double>(nullable: false),
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
                    LTV = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeEquityLoans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SecuredLinesOfCredit",
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
                    HasOriginationFee = table.Column<bool>(nullable: false),
                    OriginationFee = table.Column<double>(nullable: true),
                    HasAnnualFee = table.Column<bool>(nullable: false),
                    AnnualFee = table.Column<double>(nullable: true),
                    HasAdvanceFee = table.Column<bool>(nullable: false),
                    AdvanceFee = table.Column<double>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    CollateralToLocRatio = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecuredLinesOfCredit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SecuredPersonalLoans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LowApr = table.Column<double>(nullable: false),
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
                    CollateralToLoanRatio = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecuredPersonalLoans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnsecuredLinesOfCredit",
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
                    HasOriginationFee = table.Column<bool>(nullable: false),
                    OriginationFee = table.Column<double>(nullable: true),
                    HasAnnualFee = table.Column<bool>(nullable: false),
                    AnnualFee = table.Column<double>(nullable: true),
                    HasAdvanceFee = table.Column<bool>(nullable: false),
                    AdvanceFee = table.Column<double>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnsecuredLinesOfCredit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnsecuredPersonalLoans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LowApr = table.Column<double>(nullable: false),
                    HighApr = table.Column<double>(nullable: true),
                    InstitutionId = table.Column<int>(nullable: true),
                    MinimumAmount = table.Column<double>(nullable: true),
                    MaximumAmount = table.Column<double>(nullable: true),
                    MinimumTermInMonths = table.Column<int>(nullable: true),
                    MaximumTermInMonths = table.Column<int>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    HasFees = table.Column<bool>(nullable: false),
                    OriginationFee = table.Column<double>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnsecuredPersonalLoans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLinesOfCredit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    CreditLine = table.Column<double>(nullable: false),
                    InstitutionId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    AccountOpenDate = table.Column<DateTime>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    APR = table.Column<double>(nullable: false),
                    ImagePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLinesOfCredit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogPostId = table.Column<int>(nullable: false),
                    AuthorId = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    UpdateReason = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_BlogPosts_BlogPostId",
                        column: x => x.BlogPostId,
                        principalTable: "BlogPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_BlogPostId",
                table: "Comments",
                column: "BlogPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_CashBacks_CreditCards_CreditCardId",
                table: "CashBacks",
                column: "CreditCardId",
                principalTable: "CreditCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashBacks_CreditCards_CreditCardId",
                table: "CashBacks");

            migrationBuilder.DropTable(
                name: "AutoLoans");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "HomeEquityLinesOfCredit");

            migrationBuilder.DropTable(
                name: "HomeEquityLoans");

            migrationBuilder.DropTable(
                name: "SecuredLinesOfCredit");

            migrationBuilder.DropTable(
                name: "SecuredPersonalLoans");

            migrationBuilder.DropTable(
                name: "UnsecuredLinesOfCredit");

            migrationBuilder.DropTable(
                name: "UnsecuredPersonalLoans");

            migrationBuilder.DropTable(
                name: "UserLinesOfCredit");

            migrationBuilder.DropTable(
                name: "BlogPosts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CashBacks",
                table: "CashBacks");

            migrationBuilder.RenameTable(
                name: "CashBacks",
                newName: "CashBack");

            migrationBuilder.RenameIndex(
                name: "IX_CashBacks_CreditCardId",
                table: "CashBack",
                newName: "IX_CashBack_CreditCardId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CashBack",
                table: "CashBack",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CashBack_CreditCards_CreditCardId",
                table: "CashBack",
                column: "CreditCardId",
                principalTable: "CreditCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
