using MinimalApiShop.Configuration;
using MinimalApiShop.Endpoints;
using MinimalApiShop.Endpoints.OrderEndpoints;
using MinimalApiShop.Endpoints.ProductEndpoints;
using MinimalApiShop.Endpoints.UserEndpoints;
using MinimalApiShop.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureService();
builder.Services.AddSwaggerGenCustom();

builder.Services.AddDbContextCustom(builder);

builder.Services.AuthenticationCustom(builder);
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseAuthentication()
   .UseAuthorization();

app.GetProductByIdEndpoint();
app.GetProductsByCategoryEndpoint();
app.AddProductEndpoint();
app.AddProductAtributeEndpoint();
app.ChangeAtributeProductEndpoint();
app.ChangeQuantityProductEndpont();
app.DeleteProductEndpoint();

app.AddOrderEndpoint();
app.DeleteFromOrderEndpoint();
app.GetOrdersEndpoint();

app.AddUserEndpoint();
app.LoginUserEndpoint();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();