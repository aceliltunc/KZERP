


using System.Text.Json;
using KZERP.Core.Entities.Warehouses;
using Microsoft.AspNetCore.Mvc;

namespace KZERP.MVC.Controllers
{
    public class WarehousesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WarehousesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient("KZERPApiClient");
            var response = await httpClient.GetAsync("api/Warehouses");
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var warehouses = JsonSerializer.Deserialize<List<Warehouse>>(jsonString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return View(warehouses);
        }
    }
}