using Microsoft.AspNetCore.Mvc;
using WebKhoaHoc.Models;
using WebKhoaHoc.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace WebKhoaHoc.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // --- 1. TRANG BÁO LỖI QUYỀN TRUY CẬP (Sửa lỗi 404 của bạn) ---
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        // --- 2. ĐĂNG KÝ ---
        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.UserName,
                    Email = model.Email ?? $"{model.UserName}@gmail.com", // Ưu tiên email từ model
                    HoTen = model.HoTen ?? model.UserName
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Gán Role mặc định là User
                    await _userManager.AddToRoleAsync(user, "User");

                    // Tự động đăng nhập luôn sau khi đăng ký thành công
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        // --- 3. ĐĂNG NHẬP ---
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            // Lưu lại đường dẫn người dùng định vào trước đó
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    // Nếu có returnUrl hợp lệ thì quay lại trang đó, không thì về Home/Admin
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    var user = await _userManager.FindByNameAsync(model.UserName);
                    if (user != null && await _userManager.IsInRoleAsync(user, "Admin"))
                    {
                        return RedirectToAction("Index", "AdminKhoaHoc", new { area = "" });
                    }
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Tài khoản hoặc mật khẩu không chính xác.");
            }
            return View(model);
        }

        // --- 4. ĐĂNG XUẤT ---
        // Thay đổi sang HttpGet để dễ dàng gọi từ Link hoặc HttpPost tùy ý
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}