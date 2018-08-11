using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Bjss.PriceBasket.Infrastructure.DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    QuantityAvailable = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpecialOffers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiscountAssemblyFullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialOffers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BuyTwoGetOneHalfPriceSpecialOffers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuyTwoOfProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiscountPercent = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    DiscountedProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpecialOfferId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyTwoGetOneHalfPriceSpecialOffers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuyTwoGetOneHalfPriceSpecialOffers_Products_BuyTwoOfProductId",
                        column: x => x.BuyTwoOfProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BuyTwoGetOneHalfPriceSpecialOffers_Products_DiscountedProductId",
                        column: x => x.DiscountedProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BuyTwoGetOneHalfPriceSpecialOffers_SpecialOffers_SpecialOfferId",
                        column: x => x.SpecialOfferId,
                        principalTable: "SpecialOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscountedProductsSpecialOffers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiscountPercent = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    DiscountedProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpecialOfferId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountedProductsSpecialOffers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiscountedProductsSpecialOffers_Products_DiscountedProductId",
                        column: x => x.DiscountedProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiscountedProductsSpecialOffers_SpecialOffers_SpecialOfferId",
                        column: x => x.SpecialOfferId,
                        principalTable: "SpecialOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuyTwoGetOneHalfPriceSpecialOffers_BuyTwoOfProductId",
                table: "BuyTwoGetOneHalfPriceSpecialOffers",
                column: "BuyTwoOfProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BuyTwoGetOneHalfPriceSpecialOffers_DiscountedProductId",
                table: "BuyTwoGetOneHalfPriceSpecialOffers",
                column: "DiscountedProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BuyTwoGetOneHalfPriceSpecialOffers_SpecialOfferId",
                table: "BuyTwoGetOneHalfPriceSpecialOffers",
                column: "SpecialOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountedProductsSpecialOffers_DiscountedProductId",
                table: "DiscountedProductsSpecialOffers",
                column: "DiscountedProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountedProductsSpecialOffers_SpecialOfferId",
                table: "DiscountedProductsSpecialOffers",
                column: "SpecialOfferId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuyTwoGetOneHalfPriceSpecialOffers");

            migrationBuilder.DropTable(
                name: "DiscountedProductsSpecialOffers");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "SpecialOffers");
        }
    }
}
