using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WaiterApplication.Infrastructure.Migrations
{
    public partial class AddedNullableProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Promotions_Orders_OrderId",
                table: "Promotions");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "Promotions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Promotions_Orders_OrderId",
                table: "Promotions",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Promotions_Orders_OrderId",
                table: "Promotions");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "Promotions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Promotions_Orders_OrderId",
                table: "Promotions",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
