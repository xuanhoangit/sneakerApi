using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SneakerAPI.Api.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Brand__Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand__Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand__Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand__Logo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand__CreatedUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand__Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Brand__Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Category__Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category__Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Category__Id);
                });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Color__Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Color__Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Color__Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Role__Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role__Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Role__Id);
                });

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    Size__Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Size__Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.Size__Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Tag__Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tag__Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Tag__Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Account__Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Account__Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Account__PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Account__RoleId = table.Column<int>(type: "int", nullable: true),
                    Account__IsActive = table.Column<bool>(type: "bit", nullable: true),
                    Account__IsBlocked = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Account__Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Roles_Account__RoleId",
                        column: x => x.Account__RoleId,
                        principalTable: "Roles",
                        principalColumn: "Role__Id");
                });

            migrationBuilder.CreateTable(
                name: "CustomerInfos",
                columns: table => new
                {
                    CustomerInfo__Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerInfo__FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerInfo__LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerInfo__Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerInfo__Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerInfo__TotalSpent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustomerInfo__SpendingPoint = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustomerInfo__AccountId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerInfos", x => x.CustomerInfo__Id);
                    table.ForeignKey(
                        name: "FK_CustomerInfos_Accounts_CustomerInfo__AccountId",
                        column: x => x.CustomerInfo__AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Account__Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Order__Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order__CreatedByAccountId = table.Column<int>(type: "int", nullable: false),
                    Order__Status = table.Column<int>(type: "int", nullable: false),
                    Order__Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Order__Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Order__CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Order__Id);
                    table.ForeignKey(
                        name: "FK_Orders_Accounts_Order__CreatedByAccountId",
                        column: x => x.Order__CreatedByAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Account__Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Product__Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product__Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Product__Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Product__CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Product__UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Product__CreatedByAccountId = table.Column<int>(type: "int", nullable: false),
                    Product__BrandId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Product__Id);
                    table.ForeignKey(
                        name: "FK_Products_Accounts_Product__CreatedByAccountId",
                        column: x => x.Product__CreatedByAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Account__Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Brands_Product__BrandId",
                        column: x => x.Product__BrandId,
                        principalTable: "Brands",
                        principalColumn: "Brand__Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StaffInfos",
                columns: table => new
                {
                    StaffInfo__Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffInfo__FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StaffInfo__LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StaffInfo__Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StaffInfo__Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StaffInfo__AccountId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffInfos", x => x.StaffInfo__Id);
                    table.ForeignKey(
                        name: "FK_StaffInfos_Accounts_StaffInfo__AccountId",
                        column: x => x.StaffInfo__AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Account__Id");
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Address__Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address__FullAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address__Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address__IsDefault = table.Column<bool>(type: "bit", nullable: true),
                    Address__ReceiverName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address__CustomerInfo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Address__Id);
                    table.ForeignKey(
                        name: "FK_Addresses_CustomerInfos_Address__CustomerInfo",
                        column: x => x.Address__CustomerInfo,
                        principalTable: "CustomerInfos",
                        principalColumn: "CustomerInfo__Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetail__Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDetail__OrderId = table.Column<int>(type: "int", nullable: false),
                    Order__Id = table.Column<int>(type: "int", nullable: true),
                    OrderDetail__ProductColorSizeId = table.Column<int>(type: "int", nullable: false),
                    OrderDetail__Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderDetail__UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderDetail__TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    ProductCategory__Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductCategory__ProductId = table.Column<int>(type: "int", nullable: true),
                    Product__Id = table.Column<int>(type: "int", nullable: true),
                    ProductCategory__CategoryId = table.Column<int>(type: "int", nullable: true),
                    Category__Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.ProductCategory__Id);
                    table.ForeignKey(
                        name: "FK_ProductCategories_Categories_Category__Id",
                        column: x => x.Category__Id,
                        principalTable: "Categories",
                        principalColumn: "Category__Id");
                    table.ForeignKey(
                        name: "FK_ProductCategories_Products_Product__Id",
                        column: x => x.Product__Id,
                        principalTable: "Products",
                        principalColumn: "Product__Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductColors",
                columns: table => new
                {
                    ProductColor__Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductColor__Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductColor__ColorId = table.Column<int>(type: "int", nullable: false),
                    ProductColor__ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductColors", x => x.ProductColor__Id);
                    table.ForeignKey(
                        name: "FK_ProductColors_Colors_ProductColor__ColorId",
                        column: x => x.ProductColor__ColorId,
                        principalTable: "Colors",
                        principalColumn: "Color__Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductColors_Products_ProductColor__ProductId",
                        column: x => x.ProductColor__ProductId,
                        principalTable: "Products",
                        principalColumn: "Product__Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTags",
                columns: table => new
                {
                    ProductTag__Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductTag__ProductId = table.Column<int>(type: "int", nullable: false),
                    Product__Id = table.Column<int>(type: "int", nullable: true),
                    ProductTag__TagId = table.Column<int>(type: "int", nullable: false),
                    Tag__Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTags", x => x.ProductTag__Id);
                    table.ForeignKey(
                        name: "FK_ProductTags_Products_Product__Id",
                        column: x => x.Product__Id,
                        principalTable: "Products",
                        principalColumn: "Product__Id");
                    table.ForeignKey(
                        name: "FK_ProductTags_Tags_Tag__Id",
                        column: x => x.Tag__Id,
                        principalTable: "Tags",
                        principalColumn: "Tag__Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductColorSizes",
                columns: table => new
                {
                    ProductColorSize__Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductColorSize__Quantity = table.Column<int>(type: "int", nullable: false),
                    ProductColorSize__SizeId = table.Column<int>(type: "int", nullable: false),
                    ProductColorSize__ProductColorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductColorSizes", x => x.ProductColorSize__Id);
                    table.ForeignKey(
                        name: "FK_ProductColorSizes_ProductColors_ProductColorSize__ProductColorId",
                        column: x => x.ProductColorSize__ProductColorId,
                        principalTable: "ProductColors",
                        principalColumn: "ProductColor__Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductColorSizes_Sizes_ProductColorSize__SizeId",
                        column: x => x.ProductColorSize__SizeId,
                        principalTable: "Sizes",
                        principalColumn: "Size__Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Account__RoleId",
                table: "Accounts",
                column: "Account__RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_Address__CustomerInfo",
                table: "Addresses",
                column: "Address__CustomerInfo");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerInfos_CustomerInfo__AccountId",
                table: "CustomerInfos",
                column: "CustomerInfo__AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_Order__Id",
                table: "OrderDetails",
                column: "Order__Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Order__CreatedByAccountId",
                table: "Orders",
                column: "Order__CreatedByAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_Category__Id",
                table: "ProductCategories",
                column: "Category__Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_Product__Id",
                table: "ProductCategories",
                column: "Product__Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColors_ProductColor__ColorId",
                table: "ProductColors",
                column: "ProductColor__ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColors_ProductColor__ProductId",
                table: "ProductColors",
                column: "ProductColor__ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColorSizes_ProductColorSize__ProductColorId",
                table: "ProductColorSizes",
                column: "ProductColorSize__ProductColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColorSizes_ProductColorSize__SizeId",
                table: "ProductColorSizes",
                column: "ProductColorSize__SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Product__BrandId",
                table: "Products",
                column: "Product__BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Product__CreatedByAccountId",
                table: "Products",
                column: "Product__CreatedByAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTags_Product__Id",
                table: "ProductTags",
                column: "Product__Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTags_Tag__Id",
                table: "ProductTags",
                column: "Tag__Id");

            migrationBuilder.CreateIndex(
                name: "IX_StaffInfos_StaffInfo__AccountId",
                table: "StaffInfos",
                column: "StaffInfo__AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "ProductColorSizes");

            migrationBuilder.DropTable(
                name: "ProductTags");

            migrationBuilder.DropTable(
                name: "StaffInfos");

            migrationBuilder.DropTable(
                name: "CustomerInfos");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "ProductColors");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
