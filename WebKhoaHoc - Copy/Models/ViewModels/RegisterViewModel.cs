using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebKhoaHoc.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Chưa nhập tài khoản")]
        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }

        // --- THÊM HỌ TÊN ---
        [Required(ErrorMessage = "Chưa nhập họ tên")]
        [Display(Name = "Họ và tên")]
        public string HoTen { get; set; }

        // --- THÊM EMAIL ---
        [Required(ErrorMessage = "Chưa nhập email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Chưa nhập mật khẩu")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Chưa xác nhận mật khẩu")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu không khớp")]
        [Display(Name = "Xác nhận mật khẩu")]
        public string ConfirmPassword { get; set; }
    }
}