using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebKhoaHoc.Data;
using WebKhoaHoc.Models;
using Microsoft.AspNetCore.Authorization;
using System.IO;

namespace WebKhoaHoc.Controllers
{
    // [Authorize(Roles = "Admin")] 
    public class AdminKhoaHocController : Controller
    {
        private readonly ApplicationDbContextContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AdminKhoaHocController(ApplicationDbContextContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // 1. DASHBOARD & DANH SÁCH (Sử dụng container-fluid trong View để mất khoảng trắng)
        public async Task<IActionResult> Index()
        {
            var dsKhoaHoc = await _context.KhoaHocs.AsNoTracking().ToListAsync();

            ViewBag.TotalCourses = dsKhoaHoc.Count;
            ViewBag.TotalOrders = await _context.DonHangs.CountAsync();
            ViewBag.TotalRevenue = await _context.DonHangs.SumAsync(d => (decimal?)d.SoTien) ?? 0m;

            var recentOrders = await _context.DonHangs
                .OrderByDescending(d => d.Id)
                .Take(5)
                .Select(d => new { d.Id, d.SoTien })
                .ToListAsync();

            recentOrders.Reverse();
            ViewBag.ChartLabels = recentOrders.Select(x => "ĐH #" + x.Id).ToList();
            ViewBag.ChartData = recentOrders.Select(x => x.SoTien).ToList();

            return View("Dashboard", dsKhoaHoc);
        }

        // 2. CHI TIẾT
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var khoaHoc = await _context.KhoaHocs.FirstOrDefaultAsync(m => m.Id == id);
            if (khoaHoc == null) return NotFound();
            return View(khoaHoc);
        }

        // 3. TẠO MỚI
        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KhoaHoc khoaHoc, IFormFile? HinhAnhFile)
        {
            ModelState.Remove("HinhAnhFile");
            if (ModelState.IsValid)
            {
                if (HinhAnhFile != null) khoaHoc.HinhAnh = await SaveImage(HinhAnhFile);
                _context.KhoaHocs.Add(khoaHoc);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Thêm khóa học mới thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(khoaHoc);
        }

        // 4. CHỈNH SỬA
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var khoaHoc = await _context.KhoaHocs.FindAsync(id);
            if (khoaHoc == null) return NotFound();
            return View(khoaHoc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, KhoaHoc khoaHoc, IFormFile? HinhAnhFile)
        {
            if (id != khoaHoc.Id) return NotFound();
            ModelState.Remove("HinhAnhFile");

            if (ModelState.IsValid)
            {
                try
                {
                    if (HinhAnhFile != null)
                    {
                        DeleteImage(khoaHoc.HinhAnh);
                        khoaHoc.HinhAnh = await SaveImage(HinhAnhFile);
                    }
                    _context.Update(khoaHoc);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cập nhật thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.KhoaHocs.Any(e => e.Id == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(khoaHoc);
        }

        // --- 5. CHỨC NĂNG XÓA (DELETE) ---

        // Bước 1: Hiển thị giao diện xác nhận xóa - Sửa lỗi 404
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var khoaHoc = await _context.KhoaHocs
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (khoaHoc == null) return NotFound();

            return View(khoaHoc);
        }

        // Bước 2: Thực hiện xóa thực tế trong Database
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var khoaHoc = await _context.KhoaHocs.FindAsync(id);

            if (khoaHoc != null)
            {
                // Xóa ảnh vật lý để tránh rác server
                DeleteImage(khoaHoc.HinhAnh);

                _context.KhoaHocs.Remove(khoaHoc);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Đã xóa khóa học thành công!";
            }

            return RedirectToAction(nameof(Index));
        }

        // --- HELPERS ---
        private async Task<string> SaveImage(IFormFile file)
        {
            string folderPath = Path.Combine(_hostEnvironment.WebRootPath, "images", "courses");
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string path = Path.Combine(folderPath, fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return fileName;
        }

        private void DeleteImage(string? fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "images", "courses", fileName);
                if (System.IO.File.Exists(imagePath)) System.IO.File.Delete(imagePath);
            }
        }
    }
}