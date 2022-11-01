﻿using MinimalApiShop.Services;

namespace MinimalApiShop.Configuration;

public static class ServiceCollectionExtensions
{
    public static void ConfigureService(this IServiceCollection service)
    {
        service.AddSingleton<IProductService, ProductService>();
    }
}
