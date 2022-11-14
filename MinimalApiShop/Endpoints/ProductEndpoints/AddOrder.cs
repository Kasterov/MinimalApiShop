using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalApiShop.Data;
using MinimalApiShop.Requests.Products;
using MinimalApiShop.Responses;
using MinimalApiShop.Services.Products;
using System.Security.Claims;

namespace MinimalApiShop.Endpoints.ProductEndpoints;

public static class AddOrder
{
    public static void AddOrderEndpoint(this WebApplication app)
    {
        app.MapPost("/api/Shop/order", [Authorize(Roles = "User")] async
           (InternetShopContext _shopContext,
           [FromServices] IProductService _productService,
           int id) =>
        {
            var product = await _productService.GetProductById(id);
            var user = _shopContext.Users.FirstOrDefaultAsync(x => x.Name == ClaimTypes.Name);

            if (user is null)
            {
                throw new UnauthorizedAccessException();
            }

        }).WithTags("Order");
    }
}
