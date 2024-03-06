using Data_Access_Layer.Models.SneatProduct;
using DTOLayer.DTOs_Models.Product_DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Access_Layer.Interface.SneatProduct
{
    public interface ProductInterface
    {
        Task<string> CreateProduct(ProductDTO product, IFormFile file, IFormFile[] MultiImage);

        public List<Product> GetProducts(string AdminId);

        public Product GetProductData(int id);

        Task<string> EditProduct(ProductDTO product, IFormFile file, IFormFile[] MultiImage);

        public string DeleteProduct(ProductDTO product);

    }
}
