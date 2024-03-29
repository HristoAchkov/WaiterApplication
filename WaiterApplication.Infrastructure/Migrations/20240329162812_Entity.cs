using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WaiterApplication.Infrastructure.Migrations
{
    public partial class Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "OrderDishes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UnitOfMeasurement",
                table: "InventoryItems",
                type: "nvarchar(max)",
                nullable: true,
                comment: "Item unit of measurement: kg, l, mg, etc...",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Item unit of measurement: kg, l, mg, etc...");

            migrationBuilder.AlterColumn<string>(
                name: "Ingredients",
                table: "Dishes",
                type: "nvarchar(max)",
                nullable: true,
                comment: "Dish ingredients",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Dish ingredients");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "OrderDishes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UnitOfMeasurement",
                table: "InventoryItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "Item unit of measurement: kg, l, mg, etc...",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "Item unit of measurement: kg, l, mg, etc...");

            migrationBuilder.AlterColumn<string>(
                name: "Ingredients",
                table: "Dishes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "Dish ingredients",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "Dish ingredients");
        }
    }
}
