using Microsoft.EntityFrameworkCore.Migrations;

namespace Dinner.Migrations
{
    public partial class RecipeHugeChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ingridients",
                table: "Recipes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IngridientsAmountKind",
                table: "Recipes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IngridientsAmountNumber",
                table: "Recipes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "Recipes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Time",
                table: "Recipes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ingridients",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "IngridientsAmountKind",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "IngridientsAmountNumber",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "Tags",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Recipes");
        }
    }
}
