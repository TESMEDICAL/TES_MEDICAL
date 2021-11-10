using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TES_MEDICAL.GUI.Migrations
{
    public partial class update9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__ChiTietDV__MaDV__5812160E",
                table: "ChiTietDV");

            migrationBuilder.DropForeignKey(
                name: "FK__ChiTietDV__MaPhi__571DF1D5",
                table: "ChiTietDV");

            migrationBuilder.DropPrimaryKey(
                name: "pk_ctdv",
                table: "ChiTietDV");

            migrationBuilder.DropColumn(
                name: "MaPhieuKham",
                table: "ChiTietDV");

            migrationBuilder.AlterColumn<byte>(
                name: "TrangThai",
                table: "PhieuKham",
                type: "tinyint",
                nullable: false,
                defaultValueSql: "(CONVERT([tinyint],(0)))",
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AddColumn<string>(
                name: "MaHD",
                table: "ChiTietDV",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK__ChiTietD__4557FE8526B532A5",
                table: "ChiTietDV",
                columns: new[] { "MaHD", "MaDV" });

            migrationBuilder.AddForeignKey(
                name: "FK__ChiTietDV__MaDV__40058253",
                table: "ChiTietDV",
                column: "MaDV",
                principalTable: "DichVu",
                principalColumn: "MaDV",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__ChiTietDV__MaHD__3F115E1A",
                table: "ChiTietDV",
                column: "MaHD",
                principalTable: "HoaDon",
                principalColumn: "MaHoaDon",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__ChiTietDV__MaDV__40058253",
                table: "ChiTietDV");

            migrationBuilder.DropForeignKey(
                name: "FK__ChiTietDV__MaHD__3F115E1A",
                table: "ChiTietDV");

            migrationBuilder.DropPrimaryKey(
                name: "PK__ChiTietD__4557FE8526B532A5",
                table: "ChiTietDV");

            migrationBuilder.DropColumn(
                name: "MaHD",
                table: "ChiTietDV");

            migrationBuilder.AlterColumn<byte>(
                name: "TrangThai",
                table: "PhieuKham",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldDefaultValueSql: "(CONVERT([tinyint],(0)))");

            migrationBuilder.AddColumn<Guid>(
                name: "MaPhieuKham",
                table: "ChiTietDV",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "pk_ctdv",
                table: "ChiTietDV",
                columns: new[] { "MaPhieuKham", "MaDV" });

            migrationBuilder.AddForeignKey(
                name: "FK__ChiTietDV__MaDV__5812160E",
                table: "ChiTietDV",
                column: "MaDV",
                principalTable: "DichVu",
                principalColumn: "MaDV",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__ChiTietDV__MaPhi__571DF1D5",
                table: "ChiTietDV",
                column: "MaPhieuKham",
                principalTable: "PhieuKham",
                principalColumn: "MaPK",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
