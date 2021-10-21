using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TES_MEDICAL.GUI.Migrations
{
    public partial class update4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "STTTOATHUOC",
                columns: table => new
                {
                    MaPK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    STT = table.Column<int>(type: "int", nullable: false),
                    UuTien = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__STTTOATH__2725E7FDFEC58D61", x => x.MaPK);
                    table.ForeignKey(
                        name: "FK__STTTOATHUO__MaPK__35BCFE0A",
                        column: x => x.MaPK,
                        principalTable: "ToaThuoc",
                        principalColumn: "MaPhieuKham",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "STTTOATHUOC");
        }
    }
}
