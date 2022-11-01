using Microsoft.EntityFrameworkCore;
using MinimalApiShop.Configuration;
using MinimalApiShop.Data;
using MinimalApiShop.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureService();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<InternetShopContext>(opt => opt
.UseSqlServer(builder.Configuration
.GetConnectionString("DefualtConnection")),ServiceLifetime.Singleton);

var app = builder.Build();

app.UseSwagger();

app.UseProductShopEndpoints();

app.UseSwaggerUI();

app.Run();