using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.DTOs_Models.Admin_DTOs
{
    public class AdminDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Email Address Is Required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        public string? Password { get; set; }
    }
}
