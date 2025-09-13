using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using KZERP.Core.Services;
using KZERP.Infrastructure.Data;
using KZERP.Identity.AppUser;
using KZERP.Identity.IdentityDbContext;
using KZERP.Identity.Services;
using Microsoft.AspNetCore.Identity;
using KZERP.Core.Interfaces.IProductService;
using KZERP.Core.Services.ProductsService;
using KZERP.Infrastructure.Repository.ProductRepository;
using KZERP.Core.Interfaces.IProductRepository;
using KZERP.Core.Interfaces.IWarehousesService;
using KZERP.Core.Services.WarehousesService;
using KZERP.Core.Interfaces.IWarehouseRepository;
using KZERP.Infrastructure.Repository.WarehousesRepository;
using KZERP.Core.Interfaces.IRfidService;
using KZERP.Core.Services.RfidService;
using KZERP.Infrastructure.Repository.RfidRepository;
using KZERP.Core.Interfaces.IRfidRepository;

var builder = WebApplication.CreateBuilder(args);

// ====================
// Database Configuration
// ====================
builder.Services.AddDbContext<KZERPDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("KZERPDatabase"),
        b => b.MigrationsAssembly("KZERP.Infrastructure")
    )
);

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("KZERPDatabase"))
);

// ====================
// Identity Configuration
// ====================
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;

    options.ClaimsIdentity.RoleClaimType = ClaimTypes.Role;
    options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;
    options.ClaimsIdentity.UserNameClaimType = ClaimTypes.Name;
})
.AddEntityFrameworkStores<AppIdentityDbContext>()
.AddDefaultTokenProviders()
.AddRoles<IdentityRole>();

// ====================
// Authentication: JWT + Cookie
// ====================
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "KZERP_Cookie";
    options.DefaultChallengeScheme = "KZERP_Cookie";
})
.AddCookie("KZERP_Cookie", options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.SlidingExpiration = true;
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict;
})
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    var cfg = builder.Configuration;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        RoleClaimType = ClaimTypes.Role,
        NameClaimType = ClaimTypes.Name,
        ValidIssuer = cfg["Jwt:Issuer"],
        ValidAudience = cfg["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(cfg["Jwt:Key"]!))
    };
    options.SaveToken = true;
});


// ====================
// Authorization Policies
// ====================
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserPolicy", policy => policy.RequireRole("User", "Worker"));
});

// ====================
// Custom Services (DI)
// ====================
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();


builder.Services.AddScoped<IProductService, ProductsService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IWarehousesService, WarehousesService>();
builder.Services.AddScoped<IWarehouseRepository, WarehousesRepository>();

builder.Services.AddScoped<IRfidService, RfidService>();
builder.Services.AddScoped<IRfidRepository, RfidRepository>();

// ====================
// MVC & Swagger
// ====================
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Lütfen 'Bearer <token>' formatında JWT token girin",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});

// ====================
// App Pipeline
// ====================
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
