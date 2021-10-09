using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TES_MEDICAL.Entities.Models
{
    public partial class DataContext : DbContext
    {
       

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Benh> Benh { get; set; }
        public virtual DbSet<BenhNhan> BenhNhan { get; set; }
        public virtual DbSet<CTTrieuChung> CTTrieuChung { get; set; }
        public virtual DbSet<ChiTietDV> ChiTietDV { get; set; }
        public virtual DbSet<ChiTietSinhHieu> ChiTietSinhHieu { get; set; }
        public virtual DbSet<ChiTietToaThuoc> ChiTietToaThuoc { get; set; }
        public virtual DbSet<ChuyenKhoa> ChuyenKhoa { get; set; }
        public virtual DbSet<DichVu> DichVu { get; set; }
        public virtual DbSet<HoaDon> HoaDon { get; set; }
        public virtual DbSet<HoaDonThuoc> HoaDonThuoc { get; set; }
        public virtual DbSet<NguoiDung> NguoiDung { get; set; }
        public virtual DbSet<NhanVienYte> NhanVienYte { get; set; }
        public virtual DbSet<PhieuDatLich> PhieuDatLich { get; set; }
        public virtual DbSet<PhieuKham> PhieuKham { get; set; }
        public virtual DbSet<STTPhieuKham> STTPhieuKham { get; set; }
        public virtual DbSet<Thuoc> Thuoc { get; set; }
        public virtual DbSet<TinTuc> TinTuc { get; set; }
        public virtual DbSet<ToaThuoc> ToaThuoc { get; set; }
        public virtual DbSet<TrieuChung> TrieuChung { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=ThanhDuy;Initial Catalog=CLINIC_DBS;Integrated Security=True;Connect Timeout=10");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Benh>(entity =>
            {
                entity.HasKey(e => e.MaBenh)
                    .HasName("PK__Benh__DB7E2D4930DDFD9A");

                entity.Property(e => e.MaBenh).ValueGeneratedNever();

                entity.Property(e => e.TenBenh)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.MaCKNavigation)
                    .WithMany(p => p.Benh)
                    .HasForeignKey(d => d.MaCK)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Benh__MaCK__1CF15040");
            });

            modelBuilder.Entity<BenhNhan>(entity =>
            {
                entity.HasKey(e => e.MaBN)
                    .HasName("PK__BenhNhan__272475ADC43575E5");

                entity.Property(e => e.MaBN).ValueGeneratedNever();

                entity.Property(e => e.DiaChi)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HoTen)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NgaySinh).HasColumnType("datetime");

                entity.Property(e => e.SDT)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CTTrieuChung>(entity =>
            {
                entity.HasKey(e => new { e.MaBenh, e.MaTrieuChung })
                    .HasName("cttc_pk");

                entity.Property(e => e.ChiTietTrieuChung).IsRequired();

                entity.HasOne(d => d.MaBenhNavigation)
                    .WithMany(p => p.CTTrieuChung)
                    .HasForeignKey(d => d.MaBenh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CTTrieuCh__MaBen__1FCDBCEB");

                entity.HasOne(d => d.MaTrieuChungNavigation)
                    .WithMany(p => p.CTTrieuChung)
                    .HasForeignKey(d => d.MaTrieuChung)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CTTrieuCh__MaTri__20C1E124");
            });

            modelBuilder.Entity<ChiTietDV>(entity =>
            {
                entity.HasKey(e => new { e.MaPhieuKham, e.MaDV })
                    .HasName("pk_ctdv");

                entity.HasOne(d => d.MaDVNavigation)
                    .WithMany(p => p.ChiTietDV)
                    .HasForeignKey(d => d.MaDV)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChiTietDV__MaDV__30F848ED");

                entity.HasOne(d => d.MaPhieuKhamNavigation)
                    .WithMany(p => p.ChiTietDV)
                    .HasForeignKey(d => d.MaPhieuKham)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChiTietDV__MaPhi__300424B4");
            });

            modelBuilder.Entity<ChiTietSinhHieu>(entity =>
            {
                entity.HasKey(e => e.MaSinhHieu)
                    .HasName("PK__ChiTietS__F33E637F329F7A58");

                entity.Property(e => e.MaSinhHieu).ValueGeneratedNever();

                entity.Property(e => e.TenSH)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ThongTinChiTiet).IsRequired();

                entity.HasOne(d => d.MaPKNavigation)
                    .WithMany(p => p.ChiTietSinhHieu)
                    .HasForeignKey(d => d.MaPK)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChiTietSin__MaPK__38996AB5");
            });

            modelBuilder.Entity<ChiTietToaThuoc>(entity =>
            {
                entity.HasKey(e => new { e.MaPK, e.MaThuoc })
                    .HasName("PK__ChiTietT__339EF89FE1BA1E8D");

                entity.Property(e => e.GhiChu).IsRequired();

                entity.HasOne(d => d.MaPKNavigation)
                    .WithMany(p => p.ChiTietToaThuoc)
                    .HasForeignKey(d => d.MaPK)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChiTietToa__MaPK__3D5E1FD2");

                entity.HasOne(d => d.MaThuocNavigation)
                    .WithMany(p => p.ChiTietToaThuoc)
                    .HasForeignKey(d => d.MaThuoc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChiTietTo__MaThu__3E52440B");
            });

            modelBuilder.Entity<ChuyenKhoa>(entity =>
            {
                entity.HasKey(e => e.MaCK)
                    .HasName("PK__ChuyenKh__27258E0D4F3F6158");

                entity.Property(e => e.MaCK).ValueGeneratedNever();

                entity.Property(e => e.TenCK)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<DichVu>(entity =>
            {
                entity.HasKey(e => e.MaDV)
                    .HasName("PK__DichVu__27258657C09A38F0");

                entity.Property(e => e.MaDV).ValueGeneratedNever();

                entity.Property(e => e.DonGia).HasColumnType("money");

                entity.Property(e => e.TenDV)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<HoaDon>(entity =>
            {
                entity.HasKey(e => e.MaHoaDon)
                    .HasName("PK__HoaDon__835ED13B0A19D0AB");

                entity.Property(e => e.MaHoaDon).ValueGeneratedNever();

                entity.Property(e => e.NgayHD).HasColumnType("datetime");

                entity.Property(e => e.TongTien).HasColumnType("money");

                entity.HasOne(d => d.MaNVNavigation)
                    .WithMany(p => p.HoaDon)
                    .HasForeignKey(d => d.MaNV)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HoaDon__MaNV__35BCFE0A");

                entity.HasOne(d => d.MaPKNavigation)
                    .WithMany(p => p.HoaDon)
                    .HasForeignKey(d => d.MaPK)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HoaDon__MaPK__34C8D9D1");
            });

            modelBuilder.Entity<HoaDonThuoc>(entity =>
            {
                entity.HasKey(e => e.MaPK)
                    .HasName("PK__HoaDonTh__2725E7FD34660270");

                entity.Property(e => e.MaPK).ValueGeneratedNever();

                entity.Property(e => e.NgayHD).HasColumnType("datetime");

                entity.Property(e => e.TongTien).HasColumnType("money");

                entity.HasOne(d => d.MaNVNavigation)
                    .WithMany(p => p.HoaDonThuoc)
                    .HasForeignKey(d => d.MaNV)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HoaDonThuo__MaNV__4316F928");

                entity.HasOne(d => d.MaPKNavigation)
                    .WithOne(p => p.HoaDonThuoc)
                    .HasForeignKey<HoaDonThuoc>(d => d.MaPK)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HoaDonThuo__MaPK__4222D4EF");
            });

            modelBuilder.Entity<NguoiDung>(entity =>
            {
                entity.HasKey(e => e.MaNguoiDung)
                    .HasName("PK__NguoiDun__C539D762B68E726F");

                entity.Property(e => e.MaNguoiDung).ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HinhAnh).HasMaxLength(250);

                entity.Property(e => e.HoTen)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MatKhau)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.SDT)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<NhanVienYte>(entity =>
            {
                entity.HasKey(e => e.MaNV)
                    .HasName("PK__NhanVien__2725D70ABB646B8A");

                entity.Property(e => e.MaNV).ValueGeneratedNever();

                entity.Property(e => e.EmailNV)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Hinh).HasMaxLength(250);

                entity.Property(e => e.HoTen)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MatKhau)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.SDTNV)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.ChuyenKhoaNavigation)
                    .WithMany(p => p.NhanVienYte)
                    .HasForeignKey(d => d.ChuyenKhoa)
                    .HasConstraintName("FK__NhanVienY__Chuye__239E4DCF");
            });

            modelBuilder.Entity<PhieuDatLich>(entity =>
            {
                entity.HasKey(e => e.MaPhieu)
                    .HasName("PK__PhieuDat__2660BFE080B5AEF6");

                entity.Property(e => e.MaPhieu).ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NgayKham).HasColumnType("datetime");

                entity.Property(e => e.NgaySinh).HasColumnType("datetime");

                entity.Property(e => e.SDT)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.TenBN)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PhieuKham>(entity =>
            {
                entity.HasKey(e => e.MaPK)
                    .HasName("PK__PhieuKha__2725E7FD1EF8C450");

                entity.Property(e => e.MaPK).ValueGeneratedNever();

                entity.Property(e => e.ChanDoan).IsRequired();

                entity.Property(e => e.HuyetAp)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Mach).HasMaxLength(50);

                entity.Property(e => e.NgayKham).HasColumnType("datetime");

                entity.Property(e => e.NgayTaiKham).HasColumnType("datetime");

                entity.Property(e => e.NhietDo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TrieuChung).IsRequired();

                entity.HasOne(d => d.MaBNNavigation)
                    .WithMany(p => p.PhieuKham)
                    .HasForeignKey(d => d.MaBN)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PhieuKham__MaBN__2B3F6F97");

                entity.HasOne(d => d.MaBSNavigation)
                    .WithMany(p => p.PhieuKham)
                    .HasForeignKey(d => d.MaBS)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PhieuKham__MaBS__2A4B4B5E");
            });

            modelBuilder.Entity<STTPhieuKham>(entity =>
            {
                entity.HasKey(e => e.MaPhieuKham)
                    .HasName("PK__STTPhieu__FACA55DF595322A2");

                entity.Property(e => e.MaPhieuKham).ValueGeneratedNever();

                entity.Property(e => e.MaUuTien)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.MaPhieuKhamNavigation)
                    .WithOne(p => p.STTPhieuKham)
                    .HasForeignKey<STTPhieuKham>(d => d.MaPhieuKham)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__STTPhieuK__MaPhi__2E1BDC42");
            });

            modelBuilder.Entity<Thuoc>(entity =>
            {
                entity.HasKey(e => e.MaThuoc)
                    .HasName("PK__Thuoc__4BB1F62090B58C0A");

                entity.Property(e => e.MaThuoc).ValueGeneratedNever();

                entity.Property(e => e.DonGia).HasColumnType("money");

                entity.Property(e => e.HinhAnh).HasMaxLength(250);

                entity.Property(e => e.TenThuoc)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ThongTin).IsRequired();

                entity.Property(e => e.Vitri)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TinTuc>(entity =>
            {
                entity.HasKey(e => e.MaBaiViet)
                    .HasName("PK__TinTuc__AEDD5647AEDAE527");

                entity.Property(e => e.MaBaiViet).ValueGeneratedNever();

                entity.Property(e => e.NoiDung).IsRequired();

                entity.Property(e => e.TieuDe)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.MaNguoiVietNavigation)
                    .WithMany(p => p.TinTuc)
                    .HasForeignKey(d => d.MaNguoiViet)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TinTuc__MaNguoiV__1273C1CD");
            });

            modelBuilder.Entity<ToaThuoc>(entity =>
            {
                entity.HasKey(e => e.MaPhieuKham)
                    .HasName("PK__ToaThuoc__FACA55DF647F27FD");

                entity.Property(e => e.MaPhieuKham).ValueGeneratedNever();

                entity.HasOne(d => d.MaPhieuKhamNavigation)
                    .WithOne(p => p.ToaThuoc)
                    .HasForeignKey<ToaThuoc>(d => d.MaPhieuKham)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ToaThuoc__MaPhie__3B75D760");
            });

            modelBuilder.Entity<TrieuChung>(entity =>
            {
                entity.HasKey(e => e.MaTrieuChung)
                    .HasName("PK__TrieuChu__F21EFBE2E12C4B13");

                entity.Property(e => e.MaTrieuChung).ValueGeneratedNever();

                entity.Property(e => e.TenTrieuChung).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
