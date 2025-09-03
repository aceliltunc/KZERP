using System.Text.Json;
using KZERP.Core.Entities.RfidLogs;
using Microsoft.AspNetCore.Mvc;

namespace KZERP.MVC.Controllers
{
    public class RFIDController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RFIDController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient("KZERPApiClient");
            var response = await httpClient.GetAsync("api/RFID");
            response.EnsureSuccessStatusCode();


            var jsonString = await response.Content.ReadAsStringAsync();
            var rfids = JsonSerializer.Deserialize<List<RfidLogs>>(jsonString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return View(rfids);
        }
    }
}