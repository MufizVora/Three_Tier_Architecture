using Data_Access_Layer.Models.SneatProduct;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models.SneatCategory
{
    [Table("Auth_Category")]
    public class Category
    {
        [Key]

        public int Id { get; set; }

        public string? AdminId { get; set; }

        [Required(ErrorMessage ="Enter Your Category Name")]
        public string? CategoryName { get; set; }

        public string? File {  get; set; }

        public List<Product> Products { get; set; }

    }
}
