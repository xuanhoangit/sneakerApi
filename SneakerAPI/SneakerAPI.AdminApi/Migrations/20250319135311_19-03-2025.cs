using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SneakerAPI.AdminApi.Migrations
{
    /// <inheritdoc />
    public partial class _19032025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_CustomerInfos_Address__CustomerInfo",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerInfos_Accounts_CustomerInfo__AccountId",
                table: "CustomerInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductColorSizes_ProductColors_ProductColorSize__ProductColorId",
                table: "ProductColorSizes");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffInfos_Accounts_StaffInfo__AccountId",
                table: "StaffInfos");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_StaffInfos_StaffInfo__AccountId",
                table: "StaffInfos");

            migrationBuilder.DropIndex(
                name: "IX_CustomerInfos_CustomerInfo__AccountId",
                table: "CustomerInfos");

            migrationBuilder.DropColumn(
                name: "Order__Amount",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Brand__CreatedByAccountId",
                table: "Brands");

            migrationBuilder.RenameColumn(
                name: "Order__Total",
                table: "Orders",
                newName: "Order__AmountDue");

            migrationBuilder.AlterColumn<int>(
                name: "StaffInfo__AccountId",
                table: "StaffInfos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order__PaymentMethod",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Order__Type",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerInfo__AccountId",
                table: "CustomerInfos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Address__CustomerInfo",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    CartItem__Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartItem__Quantity = table.Column<int>(type: "int", nullable: false),
                    CartItem__UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CartItem__AmountDue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CartItem__CreatedByAccountId = table.Column<int>(type: "int", nullable: false),
                    CartItem__ProductColorSizeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.CartItem__Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Accounts_CartItem__CreatedByAccountId",
                        column: x => x.CartItem__CreatedByAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_ProductColorSizes_CartItem__ProductColorSizeId",
                        column: x => x.CartItem__ProductColorSizeId,
                        principalTable: "ProductColorSizes",
                        principalColumn: "ProductColorSize__Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderItem__Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderItem__OrderId = table.Column<int>(type: "int", nullable: false),
                    OrderItem__ProductColorSizeId = table.Column<int>(type: "int", nullable: false),
                    OrderItem__Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderItem__UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderItem__AmountDue = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItem__Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderItem__OrderId",
                        column: x => x.OrderItem__OrderId,
                        principalTable: "Orders",
                        principalColumn: "Order__Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_ProductColorSizes_OrderItem__ProductColorSizeId",
                        column: x => x.OrderItem__ProductColorSizeId,
                        principalTable: "ProductColorSizes",
                        principalColumn: "ProductColorSize__Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StaffInfos_StaffInfo__AccountId",
                table: "StaffInfos",
                column: "StaffInfo__AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerInfos_CustomerInfo__AccountId",
                table: "CustomerInfos",
                column: "CustomerInfo__AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartItem__CreatedByAccountId",
                table: "CartItems",
                column: "CartItem__CreatedByAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartItem__ProductColorSizeId",
                table: "CartItems",
                column: "CartItem__ProductColorSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderItem__OrderId",
                table: "OrderItems",
                column: "OrderItem__OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderItem__ProductColorSizeId",
                table: "OrderItems",
                column: "OrderItem__ProductColorSizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_CustomerInfos_Address__CustomerInfo",
                table: "Addresses",
                column: "Address__CustomerInfo",
                principalTable: "CustomerInfos",
                principalColumn: "CustomerInfo__Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerInfos_Accounts_CustomerInfo__AccountId",
                table: "CustomerInfos",
                column: "CustomerInfo__AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColorSizes_ProductColors_ProductColorSize__ProductColorId",
                table: "ProductColorSizes",
                column: "ProductColorSize__ProductColorId",
                principalTable: "ProductColors",
                principalColumn: "ProductColor__Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffInfos_Accounts_StaffInfo__AccountId",
                table: "StaffInfos",
                column: "StaffInfo__AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_CustomerInfos_Address__CustomerInfo",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerInfos_Accounts_CustomerInfo__AccountId",
                table: "CustomerInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductColorSizes_ProductColors_ProductColorSize__ProductColorId",
                table: "ProductColorSizes");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffInfos_Accounts_StaffInfo__AccountId",
                table: "StaffInfos");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_StaffInfos_StaffInfo__AccountId",
                table: "StaffInfos");

            migrationBuilder.DropIndex(
                name: "IX_CustomerInfos_CustomerInfo__AccountId",
                table: "CustomerInfos");

            migrationBuilder.DropColumn(
                name: "Order__PaymentMethod",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Order__Type",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Order__AmountDue",
                table: "Orders",
                newName: "Order__Total");

            migrationBuilder.AlterColumn<int>(
                name: "StaffInfo__AccountId",
                table: "StaffInfos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<decimal>(
                name: "Order__Amount",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerInfo__AccountId",
                table: "CustomerInfos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Brand__CreatedByAccountId",
                table: "Brands",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Address__CustomerInfo",
                table: "Addresses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetail__Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order__Id = table.Column<int>(type: "int", nullable: true),
                    OrderDetail__OrderId = table.Column<int>(type: "int", nullable: false),
                    OrderDetail__ProductColorSizeId = table.Column<int>(type: "int", nullable: false),
                    OrderDetail__Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderDetail__TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderDetail__UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.OrderDetail__Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_Order__Id",
                        column: x => x.Order__Id,
                        principalTable: "Orders",
                        principalColumn: "Order__Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StaffInfos_StaffInfo__AccountId",
                table: "StaffInfos",
                column: "StaffInfo__AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerInfos_CustomerInfo__AccountId",
                table: "CustomerInfos",
                column: "CustomerInfo__AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_Order__Id",
                table: "OrderDetails",
                column: "Order__Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_CustomerInfos_Address__CustomerInfo",
                table: "Addresses",
                column: "Address__CustomerInfo",
                principalTable: "CustomerInfos",
                principalColumn: "CustomerInfo__Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerInfos_Accounts_CustomerInfo__AccountId",
                table: "CustomerInfos",
                column: "CustomerInfo__AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColorSizes_ProductColors_ProductColorSize__ProductColorId",
                table: "ProductColorSizes",
                column: "ProductColorSize__ProductColorId",
                principalTable: "ProductColors",
                principalColumn: "ProductColor__Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StaffInfos_Accounts_StaffInfo__AccountId",
                table: "StaffInfos",
                column: "StaffInfo__AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
