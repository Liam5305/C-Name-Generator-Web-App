using System.Text.Json.Serialization;

namespace RandomNameGenerator.Models
{
    public class InspMessage
    {
        [JsonPropertyName("a")]
        public string? Author { get; set; }
        [JsonPropertyName("q")]
        public string? Message { get; set; }
    }
}
