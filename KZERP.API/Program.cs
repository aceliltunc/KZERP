
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using KZERP.Infrastructure.Data;
using KZERP.Identity.AppUser;
using KZERP.Core.Interfaces.IProductService;
using KZERP.Infrastructure.Repository.ProductRepository;

using KZERP.Core.Interfaces.IWarehousesService;
using KZERP.Core.Services.ProductsService;

using KZERP.Core.Services.WarehousesService;
using KZERP.Infrastructure.Repository.WarehousesRepository;
using KZERP.Core.Interfaces.IProductRepository;
using KZERP.Core.Interfaces.IWarehouseRepository;
using KZERP.Core.Interfaces.IRfidService;
using KZERP.Core.Services.RfidService;
using KZERP.Core.Interfaces.IRfidRepository;
using KZERP.Infrastructure.Repository.RfidRepository;
using KZERP.Identity.IdentityDbContext;

var builder = WebApplication.CreateBuilder(args);

// DBContext Configuration
builder.Services.AddDbContext<KZERPDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("KZERPDatabase"),
    b => b.MigrationsAssembly("KZERP.Infrastructure")
    )
);

// IdentityDbContext
builder.Services.AddDbContext<IdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("KZERPDatabase"))

);
// Identity Configuration
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
}).AddEntityFrameworkStores<IdentityDbContext>()
  .AddDefaultTokenProviders()
  .AddRoles<IdentityRole>();



// // JWT Authentication Configuration
// builder.Services.AddAuthentication(options =>
// {

//     options.DefaultAuthenticateScheme = "JwtBearer";
//     options.DefaultChallengeScheme = "JwtBearer";   
// })
// .AddJwtBearer("JwtBearer", options =>
// { 
//     options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//     {
//         ValidateIssuer = true,
//         ValidateAudience = true,
//         ValidateLifetime = true,
//         ValidateIssuerSigningKey = true,
//         ValidIssuer = builder.Configuration["Jwt:Issuer"],
//         ValidAudience = builder.Configuration["Jwt:Audience"],
//         IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
//             System.Text.Encoding.UTF8.GetBytes(
//                 builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key is missing in configuration.")
//             )
//         ),
//         ClockSkew = TimeSpan.Zero
//     };
// });




// Services and Repositories
builder.Services.AddScoped<IProductService, ProductsService>();
builder.Services.AddScoped<IProductRepository,ProductRepository>();

// 
builder.Services.AddScoped<IWarehousesService, WarehousesService>();
builder.Services.AddScoped<IWarehouseRepository, WarehousesRepository>();
//
builder.Services.AddScoped<IRfidService, RfidService>();
builder.Services.AddScoped<IRfidRepository, RfidRepository>();


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

// using (var scope = app.Services.CreateScope())
// {
//     var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//     var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

//      if (!await roleManager.RoleExistsAsync("Admin"))
//      {
//          await roleManager.CreateAsync(new IdentityRole("Admin"));
//      }

//      var adminUser = await userManager.FindByEmailAsync("admin@kzerp.com");
//      if (adminUser == null)
//      {
//          adminUser = new ApplicationUser
//          {
//              UserName = "admin",
//              Email = "admin@kzerp.com",
//              FullName = "Admin User",
//              JobTitle = "System Administrator",
//              Department = "IT",
//              IsActive = true
//          };
//          await userManager.CreateAsync(adminUser, "kznimda$74");
//      }

//      if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
//      {
//          await userManager.AddToRoleAsync(adminUser, "Admin");
//      }

//  }


app.Run();