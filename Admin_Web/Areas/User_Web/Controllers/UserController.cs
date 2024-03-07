using Microsoft.AspNetCore.Mvc;

namespace Admin_Web.Areas.User_Web.Controllers
{
    public class UserController : Controller
    {
        [Area("User_Web")]

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
