using Data_Access_Layer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Admin_Web.Areas.User_Web.Controllers.Pages
{
    [Area("User_Web")]

    public class PagesController(Context context) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index(string slug)
        {
            if (string.IsNullOrEmpty(slug))
            {
                return NotFound(); // or handle error appropriately
            }

            // Retrieve the product based on the slug
            var product = await context.Products.FirstOrDefaultAsync(p => p.Slug == slug);

            if (product == null)
            {
                return NotFound(); // or handle error appropriately
            }

            // Set product data in ViewBag
            ViewBag.ProductName = product.ProductName;
            ViewBag.ProductPrice = product.ProductPrice;
            ViewBag.ProductDescription = product.ProductDescription;
            ViewBag.OfferPrice = product.OfferPrice;
            ViewBag.File = product.File;
            ViewBag.MultiImage = product.MultiImage.Split(',');

            // Pass the product to the view for display
            return View(product);
        }
    }
}
