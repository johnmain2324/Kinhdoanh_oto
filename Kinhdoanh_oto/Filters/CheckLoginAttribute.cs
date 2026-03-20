using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Kinhdoanh_oto.Filters
{
    public class CheckLoginAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session.GetString("KhachHang");

            if (string.IsNullOrEmpty(session))
            {
                context.Result = new RedirectToActionResult("Dangnhap", "Taikhoan", null);
            }
        }
    }
}