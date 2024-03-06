using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.DTOs_Models.Product_DTOs
{
    public class ProductDTO
    {
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

    }
}
