using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WaiterApplication.Infrastructure.Migrations
{
    public partial class PromoName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Promotions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Promotions");
        }
    }
}
