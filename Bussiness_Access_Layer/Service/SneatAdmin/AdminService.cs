using AutoMapper;
using Bussiness_Access_Layer.Interface.SneatAdmin;
using Data_Access_Layer.Data;
using Data_Access_Layer.Models.SneatAdmin;
using DTOLayer.DTOs_Models.Admin_DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Mail;

namespace Bussiness_Access_Layer.Service.SneatAdmin
{
    public class AdminService : AdminInterface
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public AdminService(Context context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
        }

        public string Registration(AdminRDTO admin)
        {
            var response = "";

            try
            {
                admin.UserName = admin.UserName.Trim();
                admin.Email = admin.Email.Trim();
                admin.Password = Encryption.Encrypt(admin.Password);

                var AdminEntity = _mapper.Map<Admin>(admin);
                _context.Admins.Add(AdminEntity);
                _context.SaveChanges();

                response = "Success";
                return response;
            }
            catch 
            {
                response = "Failed";
                return response;
            }
        }
        public Response Login(string Email, string Password)
        {
            var response = new Response();

            try
            {
                Email = Email.Trim();
                Password = Encryption.Encrypt(Password);

                var data = _context.Admins.Where(a => a.Email.ToLower() == Email.ToLower() && a.Password == Password).FirstOrDefault();
                if (data != null)
                {
                    response.Message = "Success";
                    response.Id = data.Id;
                    response.UserName = data.UserName;
                }
                else
                {
                    response.Message = "Invalid Email or Password";
                }
                return response;
            }
            catch
            {
                response.Message = "Failed";
                return response;
            }
        }

        public async Task<string> ForgotPassword(string email)
        {
            var response = "";

            try
            {
                var user = await _context.Admins.SingleOrDefaultAsync(x => x.Email == email);

                if (user != null) 
                {
                    // Generate a unique token (you can use Guid.NewGuid() for simplicity)
                    string resetToken = Guid.NewGuid().ToString();

                    // Store the token in the database with a timestamp for expiration
                    user.ResetToken = resetToken;
                    user.ResetTokenExpiration = DateTime.UtcNow.AddHours(1); // Set expiration time

                    await _context.SaveChangesAsync();

                    // Send an email with the password reset link
                    await SendPasswordResetEmail(email, resetToken);

                    return response = "Password reset link sent successfully";
                }
                else
                {
                    return response = "User not found";
                }
            }
            catch(Exception ex)
            {
                return "Failed to send password reset link";
            }
        }

        public async Task<string> ResetPassword(string token, string newPassword)
        {
            try
            {
                var user = await _context.Admins.SingleOrDefaultAsync(x => x.ResetToken == token);

                if (user != null)
                {
                    if (user.ResetTokenExpiration <= DateTime.UtcNow)
                    {
                        // Log or handle the case where the token is expired
                        return "Token expired";
                    }

                    // Reset the password
                    user.Password = Encryption.Encrypt(newPassword);
                    user.ResetToken = "-";
                    user.ResetTokenExpiration = null;

                    await _context.SaveChangesAsync();

                    return "Password reset successfully";
                }
                else
                {
                    // Log or handle the case where the user is not found or any other unexpected scenarios
                    return "Invalid token or user not found";
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return "Failed to reset password";
            }
        }

        public async Task SendPasswordResetEmail(string toEmail, string resetToken)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("test25943026@gmail.com", "inotbtixswttcxlm"),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("test25943026@gmail.com"),
                    Subject = "Password Reset",
                    Body = $@"
                        <html>
                        <head>
                            <style>
                                body {{
                                    font-family: 'Arial', sans-serif;
                                    background-color: #f4f4f4;
                                }}
                                .container {{
                                    max-width: 600px;
                                    margin: 0 auto;
                                    padding: 20px;
                                    background-color: #ffffff;
                                    border: 1px solid #dddddd;
                                    border-radius: 5px;
                                    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                                }}
                                .reset-link {{
                                    display: block;
                                    padding: 10px;
                                    background-color: blue;
                                    color: #f4f4f4;
                                    text-align: center;
                                    text-decoration: none;
                                    border-radius: 5px;
                                }}

                            </style>
                        </head>
                        <body>
                            <div class='container'>
                                <p>Hello,</p>
                                <p>Click the following link to reset your password:</p>
                                <center><a href='https://localhost:7291/Admin/ResetPassword?token={resetToken}'><button  class='reset-link'>Reset Password</button></a></center>
         
                            </div>
                                <p style='Color:#D3D3D3'>This link will expire within 1 hour</p>
                        </body>
                        </html>",
                           IsBodyHtml = true,
                                    };

                mailMessage.To.Add(toEmail);


                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                // Handle exception (log, etc.)
                throw new Exception("Failed to send email", ex);
            }
        }

        public Admin GetAdminData(int id)
        {
            var data = _context.Admins.FirstOrDefault(x => x.Id == id);
            return data;
        }

        public string AdminProfileEdit(Admin admin)
        {
            var response = "";

            try
            {
                var data = _context.Admins.Where(a=>a.Id==admin.Id).AsNoTracking().FirstOrDefault();
                admin.Password = data.Password;
                admin.ResetToken = data.ResetToken;
                _context.Entry(admin).State = EntityState.Detached;
                _context.Admins.Update(admin);
                _context.SaveChanges();

                response = "Success";
                return response;
            }
            catch
             {
                response = "Failed";
                return response;
            }
        }
    }
}