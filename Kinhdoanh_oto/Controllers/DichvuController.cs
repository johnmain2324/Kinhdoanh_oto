using Microsoft.AspNetCore.Mvc;
using Kinhdoanh_oto.Data;
using Kinhdoanh_oto.Models;
using Kinhdoanh_oto.Core.Models;

namespace Kinhdoanh_oto.Controllers
{
    public class DichvuController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DichvuController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ======================
        // TRANG DỊCH VỤ
        // ======================
        public IActionResult Index()
        {
            return View();
        }

        // ======================
        // ĐẶT LỊCH DỊCH VỤ
        // ======================
        [HttpPost]
        public IActionResult Book(ServiceBooking model)
        {
            if (!ModelState.IsValid)
                return View("Index", model);

            model.NgayTao = DateTime.Now;

            _context.ServiceBookings.Add(model);
            _context.SaveChanges();

            TempData["Success"] = "Đặt lịch thành công!";

            return RedirectToAction("Index");
        }
    }
}