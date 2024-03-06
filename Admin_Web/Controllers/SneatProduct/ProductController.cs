using Bussiness_Access_Layer.Interface.SneatCategory;
using Bussiness_Access_Layer.Interface.SneatProduct;
using DTOLayer.DTOs_Models.Product_DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Admin_Web.Controllers.SneatProduct
{
    public class ProductController : Controller
    {
        private readonly ProductInterface _product;
        private readonly CategoryInterface _category;

        public ProductController(ProductInterface product, CategoryInterface category)
        {
            _product = product;
            _category = category;
        }
        public string GetCookie(string Id)
        {
            var x = Request.Cookies[Id];
            return x;
        }
        public IActionResult ListProduct()
        {
            string adminId = HttpContext.Request.Cookies["UserId"];
            if (string.IsNullOrEmpty(adminId))
            {
                return RedirectToAction("Login", "Admin");
            }
            var adminid = GetCookie("UserId");
            var products = _product.GetProducts(adminid);
            return View(products);
        }
        [HttpGet]
        public IActionResult CreateProduct()
        {
            var adminid = GetCookie("UserId");
            var category = _category.GetCategories(adminid);
            ViewBag.category = category;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDTO product, IFormFile file, IFormFile[] MultiImage)
        {
            var response = "";

            var adminid = GetCookie("UserId");
            var category = _category.GetCategories(adminid);
            ViewBag.category = category;

            try
            {
                product.AdminId = GetCookie("UserId");
                response =await _product.CreateProduct(product, file, MultiImage);
                if (response == "Success")
                {
                    return RedirectToAction("ListProduct", "Product");
                }
                else
                {
                    ViewBag.response = "Failed to add product";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public IActionResult DetailProduct(int id)
        {
            var Products = _product.GetProductData(id);
            return View(Products);
        }
        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            var adminid = GetCookie("UserId");
            var category = _category.GetCategories(adminid);
            ViewBag.category = category;
            var Products = _product.GetProductData(id);
            return View(Products);
        }
        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductDTO product, IFormFile file, IFormFile[] MultiImage)
        {
            var response = "";

            var adminid = GetCookie("UserId");
            var category = _category.GetCategories(adminid);
            ViewBag.category = category;

            try
            {
                product.AdminId = GetCookie("UserId");
                response = await _product.EditProduct(product, file, MultiImage);

                if (response == "Success")
                {
                    return RedirectToAction("ListProduct", "Product");
                }
                else
                {
                    ViewBag.response = "Failed to edit product: " + response;
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.response = "Failed to edit product: " + ex.Message;
                return View();
            }
        }
        [HttpGet]
        public IActionResult DeleteProduct(int id)
        {
            var Products = _product.GetProductData(id);
            return View(Products);
        }
        [HttpPost]
        public IActionResult DeleteProduct(ProductDTO product)
        {
            var response = "";

            try
            {
                product.AdminId = GetCookie("UserId");
                response = _product.DeleteProduct(product);

                if (response == "Success")
                {
                    return RedirectToAction("ListProduct", "Product");
                }
                else
                {
                    ViewBag.response = response;
                    return View();
                }
            }
            catch
            {
                return View(response);
            }
        }
    }
}
