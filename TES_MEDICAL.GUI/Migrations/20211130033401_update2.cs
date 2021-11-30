using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TES_MEDICAL.GUI.Migrations
{
    public partial class update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                table: "PhieuDatLich",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GhiChu",
                table: "PhieuDatLich");

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
