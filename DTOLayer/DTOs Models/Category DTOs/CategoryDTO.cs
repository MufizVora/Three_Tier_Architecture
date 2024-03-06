using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.DTOs_Models.Category_DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        public string? AdminId { get; set; }

        [Required(ErrorMessage = "Enter Your Category Name")]
        public string? CategoryName { get; set; }

        public string? File { get; set; }
    }
}
