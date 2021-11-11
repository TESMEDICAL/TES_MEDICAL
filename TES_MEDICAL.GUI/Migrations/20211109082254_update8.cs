using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TES_MEDICAL.GUI.Migrations
{
    public partial class update8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__CTTrieuCh__MaBen__4316F928",
                table: "CTTrieuChung");

            migrationBuilder.DropForeignKey(
                name: "FK__CTTrieuCh__TenTr__440B1D61",
                table: "CTTrieuChung");

            migrationBuilder.DropPrimaryKey(
                name: "PK__TrieuChu__38C0D567B7BAD8FD",
                table: "TrieuChung");

            migrationBuilder.DropPrimaryKey(
                name: "cttc_pk",
                table: "CTTrieuChung");

            migrationBuilder.DropIndex(
                name: "IX_CTTrieuChung_TenTrieuChung",
                table: "CTTrieuChung");

            migrationBuilder.DropColumn(
                name: "TenTrieuChung",
                table: "CTTrieuChung");

            migrationBuilder.AlterColumn<string>(
                name: "TenTrieuChungKhongDau",
                table: "TrieuChung",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TenTrieuChung",
                table: "TrieuChung",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<Guid>(
                name: "MatrieuChung",
                table: "TrieuChung",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "ChiTietTrieuChung",
                table: "CTTrieuChung",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "MaTrieuChung",
                table: "CTTrieuChung",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK__TrieuChu__18521B702BAAC1B3",
                table: "TrieuChung",
                column: "MatrieuChung");

            migrationBuilder.AddPrimaryKey(
                name: "PK__CTTrieuC__E45FC2F731FF98AB",
                table: "CTTrieuChung",
                columns: new[] { "MaBenh", "MaTrieuChung" });

            migrationBuilder.CreateIndex(
                name: "UQ__TrieuChu__38C0D5667814C499",
                table: "TrieuChung",
                column: "TenTrieuChung",
                unique: true,
                filter: "[TenTrieuChung] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CTTrieuChung_MaTrieuChung",
                table: "CTTrieuChung",
                column: "MaTrieuChung");

            migrationBuilder.AddForeignKey(
                name: "FK__CTTrieuCh__MaBen__19DFD96B",
                table: "CTTrieuChung",
                column: "MaBenh",
                principalTable: "Benh",
                principalColumn: "MaBenh",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__CTTrieuCh__MaTri__1AD3FDA4",
                table: "CTTrieuChung",
                column: "MaTrieuChung",
                principalTable: "TrieuChung",
                principalColumn: "MatrieuChung",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__CTTrieuCh__MaBen__19DFD96B",
                table: "CTTrieuChung");

            migrationBuilder.DropForeignKey(
                name: "FK__CTTrieuCh__MaTri__1AD3FDA4",
                table: "CTTrieuChung");

            migrationBuilder.DropPrimaryKey(
                name: "PK__TrieuChu__18521B702BAAC1B3",
                table: "TrieuChung");

            migrationBuilder.DropIndex(
                name: "UQ__TrieuChu__38C0D5667814C499",
                table: "TrieuChung");

            migrationBuilder.DropPrimaryKey(
                name: "PK__CTTrieuC__E45FC2F731FF98AB",
                table: "CTTrieuChung");

            migrationBuilder.DropIndex(
                name: "IX_CTTrieuChung_MaTrieuChung",
                table: "CTTrieuChung");

            migrationBuilder.DropColumn(
                name: "MatrieuChung",
                table: "TrieuChung");

            migrationBuilder.DropColumn(
                name: "MaTrieuChung",
                table: "CTTrieuChung");

            migrationBuilder.AlterColumn<string>(
                name: "TenTrieuChungKhongDau",
                table: "TrieuChung",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TenTrieuChung",
                table: "TrieuChung",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ChiTietTrieuChung",
                table: "CTTrieuChung",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenTrieuChung",
                table: "CTTrieuChung",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK__TrieuChu__38C0D567B7BAD8FD",
                table: "TrieuChung",
                column: "TenTrieuChung");

            migrationBuilder.AddPrimaryKey(
                name: "cttc_pk",
                table: "CTTrieuChung",
                columns: new[] { "MaBenh", "TenTrieuChung" });

            migrationBuilder.CreateIndex(
                name: "IX_CTTrieuChung_TenTrieuChung",
                table: "CTTrieuChung",
                column: "TenTrieuChung");

            migrationBuilder.AddForeignKey(
                name: "FK__CTTrieuCh__MaBen__4316F928",
                table: "CTTrieuChung",
                column: "MaBenh",
                principalTable: "Benh",
                principalColumn: "MaBenh",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__CTTrieuCh__TenTr__440B1D61",
                table: "CTTrieuChung",
                column: "TenTrieuChung",
                principalTable: "TrieuChung",
                principalColumn: "TenTrieuChung",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
