using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TES_MEDICAL.GUI.Migrations
{
    public partial class update3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "ChiTietBenh",
                columns: table => new
                {
                    MaPK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaBenh = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KetQuaKham = table.Column<Guid>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChiTietBenh__339EF89FCA764F6C", x => new { x.MaPK, x.MaBenh });
                    table.ForeignKey(
                        name: "FK__ChiTietBenh__MaBenh__3B75D760",
                        column: x => x.MaBenh,
                        principalTable: "Benh",
                        principalColumn: "MaBenh",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__ChiTietBenh__MaPK__3A81B327",
                        column: x => x.MaPK,
                        principalTable: "PhieuKham",
                        principalColumn: "MaPK",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietBenh_MaBenh",
                table: "ChiTietBenh",
                column: "MaBenh");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietBenh");

            migrationBuilder.CreateTable(
                name: "ListResponse",
                columns: table => new
                {
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "ResponseChanDoan",
                columns: table => new
                {
                    MaBenh = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoTrieuChung = table.Column<int>(type: "int", nullable: false),
                    TenBenh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongCong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "ResponseHoaDon",
                columns: table => new
                {
                    MaHD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayHD = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenBN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenBS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenNV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "ScalarInt",
                columns: table => new
                {
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "ThongKeBenhViewModel",
                columns: table => new
                {
                    soLuong = table.Column<int>(type: "int", nullable: false),
                    tenBenh = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "ThongKeDichVuViewModel",
                columns: table => new
                {
                    Nam = table.Column<int>(type: "int", nullable: false),
                    Thang = table.Column<int>(type: "int", nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "ThongKeLuotKhamViewModel",
                columns: table => new
                {
                    luotKham = table.Column<int>(type: "int", nullable: false),
                    nam = table.Column<int>(type: "int", nullable: false),
                    thang = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "ThongKeSoLuongThuoc",
                columns: table => new
                {
                    soLuong = table.Column<int>(type: "int", nullable: false),
                    tenThuoc = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });
        }
    }
}
