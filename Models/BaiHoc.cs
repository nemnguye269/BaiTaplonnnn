using System;
using System.ComponentModel.DataAnnotations;

namespace WebKhoaHoc.Models
{
    public class BaiHoc
    {
        public int Id { get; set; }

        [MaxLength(200)]
        public string TenBaiHoc { get; set; }

        public string NoiDung { get; set; }

        // Relationship to KhoaHoc
        public int KhoaHocId { get; set; }
        public KhoaHoc KhoaHoc { get; set; }
    }
}
