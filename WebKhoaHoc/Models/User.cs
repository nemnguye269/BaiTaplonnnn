using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebKhoaHoc.Models
{
    public class User : IdentityUser
    {
        // 1. CÁC TRƯỜNG BẠN MUỐN GIỮ (Nhưng tùy chỉnh tên hoặc thêm mới)

        [Required]
        public string? HoTen { get; set; } // Thay vì để trống, ta dùng trường này lưu tên thật

        public string? RoleCustom { get; set; } // Nếu bạn muốn tự quản lý Role bằng chuỗi riêng ngoài hệ thống Identity

        public DateTime? NgayTao { get; set; } = DateTime.Now; // Thêm ngày tạo tài khoản

        /* LƯU Ý QUAN TRỌNG:
           - KHÔNG viết lại: public string UserName { get; set; }
           - KHÔNG viết lại: public string PasswordHash { get; set; }
           Vì IdentityUser đã có sẵn hai trường này rồi. Nếu bạn viết vào sẽ bị lỗi "already contains a definition".
        */
    }
}