using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WaiterApplication.Infrastructure.Migrations
{
    public partial class PromoDishAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PromoDishes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Promotion Dish Identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PromotionId = table.Column<int>(type: "int", nullable: false),
                    DishId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromoDishes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromoDishes_Dishes_DishId",
                        column: x => x.DishId,
                        principalTable: "Dishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromoDishes_Promotions_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PromoDishes_DishId",
                table: "PromoDishes",
                column: "DishId");

            migrationBuilder.CreateIndex(
                name: "IX_PromoDishes_PromotionId",
                table: "PromoDishes",
                column: "PromotionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PromoDishes");
        }
    }
}
