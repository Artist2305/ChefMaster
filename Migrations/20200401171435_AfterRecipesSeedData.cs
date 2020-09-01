using Microsoft.EntityFrameworkCore.Migrations;

namespace Dinner.Migrations
{
    public partial class AfterRecipesSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoPatch",
                table: "Recipes",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "Description", "Name", "PhotoPatch", "Rating", "RecipeType" },
                values: new object[] { 2, "Awesome taste! Awesome taste! Awesome taste! Awesome taste! Awesome taste! ", "Cookies", null, 0, 16 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "PhotoPatch",
                table: "Recipes");
        }
    }
}
