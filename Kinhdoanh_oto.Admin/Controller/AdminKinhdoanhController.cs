using Microsoft.AspNetCore.Mvc;

namespace Kinhdoanh_oto.Controllers
{
    public class AdminKinhdoanhController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}