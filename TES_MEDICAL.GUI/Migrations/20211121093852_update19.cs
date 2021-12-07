using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TES_MEDICAL.GUI.Migrations
{
    public partial class update19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "ChiTietBenhPK",
                columns: table => new
                {
                    MaPK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaBenh = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ListTrieuChung = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChiTietbenhPK__FAFAGA799DSADSA5", x => new { x.MaBenh, x.MaPK });
                    table.ForeignKey(
                        name: "FK__ChiTietBenhPK__MaBenh__3G2415E1A",
                        column: x => x.MaBenh,
                        principalTable: "Benh",
                        principalColumn: "MaBenh",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__ChiTietBenhPK__MaPk__43436334",
                        column: x => x.MaPK,
                        principalTable: "PhieuKham",
                        principalColumn: "MaPK",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietBenhPK_MaPK",
                table: "ChiTietBenhPK",
                column: "MaPK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
