using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Nookipedia.Net
{
    public abstract record Critter
    {
        protected Critter(string name, string uRL, string imageURL, int critterpediaNumber, string time, string location,
                          string rarity, int catchRequirement, int sellPrice, float tankWidth, float tankHeight,
                          string[] catchphrases, Region north, Region south)
        {
            Name = name;
            URL = uRL;
            ImageURL = imageURL;
            CritterpediaNumber = critterpediaNumber;
            Time = time;
            Location = location;
            Rarity = rarity;
            CatchRequirement = catchRequirement;
            SellPrice = sellPrice;
            TankWidth = tankWidth;
            TankHeight = tankHeight;
            Catchphrases = catchphrases;
            North = north;
            South = south;
        }

        [Required, JsonPropertyName("name")] public string Name { get; }
        [Required, JsonPropertyName("url")] public string URL { get; }
        [Required, JsonPropertyName("image_url")] public string ImageURL { get; }
        [Required, JsonPropertyName("number")] public int CritterpediaNumber { get; }
        [Required, JsonPropertyName("time")] public string Time { get; }
        [Required, JsonPropertyName("location")] public string Location { get; }
        [Required, JsonPropertyName("rarity")] public string Rarity { get; }
        [Required, JsonPropertyName("total_catch")] public int CatchRequirement { get; }
        [Required, JsonPropertyName("sell_nook")] public int SellPrice { get; }
        [Required, JsonPropertyName("tank_width")] public float TankWidth { get; }
        [Required, JsonPropertyName("tank_height")] public float TankHeight { get; }
        [Required, JsonPropertyName("catchphrases")] public string[] Catchphrases { get; }
        [Required, JsonPropertyName("north")] public Region North { get; }
        [Required, JsonPropertyName("south")] public Region South { get; }
    }

    public sealed record Fish : Critter
    {
        [JsonConstructor]
        public Fish(
            string name, string uRL, string imageURL, int critterpediaNumber, string time, string location,
            string rarity, int catchRequirement, int sellPrice, float tankWidth, float tankHeight, string[] catchphrases,
            Region north, Region south, string shadowSize, int sellPriceCJ) : base(
                name, uRL, imageURL, critterpediaNumber, time, location, rarity, catchRequirement, sellPrice, tankWidth,
                tankHeight, catchphrases, north, south)
        {
            ShadowSize = shadowSize;
            SellPriceCJ = sellPriceCJ;
        }

        [Required, JsonPropertyName("shadow_size")] public string ShadowSize { get; }
        [Required, JsonPropertyName("sell_cj")] public int SellPriceCJ { get; }

        public static string Endpoint() => "nh/fish";
        public static string Endpoint(string name) => "nh/fish/" + name;
    }

    public sealed record Bug : Critter
    {
        [JsonConstructor]
        public Bug(
            string name, string uRL, string imageURL, int critterpediaNumber, string time, string location,
            string rarity, int catchRequirement, int sellPrice, float tankWidth, float tankHeight, string[] catchphrases,
            Region north, Region south, int sellPriceFlick) : base(
                name, uRL, imageURL, critterpediaNumber, time, location, rarity, catchRequirement, sellPrice, tankWidth,
                tankHeight, catchphrases, north, south) => SellPriceFlick = sellPriceFlick;

        [Required, JsonPropertyName("sell_flick")] public int SellPriceFlick { get;  }

        public static string Endpoint() => "nh/bugs";
        public static string Endpoint(string name) => "nh/bugs/" + name;
    }

    public sealed record SeaCreature : Critter
    {
        [JsonConstructor]
        public SeaCreature(
            string name, string uRL, string imageURL, int critterpediaNumber, string time, string location,
            string rarity, int catchRequirement, int sellPrice, float tankWidth, float tankHeight, string[] catchphrases,
            Region north, Region south, string shadowSize, string shadowMovement) : base(
                name, uRL, imageURL, critterpediaNumber, time, location, rarity, catchRequirement, sellPrice, tankWidth,
                tankHeight, catchphrases, north, south)
        {
            ShadowSize = shadowSize;
            ShadowMovement = shadowMovement;
        }

        [Required, JsonPropertyName("shadow_size")] public string ShadowSize { get;  }
        [Required, JsonPropertyName("shadow_movement")] public string ShadowMovement { get;  }

        public static string Endpoint() => "nh/sea";
        public static string Endpoint(string name) => "nh/sea/" + name;
    }
}
