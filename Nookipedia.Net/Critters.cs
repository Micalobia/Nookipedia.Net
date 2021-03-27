using System.Text.Json.Serialization;

namespace Nookipedia.Net
{
    public abstract record Critter : BaseNookObject, IEndpoint
    {
        [JsonPropertyName("number")] public int CritterpediaNumber { get; init; }
        [JsonPropertyName("time")] public string Time { get; init; }
        [JsonPropertyName("location")] public string Location { get; init; }
        [JsonPropertyName("rarity")] public string Rarity { get; init; }
        [JsonPropertyName("total_catch")] public int CatchRequirement { get; init; }
        [JsonPropertyName("sell_nook")] public int SellPrice { get; init; }
        [JsonPropertyName("tank_width")] public float TankWidth { get; init; }
        [JsonPropertyName("tank_height")] public float TankHeight { get; init; }
        [JsonPropertyName("catchphrases")] public string[] Catchphrases { get; init; }
        [JsonPropertyName("north")] public Region North { get; init; }
        [JsonPropertyName("south")] public Region South { get; init; }

        public abstract string Endpoint();
        public abstract string Endpoint(string name);
    }

    public sealed record Fish : Critter
    {
        [JsonPropertyName("shadow_size")] public string ShadowSize { get; init; }
        [JsonPropertyName("sell_cj")] public string SellPriceCJ { get; init; }

        public override string Endpoint() => "nh/fish";
        public override string Endpoint(string name) => "nh/fish/" + name;
    }

    public sealed record Bug : Critter
    {
        [JsonPropertyName("sell_flick")] public string SellPriceFlick { get; init; }

        public override string Endpoint() => "nh/bugs";
        public override string Endpoint(string name) => "nh/bugs/" + name;
    }

    public sealed record SeaCreature : Critter
    {
        [JsonPropertyName("shadow_size")] public string ShadowSize { get; init; }
        [JsonPropertyName("shadow_movement")] public string ShadowMovement { get; init; }

        public override string Endpoint() => "nh/sea";
        public override string Endpoint(string name) => "nh/sea/" + name;
    }
}
