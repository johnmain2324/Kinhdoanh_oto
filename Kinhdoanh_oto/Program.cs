using Microsoft.EntityFrameworkCore;
using Kinhdoanh_oto.Data;
<<<<<<< HEAD
using Kinhdoanh_oto.Core.Service;
using Kinhdoanh_oto.Core.Interfaces;
=======
>>>>>>> 100e3314054e6d0ab23d3feb76078d32588c7324

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
<<<<<<< HEAD
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<EmailService>(); 
builder.Services.AddScoped<IAuthService, AuthService>();
=======

 builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));
>>>>>>> 100e3314054e6d0ab23d3feb76078d32588c7324

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Trangchu/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
<<<<<<< HEAD
app.UseSession();
=======

>>>>>>> 100e3314054e6d0ab23d3feb76078d32588c7324
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Trangchu}/{action=Index}/{id?}");

app.Run();