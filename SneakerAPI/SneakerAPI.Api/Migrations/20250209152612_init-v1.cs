using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SneakerAPI.Api.Migrations
{
    /// <inheritdoc />
    public partial class initv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_Products_Product__Id",
                table: "ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_Product__Id",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "Product__Id",
                table: "ProductCategories");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_ProductCategory__ProductId",
                table: "ProductCategories",
                column: "ProductCategory__ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_Products_ProductCategory__ProductId",
                table: "ProductCategories",
                column: "ProductCategory__ProductId",
                principalTable: "Products",
                principalColumn: "Product__Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_Products_ProductCategory__ProductId",
                table: "ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_ProductCategory__ProductId",
                table: "ProductCategories");

            migrationBuilder.AddColumn<int>(
                name: "Product__Id",
                table: "ProductCategories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_Product__Id",
                table: "ProductCategories",
                column: "Product__Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_Products_Product__Id",
                table: "ProductCategories",
                column: "Product__Id",
                principalTable: "Products",
                principalColumn: "Product__Id");
        }
    }
}
