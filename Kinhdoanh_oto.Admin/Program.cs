using Microsoft.EntityFrameworkCore;
using Kinhdoanh_oto.Admin.Data;

var builder = WebApplication.CreateBuilder(args);

// ==================
// Đăng ký DbContext
// ==================
builder.Services.AddDbContext<AdminDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("AdminConnection")));

// ==================
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ==================
// Configure pipeline
// ==================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Dashboard/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// ==================
// Default route Admin
// ==================
<<<<<<< HEAD

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=AdminTaikhoan}/{action=Dangnhap}/{id?}");
=======
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Taikhoan}/{action=Dangnhap}/{id?}");
>>>>>>> 100e3314054e6d0ab23d3feb76078d32588c7324

app.Run();