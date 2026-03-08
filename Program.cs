using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebKhoaHoc.Data;
using WebKhoaHoc.Models;

var builder = WebApplication.CreateBuilder(args);

// --- 1. DỊCH VỤ HỆ THỐNG ---
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- 2. CẤU HÌNH DATABASE ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Server=(localdb)\\mssqllocaldb;Database=WebKhoaHoc;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";

builder.Services.AddDbContext<ApplicationDbContextContext>(options =>
    options.UseSqlServer(connectionString));

// --- 3. CẤU HÌNH IDENTITY (THAY THẾ CHO COOKIE CŨ) ---
// Việc dùng AddIdentity giúp tạo ra các bảng AspNetUsers, AspNetRoles trong SQL
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
    .AddEntityFrameworkStores<ApplicationDbContextContext>()
    .AddDefaultTokenProviders();

// Cấu hình Cookie cho Identity
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromHours(10);
});

var app = builder.Build();

// --- 4. CẤU HÌNH PIPELINE (MIDDLEWARE) ---
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

app.UseAuthentication(); // Bắt buộc
app.UseAuthorization();  // Bắt buộc

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();