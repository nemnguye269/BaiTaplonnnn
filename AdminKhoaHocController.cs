using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebKhoaHoc.Data;
using WebKhoaHoc.Models;

namespace WebKhoaHoc.Controllers
{
    public class AdminKhoaHocController : Controller
    {
        private readonly ApplicationDbContextContext _context;

        public AdminKhoaHocController(ApplicationDbContextContext context)
        {
            _context = context;
        }

        // 1. DANH SÁCH (Index)
        public async Task<IActionResult> Index()
        {
            return View(await _context.KhoaHocs.ToListAsync());
        }

        // 2. TẠO MỚI (Create)
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KhoaHoc khoaHoc)
        {
            if (ModelState.IsValid)
            {
                _context.KhoaHocs.Add(khoaHoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(khoaHoc);
        }

        // 3. CHỈNH SỬA (Edit)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var khoaHoc = await _context.KhoaHocs.FindAsync(id);
            if (khoaHoc == null) return NotFound();

            return View(khoaHoc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, KhoaHoc khoaHoc)
        {
            if (id != khoaHoc.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(khoaHoc);
                    await _context.SaveChangesAsync();
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

        // 4. XÓA (Delete)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var khoaHoc = await _context.KhoaHocs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (khoaHoc == null) return NotFound();

            return View(khoaHoc);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var khoaHoc = await _context.KhoaHocs.FindAsync(id);
            if (khoaHoc != null)
            {
                _context.KhoaHocs.Remove(khoaHoc);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}