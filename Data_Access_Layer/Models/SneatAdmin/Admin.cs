using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models.SneatAdmin
{
    [Table("Auth_Admin")]
    public class Admin
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter Your UserName")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Enter Your Email Address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Enter Your Password")]

        public string? Password { get; set; }

        public string? ResetToken { get; set; }

        public DateTime? ResetTokenExpiration { get; set; }
    }
}