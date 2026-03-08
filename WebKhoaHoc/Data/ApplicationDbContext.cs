using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebKhoaHoc.Models;

namespace WebKhoaHoc.Data
{
    public class ApplicationDbContextContext : IdentityDbContext<User>
    {
        public ApplicationDbContextContext(DbContextOptions<ApplicationDbContextContext> options)
            : base(options)
        {
        }

        public DbSet<DanhMuc> DanhMucs { get; set; }
        public DbSet<KhoaHoc> KhoaHocs { get; set; }
        public DbSet<BaiHoc> BaiHocs { get; set; }
        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public DbSet<DanhGia> DanhGias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ---------- ĐỊNH DẠNG TIỀN ----------
            modelBuilder.Entity<KhoaHoc>()
                .Property(p => p.GiaTien)
                .HasPrecision(18, 2);

            modelBuilder.Entity<DonHang>()
                .Property(p => p.SoTien)
                .HasPrecision(18, 2);

            modelBuilder.Entity<ChiTietDonHang>()
                .Property(p => p.GiaMua)
                .HasPrecision(18, 2);

            // ---------- QUAN HỆ ----------

            // Khóa học - Danh mục
            modelBuilder.Entity<KhoaHoc>()
                .HasOne(k => k.DanhMuc)
                .WithMany(d => d.KhoaHocs)
                .HasForeignKey(k => k.DanhMucId)
                .OnDelete(DeleteBehavior.Cascade);

            // Bài học - Khóa học
            modelBuilder.Entity<BaiHoc>()
                .HasOne(b => b.KhoaHoc)
                .WithMany(k => k.BaiHocs)
                .HasForeignKey(b => b.KhoaHocId)
                .OnDelete(DeleteBehavior.Cascade);

            // Đơn hàng - User
            modelBuilder.Entity<DonHang>()
                .HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId);

            // ---------- FIX MULTIPLE CASCADE PATH ----------

            // ChiTietDonHang - DonHang
            modelBuilder.Entity<ChiTietDonHang>()
                .HasOne(c => c.DonHang)
                .WithMany(d => d.ChiTietDonHangs)
                .HasForeignKey(c => c.DonHangId)
                .OnDelete(DeleteBehavior.Cascade);

            // ChiTietDonHang - KhoaHoc
            modelBuilder.Entity<ChiTietDonHang>()
                .HasOne(c => c.KhoaHoc)
                .WithMany()
                .HasForeignKey(c => c.KhoaHocId)
                .OnDelete(DeleteBehavior.Restrict); // tránh multiple cascade

            // ---------- SEED DATA ----------

            modelBuilder.Entity<DanhMuc>().HasData(
                new DanhMuc { Id = 1, TenDanhMuc = "Lập trình Web", MoTa = "Các khóa học ASP.NET Core, React, Angular" },
                new DanhMuc { Id = 2, TenDanhMuc = "Lập trình Mobile", MoTa = "Phát triển ứng dụng Android/iOS" }
            );

            modelBuilder.Entity<KhoaHoc>().HasData(
                new KhoaHoc
                {
                    Id = 1,
                    TenKhoaHoc = "ASP.NET Core Cơ Bản",
                    GiaTien = 499000,
                    DanhMucId = 1,
                    MoTa = "Học xây dựng Web API chuyên nghiệp",
                    HinhAnh = "aspnet_basic.jpg"
                },
                new KhoaHoc
                {
                    Id = 2,
                    TenKhoaHoc = "Lập trình Flutter",
                    GiaTien = 750000,
                    DanhMucId = 2,
                    MoTa = "Xây dựng App đa nền tảng",
                    HinhAnh = "flutter.jpg"
                }
            );
        }
    }
}