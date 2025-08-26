
using Microsoft.EntityFrameworkCore;
using KZERP.Infrastructure.Data;
using KZERP.Core.Interfaces;
using KZERP.Core.Services;
using KZERP.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// DBContext Configuration
builder.Services.AddDbContext<KZERPDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("KZERPDatabase")));


// Dependency Injection for Services and Repositories
builder.Services.AddScoped<IProductService, ProductsService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();


builder.Services.AddControllers();



// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.Run();