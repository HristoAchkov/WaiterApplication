using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WaiterApplication.Infrastructure.Migrations
{
    public partial class InventoryItemConstraintsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "InventoryItems",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                comment: "Name of the item",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Name of the item");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "InventoryItems",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Name of the item",
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40,
                oldComment: "Name of the item");
        }
    }
}
