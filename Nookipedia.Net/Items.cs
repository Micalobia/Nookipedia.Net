using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Nookipedia.Net
{
    public record Photo
    {
        [JsonConstructor]
        public Photo(string name, string url, string category, int sellPrice, bool customizable, int customKits,
                     string customBodyPart, bool interactable,
                     string versionAdded, bool unlocked, float gridWidth, float gridHeight,
                     Availability[] availabilities, Price[] buyPrices, Variation[] variations)
        {
            Name = name;
            URL = url;
            Category = category;
            SellPrice = sellPrice;
            Customizable = customizable;
            CustomKits = customKits;
            CustomBodyPart = customBodyPart;
            Interactable = interactable;
            VersionAdded = versionAdded;
            Unlocked = unlocked;
            GridWidth = gridWidth;
            GridHeight = gridHeight;
            Availabilities = availabilities;
            BuyPrices = buyPrices;
            Variations = variations;
        }

        [Required, JsonPropertyName("name")] public string Name { get; }
        [Required, JsonPropertyName("url")] public string URL { get; }
        [Required, JsonPropertyName("category")] public string Category { get; }
        [Required, JsonPropertyName("sell")] public int SellPrice { get; }
        [Required, JsonPropertyName("customizable")] public bool Customizable { get; }
        [Required, JsonPropertyName("custom_kits")] public int CustomKits { get; }
        [Required, JsonPropertyName("custom_body_part")] public string CustomBodyPart { get; }
        [Required, JsonPropertyName("interactable")] public bool Interactable { get; }
        [Required, JsonPropertyName("version_added")] public string VersionAdded { get; }
        [Required, JsonPropertyName("unlocked")] public bool Unlocked { get; }
        [Required, JsonPropertyName("grid_width")] public float GridWidth { get; }
        [Required, JsonPropertyName("grid_height")] public float GridHeight { get; }
        [Required, JsonPropertyName("availability")] public Availability[] Availabilities { get; }
        [Required, JsonPropertyName("buy")] public Price[] BuyPrices { get; }
        [Required, JsonPropertyName("variations")] public Variation[] Variations { get; }

        internal static string Endpoint() => "nh/photo";
        internal static string Endpoint(string name) => $"nh/photo/{name}";

        public record Availability
        {
            [JsonConstructor]
            public Availability(string from, string note)
            {
                From = from;
                Note = note;
            }

            [Required, JsonPropertyName("from")] public string From { get; }
            [Required, JsonPropertyName("note")] public string Note { get; }
        }

        public record Variation
        {
            [JsonConstructor]
            public Variation(string splash, string imageURL, Color[] colors)
            {
                Splash = splash;
                ImageURL = imageURL;
                Colors = colors;
            }

            [Required, JsonPropertyName("variation")] public string Splash { get; }
            [Required, JsonPropertyName("image_url")] public string ImageURL { get; }
            [Required, JsonPropertyName("colors")] public Color[] Colors { get; }
        }
    }

    public record Clothing
    {
        [JsonConstructor]
        public Clothing(string url, string name, ClothingCategory category, int sellPrice, int variationTotal,
                        bool villEquip, string seasonality, ClothingGender gender, ClothingGender villGender,
                        string versionAdded, bool unlocked, string notes, ClothingLabel[] labels, ClothingStyle[] styles,
                        Availability[] availabilities, Price[] buyPrices, Variation[] variations)
        {
            URL = url;
            Name = name;
            Category = category;
            SellPrice = sellPrice;
            VariationTotal = variationTotal;
            VillEquip = villEquip;
            Seasonality = seasonality;
            Gender = gender;
            VillGender = villGender;
            VersionAdded = versionAdded;
            Unlocked = unlocked;
            Notes = notes;
            Labels = labels;
            Styles = styles;
            Availabilities = availabilities;
            BuyPrices = buyPrices;
            Variations = variations;
        }

        [Required, JsonPropertyName("url")] public string URL { get; }
        [Required, JsonPropertyName("name")] public string Name { get; }
        [Required, JsonPropertyName("category")] public ClothingCategory Category { get; }
        [Required, JsonPropertyName("sell")] public int SellPrice { get; }
        [Required, JsonPropertyName("variation_total")] public int VariationTotal { get; }
        [Required, JsonPropertyName("vill_equip")] public bool VillEquip { get; }
        [Required, JsonPropertyName("seasonality")] public string Seasonality { get; }
        [Required, JsonPropertyName("gender")] public ClothingGender Gender { get; }
        [Required, JsonPropertyName("vill_gender")] public ClothingGender VillGender { get; }
        [Required, JsonPropertyName("version_added")] public string VersionAdded { get; }
        [Required, JsonPropertyName("unlocked")] public bool Unlocked { get; }
        [Required, JsonPropertyName("notes")] public string Notes { get; }
        [Required, JsonPropertyName("label")] public ClothingLabel[] Labels { get; }
        [Required, JsonPropertyName("styles")] public ClothingStyle[] Styles { get; }
        [Required, JsonPropertyName("availability")] public Availability[] Availabilities { get; }
        [Required, JsonPropertyName("buy")] public Price[] BuyPrices { get; }
        [Required, JsonPropertyName("variations")] public Variation[] Variations { get; }

        internal static string Endpoint() => "nh/clothing";
        internal static string Endpoint(string name) => $"nh/clothing/{name}";

        public record Availability
        {
            [JsonConstructor]
            public Availability(string from, string note)
            {
                From = from;
                Note = note;
            }

            [Required, JsonPropertyName("from")] public string From { get; }
            [Required, JsonPropertyName("note")] public string Note { get; }
        }

        public record Variation
        {
            [JsonConstructor]
            public Variation(string splash, string imageURL, Color[] colors)
            {
                Splash = splash;
                ImageURL = imageURL;
                Colors = colors;
            }

            [Required, JsonPropertyName("variation")] public string Splash { get; }
            [Required, JsonPropertyName("image_url")] public string ImageURL { get; }
            [Required, JsonPropertyName("colors")] public Color[] Colors { get; }
        }
    }
}
