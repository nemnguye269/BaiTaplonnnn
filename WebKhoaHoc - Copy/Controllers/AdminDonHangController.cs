using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using WebKhoaHoc.Data;

namespace WebKhoaHoc.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminDonHangController : Controller
    {
        private readonly ApplicationDbContextContext _context;

        public AdminDonHangController(ApplicationDbContextContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var donHangs = await _context.DonHangs
                .Include(d => d.KhoaHoc)
                .Include(d => d.User) // Đảm bảo bảng DonHang có User
                .OrderByDescending(d => d.NgayMua)
                .ToListAsync();
            return View(donHangs);
        }

        // Thêm các hàm Update trạng thái đơn hàng nếu cần...
    }
}