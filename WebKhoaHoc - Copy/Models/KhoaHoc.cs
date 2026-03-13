using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity; // Cần thêm cái này để dùng IdentityUser hoặc User custom

namespace WebKhoaHoc.Models
{
    [Table("KhoaHoc")]
    public class KhoaHoc
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên khóa học không được để trống")]
        [MinLength(10, ErrorMessage = "Tên khóa học phải có tối thiểu 10 ký tự")]
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

        [Required(ErrorMessage = "Vui lòng nhập link video")]
        [RegularExpression(@"^(https?://)?(www\.)?(youtube\.com/watch\?v=|youtu\.be/)[a-zA-Z0-9_-]{11}$",
     ErrorMessage = "Link YouTube không hợp lệ")]
        public string VideoUrl { get; set; }

        // --- LIÊN KẾT GIẢNG VIÊN ---
        [Display(Name = "Mã Giảng Viên")]
        public string? GiangVienId { get; set; }

        [ForeignKey("GiangVienId")]
        public virtual User? GiangVien { get; set; } // Liên kết trực tiếp tới bảng User

        // --- LIÊN KẾT DANH MỤC ---
        [Required(ErrorMessage = "Vui lòng chọn danh mục")]
        [Display(Name = "Danh Mục")]
        public int DanhMucId { get; set; }

        [ForeignKey("DanhMucId")]
        public virtual DanhMuc? DanhMuc { get; set; }

        // --- CÁC DANH SÁCH LIÊN QUAN ---
        public virtual ICollection<BaiHoc>? BaiHocs { get; set; }

        public virtual ICollection<DonHang>? DonHangs { get; set; } = new List<DonHang>();
    }
}