using System.Text.Json.Serialization;

namespace Nookipedia.Net
{
    public record Villager : BaseNookObject, IListEndpoint
    {
        [JsonPropertyName("alt_name")] public string? AlternativeName { get; init; }
        [JsonPropertyName("title_color")] public string? TitleColor { get; set; }
        [JsonPropertyName("text_color")] public string? TextColor { get; init; }
        [JsonPropertyName("id")] public string? GameID { get; init; }
        [JsonPropertyName("species")] public Species Species { get; init; }
        [JsonPropertyName("personality")] public Personality Personality { get; init; }
        [JsonPropertyName("gender")] public Gender Gender { get; init; }
        [JsonPropertyName("birthday")] public string? Birthday { get; init; }
        [JsonPropertyName("sign")] public StarSign StarSign { get; init; }
        [JsonPropertyName("quote")] public string? Quote { get; init; }
        [JsonPropertyName("phrase")] public string? Phrase { get; init; }
        [JsonPropertyName("prev_phrases")] public string[]? PastPhrases { get; init; }
        [JsonPropertyName("clothing")] public string? Clothing { get; init; }
        [JsonPropertyName("islander")] public bool Islander { get; init; }
        [JsonPropertyName("debut")] public Game Debut { get; init; }
        [JsonPropertyName("appearances")] public Game[]? Appearances { get; init; }
        [JsonPropertyName("nh_details")] public NewHorizonsInfo? NHDetails { get; init; }

        public string Endpoint() => "villagers";
        public record NewHorizonsInfo
        {
            [JsonPropertyName("image_url")] public string? ImageURL { get; init; }
            [JsonPropertyName("photo_url")] public string? PhotoURL { get; init; }
            [JsonPropertyName("icon_url")] public string? IconURL { get; init; }
            [JsonPropertyName("quote")] public string? Quote { get; init; }
            [JsonPropertyName("sub-personality")] public SubPersonality SubPersonality { get; init; }
            [JsonPropertyName("catchphrase")] public string? Catchphrase { get; init; }
            [JsonPropertyName("clothing")] public string? Clothing { get; init; }
            [JsonPropertyName("clothing_variation")] public string? ClothingVariation { get; init; }
            [JsonPropertyName("fav_styles")] public string[]? FavoriteClothingStyles { get; init; }
            [JsonPropertyName("fav_colors")] public string[]? FavoriteColors { get; init; }
            [JsonPropertyName("hobby")] public Hobby Hobby { get; init; }
            [JsonPropertyName("house_interior_url")] public string? HouseInteriorURL { get; init; }
            [JsonPropertyName("house_exterior_url")] public string? HouseExteriorURL { get; init; }
            [JsonPropertyName("house_wallpaper")] public string? HouseWallpaper { get; init; }
            [JsonPropertyName("house_flooring")] public string? HouseFlooring { get; init; }
            [JsonPropertyName("house_music")] public string? HouseMusic { get; init; }
            [JsonPropertyName("house_music_note")] public string? HouseMusicNote { get; init; }
        }
    }
}
