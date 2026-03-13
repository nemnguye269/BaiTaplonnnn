using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebKhoaHoc.Models
{
    [Table("DanhGia")]
    public class DanhGia
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nội dung đánh giá không được để trống")]
        [Display(Name = "Nội dung")]
        public string NoiDung { get; set; }

        [Range(1, 5, ErrorMessage = "Số sao phải từ 1 đến 5")]
        [Display(Name = "Số sao")]
        public int SoSao { get; set; }

        public DateTime NgayDanhGia { get; set; } = DateTime.Now;

        // --- LIÊN KẾT DỮ LIỆU (FK) ---

        [Required]
        public int KhoaHocId { get; set; }

        [ForeignKey("KhoaHocId")]
        public virtual KhoaHoc? KhoaHoc { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
    }
}