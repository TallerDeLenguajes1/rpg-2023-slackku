using System.Text.Json.Serialization;

namespace UserInterface
{
    public class Root
    {
        [JsonPropertyName("activity")]
        public string? activity { get; set; }

        [JsonPropertyName("type")]
        public string? type { get; set; }

        [JsonPropertyName("participants")]
        public int participants { get; set; }

        [JsonPropertyName("price")]
        public double price { get; set; }

        [JsonPropertyName("link")]
        public string? link { get; set; }

        [JsonPropertyName("key")]
        public string? key { get; set; }

        [JsonPropertyName("accessibility")]
        public int accessibility { get; set; }
    }
}