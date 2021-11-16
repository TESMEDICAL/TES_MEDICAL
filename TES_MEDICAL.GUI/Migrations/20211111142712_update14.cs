using Microsoft.EntityFrameworkCore.Migrations;

namespace TES_MEDICAL.GUI.Migrations
{
    public partial class update14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenTrieuChungKhongDau",
                table: "TrieuChung");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TenTrieuChungKhongDau",
                table: "TrieuChung",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
