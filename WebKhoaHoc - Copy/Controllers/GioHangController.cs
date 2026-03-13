using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WebKhoaHoc.Data;
using WebKhoaHoc.Models;

namespace WebKhoaHoc.Controllers
{
    public class GioHangController : Controller
    {
        private readonly ApplicationDbContextContext _context;
        private readonly UserManager<User> _userManager;

        public GioHangController(ApplicationDbContextContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // API: Lấy thông tin tổng quát để hiển thị lên Header
        [HttpGet]
        public async Task<IActionResult> GetCartSummary()
        {
            var userId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(userId))
            {
                return Json(new { totalAmount = "0 đ", count = 0 });
            }

            // Sử dụng bảng DonHangs theo cấu trúc DB hiện tại của bạn
            var cartItems = await _context.DonHangs
                .Where(g => g.UserId == userId)
                .Include(g => g.KhoaHoc)
                .ToListAsync();

            var total = cartItems.Sum(x => x.KhoaHoc?.GiaTien ?? 0);
            var count = cartItems.Count;

            return Json(new
            {
                totalAmount = total.ToString("#,##0") + " đ",
                count = count
            });
        }

        // API: Xử lý đăng ký khóa học (Thêm vào bảng DonHangs)
        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return Json(new { success = false, message = "Chưa đăng nhập" });
            }

            // Kiểm tra xem người dùng đã đăng ký khóa học này chưa
            var existingItem = await _context.DonHangs
                .FirstOrDefaultAsync(g => g.UserId == userId && g.KhoaHocId == id);

            if (existingItem == null)
            {
                var newOrder = new DonHang
                {
                    UserId = userId,
                    KhoaHocId = id,
                    NgayMua = DateTime.Now // use existing model property
                };
                _context.DonHangs.Add(newOrder);
                await _context.SaveChangesAsync();
            }

            return Json(new { success = true });
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}