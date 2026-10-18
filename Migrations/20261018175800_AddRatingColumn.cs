using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recipendium_Reparsed.Migrations
{
    /// <inheritdoc />
    public partial class AddRatingColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Recipe",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Recipe");
        }
    }
}
