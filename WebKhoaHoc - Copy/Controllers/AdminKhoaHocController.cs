using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebKhoaHoc.Data;
using WebKhoaHoc.Models;

namespace WebKhoaHoc.Controllers
{
    // Bật phân quyền cho cả Admin và GiangVien
    [Authorize(Roles = "Admin,GiangVien")]
    public class AdminKhoaHocController : Controller
    {
        private readonly ApplicationDbContextContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly UserManager<User> _userManager;

        public AdminKhoaHocController(ApplicationDbContextContext context,
                                     IWebHostEnvironment hostEnvironment,
                                     UserManager<User> userManager)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _userManager = userManager;
        }

        // --- 1. DANH SÁCH KHÓA HỌC ---
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var isAdmin = User.IsInRole("Admin");

            // Nếu là Admin thì lấy hết, nếu là GiangVien thì chỉ lấy của họ
            var query = _context.KhoaHocs.Include(k => k.DanhMuc).AsNoTracking();

            if (!isAdmin)
            {
                query = query.Where(k => k.GiangVienId == userId);
            }

            var dsKhoaHoc = await query.OrderByDescending(k => k.Id).ToListAsync();
            return View(dsKhoaHoc);
        }

        // --- 2. XEM CHI TIẾT ---
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var khoaHoc = await _context.KhoaHocs
                .Include(k => k.DanhMuc)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (khoaHoc == null) return NotFound();

            // Kiểm tra quyền: Admin hoặc Chính chủ mới được xem
            if (!User.IsInRole("Admin") && khoaHoc.GiangVienId != _userManager.GetUserId(User))
                return RedirectToAction("AccessDenied", "Account");

            return View(khoaHoc);
        }

        // --- 3. TẠO MỚI ---
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.DanhMucId = new SelectList(await _context.DanhMucs.ToListAsync(), "Id", "TenDanhMuc");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KhoaHoc khoaHoc, IFormFile? HinhAnhFile)
        {
            ModelState.Remove("HinhAnhFile");
            ModelState.Remove("DanhMuc");
            ModelState.Remove("BaiHocs");
            ModelState.Remove("GiangVienId");

            if (ModelState.IsValid)
            {
                khoaHoc.GiangVienId = _userManager.GetUserId(User);
                khoaHoc.HinhAnh = HinhAnhFile != null ? await SaveImage(HinhAnhFile) : "default.jpg";

                _context.KhoaHocs.Add(khoaHoc);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Thêm thành công khóa học!";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.DanhMucId = new SelectList(await _context.DanhMucs.ToListAsync(), "Id", "TenDanhMuc", khoaHoc.DanhMucId);
            return View(khoaHoc);
        }

        // --- 4. CHỈNH SỬA ---
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var khoaHoc = await _context.KhoaHocs.FindAsync(id);
            if (khoaHoc == null) return NotFound();

            // Quyền chỉnh sửa
            if (!User.IsInRole("Admin") && khoaHoc.GiangVienId != _userManager.GetUserId(User))
                return RedirectToAction("AccessDenied", "Account");

            ViewBag.DanhMucId = new SelectList(await _context.DanhMucs.ToListAsync(), "Id", "TenDanhMuc", khoaHoc.DanhMucId);
            return View(khoaHoc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, KhoaHoc khoaHoc, IFormFile? HinhAnhFile)
        {
            if (id != khoaHoc.Id) return NotFound();

            var existingCourse = await _context.KhoaHocs.AsNoTracking().FirstOrDefaultAsync(k => k.Id == id);
            if (existingCourse == null) return NotFound();

            // Quyền chỉnh sửa (POST)
            if (!User.IsInRole("Admin") && existingCourse.GiangVienId != _userManager.GetUserId(User))
                return RedirectToAction("AccessDenied", "Account");

            ModelState.Remove("HinhAnhFile");
            ModelState.Remove("DanhMuc");
            ModelState.Remove("BaiHocs");
            ModelState.Remove("GiangVienId");

            if (ModelState.IsValid)
            {
                try
                {
                    khoaHoc.GiangVienId = existingCourse.GiangVienId; // Giữ nguyên người tạo gốc
                    if (HinhAnhFile != null)
                    {
                        DeleteImage(existingCourse.HinhAnh);
                        khoaHoc.HinhAnh = await SaveImage(HinhAnhFile);
                    }
                    else
                    {
                        khoaHoc.HinhAnh = existingCourse.HinhAnh;
                    }

                    _context.Update(khoaHoc);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cập nhật thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.KhoaHocs.Any(e => e.Id == id)) return NotFound();
                    else throw;
                }
            }
            ViewBag.DanhMucId = new SelectList(await _context.DanhMucs.ToListAsync(), "Id", "TenDanhMuc", khoaHoc.DanhMucId);
            return View(khoaHoc);
        }

        // --- 5. XÓA ---
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var khoaHoc = await _context.KhoaHocs
                .Include(k => k.DanhMuc)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (khoaHoc == null) return NotFound();

            // Quyền xóa
            if (!User.IsInRole("Admin") && khoaHoc.GiangVienId != _userManager.GetUserId(User))
                return RedirectToAction("AccessDenied", "Account");

            return View(khoaHoc);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var khoaHoc = await _context.KhoaHocs.FindAsync(id);
            if (khoaHoc == null) return RedirectToAction(nameof(Index));

            // Kiểm tra quyền xóa (POST)
            if (!User.IsInRole("Admin") && khoaHoc.GiangVienId != _userManager.GetUserId(User))
                return RedirectToAction("AccessDenied", "Account");

            DeleteImage(khoaHoc.HinhAnh);
            _context.KhoaHocs.Remove(khoaHoc);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Đã xóa khóa học thành công!";

            return RedirectToAction(nameof(Index));
        }

        // --- HELPERS ---
        private async Task<string> SaveImage(IFormFile file)
        {
            string wwwRoot = _hostEnvironment.WebRootPath;
            string folderPath = Path.Combine(wwwRoot, "images");
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

            var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return fileName;
        }

        private void DeleteImage(string? fileName)
        {
            if (string.IsNullOrEmpty(fileName) || fileName == "default.jpg") return;
            var filePath = Path.Combine(_hostEnvironment.WebRootPath, "images", fileName);
            if (System.IO.File.Exists(filePath)) System.IO.File.Delete(filePath);
        }
    }
}