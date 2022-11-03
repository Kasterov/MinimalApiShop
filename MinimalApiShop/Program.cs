using MinimalApiShop.Configuration;
using MinimalApiShop.Endpoints;
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

app.UseAuthentication();
app.UseAuthorization();

app.UseUserEndpoints();
app.UseProductShopEndpoints();


app.UseSwagger();
app.UseSwaggerUI();

app.Run();