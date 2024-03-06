using Data_Access_Layer.Models.SneatAdmin;
using Data_Access_Layer.Models.SneatCategory;
using Data_Access_Layer.Models.SneatDashboard;
using Data_Access_Layer.Models.SneatProduct;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) 
        { }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Slider> Sliders { get; set; }
    }
}
