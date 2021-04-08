using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Nookipedia.Net
{
    public record Recipe
    {
        [JsonConstructor]
        public Recipe(string name, string url, string imageURL, int gameID, Price[] buyPrices, int sellPrice, int recipeRequirement, Availability[] availiabilities, Material[] materials)
        {
            Name = name;
            URL = url;
            ImageURL = imageURL;
            GameID = gameID;
            BuyPrices = buyPrices;
            SellPrice = sellPrice;
            RecipeRequirement = recipeRequirement;
            Availiabilities = availiabilities;
            Materials = materials;
        }

        [Required, JsonPropertyName("name")] public string Name { get; init; }
        [Required, JsonPropertyName("url")] public string URL { get; init; }
        [Required, JsonPropertyName("image_url")] public string ImageURL { get; init; }
        [Required, JsonPropertyName("serial_id")] public int GameID { get; init; }
        [Required, JsonPropertyName("buy")] public Price[] BuyPrices { get; init; }
        [Required, JsonPropertyName("sell")] public int SellPrice { get; private init; }
        [Required, JsonPropertyName("recipes_to_unlock")] public int RecipeRequirement { get; init; }
        [Required, JsonPropertyName("availability")] public Availability[] Availiabilities { get; init; }
        [Required, JsonPropertyName("materials")] public Material[] Materials { get; init; }

        public static string Endpoint(string name) => "nh/recipes/" + name;
        public static string Endpoint() => "nh/recipes";

        public record Availability
        {
            [JsonConstructor]
            public Availability(string from, string note)
            {
                From = from;
                Note = note;
            }

            [Required, JsonPropertyName("from")] public string From { get; init; }
            [Required, JsonPropertyName("note")] public string Note { get; init; }
        }

        public record Material
        {
            [JsonConstructor]
            public Material(string name, int count)
            {
                Name = name;
                Count = count;
            }

            [Required, JsonPropertyName("name")] public string Name { get; init; }
            [Required, JsonPropertyName("count")] public int Count { get; init; }
        }
    }
}
