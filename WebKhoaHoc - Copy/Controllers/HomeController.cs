using Microsoft.AspNetCore.Mvc;
using WebKhoaHoc.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebKhoaHoc.Models;

namespace WebKhoaHoc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContextContext _context;

        public HomeController(ApplicationDbContextContext context)
        {
            _context = context;
        }

        // 1. TRANG CHỦ (INDEX): Tích hợp Tìm kiếm + Lọc + Sắp xếp
        public async Task<IActionResult> Index(string searchString, string sortOrder, string priceFilter)
        {
            // Lưu trạng thái để hiển thị lại trên giao diện (UI)
            ViewData["CurrentFilter"] = searchString;
            ViewData["PriceFilter"] = priceFilter;
            ViewData["CurrentSort"] = sortOrder;

            // Sử dụng Include(k => k.DanhMuc) để hiển thị tên danh mục ở trang chủ
            var khoahocs = _context.KhoaHocs.Include(k => k.DanhMuc).AsNoTracking();

            // A. Tìm kiếm theo tên
            if (!String.IsNullOrEmpty(searchString))
            {
                khoahocs = khoahocs.Where(s => s.TenKhoaHoc.Contains(searchString));
            }

            // B. Lọc theo giá
            if (!String.IsNullOrEmpty(priceFilter))
            {
                if (priceFilter == "free")
                {
                    khoahocs = khoahocs.Where(k => k.GiaTien == 0);
                }
                else if (priceFilter == "paid")
                {
                    khoahocs = khoahocs.Where(k => k.GiaTien > 0);
                }
            }

            // C. Sắp xếp
            switch (sortOrder)
            {
                case "Price_Asc":
                    khoahocs = khoahocs.OrderBy(s => s.GiaTien);
                    break;
                case "Price_Desc":
                    khoahocs = khoahocs.OrderByDescending(s => s.GiaTien);
                    break;
                default: // Mới nhất lên đầu
                    khoahocs = khoahocs.OrderByDescending(s => s.Id);
                    break;
            }

            return View(await khoahocs.ToListAsync());
        }

        // 2. CHI TIẾT KHÓA HỌC (DETAILS) - DÀNH CHO USER
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            // Bổ sung Include để lấy thêm thông tin Danh mục và danh sách Bài học
            var khoaHoc = await _context.KhoaHocs
                .Include(k => k.DanhMuc)
                .Include(k => k.BaiHocs)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (khoaHoc == null) return NotFound();

            return View(khoaHoc);
        }

        // 3. KHÓA HỌC CỦA TÔI (MY COURSES)
        public async Task<IActionResult> MyCourses(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            // Lấy danh sách khóa học, bắt buộc Include BaiHocs để lấy link Youtube ở View
            var query = _context.KhoaHocs
                .Include(k => k.DanhMuc)
                .Include(k => k.BaiHocs)
                .AsNoTracking();

            // Tìm kiếm trong khóa học cá nhân
            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.TenKhoaHoc.Contains(searchString));
            }

            // Sắp xếp khóa học mới đăng ký lên đầu
            var result = await query.OrderByDescending(k => k.Id).ToListAsync();

            return View(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}