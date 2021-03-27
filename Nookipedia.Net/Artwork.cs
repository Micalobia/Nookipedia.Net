using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Nookipedia.Net
{
    [JsonObject]
    public class Artwork : BaseNookObject, IEndpoint
    {
        [JsonProperty("has_fake")] public bool HasFake { get; private set; }
        [JsonProperty("fake_image_url")] public string FakeImageURL { get; private set; }
        [JsonProperty("art_name")] public string IRLName { get; private set; }
        [JsonProperty("author")] public string IRLAuthor { get; private set; }
        [JsonProperty("year")] public string IRLCreationYear { get; private set; }
        [JsonProperty("art_style")] public string Artstyle { get; private set; }
        [JsonProperty("description")] public string Description { get; private set; }
        [JsonProperty("buy")] public int BuyPrice { get; private set; }
        [JsonProperty("sell")] public int SellPrice { get; private set; }
        [JsonProperty("availability")] public string Availability { get; private set; }
        [JsonProperty("authenticity")] public string DescriptionOfFake { get; private set; }
        [JsonProperty("width")] public float Width { get; private set; }
        [JsonProperty("length")] public float Length { get; private set; }

        public string Endpoint() => "nh/art";
        public string Endpoint(string sub) => "nh/art/" + sub;
    }
}
