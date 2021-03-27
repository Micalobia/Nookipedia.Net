using Newtonsoft.Json;

namespace Nookipedia.Net
{
    [JsonObject]
    public class Villager : BaseNookObject, IListEndpoint
    {
        [JsonProperty("alt_name")] public string AlternativeName { get; private set; }
        [JsonProperty("title_color")] public string TitleColor { get; private set; }
        [JsonProperty("text_color")] public string TextColor { get; private set; }
        [JsonProperty("id")] public string GameID { get; private set; }
        [JsonProperty("species")] public Species Species { get; private set; }
        [JsonProperty("personality")] public Personality Personality { get; private set; }
        [JsonProperty("gender")] public Gender Gender { get; private set; }
        [JsonProperty("birthday")] public string Birthday { get; private set; }
        [JsonProperty("sign")] public StarSign StarSign { get; private set; }
        [JsonProperty("quote")] public string Quote { get; private set; }
        [JsonProperty("phrase")] public string Phrase { get; private set; }
        [JsonProperty("prev_phrases")] public string[] PastPhrases { get; private set; }
        [JsonProperty("clothing")] public string Clothing { get; private set; }
        [JsonProperty("islander")] public bool Islander { get; private set; }
        [JsonProperty("debut")] public Game Debut { get; private set; }
        [JsonProperty("appearances")] public Game[] Appearances { get; private set; }
        [JsonProperty("nh_details")] public NewHorizonsInfo NHDetails { get; private set; }

        public string Endpoint() => "villagers";

        [JsonObject]
        public class NewHorizonsInfo
        {
            [JsonProperty("image_url")] public string ImageURL { get; private set; }
            [JsonProperty("photo_url")] public string PhotoURL { get; private set; }
            [JsonProperty("icon_url")] public string IconURL { get; private set; }
            [JsonProperty("quote")] public string Quote { get; private set; }
            [JsonProperty("sub-personality")] public SubPersonality SubPersonality { get; private set; }
            [JsonProperty("catchphrase")] public string Catchphrase { get; private set; }
            [JsonProperty("clothing")] public string Clothing { get; private set; }
            [JsonProperty("clothing_variation")] public string ClothingVariation { get; private set; }
            [JsonProperty("fav_styles")] public string[] FavoriteClothingStyles { get; private set; }
            [JsonProperty("fav_colors")] public string[] FavoriteColors { get; private set; }
            [JsonProperty("hobby")] public Hobby Hobby { get; private set; }
            [JsonProperty("house_interior_url")] public string HouseInteriorURL { get; private set; }
            [JsonProperty("house_exterior_url")] public string HouseExteriorURL { get; private set; }
            [JsonProperty("house_wallpaper")] public string HouseWallpaper { get; private set; }
            [JsonProperty("house_flooring")] public string HouseFlooring { get; private set; }
            [JsonProperty("house_music")] public string HouseMusic { get; private set; }
            [JsonProperty("house_music_note")] public string HouseMusicNote { get; private set; }
        }
    }
}
