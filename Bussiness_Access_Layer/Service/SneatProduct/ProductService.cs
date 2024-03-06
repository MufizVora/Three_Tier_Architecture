using AutoMapper;
using Bussiness_Access_Layer.Interface.SneatProduct;
using Data_Access_Layer.Data;
using Data_Access_Layer.Models.SneatProduct;
using DTOLayer.DTOs_Models.Product_DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Bussiness_Access_Layer.Service.SneatProduct
{
    public class ProductService : ProductInterface
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        private readonly ProductFileUpload _file;
        public ProductService(Context context, IMapper mapper, IHttpContextAccessor httpContextAccessor, ProductFileUpload file) 
        { 
            _context = context;
            _mapper = mapper;
            _file = file;
        }

        public List<Product> GetProducts(string AdminId)
        {
            var data = _context.Products.Where(a => a.AdminId == AdminId).Include(a => a.Category).ToList();
            return data;
        }

        public async Task<string> CreateProduct(ProductDTO product, IFormFile file, IFormFile[] MultiImage)
        {
            var response = "";

            try
            {
                int count = 0;
               foreach(var item in MultiImage)
                {
                    if(count == 0)
                    {
                        var path = await _file.UploadProductFile(item);
                        product.File += path;
                    }
                    else
                    {
                        var filepath = await _file.UploadProductFile(item);
                        product.MultiImage += filepath + ",";
                    }
                    count++;
                }
                
                product.Available = true;
                var ProEntity = _mapper.Map<Product>(product);
                _context.Products.Add(ProEntity);
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

        public Product GetProductData(int id)
        {
            var data = _context.Products.FirstOrDefault(a => a.Id == id);
            return data;
        }

        public async Task<string> EditProduct(ProductDTO product, IFormFile file, IFormFile[] MultiImage)
        {
            var response = "";

            try
            {
                var existingProduct = _context.Products.Find(product.Id);

                if (existingProduct != null)
                {
                    string newFilePath = null;

                    if (file != null)
                    {
                        var newPath = await _file.UploadProductFile(file);
                        existingProduct.File = newPath;
                    }

                    // Check if new multiple images are uploaded
                    if (MultiImage != null && MultiImage.Length > 0)
                    {
                        foreach (var item in MultiImage)
                        {
                            var imagePath = await _file.UploadProductFile(item);
                            existingProduct.MultiImage += imagePath + ",";
                        }
                    }


                    // Update other properties of the product
                    existingProduct.CategoryId = product.CategoryId;
                    existingProduct.ProductName = product.ProductName;
                    existingProduct.Available = product.Available;
                    existingProduct.ProductPrice = product.ProductPrice;
                    existingProduct.IsOffer = product.IsOffer;
                    existingProduct.OfferPrice = product.OfferPrice;
                    existingProduct.ProductDescription = product.ProductDescription;

                    // If a new file is uploaded, update the file path; otherwise, keep the existing file
                    existingProduct.File = newFilePath ?? existingProduct.File;

                    _context.Products.Update(existingProduct);
                    _context.SaveChanges();

                    response = "Success";
                }
                else
                {
                    response = "Product not found";
                }

                return response;
            }
            catch
            {
                response = "Failed";
                return response;
            }
        }


        //public async Task<string> EditProduct(ProductDTO product, IFormFile file, IFormFile[] MultiImage)
        //{
        //    var response = "";

        //    try
        //    {
        //        var existingProduct = _context.Products.Find(product.Id);

        //        if (existingProduct != null)
        //        {
        //            // Check if a new file is uploaded
        //            if (file != null)
        //            {
        //                var newPath = await _file.UploadProductFile(file);
        //                existingProduct.File = newPath;
        //            }

        //            // Check if new multiple images are uploaded
        //            if (MultiImage != null && MultiImage.Length > 0)
        //            {
        //                foreach (var item in MultiImage)
        //                {
        //                    var imagePath = await _file.UploadProductFile(item);
        //                    existingProduct.MultiImage += imagePath + ",";
        //                }
        //            }

        //            // Update other properties
        //            existingProduct.CategoryId = product.CategoryId;
        //            existingProduct.ProductName = product.ProductName;
        //            existingProduct.Available = product.Available;
        //            existingProduct.ProductPrice = product.ProductPrice;
        //            existingProduct.IsOffer = product.IsOffer;
        //            existingProduct.OfferPrice = product.OfferPrice;
        //            existingProduct.ProductDescription = product.ProductDescription;
        //            // Update other properties as needed

        //            _context.SaveChanges();
        //            response = "Success";
        //        }
        //        else
        //        {
        //            response = "Product not found";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response = "Failed: " + ex.Message;
        //    }

        //    return response;
        //}

        //public async Task<string> EditProduct(ProductDTO product, IFormFile file, IFormFile[] MultiImage)
        //{
        //    var response = "";

        //    try
        //    {
        //        if (file != null) // Check if a single image is uploaded
        //        {
        //            var newPath = await _file.UploadProductFile(file);
        //            product.File = newPath;
        //            product.MultiImage = null; // Reset MultiImage since single image is uploaded
        //        }
        //        else if (MultiImage != null && MultiImage.Length > 0) // Check if multiple images are uploaded
        //        {
        //            var multiImagesPaths = new List<string>();
        //            foreach (var item in MultiImage)
        //            {
        //                var imagePath = await _file.UploadProductFile(item);
        //                multiImagesPaths.Add(imagePath);
        //            }
        //            product.MultiImage = string.Join(",", multiImagesPaths);
        //            product.File = null; // Reset File since multiple images are uploaded
        //        }

        //        var existingProduct = _context.Products.Find(product.Id);

        //        if (existingProduct != null)
        //        {
        //            // Update other properties
        //            existingProduct.CategoryId = product.CategoryId;
        //            existingProduct.ProductName = product.ProductName;
        //            existingProduct.Available = product.Available;
        //            existingProduct.ProductPrice = product.ProductPrice;
        //            existingProduct.IsOffer = product.IsOffer;
        //            existingProduct.OfferPrice = product.OfferPrice;
        //            existingProduct.ProductDescription = product.ProductDescription;
        //            // Update other properties as needed

        //            // If single image is uploaded, update File property
        //            if (file != null)
        //            {
        //                existingProduct.File = product.File;
        //            }
        //            // If multiple images are uploaded, update MultiImage property
        //            else if (MultiImage != null && MultiImage.Length > 0)
        //            {
        //                existingProduct.MultiImage = product.MultiImage;
        //            }

        //            _context.SaveChanges();
        //            response = "Success";
        //        }
        //        else
        //        {
        //            response = "Product not found";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response = "Failed: " + ex.Message;
        //    }

        //    return response;
        //}

        public string DeleteProduct(ProductDTO product)
        {
            var response = "";

            try
            {
                var ProEntity = _mapper.Map<Product>(product);
                _context.Products.Remove(ProEntity);
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
