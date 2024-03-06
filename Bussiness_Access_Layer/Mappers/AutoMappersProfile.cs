using AutoMapper;
using Data_Access_Layer.Models.SneatAdmin;
using Data_Access_Layer.Models.SneatCategory;
using Data_Access_Layer.Models.SneatDashboard;
using Data_Access_Layer.Models.SneatProduct;
using DTOLayer.DTOs_Models.Admin_DTOs;
using DTOLayer.DTOs_Models.Category_DTOs;
using DTOLayer.DTOs_Models.Product_DTOs;
using DTOLayer.DTOs_Models.Slider_DTOs;


namespace Bussiness_Access_Layer.Mappers
{
    public class AutoMappersProfile : Profile
    {
        public AutoMappersProfile()
        {
            CreateMap<Admin, AdminRDTO>().ReverseMap();
            CreateMap<Admin, AdminEDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Slider, SliderDTO>().ReverseMap();
        }
    }
}