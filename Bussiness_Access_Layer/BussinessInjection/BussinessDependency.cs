using Bussiness_Access_Layer.Interface.SneatAdmin;
using Bussiness_Access_Layer.Interface.SneatCategory;
using Bussiness_Access_Layer.Interface.SneatProduct;
using Bussiness_Access_Layer.Interface.SneatSlider;
using Bussiness_Access_Layer.Mappers;
using Bussiness_Access_Layer.Service;
using Bussiness_Access_Layer.Service.SneatAdmin;
using Bussiness_Access_Layer.Service.SneatCategory;
using Bussiness_Access_Layer.Service.SneatProduct;
using Bussiness_Access_Layer.Service.SneatSlider;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bussiness_Access_Layer.BussinessInjection
{
    public static class BussinessDependency
    {
        public static void BussinessBLL(this IServiceCollection builder, IConfiguration configuration)
        {
            builder.AddScoped<AdminInterface, AdminService>();
            builder.AddAutoMapper(typeof(AutoMappersProfile));
            builder.AddScoped<FileUpload>();
            builder.AddHttpContextAccessor();
            builder.AddScoped<CategoryInterface, CategoryService>();
            builder.AddScoped<ProductInterface, ProductService>();
            builder.AddScoped<ProductFileUpload>();
            builder.AddHttpContextAccessor();
            builder.AddScoped<SliderInterface, SliderService>();
            builder.AddScoped<SliderFileUpload>();
        }
    }
}
