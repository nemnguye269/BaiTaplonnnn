using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebKhoaHoc.Data;
using WebKhoaHoc.Models;

namespace WebKhoaHoc.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ApplicationDbContextContext _context;

        public CheckoutController(ApplicationDbContextContext context)
        {
            _context = context;
        }

        // 1. XÁC NHẬN ĐƠN HÀNG (Hiển thị trang hóa đơn)
        public async Task<IActionResult> Confirm(int id)
        {
            // Kiểm tra đăng nhập chưa
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account"); // Chưa thì bắt đăng nhập
            }

            var khoaHoc = await _context.KhoaHocs.FindAsync(id);
            if (khoaHoc == null) return NotFound();

            return View(khoaHoc);
        }

        // 2. XỬ LÝ THANH TOÁN (Khi bấm nút "Thanh toán ngay")
        [HttpPost]
        public async Task<IActionResult> Payment(int id)
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");

            var khoaHoc = await _context.KhoaHocs.FindAsync(id);
            if (khoaHoc == null) return NotFound();

            // Lấy ID người dùng hiện tại
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Tạo đơn hàng mới
            var donHang = new DonHang
            {
                UserId = userId,
                KhoaHocId = id,
                NgayMua = DateTime.Now,
                SoTien = khoaHoc.GiaTien,
                TrangThai = true
            };

            _context.DonHangs.Add(donHang);
            await _context.SaveChangesAsync();

            // Chuyển hướng đến trang thành công
            return RedirectToAction("Success");
        }

        // 3. TRANG THÔNG BÁO THÀNH CÔNG
        public IActionResult Success()
        {
            return View();
        }
    }
}