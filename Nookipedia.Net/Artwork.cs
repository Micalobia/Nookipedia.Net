using System.Text.Json.Serialization;

namespace Nookipedia.Net
{
    public record Artwork : BaseNookObject, IEndpoint
    {
        [JsonPropertyName("has_fake")] public bool HasFake { get; init; }
        [JsonPropertyName("fake_image_url")] public string FakeImageURL { get; init; }
        [JsonPropertyName("art_name")] public string IRLName { get; init; }
        [JsonPropertyName("author")] public string IRLAuthor { get; init; }
        [JsonPropertyName("year")] public string IRLCreationYear { get; init; }
        [JsonPropertyName("art_style")] public string Artstyle { get; init; }
        [JsonPropertyName("description")] public string Description { get; init; }
        [JsonPropertyName("buy")] public int BuyPrice { get; init; }
        [JsonPropertyName("sell")] public int SellPrice { get; init; }
        [JsonPropertyName("availability")] public string Availability { get; init; }
        [JsonPropertyName("authenticity")] public string DescriptionOfFake { get; init; }
        [JsonPropertyName("width")] public float Width { get; init; }
        [JsonPropertyName("length")] public float Length { get; init; }

        public string Endpoint() => "nh/art";
        public string Endpoint(string sub) => "nh/art/" + sub;
    }
}
