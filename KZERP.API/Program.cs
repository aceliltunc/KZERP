
using Microsoft.EntityFrameworkCore;
using KZERP.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// DBContext Configuration
builder.Services.AddDbContext<KZERPDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("KZERPDatabase")));

builder.Services.AddControllers();



// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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