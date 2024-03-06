using Data_Access_Layer.Models.SneatAdmin;
using DTOLayer.DTOs_Models.Admin_DTOs;
using Microsoft.AspNetCore.Http;
namespace Bussiness_Access_Layer.Interface.SneatAdmin
{
    public interface AdminInterface
    {
        public string Registration(AdminRDTO admin);

        public Response Login(string Email,  string Password);

        public Task SendPasswordResetEmail(string toEmail, string resetToken);

        public Task<string> ForgotPassword(string email);

        public Task<string> ResetPassword(string token, string newPassword);

        public Admin GetAdminData(int id);

        public string AdminProfileEdit(Admin admin);

    }
}