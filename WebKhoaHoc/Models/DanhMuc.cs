using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebKhoaHoc.Models
{
    [Table("DanhMuc")]
    public class DanhMuc
    {
        [Key] // Định nghĩa Primary Key
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        [StringLength(200, ErrorMessage = "Tên danh mục không được vượt quá 200 ký tự")]
        [Display(Name = "Tên Danh Mục")]
        public string TenDanhMuc { get; set; }

        [StringLength(1000, ErrorMessage = "Mô tả không được vượt quá 1000 ký tự")]
        [Display(Name = "Mô Tả")]
        public string? MoTa { get; set; }

        // Liên kết 1-n với KhoaHoc (Navigation Property)
        // Giúp Entity Framework nhận diện mối quan hệ cha-con
        public virtual ICollection<KhoaHoc>? KhoaHocs { get; set; }
    }
}   