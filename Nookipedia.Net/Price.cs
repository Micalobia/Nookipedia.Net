using System.Text.Json.Serialization;

namespace Nookipedia.Net
{
    public record Price
    {
        [JsonPropertyName("price")] public int Cost { get; init; }
        [JsonPropertyName("currency")] public Currency Currency { get; init; }
    }
}
