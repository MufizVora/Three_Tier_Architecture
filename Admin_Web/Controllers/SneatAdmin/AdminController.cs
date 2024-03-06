using AutoMapper;
using Bussiness_Access_Layer.Interface.SneatAdmin;
using Data_Access_Layer.Migrations;
using Data_Access_Layer.Models.SneatAdmin;
using DTOLayer.DTOs_Models.Admin_DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Admin_Web.Controllers.SneatAdmin
{
    public class AdminController : Controller
    {
        private readonly AdminInterface _adminInterface;
        private readonly IMapper _mapper;
        
        public AdminController(AdminInterface adminInterface, IMapper mapper)
        {
            _adminInterface = adminInterface;
            _mapper = mapper;
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
            return View();
        }
        [HttpGet]
        public IActionResult Admin_Register()
        {
            return View();
        }
        [HttpPost]
        public  IActionResult Admin_Register(AdminRDTO admin)
        {
            var response = "";

            try
            {
                response = _adminInterface.Registration(admin);
                if (response == "Success")
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.response = "Failed";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public IActionResult Admin_Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Admin_Login(Admin adminL)
        {
            var response = new Response();

            try
            {
                response = _adminInterface.Login(adminL.Email, adminL.Password);
                if (response.Message == "Success")
                {
                    SetCookie("UserId", response.Id.ToString());
                    SetCookie("UserName", response.UserName);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.response = response.Message;
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
        public IActionResult Admin_Logout()
        {
            Response.Cookies.Delete("UserId");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var response = await _adminInterface.ForgotPassword(email);
            if (response == "Password reset link sent successfully")
            {
                return RedirectToAction("Password", "Admin");
            }
            ViewBag.Message = response;
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string token)
        {
            // You may want to add additional validation or UI elements here
            ViewBag.Token = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(string ResetToken, string Password)
        {
            var response = await _adminInterface.ResetPassword(ResetToken, Password);


            ViewBag.Message = response;

            if (response == "Password reset successfully")
            {
                // Redirect to login page or any other desired page after completing password reset process
                return RedirectToAction("Admin_Login", "Admin");
            }

            return View();
        }
        [HttpGet]
        public IActionResult AdminProfile()
        {
            var id = GetCookie("UserId");

            var admin = _adminInterface.GetAdminData(Convert.ToInt32(id));
            return View(admin);
        }
        public IActionResult AdminEdit()
        {
            var id = GetCookie("UserId");

            var admin = _adminInterface.GetAdminData(Convert.ToInt32(id));
            var AdminEntity = _mapper.Map<AdminEDTO>(admin);
            return View(AdminEntity);
        }
        [HttpPost]
        public IActionResult AdminEdit(AdminEDTO admin)
        {
            var response = "";

            try
            {

                var id = GetCookie("UserId");
                admin.Id = Convert.ToInt32(id);
                var AdminEntity = _mapper.Map<Admin>(admin);
                response = _adminInterface.AdminProfileEdit(AdminEntity);
                if (response == "Success")
                {
                    // ToString display updated name on browser simply, setcookie
                    SetCookie("UserName", admin.UserName);
                    return RedirectToAction("AdminProfile", "Admin");
                }
                else
                {
                    ViewBag.response = "Failed to update profile, Please try again";
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