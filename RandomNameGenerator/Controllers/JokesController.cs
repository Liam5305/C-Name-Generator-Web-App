using Microsoft.AspNetCore.Mvc;
using RandomNameGenerator.Models;
using System.Text.Json;

namespace RandomNameGenerator.Controllers
{
    public class JokesController : Controller
    {
        private readonly HttpClient _client;

        public JokesController()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _client.GetAsync("https://icanhazdadjoke.com/");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var joke = JsonSerializer.Deserialize<DadJokes>(content);

                return View(joke);
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }
        }
    }
}
