using Microsoft.EntityFrameworkCore;
using MinimalApiShop.Configuration;
using MinimalApiShop.Data;
using MinimalApiShop.Endpoints;
using MinimalApiShop.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureService();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<InternetShopContext>(opt => opt
.UseSqlServer(builder.Configuration
.GetConnectionString("DefualtConnection")),ServiceLifetime.Singleton);

var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseProductShopEndpoints();
app.UseUserEndpoints();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();