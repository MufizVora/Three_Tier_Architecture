using AutoMapper;
using Bussiness_Access_Layer.Interface.SneatCategory;
using Data_Access_Layer.Data;
using Data_Access_Layer.Models.SneatCategory;
using Data_Access_Layer.Models.SneatProduct;
using DTOLayer.DTOs_Models.Category_DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Bussiness_Access_Layer.Service.SneatCategory
{
    public class CategoryService : CategoryInterface
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        private readonly FileUpload _file;


        public CategoryService (Context context, IMapper mapper, IHttpContextAccessor httpContextAccessor, FileUpload file)
        {
            _context = context;
            _mapper = mapper;
            _file = file;
        }
        public async Task<string> CategoryCreate(CategoryDTO category, IFormFile file)
        {
            var response = "";

            try
            {
                var filepath = await _file.UploadProductFile(file);
                category.File = filepath;
                var CatEntity = _mapper.Map<Category>(category);
                _context.Categories.Add(CatEntity);
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

        public List<Category> GetCategories(string adminId)
        {
            var data = _context.Categories.Where(a => a.AdminId == adminId).Include(a => a.Products).ToList();
            return data;
        }

        public Category GetCategoryData(int id)
        {
            var data = _context.Categories.FirstOrDefault(a => a.Id == id);
            return data;
        }
        public async Task<string> EditCategory(CategoryDTO category, IFormFile file)
        {
            var response = "";

            try
            {
                var existingCategory = _context.Categories.Find(category.Id);

                if (existingCategory != null)
                {
                    string newFilePath = null;

                    if (file != null)
                    {
                        // Handle image upload logic (save to server, update database)
                        newFilePath = await _file.UploadProductFile(file);
                    }

                    // Update other properties of the category
                    existingCategory.CategoryName = category.CategoryName;

                    // If a new file is uploaded, update the file path; otherwise, keep the existing file
                    existingCategory.File = newFilePath ?? existingCategory.File;

                    _context.Categories.Update(existingCategory);
                    _context.SaveChanges();

                    response = "Success";
                }
                else
                {
                    response = "Category not found";
                }

                return response;
            }
            catch
            {
                response = "Failed";
                return response;
            }
        }

        public string DeleteCategory(CategoryDTO category)
        {
            var response = "";

            try
            {
                var CatEntity = _mapper.Map<Category>(category);
                _context.Categories.Remove(CatEntity);
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
