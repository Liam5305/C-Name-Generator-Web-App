using RandomNameGenerator.Models;
using System.Text.Json;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

namespace RandomNameGenerator.Controllers
{
    public class NameController : Controller
    {
        private const string API_URL = "https://randomuser.me/api/";
        private readonly ClanNameGenerator _generator = new ClanNameGenerator();

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GenerateName()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
                    var response = await client.GetAsync($"{API_URL}?inc=name&noinfo");
                    var content = await response.Content.ReadAsStringAsync();

                    Console.WriteLine($"API Response: {content}");

                    if (!response.IsSuccessStatusCode)
                    {
                        return StatusCode((int)response.StatusCode, "API request failed");
                    }

                    using (JsonDocument document = JsonDocument.Parse(content))
                    {
                        var root = document.RootElement;
                        var firstResult = root.GetProperty("results")[0];
                        var nameProperty = firstResult.GetProperty("name");
                        var name = new Names
                        {
                            FirstName = nameProperty.GetProperty("first").GetString(),
                            LastName = nameProperty.GetProperty("last").GetString()
                        };
                        return Json(name);
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"HTTP Request Error: {ex}");
                    return StatusCode(500, "Failed to contact name service");
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"JSON Parse Error: {ex}");
                    return StatusCode(500, "Failed to parse name data");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"General Error: {ex}");
                    return StatusCode(500, "An unexpected error occurred");
                }
            }
        }

        public IActionResult GenerateClanName()
        {
            return Json(new { clanName = _generator.GenerateName() });
        }
    }

    public class ClanNameGenerator
    {
        private readonly List<string> _prefixes = new List<string>
        {
            "Dark", "Storm", "Blood", "Iron", "Shadow", "Fire", "Ice",
            "Thunder", "Night", "Dawn", "Dusk", "Wolf", "Dragon", "Star"
        };

        private readonly List<string> _suffixes = new List<string>
        {
            "blade", "heart", "claw", "fang", "wing", "fury", "guard",
            "shield", "soul", "spirit", "hunter", "walker", "bringer", "seeker"
        };

        private readonly Random _random = new Random();

        public string GenerateName()
        {
            var prefix = _prefixes[_random.Next(_prefixes.Count)];
            var suffix = _suffixes[_random.Next(_suffixes.Count)];
            return $"{prefix}{suffix}";
        }
    }
}