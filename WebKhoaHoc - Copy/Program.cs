using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebKhoaHoc.Data;
using WebKhoaHoc.Models;

var builder = WebApplication.CreateBuilder(args);

// --- 1. ĐĂNG KÝ DỊCH VỤ (SERVICES) ---
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- 2. CẤU HÌNH DATABASE ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Server=(localdb)\\mssqllocaldb;Database=WebKhoaHoc;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";

builder.Services.AddDbContext<ApplicationDbContextContext>(options =>
    options.UseSqlServer(connectionString));

// --- 3. CẤU HÌNH IDENTITY ---
builder.Services.AddIdentity<User, IdentityRole>(options => {
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContextContext>()
.AddDefaultTokenProviders();

// Cấu hình Cookie (Phải đặt SAU AddIdentity)
builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromHours(10);
    options.SlidingExpiration = true;
});

var app = builder.Build();

// --- 4. KHỞI TẠO DỮ LIỆU (SEED DATA) ---
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var db = services.GetRequiredService<ApplicationDbContextContext>();
        db.Database.Migrate();

        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<User>>();

        // Khởi tạo Role
        string[] roleNames = { "Admin", "User", "GiangVien" };
        foreach (var roleName in roleNames)
        {
            if (!roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
            {
                roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
            }
        }

        // Tạo Admin mặc định
        var adminEmail = "admin@webkhoahoc.com";
        var adminUser = userManager.FindByEmailAsync(adminEmail).GetAwaiter().GetResult();

        if (adminUser == null)
        {
            var user = new User
            {
                UserName = adminEmail,
                Email = adminEmail,
                HoTen = "Quản trị viên",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString() // Đảm bảo Stamp không null
            };
            var result = userManager.CreateAsync(user, "Admin123@").GetAwaiter().GetResult();
            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, "Admin").GetAwaiter().GetResult();
            }
        }
        else
        {
            // ÉP BUỘC GÁN QUYỀN: Nếu tài khoản tồn tại nhưng chưa có quyền Admin
            var isInRole = userManager.IsInRoleAsync(adminUser, "Admin").GetAwaiter().GetResult();
            if (!isInRole)
            {
                userManager.AddToRoleAsync(adminUser, "Admin").GetAwaiter().GetResult();
                // Làm mới SecurityStamp để Cookie cũ bị vô hiệu hóa, bắt buộc cập nhật quyền mới
                userManager.UpdateSecurityStampAsync(adminUser).GetAwaiter().GetResult();
            }
        }
        // Thêm đoạn này vào Program.cs (trong phần Seed Data)
        var gvEmail = "giangvien@webkhoahoc.com";
        var gvUser = userManager.FindByEmailAsync(gvEmail).GetAwaiter().GetResult();

        if (gvUser == null)
        {
            var user = new User
            {
                UserName = gvEmail,
                Email = gvEmail,
                HoTen = "Giảng Viên Demo",
                EmailConfirmed = true
            };
            var result = userManager.CreateAsync(user, "Gv123@").GetAwaiter().GetResult();
            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, "GiangVien").GetAwaiter().GetResult();
            }
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Lỗi khởi tạo dữ liệu.");
    }
}


// --- 5. PIPELINE XỬ LÝ YÊU CẦU ---
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

// Thứ tự này cực kỳ quan trọng
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();