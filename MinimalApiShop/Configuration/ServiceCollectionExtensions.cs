using MinimalApiShop.Requests.Validators;
using FluentValidation;
using MinimalApiShop.Services.Products;
using MinimalApiShop.Services.Users;
using Microsoft.AspNetCore.Identity;
using MinimalApiShop.Models.Users;
using MinimalApiShop.Services.Orders;

namespace MinimalApiShop.Configuration;

public static class ServiceCollectionExtensions
{
    public static void ConfigureService(this IServiceCollection service)
    {
        service.AddScoped<IProductService, ProductService>()
               .AddValidatorsFromAssemblyContaining<ProductValidator>(ServiceLifetime.Singleton)
               .AddScoped<IJwtGenerator, JwtGenerator>()
               .AddScoped<IUserService, UserService>()
               .AddScoped<IVerifyPasswordService, VerifyPasswordService>()
               .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
               .AddSingleton<IIdentity, UserIdentity>()
               .AddSingleton<IOrderService, OrderService>();
    }
}
