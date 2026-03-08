using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebKhoaHoc.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Chưa nhập tài khoản")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Chưa nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Chưa xác nhận mật khẩu")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu không khớp")]
        public string ConfirmPassword { get; set; }
    }
}