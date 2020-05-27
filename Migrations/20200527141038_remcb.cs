using Microsoft.EntityFrameworkCore.Migrations;

namespace CardPortfolio.Migrations
{
    public partial class remcb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CashBacks");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ca6a4d9d-5ef6-4bd6-a759-28e5f78560a4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "386c25b0-c1fb-4258-93a0-b8f0820f1b7e", "c44dfdcd-da6f-4a2b-84cc-7954edd08c0f", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "386c25b0-c1fb-4258-93a0-b8f0820f1b7e");

            migrationBuilder.CreateTable(
                name: "CashBacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CashBackAmount = table.Column<double>(type: "float", nullable: true),
                    CashBackCategory = table.Column<int>(type: "int", nullable: false),
                    CashBackSpendLimit = table.Column<double>(type: "float", nullable: true),
                    CashBackType = table.Column<int>(type: "int", nullable: false),
                    CreditCardId = table.Column<int>(type: "int", nullable: true),
                    HasLimit = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashBacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CashBacks_CreditCards_CreditCardId",
                        column: x => x.CreditCardId,
                        principalTable: "CreditCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ca6a4d9d-5ef6-4bd6-a759-28e5f78560a4", "109fc975-345c-40be-bad0-178008f666ec", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_CashBacks_CreditCardId",
                table: "CashBacks",
                column: "CreditCardId");
        }
    }
}
