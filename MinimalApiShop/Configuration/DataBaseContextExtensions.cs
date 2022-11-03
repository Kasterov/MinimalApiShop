using Microsoft.EntityFrameworkCore;
using MinimalApiShop.Data;

namespace MinimalApiShop.Configuration;

public static class DataBaseContextExtensions
{
    public static void AddDbContextCustom(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddDbContext<InternetShopContext>(opt => opt
            .UseSqlServer(builder.Configuration
            .GetConnectionString("DefualtConnection")), ServiceLifetime.Singleton);
    }
}
