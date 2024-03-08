using Data_Access_Layer.Data;
using Microsoft.AspNetCore.Mvc;

namespace Admin_Web.Areas.User_Web.Controllers
{
    public class UserController(Context context) : Controller
    {
        [Area("User_Web")]

        [HttpGet]
        public IActionResult Index()
        {
            var carousel = context.Sliders.ToList();
            ViewBag.carousel = carousel;
            var category = context.Categories.ToList();
            ViewBag.Category = category;
            var product = context.Products.ToList();
            ViewBag.Product = product;
            return View();
        }
    }
}
