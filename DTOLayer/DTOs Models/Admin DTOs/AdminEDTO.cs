using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.DTOs_Models.Admin_DTOs
{
    public class AdminEDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter Your UserName")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Enter Your Email Address")]
        public string? Email { get; set; }

    }
}
