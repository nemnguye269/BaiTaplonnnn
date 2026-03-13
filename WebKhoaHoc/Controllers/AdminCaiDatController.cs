using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebKhoaHoc.Data;
using WebKhoaHoc.Models;

namespace WebKhoaHoc.Controllers
{
    public class AdminCaiDatController : Controller
    {
        private readonly ApplicationDbContextContext _context;

        public AdminCaiDatController(ApplicationDbContextContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Lấy dòng cấu hình đầu tiên, nếu chưa có thì tạo mới để tránh lỗi null
            var config = await _context.CauHinhs.FirstOrDefaultAsync();
            if (config == null)
            {
                config = new CauHinh { TenWebsite = "WebKhoaHoc" };
            }
            return View(config);
        }

        [HttpPost]
        public async Task<IActionResult> Save(CauHinh model)
        {
            if (model.Id == 0) {
                _context.CauHinhs.Add(model);
            } else {
                _context.CauHinhs.Update(model);
            }
            
            await _context.SaveChangesAsync();
            TempData["Success"] = "Cập nhật cài đặt thành công!";
            return RedirectToAction(nameof(Index));
        }
    }
}