using Microsoft.EntityFrameworkCore.Migrations;

namespace Dinner.Migrations
{
    public partial class SeedRecipeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "Description", "Name", "Rating", "RecipeType"},
                values: new object[] { 1, "Awesome taste! Awesome taste! Awesome taste! Awesome taste! Awesome taste! ", "Coocked chicken breast", 0, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
