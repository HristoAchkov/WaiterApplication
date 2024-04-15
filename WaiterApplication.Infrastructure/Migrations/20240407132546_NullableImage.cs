using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WaiterApplication.Infrastructure.Migrations
{
    public partial class NullableImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TableName",
                table: "Tables",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                comment: "Name of the table",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "Name of the table");

            migrationBuilder.AlterColumn<int>(
                name: "TableNumber",
                table: "Orders",
                type: "int",
                nullable: false,
                comment: "Foreign Key Table Identifier",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Dishes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "Dish name",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldComment: "Dish name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TableName",
                table: "Tables",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "Name of the table",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldComment: "Name of the table");

            migrationBuilder.AlterColumn<int>(
                name: "TableNumber",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Foreign Key Table Identifier");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Dishes",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                comment: "Dish name",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "Dish name");
        }
    }
}
