using Data_Access_Layer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data_Access_Layer.DependencyInjection
{
    public static class DatabaseDependency
    {
        public static void DataBaseDLL(this IServiceCollection builder, IConfiguration configuration)
        {
            builder.AddDbContext<Context>(Options => Options.UseNpgsql(configuration.GetConnectionString("db")));
        }
    }
}