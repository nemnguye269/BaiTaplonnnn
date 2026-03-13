using System.ComponentModel.DataAnnotations;

namespace WebKhoaHoc.Models
{
    public class CauHinh
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Tên Website")]
        public string TenWebsite { get; set; }

        [Display(Name = "Hotline liên hệ")]
        public string Hotline { get; set; }

        [Display(Name = "Email hỗ trợ")]
        public string EmailLienHe { get; set; }

        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }

        [Display(Name = "Logo Website (URL)")]
        public string LogoUrl { get; set; }
    }
}