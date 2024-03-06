using Data_Access_Layer.Models.SneatCategory;
using DTOLayer.DTOs_Models.Category_DTOs;
using Microsoft.AspNetCore.Http;

namespace Bussiness_Access_Layer.Interface.SneatCategory
{
    public interface CategoryInterface
    {
         Task<string> CategoryCreate(CategoryDTO category, IFormFile file);

        public List<Category> GetCategories(string adminId);
        
        public Category GetCategoryData(int id);

        Task<string> EditCategory(CategoryDTO category, IFormFile file);

        public string DeleteCategory(CategoryDTO category);
    }
}
