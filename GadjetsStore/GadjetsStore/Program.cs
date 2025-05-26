using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ICategoryService,CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<GadjetsStoreDBContext>(options => options.UseSqlServer("Data Source=SRV2\\PUPILS;Initial Catalog=GadjetsStoreDB;Integrated Security=True;TrustServerCertificate=True"));

builder.Services.AddOpenApi();

var app = builder.Build();
app.UseHttpsRedirection();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "My API V1");
    });
}

app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

app.Run();
