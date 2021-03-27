using Newtonsoft.Json;

namespace Nookipedia.Net
{
    [JsonObject]
    public class Recipe : BaseNookObject, IEndpoint
    {
        [JsonProperty("serial_id")] public int GameID { get; private set; }
        [JsonProperty("buy")] public Price[] BuyPrices { get; private set; }
        [JsonProperty("sell")] public int SellPrice { get; private set; }
        [JsonProperty("recipes_to_unlock")] public int RecipeRequirement { get; private set; }
        [JsonProperty("availability")] public Availability[] Availiabilities { get; private set; }
        [JsonProperty("materials")] public Material[] Materials { get; private set; }

        public string Endpoint(string sub) => "nh/recipes/" + sub;
        public string Endpoint() => "nh/recipes";

        [JsonObject]
        public class Price
        {
            [JsonProperty("price")] public int Cost { get; private set; }
            [JsonProperty("currency")] public Currency Currency { get; private set; }
        }

        [JsonObject]
        public class Availability
        {
            [JsonProperty("from")] public string From { get; private set; }
            [JsonProperty("note")] public string Note { get; private set; }
        }

        [JsonObject]
        public class Material
        {
            [JsonProperty("name")] public string Name { get; private set; }
            [JsonProperty("count")] public int Count { get; private set; }
        }
    }
}
