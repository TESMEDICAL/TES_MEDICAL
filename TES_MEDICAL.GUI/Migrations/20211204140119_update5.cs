using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TES_MEDICAL.GUI.Migrations
{
    public partial class update5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhieuKham_MaBenh",
                table: "PhieuKham");

           

            migrationBuilder.RenameColumn(
                name: "MaBenh",
                table: "PhieuKham",
                newName: "BenhMaBenh");

            migrationBuilder.RenameIndex(
                name: "IX_PhieuKham_MaBenh",
                table: "PhieuKham",
                newName: "IX_PhieuKham_BenhMaBenh");

            migrationBuilder.AddForeignKey(
                name: "FK_PhieuKham_Benh_BenhMaBenh",
                table: "PhieuKham",
                column: "BenhMaBenh",
                principalTable: "Benh",
                principalColumn: "MaBenh",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhieuKham_Benh_BenhMaBenh",
                table: "PhieuKham");

            migrationBuilder.RenameColumn(
                name: "BenhMaBenh",
                table: "PhieuKham",
                newName: "MaBenh");

            migrationBuilder.RenameIndex(
                name: "IX_PhieuKham_BenhMaBenh",
                table: "PhieuKham",
                newName: "IX_PhieuKham_MaBenh");

            migrationBuilder.AddColumn<string>(
                name: "KetQuaKham",
                table: "PhieuKham",
                type: "nvarchar(max)",
                nullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_PhieuKham_MaBenh",
                table: "PhieuKham",
                column: "MaBenh",
                principalTable: "Benh",
                principalColumn: "MaBenh",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
