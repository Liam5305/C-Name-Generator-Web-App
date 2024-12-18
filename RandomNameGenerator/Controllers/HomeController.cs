using Microsoft.AspNetCore.Mvc;
using RandomNameGenerator.Models;
using System.Diagnostics;
using System.Text.Json;

namespace RandomNameGenerator.Controllers
{
    // In HomeController.cs
    public class HomeController : Controller
    {
        private readonly HttpClient _client;

        public HomeController()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _client.GetAsync("https://zenquotes.io/api/quotes/");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var quotes = JsonSerializer.Deserialize<InspMessage[]>(content);
                var inspo = quotes?[0];
                return View(inspo);
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }
        }
    }
}
