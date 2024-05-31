using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WaiterApplication.Infrastructure.Migrations
{
    public partial class PromotionOrderIdAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Dishes_PromotionId",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "PromotionId",
                table: "Dishes");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Promotions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Promotions_OrderId",
                table: "Promotions",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Promotions_Orders_OrderId",
                table: "Promotions",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Promotions_Orders_OrderId",
                table: "Promotions");

            migrationBuilder.DropIndex(
                name: "IX_Promotions_OrderId",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Promotions");

            migrationBuilder.AddColumn<int>(
                name: "PromotionId",
                table: "Dishes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dishes_PromotionId",
                table: "Dishes",
                column: "PromotionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_Promotions_PromotionId",
                table: "Dishes",
                column: "PromotionId",
                principalTable: "Promotions",
                principalColumn: "Id");
        }
    }
}
