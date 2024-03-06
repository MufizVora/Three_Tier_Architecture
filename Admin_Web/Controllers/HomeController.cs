using Admin_Web.Models;
using Bussiness_Access_Layer.Interface.SneatAdmin;
using Bussiness_Access_Layer.Interface.SneatSlider;
using DTOLayer.DTOs_Models.Slider_DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Admin_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AdminInterface _adminInterface;
        private readonly SliderInterface _sliderInterface;

        public HomeController(ILogger<HomeController> logger, AdminInterface adminInterface, SliderInterface sliderInterface)
        {
            _logger = logger;
            _adminInterface = adminInterface;
            _sliderInterface = sliderInterface;
        }
        public string GetCookie(string Id)
        {
            var x = Request.Cookies[Id];
            return x;
        }

        public void SetCookie(string key, string value, double? days = 365)
        {
            var option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays((double)days);
            option.Secure = true;
            Response.Cookies.Append(key, value, option);
        }

        public IActionResult Index()
        {
            if (GetCookie("UserId") == null)
                return Redirect("Admin/Admin_Login");

            var adminid = GetCookie("UserId");
            var sliders = _sliderInterface.GetSliders(adminid);
            return View(sliders);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public IActionResult CreateSlider()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateSlider(SliderDTO slider, IFormFile file, IFormFile[] MultiImage)
        {
            var response = "";

            try
            {
                slider.AdminId = GetCookie("UserId");
                response = await _sliderInterface.CreateSlider(slider, file, MultiImage);
                if (response == "Success")
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.response = "Failed to add slider";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

    }
}
