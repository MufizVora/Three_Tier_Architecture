using Data_Access_Layer.Data;
using Microsoft.AspNetCore.Mvc;

namespace Admin_Web.Areas.User_Web.Controllers.Pages
{
    [Area("User_Web")]

    public class PagesController(Context context) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
