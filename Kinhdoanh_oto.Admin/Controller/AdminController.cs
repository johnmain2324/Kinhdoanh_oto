using Microsoft.AspNetCore.Mvc;
using Kinhdoanh_oto.Admin.Models;

namespace Kinhdoanh_oto.Admin.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}