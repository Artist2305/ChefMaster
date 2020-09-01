using Microsoft.EntityFrameworkCore.Migrations;

namespace Dinner.Migrations
{
    public partial class StabilizeRecipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IngridientsAmountKind",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "IngridientsAmountNumber",
                table: "Recipes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IngridientsAmountKind",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IngridientsAmountNumber",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
