using Microsoft.EntityFrameworkCore;
using StoreBackend.DomainService;
using StoreBackend.Facade;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configuración del DbContext con SQL Server[cite: 9]
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// Repositories[cite: 10]
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Services[cite: 10]
builder.Services.AddScoped<IProductService, ProductService>();

// Facades[cite: 10]
builder.Services.AddScoped<IProductFacade, ProductFacade>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();