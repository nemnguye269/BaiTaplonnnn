using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebKhoaHoc.Models
{
    [Table("BaiHoc")]
    public class BaiHoc
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tiêu Đề Bài Học")]
        public string TieuDe { get; set; }

        public string? VideoUrl { get; set; }

        // Khóa ngoại liên kết tới Khóa học
        public int KhoaHocId { get; set; }

        [ForeignKey("KhoaHocId")]
        public virtual KhoaHoc? KhoaHoc { get; set; }
    }
}