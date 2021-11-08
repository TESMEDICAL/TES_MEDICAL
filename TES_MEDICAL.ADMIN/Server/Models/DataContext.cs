using System;
using TES_MEDICAL.ADMIN.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TES_MEDICAL.ADMIN.Server.Models
{
    public partial class DataContext : DbContext
    {
      
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdminUser> AdminUser { get; set; }
        public virtual DbSet<CartDetail> CartDetail { get; set; }
        public virtual DbSet<DonHang> DonHang { get; set; }
        public virtual DbSet<KhachHang> KhachHang { get; set; }
        public virtual DbSet<PhanLoai> PhanLoai { get; set; }
        public virtual DbSet<Product> Product { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AdminUser>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<CartDetail>(entity =>
            {
                entity.HasKey(e => new { e.MaDH, e.MaTD })
                    .HasName("pk_dhct");

                entity.HasOne(d => d.MaDHNavigation)
                    .WithMany(p => p.CartDetail)
                    .HasForeignKey(d => d.MaDH)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ct_dh");

                entity.HasOne(d => d.MaTDNavigation)
                    .WithMany(p => p.CartDetail)
                    .HasForeignKey(d => d.MaTD)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ct_TD");
            });

            modelBuilder.Entity<DonHang>(entity =>
            {
                entity.HasKey(e => e.MaDH)
                    .HasName("PK__DonHang__27258661700BFB99");

                entity.Property(e => e.MaDH).ValueGeneratedNever();

                entity.Property(e => e.DiaChi)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ThoiGian).HasColumnType("datetime");

                entity.HasOne(d => d.MaKHNavigation)
                    .WithMany(p => p.DonHang)
                    .HasForeignKey(d => d.MaKH)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DonHang__MaKH__1FCDBCEB");
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DiaChi)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MatKhau)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.NgaySinh).HasColumnType("datetime");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PhanLoai>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.MaLoai, "IX_Product_MaLoai");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Gia).HasColumnType("money");

                entity.Property(e => e.QrURL)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.TenMon)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.MaLoaiNavigation)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.MaLoai)
                    .HasConstraintName("FK__Product__MaLoai__36B12243");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
