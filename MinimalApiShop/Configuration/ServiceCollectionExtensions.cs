using MinimalApiShop.Requests.Validators;
using MinimalApiShop.Services;
using FluentValidation;

namespace MinimalApiShop.Configuration;

public static class ServiceCollectionExtensions
{
    public static void ConfigureService(this IServiceCollection service)
    {
        service.AddSingleton<IProductService, ProductService>()
               .AddValidatorsFromAssemblyContaining<ProductValidator>(ServiceLifetime.Singleton);
    }
}
