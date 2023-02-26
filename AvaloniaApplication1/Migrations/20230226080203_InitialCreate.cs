using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AvaloniaApplication1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    clientId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fio = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    passport = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    phone = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("clients_pkey", x => x.clientId);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    manufCountry = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    brand = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    model = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    color = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Count = table.Column<int>(type: "integer", nullable: true),
                    price = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("products_pkey", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "typePurchases",
                columns: table => new
                {
                    purchaseId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    productIdFk = table.Column<int>(type: "integer", nullable: true),
                    clientIdFk = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("typePurchase_pkey", x => x.purchaseId);
                });

            migrationBuilder.CreateTable(
                name: "purchases",
                columns: table => new
                {
                    purchaseId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    productIdFk = table.Column<int>(type: "integer", nullable: true),
                    clientIdFk = table.Column<int>(type: "integer", nullable: true),
                    typePurchaseIdFk = table.Column<int>(type: "integer", maxLength: 255, nullable: true),
                    datePurchase = table.Column<DateOnly>(type: "date", nullable: true),
                    TypesPurchaseTypeId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("purchase_pkey", x => x.purchaseId);
                    table.ForeignKey(
                        name: "FK_purchases_typePurchases_TypesPurchaseTypeId",
                        column: x => x.TypesPurchaseTypeId,
                        principalTable: "typePurchases",
                        principalColumn: "purchaseId");
                    table.ForeignKey(
                        name: "payments_clientIdFk_fkey",
                        column: x => x.clientIdFk,
                        principalTable: "clients",
                        principalColumn: "clientId");
                    table.ForeignKey(
                        name: "purchases_productIdFk_fkey",
                        column: x => x.productIdFk,
                        principalTable: "products",
                        principalColumn: "ProductId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_purchases_clientIdFk",
                table: "purchases",
                column: "clientIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_purchases_productIdFk",
                table: "purchases",
                column: "productIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_purchases_TypesPurchaseTypeId",
                table: "purchases",
                column: "TypesPurchaseTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "purchases");

            migrationBuilder.DropTable(
                name: "typePurchases");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "products");
        }
    }
}
