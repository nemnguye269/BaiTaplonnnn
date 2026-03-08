using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebKhoaHoc.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Chưa nhập tài khoản")]
        [DisplayName("Tài khoản")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Chưa nhập mật khẩu")]
        [DisplayName("Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}