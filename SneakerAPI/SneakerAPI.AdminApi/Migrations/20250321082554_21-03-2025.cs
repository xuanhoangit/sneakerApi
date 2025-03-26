using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SneakerAPI.AdminApi.Migrations
{
    /// <inheritdoc />
    public partial class _21032025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderItem__AmountDue",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "OrderItem__UnitPrice",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "CartItem__AmountDue",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "CartItem__UnitPrice",
                table: "CartItems");

            migrationBuilder.AddColumn<long>(
                name: "Order__PaymentCode",
                table: "Orders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Order__PaymentCode",
                table: "Orders",
                column: "Order__PaymentCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_Order__PaymentCode",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Order__PaymentCode",
                table: "Orders");

            migrationBuilder.AddColumn<decimal>(
                name: "OrderItem__AmountDue",
                table: "OrderItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OrderItem__UnitPrice",
                table: "OrderItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CartItem__AmountDue",
                table: "CartItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CartItem__UnitPrice",
                table: "CartItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
