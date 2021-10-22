using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TES_MEDICAL.ENTITIES.Migrations
{
    public partial class update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BenhNhan",
                columns: table => new
                {
                    MaBN = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SDT = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "datetime", nullable: false),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BenhNhan__272475AD90105519", x => x.MaBN);
                });

            migrationBuilder.CreateTable(
                name: "ChuyenKhoa",
                columns: table => new
                {
                    MaCK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenCK = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChuyenKh__27258E0D1F0FE375", x => x.MaCK);
                });

            migrationBuilder.CreateTable(
                name: "DichVu",
                columns: table => new
                {
                    MaDV = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenDV = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DonGia = table.Column<decimal>(type: "money", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DichVu__2725865790C1C308", x => x.MaDV);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDung",
                columns: table => new
                {
                    MaNguoiDung = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    MatKhau = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SDT = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    HinhAnh = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ChucVu = table.Column<byte>(type: "tinyint", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NguoiDun__C539D7624ABCA545", x => x.MaNguoiDung);
                });

            migrationBuilder.CreateTable(
                name: "PhieuDatLich",
                columns: table => new
                {
                    MaPhieu = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SDT = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    TenBN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime", nullable: false),
                    NgayKham = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PhieuDat__2660BFE037FE92E3", x => x.MaPhieu);
                });

            migrationBuilder.CreateTable(
                name: "Thuoc",
                columns: table => new
                {
                    MaThuoc = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenThuoc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Vitri = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    DonGia = table.Column<decimal>(type: "money", nullable: false),
                    ThongTin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false),
                    HinhAnh = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Thuoc__4BB1F620DA17E5F3", x => x.MaThuoc);
                });

            migrationBuilder.CreateTable(
                name: "TrieuChung",
                columns: table => new
                {
                    TenTrieuChung = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TenTrieuChungKhongDau = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TrieuChu__38C0D567B7BAD8FD", x => x.TenTrieuChung);
                });

            migrationBuilder.CreateTable(
                name: "Benh",
                columns: table => new
                {
                    MaBenh = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenBenh = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ThongTin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaCK = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Benh__DB7E2D498DB80DEE", x => x.MaBenh);
                    table.ForeignKey(
                        name: "FK__Benh__MaCK__1CF15040",
                        column: x => x.MaCK,
                        principalTable: "ChuyenKhoa",
                        principalColumn: "MaCK",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NhanVienYte",
                columns: table => new
                {
                    MaNV = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmailNV = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    MatKhau = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SDTNV = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    ChucVu = table.Column<byte>(type: "tinyint", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false),
                    Hinh = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ChuyenKhoa = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NhanVien__2725D70A99F2EA0C", x => x.MaNV);
                    table.ForeignKey(
                        name: "FK__NhanVienY__Chuye__20C1E124",
                        column: x => x.ChuyenKhoa,
                        principalTable: "ChuyenKhoa",
                        principalColumn: "MaCK",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TinTuc",
                columns: table => new
                {
                    MaBaiViet = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TieuDe = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false),
                    MaNguoiViet = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TinTuc__AEDD56476D752A3D", x => x.MaBaiViet);
                    table.ForeignKey(
                        name: "FK__TinTuc__MaNguoiV__1273C1CD",
                        column: x => x.MaNguoiViet,
                        principalTable: "NguoiDung",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CTTrieuChung",
                columns: table => new
                {
                    MaBenh = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenTrieuChung = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ChiTietTrieuChung = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("cttc_pk", x => new { x.MaBenh, x.TenTrieuChung });
                    table.ForeignKey(
                        name: "FK__CTTrieuCh__MaBen__4316F928",
                        column: x => x.MaBenh,
                        principalTable: "Benh",
                        principalColumn: "MaBenh",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__CTTrieuCh__TenTr__440B1D61",
                        column: x => x.TenTrieuChung,
                        principalTable: "TrieuChung",
                        principalColumn: "TenTrieuChung",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhieuKham",
                columns: table => new
                {
                    MaPK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaBS = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaBN = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mach = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NhietDo = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    HuyetAp = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    TrieuChungSoBo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KetQuaKham = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChanDoan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayKham = table.Column<DateTime>(type: "datetime", nullable: false),
                    NgayTaiKham = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PhieuKha__2725E7FDD3F81957", x => x.MaPK);
                    table.ForeignKey(
                        name: "FK__PhieuKham__MaBN__2B3F6F97",
                        column: x => x.MaBN,
                        principalTable: "BenhNhan",
                        principalColumn: "MaBN",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__PhieuKham__MaBS__2A4B4B5E",
                        column: x => x.MaBS,
                        principalTable: "NhanVienYte",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDV",
                columns: table => new
                {
                    MaPhieuKham = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaDV = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ctdv", x => new { x.MaPhieuKham, x.MaDV });
                    table.ForeignKey(
                        name: "FK__ChiTietDV__MaDV__5812160E",
                        column: x => x.MaDV,
                        principalTable: "DichVu",
                        principalColumn: "MaDV",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__ChiTietDV__MaPhi__571DF1D5",
                        column: x => x.MaPhieuKham,
                        principalTable: "PhieuKham",
                        principalColumn: "MaPK",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietSinhHieu",
                columns: table => new
                {
                    MaSinhHieu = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaPK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenSH = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ThongTinChiTiet = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChiTietS__F33E637FDFBD41BE", x => x.MaSinhHieu);
                    table.ForeignKey(
                        name: "FK__ChiTietSin__MaPK__35BCFE0A",
                        column: x => x.MaPK,
                        principalTable: "PhieuKham",
                        principalColumn: "MaPK",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HoaDon",
                columns: table => new
                {
                    MaHoaDon = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    MaPK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaNV = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NgayHD = table.Column<DateTime>(type: "datetime", nullable: false),
                    TongTien = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__HoaDon__835ED13BE8E6487A", x => x.MaHoaDon);
                    table.ForeignKey(
                        name: "FK__HoaDon__MaNV__35BCFE0A",
                        column: x => x.MaNV,
                        principalTable: "NhanVienYte",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__HoaDon__MaPK__34C8D9D1",
                        column: x => x.MaPK,
                        principalTable: "PhieuKham",
                        principalColumn: "MaPK",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "STTPhieuKham",
                columns: table => new
                {
                    MaPhieuKham = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    STT = table.Column<int>(type: "int", nullable: false),
                    MaUuTien = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__STTPhieu__FACA55DF6DAFE0DA", x => x.MaPhieuKham);
                    table.ForeignKey(
                        name: "FK__STTPhieuK__MaPhi__2B3F6F97",
                        column: x => x.MaPhieuKham,
                        principalTable: "PhieuKham",
                        principalColumn: "MaPK",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ToaThuoc",
                columns: table => new
                {
                    MaPhieuKham = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrangThai = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ToaThuoc__FACA55DF1E8609CC", x => x.MaPhieuKham);
                    table.ForeignKey(
                        name: "FK__ToaThuoc__MaPhie__38996AB5",
                        column: x => x.MaPhieuKham,
                        principalTable: "PhieuKham",
                        principalColumn: "MaPK",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietToaThuoc",
                columns: table => new
                {
                    MaPK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaThuoc = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChiTietT__339EF89FCA764F6C", x => new { x.MaPK, x.MaThuoc });
                    table.ForeignKey(
                        name: "FK__ChiTietTo__MaThu__3B75D760",
                        column: x => x.MaThuoc,
                        principalTable: "Thuoc",
                        principalColumn: "MaThuoc",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__ChiTietToa__MaPK__3A81B327",
                        column: x => x.MaPK,
                        principalTable: "ToaThuoc",
                        principalColumn: "MaPhieuKham",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HoaDonThuoc",
                columns: table => new
                {
                    MaPK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaHD = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    NgayHD = table.Column<DateTime>(type: "datetime", nullable: false),
                    MaNV = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TongTien = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__HoaDonTh__2725E7FD6887BBCA", x => x.MaPK);
                    table.ForeignKey(
                        name: "FK__HoaDonThuo__MaNV__440B1D61",
                        column: x => x.MaNV,
                        principalTable: "NhanVienYte",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__HoaDonThuo__MaPK__4316F928",
                        column: x => x.MaPK,
                        principalTable: "ToaThuoc",
                        principalColumn: "MaPhieuKham",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Benh_MaCK",
                table: "Benh",
                column: "MaCK");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDV_MaDV",
                table: "ChiTietDV",
                column: "MaDV");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietSinhHieu_MaPK",
                table: "ChiTietSinhHieu",
                column: "MaPK");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietToaThuoc_MaThuoc",
                table: "ChiTietToaThuoc",
                column: "MaThuoc");

            migrationBuilder.CreateIndex(
                name: "IX_CTTrieuChung_TenTrieuChung",
                table: "CTTrieuChung",
                column: "TenTrieuChung");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_MaNV",
                table: "HoaDon",
                column: "MaNV");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_MaPK",
                table: "HoaDon",
                column: "MaPK");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDonThuoc_MaNV",
                table: "HoaDonThuoc",
                column: "MaNV");

            migrationBuilder.CreateIndex(
                name: "UQ__HoaDonTh__2725A6E187D21950",
                table: "HoaDonThuoc",
                column: "MaHD",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NhanVienYte_ChuyenKhoa",
                table: "NhanVienYte",
                column: "ChuyenKhoa");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuKham_MaBN",
                table: "PhieuKham",
                column: "MaBN");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuKham_MaBS",
                table: "PhieuKham",
                column: "MaBS");

            migrationBuilder.CreateIndex(
                name: "IX_TinTuc_MaNguoiViet",
                table: "TinTuc",
                column: "MaNguoiViet");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietDV");

            migrationBuilder.DropTable(
                name: "ChiTietSinhHieu");

            migrationBuilder.DropTable(
                name: "ChiTietToaThuoc");

            migrationBuilder.DropTable(
                name: "CTTrieuChung");

            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.DropTable(
                name: "HoaDonThuoc");

            migrationBuilder.DropTable(
                name: "PhieuDatLich");

            migrationBuilder.DropTable(
                name: "STTPhieuKham");

            migrationBuilder.DropTable(
                name: "STTTOATHUOC");

            migrationBuilder.DropTable(
                name: "TinTuc");

            migrationBuilder.DropTable(
                name: "DichVu");

            migrationBuilder.DropTable(
                name: "Thuoc");

            migrationBuilder.DropTable(
                name: "Benh");

            migrationBuilder.DropTable(
                name: "TrieuChung");

            migrationBuilder.DropTable(
                name: "ToaThuoc");

            migrationBuilder.DropTable(
                name: "NguoiDung");

            migrationBuilder.DropTable(
                name: "PhieuKham");

            migrationBuilder.DropTable(
                name: "BenhNhan");

            migrationBuilder.DropTable(
                name: "NhanVienYte");

            migrationBuilder.DropTable(
                name: "ChuyenKhoa");
        }
    }
}
