using Data_Access_Layer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Admin_Web.Areas.User_Web.Controllers
{
    [Area("User_Web")]
    public class UserController(Context context) : Controller
    {
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
        [HttpGet]
        public IActionResult CategoryWiseProduct(int categoryId)
        {
            // Retrieve products based on the provided category ID
            var category = context.Categories.FirstOrDefault(c => c.Id == categoryId);

            if (category != null)
            {
                var product = context.Products.Where(p => p.CategoryId == categoryId).ToList();
                var carousel = context.Sliders.ToList(); // Fetch slider data
                ViewBag.Product = product;
                ViewBag.carousel = carousel; // Pass slider data to the view
                return View(product);
            }
            else
            {
                return NotFound(); // Handle the case where the category is not found
            }
        }
    }
}
