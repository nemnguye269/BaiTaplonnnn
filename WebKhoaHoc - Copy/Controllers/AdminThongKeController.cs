using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WebKhoaHoc.Data;
using WebKhoaHoc.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebKhoaHoc.Controllers
{
    [Authorize(Roles = "Admin")] // Đừng quên mở khóa dòng này để bảo mật
    public class AdminThongKeController : Controller
    {
        private readonly ApplicationDbContextContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminThongKeController(ApplicationDbContextContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            // 1. Thống kê người dùng theo vai trò
            var userStats = new Dictionary<string, int>();
            var roles = await _roleManager.Roles.ToListAsync();
            foreach (var role in roles)
            {
                var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
                userStats.Add(role.Name, usersInRole.Count);
            }
            ViewBag.UserStats = userStats;

            // 2. Top 5 khóa học (Dùng Join để lấy tên luôn trong 1 câu lệnh SQL)
            var topCourses = await _context.DonHangs
                .GroupBy(d => d.KhoaHocId)
                .Select(g => new TopCourseViewModel
                {
                    KhoaHocId = g.Key,
                    SoHocVien = g.Count()
                })
                .OrderByDescending(x => x.SoHocVien)
                .Take(5)
                .ToListAsync();

            // Gán tên khóa học nhanh chóng
            var courseIds = topCourses.Select(x => x.KhoaHocId).ToList();
            var courseNames = await _context.KhoaHocs
                .Where(k => courseIds.Contains(k.Id))
                .ToDictionaryAsync(k => k.Id, k => k.TenKhoaHoc);

            foreach (var item in topCourses)
            {
                item.TenKhoaHoc = courseNames.ContainsKey(item.KhoaHocId) ? courseNames[item.KhoaHocId] : "N/A";
            }
            ViewBag.TopCourses = topCourses;

            // 3. Hiệu suất đào tạo
            var allCourses = await _context.KhoaHocs
                .Include(kh => kh.BaiHocs)
                .Include(kh => kh.DonHangs)
                .ToListAsync();

            var allUsers = await _userManager.Users.ToListAsync();

            var instructorStats = allCourses
                .GroupBy(kh => kh.GiangVienId ?? "Unassigned")
                .Select(g => {
                    var instructor = allUsers.FirstOrDefault(u => u.Id == g.Key);
                    return new InstructorReportViewModel
                    {
                        // Ưu tiên lấy HoTen, nếu không có thì lấy UserName
                        TenGiangVien = instructor?.HoTen ?? instructor?.UserName ?? "Chưa xác định",
                        DanhSachKhoaHoc = g.Select(kh => new CourseDetailInfo
                        {
                            TenKhoaHoc = kh.TenKhoaHoc,
                            SoBaiGiang = kh.BaiHocs?.Count ?? 0,
                            SoHocVien = kh.DonHangs?.Count ?? 0
                        }).ToList()
                    };
                })
                .ToList();

            return View(instructorStats);
        }
    }

    // ViewModel giữ nguyên như bạn đã viết...
}