using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WaiterApplication.Infrastructure.Migrations
{
    public partial class Promotion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "ValidFrom",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "ValidTo",
                table: "Promotions");

            migrationBuilder.RenameColumn(
                name: "DiscountAmount",
                table: "Promotions",
                newName: "Price");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_Promotions_PromotionId",
                table: "Dishes");

            migrationBuilder.DropIndex(
                name: "IX_Dishes_PromotionId",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "PromotionId",
                table: "Dishes");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Promotions",
                newName: "DiscountAmount");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Promotions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidFrom",
                table: "Promotions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidTo",
                table: "Promotions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
