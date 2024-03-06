using Bussiness_Access_Layer.Interface.SneatCategory;
using DTOLayer.DTOs_Models.Category_DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Admin_Web.Controllers.SneatCategory
{
    public class CategoryController : Controller
    {
        private readonly CategoryInterface _category;
        public CategoryController(CategoryInterface category)
        {
            _category = category;
        }
        public string GetCookie(string Id)
        {
            var x = Request.Cookies[Id];
            return x;
        }
        [HttpGet]
        public IActionResult ListCategory()
        {
            var adminid = GetCookie("UserId");
            var categories = _category.GetCategories(adminid);
            return View(categories);
        }
        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryDTO category, IFormFile file)
        {
            var response = "";

            try
            {
                category.AdminId = GetCookie("UserId");
                response = await _category.CategoryCreate(category, file);
                if (response == "Success")
                {
                    return RedirectToAction("ListCategory", "Category");
                }
                else
                {
                    ViewBag.response = "Failed to add category";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public IActionResult DetailCategory(int id)
        {
            var Categories = _category.GetCategoryData(id);
            return View(Categories);
        }
        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            var data = _category.GetCategoryData(id);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> EditCategory(CategoryDTO category, IFormFile file)
        {
            var response = "";

            try
            {
                category.AdminId = GetCookie("UserId");
                response = await _category.EditCategory(category, file);

                if (response == "Success")
                {
                    return RedirectToAction("ListCategory", "Category");
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
        [HttpGet]
        public IActionResult DeleteCategory(int id)
        {
            var Categories = _category.GetCategoryData(id);
            return View(Categories);
        }
        [HttpPost]
        public IActionResult DeleteCategory(CategoryDTO category)
        {
            var response = "";

            try
            {
                category.AdminId = GetCookie("UserId");
                response = _category.DeleteCategory(category);

                if (response == "Success")
                {
                    return RedirectToAction("ListCategory", "Category");
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
