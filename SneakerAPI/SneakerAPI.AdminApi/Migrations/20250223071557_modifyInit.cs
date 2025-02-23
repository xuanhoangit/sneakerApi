using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SneakerAPI.AdminApi.Migrations
{
    /// <inheritdoc />
    public partial class modifyInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand__CreatedUserId",
                table: "Brands");

            migrationBuilder.AddColumn<int>(
                name: "Brand__CreatedByAccountId",
                table: "Brands",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand__CreatedByAccountId",
                table: "Brands");

            migrationBuilder.AddColumn<string>(
                name: "Brand__CreatedUserId",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
