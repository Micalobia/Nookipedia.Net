using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Reflection;

namespace Nookipedia.Net
{
    [JsonObject]
    public abstract class Critter : BaseNookObject, IEndpoint
    {
        [JsonProperty("number")] public int CritterpediaNumber { get; private set; }
        [JsonProperty("time")] public string Time { get; private set; }
        [JsonProperty("location")] public string Location { get; private set; }
        [JsonProperty("rarity")] public string Rarity { get; private set; }
        [JsonProperty("total_catch")] public int CatchRequirement { get; private set; }
        [JsonProperty("sell_nook")] public int SellPrice { get; private set; }
        [JsonProperty("tank_width")] public float TankWidth { get; private set; }
        [JsonProperty("tank_height")] public float TankHeight { get; private set; }
        [JsonProperty("catchphrases")] public string[] Catchphrases { get; private set; }
        [JsonProperty("north")] public Region North { get; private set; }
        [JsonProperty("south")] public Region South { get; private set; }

        public abstract string Endpoint();
        public abstract string Endpoint(string name);
    }

    [JsonObject]
    public sealed class Fish : Critter
    {
        [JsonProperty("shadow_size")] public string ShadowSize { get; private set; }
        [JsonProperty("sell_cj")] public string SellPriceCJ { get; private set; }

        public override string Endpoint() => "nh/fish";
        public override string Endpoint(string name) => "nh/fish/" + name;
    }

    [JsonObject]
    public sealed class Bug : Critter
    {
        [JsonProperty("sell_flick")] public string SellPriceFlick { get; private set; }

        public override string Endpoint() => "nh/bugs";
        public override string Endpoint(string name) => "nh/bugs/" + name;
    }

    [JsonObject]
    public sealed class SeaCreature : Critter
    {
        [JsonProperty("shadow_size")] public string ShadowSize { get; private set; }
        [JsonProperty("shadow_movement")] public string ShadowMovement { get; private set; }

        public override string Endpoint() => "nh/sea";
        public override string Endpoint(string name) => "nh/sea/" + name;
    }
}
