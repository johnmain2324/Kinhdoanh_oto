using Microsoft.AspNetCore.Mvc;
using Kinhdoanh_oto.Data;
using Kinhdoanh_oto.Models;
using Kinhdoanh_oto.Core.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Kinhdoanh_oto.Controllers
{
    public class MuaxeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MuaxeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int id = 1)
        {
            var car = _context.Cars.FirstOrDefault(x => x.Id == id);

            if (car == null)
                return NotFound();

            return View(car);
        }

        public IActionResult Datcoc(int id)
        {
            var car = _context.Cars.FirstOrDefault(x => x.Id == id);

            if (car == null)
                return NotFound();

            var model = new DatcocViewModel
            {
                CarId = car.Id,
                TenXe = car.TenXe,
                GiaXe = car.GiaNiemYet ?? 0,
                Thumbnail = car.Thumbnail,
                TienDatCoc = 50000000
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult CreateDeposit(DatcocViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Datcoc", model);

            // ✅ 1. CHECK KHÁCH HÀNG
            var customer = _context.Khachhang
                .FirstOrDefault(x => x.Sodienthoai == model.Sodienthoai);

            // ✅ 2. CREATE nếu chưa có
            if (customer == null)
            {
                customer = new Khachhang
                {
                    Hoten = model.Hoten,
                    Sodienthoai = model.Sodienthoai,
                    Email = model.Email,
                    CCCD = model.CCCD,
                    Diachi = model.Diachi,
                    Ngaytao = DateTime.Now // ✅ FIX datetime
                };

                _context.Khachhang.Add(customer);
                _context.SaveChanges();
            }
            else
            {
                // ✅ Update info nếu đã tồn tại (optional nhưng nên có)
                customer.Hoten = model.Hoten;
                customer.Email = model.Email;
                customer.CCCD = model.CCCD;
                customer.Diachi = model.Diachi;

                _context.SaveChanges();
            }

            // ✅ 3. LƯU ĐẶT CỌC (FIX datetime lỗi)
            var deposit = new Datcoc
            {
                KhachhangId = customer.Id,
                XeId = model.CarId,
                Sotien = model.TienDatCoc,
                Ngaydat = DateTime.Now // 🔥 FIX CHÍNH Ở ĐÂY
            };

            _context.Datcoc.Add(deposit);
            _context.SaveChanges();

            // ✅ 4. REDIRECT SANG PAYMENT
            return RedirectToAction("Payment", new { id = deposit.Id });
        }

        public IActionResult Payment(int id)
        {
            var datcoc = _context.Datcoc
                .Include(d => d.Khachhang)
                .FirstOrDefault(d => d.Id == id);

            if (datcoc == null)
                return NotFound();

            var car = _context.Cars.FirstOrDefault(x => x.Id == datcoc.XeId);

            if (car == null)
                return NotFound();

            var vm = new DatcocViewModel
            {
                CarId = car.Id,
                TenXe = car.TenXe,
                GiaXe = car.GiaNiemYet ?? 0,
                Thumbnail = car.Thumbnail,
                TienDatCoc = datcoc.Sotien,

                Hoten = datcoc.Khachhang?.Hoten,
                CCCD = datcoc.Khachhang?.CCCD,
                Sodienthoai = datcoc.Khachhang?.Sodienthoai
            };

            return View(vm);
        }
    }
}