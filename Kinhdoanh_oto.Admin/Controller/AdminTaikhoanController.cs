using Microsoft.AspNetCore.Mvc;
using Kinhdoanh_oto.Admin.Data;
using System.Linq;

namespace Kinhdoanh_oto.Admin.Controllers
{
    public class AdminTaikhoanController : Controller
    {
        private readonly AdminDbContext _context;

        public AdminTaikhoanController(AdminDbContext context)
        {
            _context = context;
        }

        public IActionResult Dangnhap()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Dangnhap(string email, string matkhau, string mabaomat)
        {
            var admin = _context.Nguoiquantris
                .FirstOrDefault(x => x.Email == email);

            if (admin == null)
            {
                ViewBag.Loi = "Tài khoản không tồn tại";
                return View();
            }

            // Kiểm tra đang bị khóa
            if (admin.ThoigianKhoa != null && admin.ThoigianKhoa > DateTime.Now)
            {
                var thoigianConLai = Math.Ceiling(
                    (admin.ThoigianKhoa.Value - DateTime.Now).TotalMinutes
                );

                ViewBag.Loi = $"Tài khoản bị khóa. Vui lòng thử lại sau {thoigianConLai} phút.";
                return View();
            }

            // Sai thông tin
            if (admin.Matkhau != matkhau || admin.Mabaomat != mabaomat)
            {
                admin.Solandangsai++;

                if (admin.Solandangsai >= 3)
                {
                    admin.ThoigianKhoa = DateTime.Now.AddMinutes(5);
                    admin.Solandangsai = 0;

                    ViewBag.Loi = "Bạn đã nhập sai quá 3 lần. Tài khoản bị khóa 5 phút.";
                }
                else
                {
                    ViewBag.Loi = $"Sai thông tin. Lần sai: {admin.Solandangsai}/3";
                }

                _context.SaveChanges();
                return View();
            }

            // Đăng nhập thành công
            admin.Solandangsai = 0;
            admin.ThoigianKhoa = null;
            _context.SaveChanges();

            return RedirectToAction("Index", "Dashboard");
        }
    }
}