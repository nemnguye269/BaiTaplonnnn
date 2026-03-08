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
            // Lưu trạng thái để hiển thị lại trên View
            ViewData["CurrentFilter"] = searchString;
            ViewData["PriceFilter"] = priceFilter;

            var khoahocs = from k in _context.KhoaHocs.AsNoTracking()
                           select k;

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
                default: // Mặc định: Mới nhất lên đầu
                    khoahocs = khoahocs.OrderByDescending(s => s.Id);
                    break;
            }

            return View(await khoahocs.ToListAsync());
        }

        // 2. CHI TIẾT (DETAILS)
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var khoaHoc = await _context.KhoaHocs
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (khoaHoc == null) return NotFound();

            return View(khoaHoc);
        }

        // 3. KHÓA HỌC CỦA TÔI (MY COURSES)
        public async Task<IActionResult> MyCourses(string searchString)
        {
            // Demo: Lấy tất cả khóa học (Thực tế sẽ join bảng UserCourse)
            var khoahocs = from k in _context.KhoaHocs.AsNoTracking()
                           select k;

            if (!String.IsNullOrEmpty(searchString))
            {
                khoahocs = khoahocs.Where(s => s.TenKhoaHoc.Contains(searchString));
            }

            ViewData["CurrentFilter"] = searchString;
            return View(await khoahocs.ToListAsync());
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