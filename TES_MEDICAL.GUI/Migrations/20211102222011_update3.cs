using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TES_MEDICAL.GUI.Migrations
{
    public partial class update3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__TinTuc__MaTL__71D1E811",
                table: "TinTuc");

            migrationBuilder.AlterColumn<Guid>(
                name: "MaTL",
                table: "TinTuc",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Chiều",
                table: "ChiTietToaThuoc",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LanTrongNgay",
                table: "ChiTietToaThuoc",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Sang",
                table: "ChiTietToaThuoc",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Trua",
                table: "ChiTietToaThuoc",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TruocKhian",
                table: "ChiTietToaThuoc",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "VienMoiLan",
                table: "ChiTietToaThuoc",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK__TinTuc__MaTL__71D1E811",
                table: "TinTuc",
                column: "MaTL",
                principalTable: "TheLoai",
                principalColumn: "MaTL",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__TinTuc__MaTL__71D1E811",
                table: "TinTuc");

            migrationBuilder.DropColumn(
                name: "Chiều",
                table: "ChiTietToaThuoc");

            migrationBuilder.DropColumn(
                name: "LanTrongNgay",
                table: "ChiTietToaThuoc");

            migrationBuilder.DropColumn(
                name: "Sang",
                table: "ChiTietToaThuoc");

            migrationBuilder.DropColumn(
                name: "Trua",
                table: "ChiTietToaThuoc");

            migrationBuilder.DropColumn(
                name: "TruocKhian",
                table: "ChiTietToaThuoc");

            migrationBuilder.DropColumn(
                name: "VienMoiLan",
                table: "ChiTietToaThuoc");

            migrationBuilder.AlterColumn<Guid>(
                name: "MaTL",
                table: "TinTuc",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK__TinTuc__MaTL__71D1E811",
                table: "TinTuc",
                column: "MaTL",
                principalTable: "TheLoai",
                principalColumn: "MaTL",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
