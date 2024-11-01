﻿using Data_Access_Layer.Data;
using Microsoft.AspNetCore.Mvc;

namespace Admin_Web.Areas.User_Web.Controllers.Product
{
    [Area("User_Web")]

    public class ProductController(Context context) : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            var product = context.Products.ToList();
            ViewBag.Product = product;
            return View();
        }
    }
}
