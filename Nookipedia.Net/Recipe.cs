using System.Text.Json.Serialization;

namespace Nookipedia.Net
{
    public record Recipe : BaseNookObject, IEndpoint
    {
        [JsonPropertyName("serial_id")] public int GameID { get; init; }
        [JsonPropertyName("buy")] public Price[]? BuyPrices { get; init; }
        [JsonPropertyName("sell")] public int SellPrice { get; private init; }
        [JsonPropertyName("recipes_to_unlock")] public int RecipeRequirement { get; init; }
        [JsonPropertyName("availability")] public Availability[]? Availiabilities { get; init; }
        [JsonPropertyName("materials")] public Material[]? Materials { get; init; }

        public string Endpoint(string sub) => "nh/recipes/" + sub;
        public string Endpoint() => "nh/recipes";

        public record Price
        {
            [JsonPropertyName("price")] public int Cost { get; init; }
            [JsonPropertyName("currency")] public Currency Currency { get; init; }
        }

        public record Availability
        {
            [JsonPropertyName("from")] public string? From { get; init; }
            [JsonPropertyName("note")] public string? Note { get; init; }
        }

        public record Material
        {
            [JsonPropertyName("name")] public string? Name { get; init; }
            [JsonPropertyName("count")] public int Count { get; init; }
        }
    }
}
