using Microsoft.EntityFrameworkCore.Migrations;

namespace TES_MEDICAL.GUI.Migrations
{
    public partial class update10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DonGiaThuoc",
                table: "ChiTietToaThuoc",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DonGiaDV",
                table: "ChiTietDV",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DonGiaThuoc",
                table: "ChiTietToaThuoc");

            migrationBuilder.DropColumn(
                name: "DonGiaDV",
                table: "ChiTietDV");
        }
    }
}
