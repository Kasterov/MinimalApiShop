using MinimalApiShop.Requests.Validators;
using FluentValidation;
using MinimalApiShop.Services.Products;
using MinimalApiShop.Services.Users;

namespace MinimalApiShop.Configuration;

public static class ServiceCollectionExtensions
{
    public static void ConfigureService(this IServiceCollection service)
    {
        service.AddSingleton<IProductService, ProductService>()
               .AddValidatorsFromAssemblyContaining<ProductValidator>(ServiceLifetime.Singleton)
               .AddSingleton<IJwtGenerator, JwtGenerator>()
               .AddSingleton<IUserService, UserService>()
               .AddSingleton<IVerifyPasswordService, VerifyPasswordService>();
    }
}
