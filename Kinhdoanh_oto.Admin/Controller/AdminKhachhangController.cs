using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kinhdoanh_oto.Admin.Data;
using Kinhdoanh_oto.Admin.Models;

namespace Kinhdoanh_oto.Admin.Controllers
{
    public class AdminKhachhangController : Controller
    {
        private readonly AdminDbContext _context;

        public AdminKhachhangController(AdminDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var khachhang = _context.Khachhang.ToList();
            return View(khachhang);
        }

        public IActionResult Delete(int id)
        {
            var kh = _context.Khachhang.Find(id);

            if (kh == null)
            {
                return NotFound();
            }

            _context.Khachhang.Remove(kh);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}