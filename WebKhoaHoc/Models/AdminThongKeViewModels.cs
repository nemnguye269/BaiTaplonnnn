namespace WebKhoaHoc.Models
{
    public class TopCourseViewModel
    {
        public int KhoaHocId { get; set; }
        public string TenKhoaHoc { get; set; }
        public int SoHocVien { get; set; }
    }

    public class InstructorReportViewModel
    {
        public string TenGiangVien { get; set; }
        // Khởi tạo List để tránh lỗi NullReferenceException
        public List<CourseDetailInfo> DanhSachKhoaHoc { get; set; } = new List<CourseDetailInfo>();
    }

    public class CourseDetailInfo
    {
        public string TenKhoaHoc { get; set; }
        public int SoBaiGiang { get; set; }
        public int SoHocVien { get; set; }
    }

}