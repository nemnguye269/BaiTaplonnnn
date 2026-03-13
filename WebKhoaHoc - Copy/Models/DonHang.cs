using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace WebKhoaHoc.Models
{
    [Table("DonHang")]
    public class DonHang
    {
        [Key]
        [Display(Name = "Mã Đơn Hàng")]
        public int Id { get; set; }

        // Liên kết tới User (Identity) để biết ai là người mua
        [Required]
        [Display(Name = "Người Dùng")]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; set; } // Thuộc tính điều hướng tới bảng User

        // Liên kết tới khóa học được mua
        [Required(ErrorMessage = "Vui lòng chọn khóa học")]
        [Display(Name = "Khóa Học")]
        public int KhoaHocId { get; set; }

        [ForeignKey("KhoaHocId")]
        public virtual KhoaHoc? KhoaHoc { get; set; } // Thuộc tính điều hướng tới bảng KhoaHoc

        [Required]
        [Display(Name = "Ngày Mua")]
        [DataType(DataType.DateTime)]
        public DateTime NgayMua { get; set; } = DateTime.Now;

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Số tiền không được âm")]
        [Display(Name = "Số Tiền Thanh Toán")]
        // Lưu ý: Cột này sẽ được cấu hình Precision trong DbContext là (18,2)
        public decimal SoTien { get; set; }

        [Display(Name = "Trạng Thái")]
        public bool TrangThai { get; set; } = true; // True: Thành công, False: Đã hủy

        // Navigation collection for order details
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();
    }
}