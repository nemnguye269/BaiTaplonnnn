using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebKhoaHoc.Models
{
    [Table("KhoaHoc")]
    public class KhoaHoc
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên khóa học không được để trống")]
        [StringLength(200, ErrorMessage = "Tên khóa học không được vượt quá 200 ký tự")]
        [Display(Name = "Tên Khóa Học")]
        public string TenKhoaHoc { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập học phí")]
        [Range(0, double.MaxValue, ErrorMessage = "Học phí phải là số dương")]
        [Column("HocPhi")]
        [Display(Name = "Học Phí")]
        public decimal GiaTien { get; set; }

        [StringLength(200)]
        [Display(Name = "Hình Ảnh")]
        public string? HinhAnh { get; set; }

        [Required(ErrorMessage = "Mô tả khóa học không được để trống")]
        [Display(Name = "Mô Tả")]
        public string? MoTa { get; set; }

        // --- BỔ SUNG LIÊN KẾT ĐỂ HẾT LỖI ---

        [Required(ErrorMessage = "Vui lòng chọn danh mục")]
        [Display(Name = "Danh Mục")]
        public int DanhMucId { get; set; } // Khóa ngoại (FK)

        [ForeignKey("DanhMucId")]
        public virtual DanhMuc? DanhMuc { get; set; } // Liên kết n-1

        public virtual ICollection<BaiHoc>? BaiHocs { get; set; } // Liên kết 1-n
    }
}