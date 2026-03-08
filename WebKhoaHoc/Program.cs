using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebKhoaHoc.Data;
using WebKhoaHoc.Models;

var builder = WebApplication.CreateBuilder(args);

// --- 1. ĐĂNG KÝ DỊCH VỤ (SERVICES) ---
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- 2. CẤU HÌNH DATABASE (SQL SERVER) ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Server=(localdb)\\mssqllocaldb;Database=WebKhoaHoc;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";

// Sử dụng đúng tên class ApplicationDbContextContext mà bạn đã đặt
builder.Services.AddDbContext<ApplicationDbContextContext>(options =>
    options.UseSqlServer(connectionString));

// --- 3. CẤU HÌNH IDENTITY (XỬ LÝ NGƯỜI DÙNG & QUYỀN) ---
builder.Services.AddIdentity<User, IdentityRole>(options => {
    // Cấu hình mật khẩu đơn giản để thuận tiện cho việc làm bài tập/kiểm thử
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContextContext>() // Khớp với DbContext của bạn
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromHours(10);
    options.SlidingExpiration = true;
});

var app = builder.Build();

// --- 4. KHỞI TẠO DỮ LIỆU MẶC ĐỊNH (SEED DATA) ---
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var db = services.GetRequiredService<ApplicationDbContextContext>();
        // Tự động chạy Migration để cập nhật cấu trúc Database và Seed Data nghiệp vụ
        db.Database.Migrate();

        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<User>>();

        // Tạo các Role: Admin và User
        string[] roleNames = { "Admin", "User" };
        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // Tạo tài khoản Admin mặc định
        var adminEmail = "admin@webkhoahoc.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            var user = new User
            {
                UserName = adminEmail,
                Email = adminEmail,
                HoTen = "Quản trị viên",
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(user, "Admin123@");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Lỗi khi khởi tạo dữ liệu mặc định.");
    }
}

// --- 5. PIPELINE XỬ LÝ YÊU CẦU (MIDDLEWARE) ---
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Thứ tự Authentication phải luôn đứng trước Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();