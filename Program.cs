using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddRazorPages();
builder.Services.AddControllers();

// Configure Cookie Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Taikhoan/Dangnhap";
        options.LogoutPath = "/Taikhoan/Logout";
        options.AccessDeniedPath = "/";
    });

builder.Services.AddAuthorization();

// Initialize Firebase Admin SDK
var serviceAccountPath = builder.Configuration["Firebase:ServiceAccountPath"] 
    ?? Path.Combine(AppContext.BaseDirectory, "firebase-service-account.json");

if (System.IO.File.Exists(serviceAccountPath))
{
    FirebaseApp.Create(new AppOptions
    {
        Credential = GoogleCredential.FromFile(serviceAccountPath)
    });
}
else
{
    Console.WriteLine($"Warning: Firebase service account file not found at {serviceAccountPath}");
}

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();