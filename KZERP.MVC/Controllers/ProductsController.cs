using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using KZERP.Core.Entities;

namespace KZERP.MVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            // İsimlendirilmiş istemciyi kullan
            var httpClient = _httpClientFactory.CreateClient("MyApi");
            var response = await httpClient.GetAsync("api/products");
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var products = JsonSerializer.Deserialize<List<Product>>(jsonString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return View(products);
        }
    }
}