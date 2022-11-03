using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace MinimalApiShop.Configuration;

public static class SwaggerConfigurtionExtensions
{
    public static void AddSwaggerGenCustom(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Description = "Authorization for MinimalApiShop",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            options.OperationFilter<SecurityRequirementsOperationFilter>();
        });
    }
}
