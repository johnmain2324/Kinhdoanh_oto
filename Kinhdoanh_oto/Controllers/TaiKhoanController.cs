using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kinhdoanh_oto.Data;
using Kinhdoanh_oto.Models;
using Kinhdoanh_oto.Core.Service;
using Kinhdoanh_oto.Core.Interfaces;

namespace Kinhdoanh_oto.Controllers
{
    public class TaikhoanController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailService _emailService;
        private readonly IAuthService _authService;

        public TaikhoanController( ApplicationDbContext context, EmailService emailService, IAuthService authService)
        {
            _context = context;
            _emailService = emailService;
            _authService = authService;
        }

        // ==================== ĐĂNG NHẬP ====================

        public IActionResult Dangnhap()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Dangnhap(string sodienthoai, string matkhau)
        {
            if (string.IsNullOrEmpty(sodienthoai) || string.IsNullOrEmpty(matkhau))
            {
                ViewBag.Loi = "Vui lòng nhập đầy đủ thông tin";
                return View();
            }

            var kh = _authService.Login(sodienthoai, matkhau);

            if (kh == null || !BCrypt.Net.BCrypt.Verify(matkhau, kh.Matkhau))
            {
                ViewBag.Loi = "Sai số điện thoại hoặc mật khẩu";
                return View();
            }

            HttpContext.Session.SetString("UserId", kh.Id.ToString());
            HttpContext.Session.SetString("UserName", kh.Hoten ?? "");
            HttpContext.Session.SetString("UserPhone", kh.Sodienthoai ?? "");

            return RedirectToAction("Index", "Trangchu");
        }

        // ==================== ĐĂNG XUẤT ====================

        public IActionResult Dangxuat()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Dangnhap");
        }

        // ==================== ĐĂNG KÝ ====================

        public IActionResult Dangky()
        {
            return View();
        }

        private string TaoOTP()
        {
            Random rd = new Random();
            return rd.Next(100000, 999999).ToString();
        }

        [HttpPost]
        public IActionResult Dangky(string hoten, string email, string sodienthoai)
        {
            if (string.IsNullOrEmpty(hoten) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(sodienthoai))
            {
                ViewBag.Loi = "Vui lòng nhập đầy đủ thông tin";
                return View();
            }

            // kiểm tra số điện thoại
            var exist = _context.Khachhang
                .FirstOrDefault(x => x.Sodienthoai == sodienthoai);

            if (exist != null)
            {
                ViewBag.Loi = "Số điện thoại đã tồn tại";
                return View();
            }

            // chống spam OTP
            var lastOtp = _context.OTPDangky
                .Where(x => x.Sodienthoai == sodienthoai)
                .OrderByDescending(x => x.ThoigianTao)
                .FirstOrDefault();

            if (lastOtp != null && (DateTime.Now - lastOtp.ThoigianTao).TotalSeconds < 60)
            {
                ViewBag.Loi = "Vui lòng chờ 60 giây để gửi OTP mới";
                return View();
            }

            string otp = TaoOTP();

            _emailService.SendOTP(email, otp);

            var otpDangky = new OTPDangky
            {
                Sodienthoai = sodienthoai,
                MaOTP = otp,
                ThoigianTao = DateTime.Now,
                HetHan = DateTime.Now.AddMinutes(5),
                Trangthai = false
            };

            _context.OTPDangky.Add(otpDangky);
            _context.SaveChanges();

            TempData["Sodienthoai"] = sodienthoai;
            TempData["Hoten"] = hoten;
            TempData["Email"] = email;

            return RedirectToAction("DangkyOTP");
        }

        // ==================== XÁC NHẬN OTP ====================

        public IActionResult DangkyOTP()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DangkyOTP(string otp)
        {
            var sodienthoai = TempData.Peek("Sodienthoai")?.ToString();

            if (string.IsNullOrEmpty(sodienthoai))
                return RedirectToAction("Dangky");

            var otpCheck = _context.OTPDangky
                .Where(x => x.Sodienthoai == sodienthoai && !x.Trangthai)
                .OrderByDescending(x => x.Id)
                .FirstOrDefault();

            if (otpCheck == null || otpCheck.MaOTP != otp || otpCheck.HetHan < DateTime.Now)
            {
                ViewBag.Loi = "OTP không hợp lệ hoặc đã hết hạn";
                return View();
            }

            otpCheck.Trangthai = true;
            _context.SaveChanges();

            return RedirectToAction("DangkyThongTin");
        }

        // ==================== NHẬP THÔNG TIN ====================

        public IActionResult DangkyThongTin()
        {
            ViewBag.Sodienthoai = TempData.Peek("Sodienthoai");
            ViewBag.Hoten = TempData.Peek("Hoten");
            ViewBag.Email = TempData.Peek("Email");

            return View();
        }

        [HttpPost]
        public IActionResult DangkyThongTin(Khachhang kh)
        {
            kh.Ngaytao = DateTime.Now;

            // mã hóa mật khẩu
            kh.Matkhau = BCrypt.Net.BCrypt.HashPassword(kh.Matkhau);

            _context.Khachhang.Add(kh);
            _context.SaveChanges();

            return RedirectToAction("Dangnhap");
        }

        // ==================== LOGIN GOOGLE ====================

        [HttpPost]
        public IActionResult LoginGoogle([FromBody] GoogleLoginModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.email))
                return Json(new { success = false });

            var user = _context.Khachhang
                .FirstOrDefault(x => x.Email == model.email);

            if (user == null)
            {
                user = new Khachhang
                {
                    Hoten = model.name,
                    Email = model.email,
                    Ngaytao = DateTime.Now
                };

                _context.Khachhang.Add(user);
                _context.SaveChanges();
            }

            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("UserName", user.Hoten ?? "");
            HttpContext.Session.SetString("UserEmail", user.Email ?? "");

            return Json(new { success = true });
        }
    }
}