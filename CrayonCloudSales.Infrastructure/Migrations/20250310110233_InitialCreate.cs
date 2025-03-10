using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CrayonCloudSales.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SoftwareLicenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    SoftwareServiceId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValidToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CcpSubscriptionId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftwareLicenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoftwareLicenses_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Email", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, "admin@contoso.com", true, "Contoso Ltd" },
                    { 2, "admin@fabrikam.com", true, "Fabrikam Inc" }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "CreatedDate", "CustomerId", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, true, "Contoso Main Account" },
                    { 2, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, true, "Contoso Development" },
                    { 3, new DateTime(2021, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, true, "Fabrikam Main Account" }
                });

            migrationBuilder.InsertData(
                table: "SoftwareLicenses",
                columns: new[] { "Id", "AccountId", "CcpSubscriptionId", "PurchaseDate", "Quantity", "SoftwareServiceId", "State", "ValidToDate" },
                values: new object[,]
                {
                    { 1, 1, "SUB-001", new DateTime(2024, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 1, 0, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, "SUB-002", new DateTime(2024, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 2, 0, new DateTime(2025, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CustomerId",
                table: "Accounts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SoftwareLicenses_AccountId",
                table: "SoftwareLicenses",
                column: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SoftwareLicenses");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
