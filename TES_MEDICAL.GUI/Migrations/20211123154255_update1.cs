using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TES_MEDICAL.GUI.Migrations
{
    public partial class update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

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
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    TenBN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime", nullable: false),
                    NgayKham = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PhieuDat__2660BFE037FE92E3", x => x.MaPhieu);
                });

            migrationBuilder.CreateTable(
                name: "TheLoai",
                columns: table => new
                {
                    MaTL = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenTL = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TheLoai__272500715188CB73", x => x.MaTL);
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
                    MatrieuChung = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenTrieuChung = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TrieuChu__18521B702BAAC1B3", x => x.MatrieuChung);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "NhanVienYTe",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ChucVu = table.Column<byte>(type: "tinyint", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false),
                    Hinh = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ChuyenKhoa = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Theme = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NhanVien__2725D70A99F2EA0C", x => x.Id);
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
                    Hinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TieuDe = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false),
                    MaNguoiViet = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaTL = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ThoiGian = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TinTuc__AEDD5647A9FE161F", x => x.MaBaiViet);
                    table.ForeignKey(
                        name: "FK__TinTuc__MaNguoiV__1273C1CD",
                        column: x => x.MaNguoiViet,
                        principalTable: "NguoiDung",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__TinTuc__MaTL__71D1E811",
                        column: x => x.MaTL,
                        principalTable: "TheLoai",
                        principalColumn: "MaTL",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CTTrieuChung",
                columns: table => new
                {
                    MaBenh = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaTrieuChung = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CTTrieuC__E45FC2F731FF98AB", x => new { x.MaBenh, x.MaTrieuChung });
                    table.ForeignKey(
                        name: "FK__CTTrieuCh__MaBen__19DFD96B",
                        column: x => x.MaBenh,
                        principalTable: "Benh",
                        principalColumn: "MaBenh",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__CTTrieuCh__MaTri__1AD3FDA4",
                        column: x => x.MaTrieuChung,
                        principalTable: "TrieuChung",
                        principalColumn: "MatrieuChung",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "varchar(50)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_NhanVienYTe_UserId",
                        column: x => x.UserId,
                        principalTable: "NhanVienYTe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_NhanVienYTe_UserId",
                        column: x => x.UserId,
                        principalTable: "NhanVienYTe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(50)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_NhanVienYTe_UserId",
                        column: x => x.UserId,
                        principalTable: "NhanVienYTe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(50)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_NhanVienYTe_UserId",
                        column: x => x.UserId,
                        principalTable: "NhanVienYTe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhieuKham",
                columns: table => new
                {
                    MaPK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaBS = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    MaBN = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mach = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NhietDo = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    HuyetAp = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    TrieuChungSoBo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KetQuaKham = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChanDoan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayKham = table.Column<DateTime>(type: "datetime", nullable: false),
                    NgayTaiKham = table.Column<DateTime>(type: "datetime", nullable: true),
                    TrangThai = table.Column<byte>(type: "tinyint", nullable: false, defaultValueSql: "(CONVERT([tinyint],(0)))"),
                    MaBenh = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                        principalTable: "NhanVienYTe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhieuKham_MaBenh",
                        column: x => x.MaBenh,
                        principalTable: "Benh",
                        principalColumn: "MaBenh",
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
                    MaNV = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    NgayHD = table.Column<DateTime>(type: "datetime", nullable: false),
                    TongTien = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__HoaDon__835ED13BE8E6487A", x => x.MaHoaDon);
                    table.ForeignKey(
                        name: "FK__HoaDon__MaNV__35BCFE0A",
                        column: x => x.MaNV,
                        principalTable: "NhanVienYTe",
                        principalColumn: "Id",
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
                    MaUuTien = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
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
                name: "ChiTietDV",
                columns: table => new
                {
                    MaHD = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    MaDV = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DonGiaDV = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChiTietD__4557FE8526B532A5", x => new { x.MaHD, x.MaDV });
                    table.ForeignKey(
                        name: "FK__ChiTietDV__MaDV__40058253",
                        column: x => x.MaDV,
                        principalTable: "DichVu",
                        principalColumn: "MaDV",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__ChiTietDV__MaHD__3F115E1A",
                        column: x => x.MaHD,
                        principalTable: "HoaDon",
                        principalColumn: "MaHoaDon",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietToaThuoc",
                columns: table => new
                {
                    MaPK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaThuoc = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    DonGiaThuoc = table.Column<decimal>(type: "money", nullable: true),
                    LanTrongNgay = table.Column<int>(type: "int", nullable: false),
                    VienMoiLan = table.Column<int>(type: "int", nullable: false),
                    TruocKhian = table.Column<bool>(type: "bit", nullable: false),
                    Sang = table.Column<bool>(type: "bit", nullable: false),
                    Trua = table.Column<bool>(type: "bit", nullable: false),
                    Chieu = table.Column<bool>(type: "bit", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    MaNV = table.Column<string>(type: "varchar(50)", nullable: true),
                    TongTien = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__HoaDonTh__2725E7FD6887BBCA", x => x.MaPK);
                    table.ForeignKey(
                        name: "FK__HoaDonThuo__MaNV__440B1D61",
                        column: x => x.MaNV,
                        principalTable: "NhanVienYTe",
                        principalColumn: "Id",
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
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

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
                name: "IX_CTTrieuChung_MaTrieuChung",
                table: "CTTrieuChung",
                column: "MaTrieuChung");

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
                name: "EmailIndex",
                table: "NhanVienYTe",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_NhanVienYTe_ChuyenKhoa",
                table: "NhanVienYTe",
                column: "ChuyenKhoa");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "NhanVienYTe",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuKham_MaBenh",
                table: "PhieuKham",
                column: "MaBenh");

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

            migrationBuilder.CreateIndex(
                name: "IX_TinTuc_MaTL",
                table: "TinTuc",
                column: "MaTL");

            migrationBuilder.CreateIndex(
                name: "UQ__TrieuChu__38C0D5667814C499",
                table: "TrieuChung",
                column: "TenTrieuChung",
                unique: true,
                filter: "[TenTrieuChung] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ChiTietDV");

            migrationBuilder.DropTable(
                name: "ChiTietSinhHieu");

            migrationBuilder.DropTable(
                name: "ChiTietToaThuoc");

            migrationBuilder.DropTable(
                name: "CTTrieuChung");

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
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "DichVu");

            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.DropTable(
                name: "Thuoc");

            migrationBuilder.DropTable(
                name: "TrieuChung");

            migrationBuilder.DropTable(
                name: "ToaThuoc");

            migrationBuilder.DropTable(
                name: "NguoiDung");

            migrationBuilder.DropTable(
                name: "TheLoai");

            migrationBuilder.DropTable(
                name: "PhieuKham");

            migrationBuilder.DropTable(
                name: "BenhNhan");

            migrationBuilder.DropTable(
                name: "NhanVienYTe");

            migrationBuilder.DropTable(
                name: "Benh");

            migrationBuilder.DropTable(
                name: "ChuyenKhoa");
        }
    }
}
