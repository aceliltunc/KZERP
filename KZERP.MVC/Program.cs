var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

////////////////////////////////
// HttpClient yapılandırması
builder.Services.AddHttpClient("MyApi", client =>
{
    // API'nin adresini burada belirt
    client.BaseAddress = new Uri("http://localhost:5276/"); 
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    // Bu kısım, self-signed sertifika hatalarını yok sayar
    var handler = new HttpClientHandler();
    handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
    return handler;
});

////
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
