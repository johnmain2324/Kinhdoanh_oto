using Microsoft.AspNetCore.Mvc;
using Kinhdoanh_oto.Data;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Kinhdoanh_oto.Controllers
{
    public class TrangchuController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrangchuController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");

            var cars = _context.Cars
                .OrderByDescending(x => x.Id)
                .Take(8)
                .ToList();

            return View(cars);
        }
    }
}