using Kinhdoanh_oto.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Kinhdoanh_oto.Controllers
{
    public class XeController : Controller
    {
        [CheckLogin]
        public IActionResult Muaxe()
        {
            return View();
        }

        [CheckLogin]
        public IActionResult Chitiet(int id)
        {
            return View();
        }

        public IActionResult Sosanh()
        {
            return View();
        }
    }
}