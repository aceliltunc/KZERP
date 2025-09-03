
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using KZERP.Core.Services;
using KZERP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using KZERP.Identity.AppUser;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// DBContext Configuration
builder.Services.AddDbContext<KZERPDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("KZERPDatabase"),
    b => b.MigrationsAssembly("KZERP.Infrastructure")
    )
);

// Identity ekle
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<KZERPDbContext>()
    .AddDefaultTokenProviders();


// Add services to the container.
builder.Services.AddControllersWithViews();

////////////////////////////////
// HttpClient yapılandırması
builder.Services.AddHttpClient("KZERPApiClient", client =>
{
    // API'nin adresini burada belirt
    client.BaseAddress = new Uri("http://localhost:5276/"); 
    client.DefaultRequestHeaders.Add("Accept", "application/json");
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    // Bu kısım, self-signed sertifika hatalarını yok sayar
    var handler = new HttpClientHandler();
    handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
    return handler;
});

////


///     Authentication yapılandırması
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.LogoutPath = "/Account/Logout";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.SlidingExpiration = true;
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;
    });


// Authorization politikaları
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy =>
        policy.RequireRole("Admin")); // Rol claim’i cookie’ye ekleme
});


//builder.Services.AddScoped<IUserClaimsService, UserClaimsService>();

// 
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
