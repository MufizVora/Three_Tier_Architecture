using Data_Access_Layer.Models.SneatCategory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models.SneatProduct
{
    [Table("Auth_Product")]
    public class Product
    {
        [Key]

        public int Id { get; set; }

        public int CategoryId { get; set; }

        public string? AdminId { get; set; }

        public bool Available { get; set; }

        [Required(ErrorMessage = "Enter Your Product Name")]
        public string? ProductName { get; set; }

        public int ProductPrice { get; set; }

        public bool IsOffer { get; set; }

        public int OfferPrice { get; set; }

        public string? ProductDescription { get; set; }

        public string? File { get; set; }

        public string? MultiImage { get; set; }

        public Category Category { get; set; }

    }
}
