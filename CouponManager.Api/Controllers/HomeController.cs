using Microsoft.AspNetCore.Mvc;

namespace CouponManager.Api.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
