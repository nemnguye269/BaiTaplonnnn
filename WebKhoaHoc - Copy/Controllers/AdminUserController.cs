using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebKhoaHoc.Models; // Quan trọng: Phải có dòng này để nhận ViewModel

namespace WebKhoaHoc.Controllers
{
    public class AdminUserController : Controller
    {
        private readonly UserManager<User> _userManager;

        public AdminUserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            // 1. Lấy danh sách tất cả User từ Database
            var users = await _userManager.Users.ToListAsync();

            // 2. Tạo danh sách chứa cả User và Role tương ứng
            var userWithRoles = new List<UserRoleViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userWithRoles.Add(new UserRoleViewModel
                {
                    User = user,
                    Roles = roles.ToList()
                });
            }

            // 3. Trả về View danh sách đã xử lý
            return View(userWithRoles);
        }

        // Các hàm ToggleLock giữ nguyên...
    }
}