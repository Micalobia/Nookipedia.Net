using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Nookipedia.Net
{
    public record Artwork
    {
        [JsonConstructor]
        public Artwork(
            string name, string uRL, string imageURL, bool hasFake, string fakeImageURL, string iRLName,
            string iRLAuthor, string iRLCreationYear, string artstyle, string description, int buyPrice, int sellPrice,
            string availability, string descriptionOfFake, float width, float length)
        {
            Name = name;
            URL = uRL;
            ImageURL = imageURL;
            HasFake = hasFake;
            FakeImageURL = fakeImageURL;
            IRLName = iRLName;
            IRLAuthor = iRLAuthor;
            IRLCreationYear = iRLCreationYear;
            Artstyle = artstyle;
            Description = description;
            BuyPrice = buyPrice;
            SellPrice = sellPrice;
            Availability = availability;
            DescriptionOfFake = descriptionOfFake;
            Width = width;
            Length = length;
        }

        [Required, JsonPropertyName("name")] public string Name { get; }
        [Required, JsonPropertyName("url")] public string URL { get; }
        [Required, JsonPropertyName("image_url")] public string ImageURL { get; }
        [Required, JsonPropertyName("has_fake")] public bool HasFake { get; }
        [Required, JsonPropertyName("fake_image_url")] public string FakeImageURL { get; }
        [Required, JsonPropertyName("art_name")] public string IRLName { get; }
        [Required, JsonPropertyName("author")] public string IRLAuthor { get; }
        [Required, JsonPropertyName("year")] public string IRLCreationYear { get; }
        [Required, JsonPropertyName("art_style")] public string Artstyle { get; }
        [Required, JsonPropertyName("description")] public string Description { get; }
        [Required, JsonPropertyName("buy")] public int BuyPrice { get; }
        [Required, JsonPropertyName("sell")] public int SellPrice { get; }
        [Required, JsonPropertyName("availability")] public string Availability { get; }
        [Required, JsonPropertyName("authenticity")] public string DescriptionOfFake { get; }
        [Required, JsonPropertyName("width")] public float Width { get; }
        [Required, JsonPropertyName("length")] public float Length { get; }

        public static string Endpoint() => "nh/art";
        public static string Endpoint(string name) => "nh/art/" + name;
    }
}
