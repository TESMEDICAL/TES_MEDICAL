using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TES_MEDICAL.GUI.Migrations
{
    public partial class update12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MaBenh",
                table: "PhieuKham",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhieuKham_MaBenh",
                table: "PhieuKham",
                column: "MaBenh");

            migrationBuilder.AddForeignKey(
                name: "FK_PhieuKham_MaBenh",
                table: "PhieuKham",
                column: "MaBenh",
                principalTable: "Benh",
                principalColumn: "MaBenh",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhieuKham_MaBenh",
                table: "PhieuKham");

            migrationBuilder.DropIndex(
                name: "IX_PhieuKham_MaBenh",
                table: "PhieuKham");

            migrationBuilder.DropColumn(
                name: "MaBenh",
                table: "PhieuKham");
        }
    }
}
