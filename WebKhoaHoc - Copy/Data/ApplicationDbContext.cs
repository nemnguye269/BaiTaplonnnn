using Microsoft.AspNetCore.Identity;
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
        public DbSet<CauHinh> CauHinhs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. CẤU HÌNH DECIMAL
            modelBuilder.Entity<KhoaHoc>().Property(k => k.GiaTien).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<DonHang>().Property(d => d.SoTien).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<ChiTietDonHang>().Property(c => c.GiaMua).HasColumnType("decimal(18,2)");

            // 2. CHẶN CASCADE CYCLE
            modelBuilder.Entity<ChiTietDonHang>()
                .HasOne(c => c.KhoaHoc)
                .WithMany()
                .HasForeignKey(c => c.KhoaHocId)
                .OnDelete(DeleteBehavior.Restrict);

            // 3. SEED ROLE ADMIN
            string adminRoleId = Guid.NewGuid().ToString();
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = adminRoleId,
                Name = "Admin",
                NormalizedName = "ADMIN"
            });

            // 4. SEED NGƯỜI DÙNG CÓ MẬT KHẨU (Abc@123)
            var hasher = new PasswordHasher<User>();
            var users = new List<User>();

            for (int i = 1; i <= 55; i++)
            {
                var userName = i <= 5 ? $"instructor{i}" : $"student{i - 5}";
                var email = i <= 5 ? $"gv{i}@web.com" : $"sv{i - 5}@web.com";

                var user = new User
                {
                    Id = i.ToString(),
                    UserName = userName,
                    Email = email,
                    NormalizedUserName = userName.ToUpper(),
                    NormalizedEmail = email.ToUpper(),
                    HoTen = i <= 5 ? $"Giảng viên {i}" : $"Học viên {i - 5}",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                user.PasswordHash = hasher.HashPassword(user, "Abc@123");
                users.Add(user);
            }
            modelBuilder.Entity<User>().HasData(users);

            // 5. GÁN QUYỀN ADMIN CHO USER SỐ 1
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = "1"
            });

            // 6. SEED DỮ LIỆU KHÁC (DANH MỤC, KHÓA HỌC, ĐƠN HÀNG)
            var random = new Random();

            modelBuilder.Entity<DanhMuc>().HasData(
                new DanhMuc { Id = 1, TenDanhMuc = "Lập trình Web", MoTa = "ASP.NET Core" },
                new DanhMuc { Id = 2, TenDanhMuc = "Lập trình Mobile", MoTa = "Flutter" },
                new DanhMuc { Id = 3, TenDanhMuc = "Data Science", MoTa = "Python" },
                new DanhMuc { Id = 4, TenDanhMuc = "Thiết kế Đồ họa", MoTa = "Photoshop" },
                new DanhMuc { Id = 5, TenDanhMuc = "Ngoại ngữ", MoTa = "English" }
            );

            // --- ĐÃ THÊM MOTA VÀO ĐÂY ---
            for (int i = 1; i <= 15; i++)
            {
                modelBuilder.Entity<KhoaHoc>().HasData(new KhoaHoc
                {
                    Id = i,
                    TenKhoaHoc = $"Khóa học số {i}",
                    GiaTien = random.Next(20, 100) * 10000,
                    DanhMucId = random.Next(1, 6),
                    HinhAnh = "course_default.jpg",
                    MoTa = $"Đây là mô tả chi tiết cho khóa học chuyên sâu số {i}.", // Sửa lỗi Required
                    VideoUrl = "https://www.youtube.com/watch?v=dQw4w9WgXcQ" // Đề phòng lỗi Required tiếp theo
                });
            }

            for (int i = 1; i <= 150; i++)
            {
                int chosenId = random.Next(1, 16);
                modelBuilder.Entity<DonHang>().HasData(new DonHang
                {
                    Id = i,
                    UserId = random.Next(6, 56).ToString(),
                    KhoaHocId = chosenId,
                    NgayMua = DateTime.Now.AddDays(-random.Next(1, 30)),
                    SoTien = 400000m,
                    TrangThai = true
                });

                modelBuilder.Entity<ChiTietDonHang>().HasData(new ChiTietDonHang
                {
                    Id = i,
                    DonHangId = i,
                    KhoaHocId = chosenId,
                    GiaMua = 400000m
                });
            }
        }
    }
}