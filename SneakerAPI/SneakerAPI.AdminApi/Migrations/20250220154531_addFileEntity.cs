using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SneakerAPI.AdminApi.Migrations
{
    /// <inheritdoc />
    public partial class addFileEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    ProductColorFile__Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductColorFile__Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductColorFile__ProductColorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.ProductColorFile__Id);
                    table.ForeignKey(
                        name: "FK_Files_ProductColors_ProductColorFile__ProductColorId",
                        column: x => x.ProductColorFile__ProductColorId,
                        principalTable: "ProductColors",
                        principalColumn: "ProductColor__Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Files_ProductColorFile__ProductColorId",
                table: "Files",
                column: "ProductColorFile__ProductColorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");
        }
    }
}
