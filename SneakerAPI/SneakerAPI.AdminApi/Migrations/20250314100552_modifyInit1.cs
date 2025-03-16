using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SneakerAPI.AdminApi.Migrations
{
    /// <inheritdoc />
    public partial class modifyInit1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductColor__Status",
                table: "ProductColors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductColor__Status",
                table: "ProductColors");
        }
    }
}
