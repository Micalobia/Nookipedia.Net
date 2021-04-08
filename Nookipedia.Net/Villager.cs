using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Nookipedia.Net
{
    public record Villager
    {
        [JsonConstructor]
        public Villager(
            string name, string url, string imageURL, string alternativeName, string titleColor,
            string textColor, string gameID, Species species, Personality personality, Gender gender, string birthday,
            StarSign starSign, string quote, string phrase, string[] pastPhrases, string clothing,
            bool islander, Game debut, Game[] appearances, NewHorizonsInfo? nHDetails = null)
        {
            Name = name;
            URL = url;
            ImageURL = imageURL;
            AlternativeName = alternativeName;
            TitleColor = titleColor;
            TextColor = textColor;
            GameID = gameID;
            Species = species;
            Personality = personality;
            Gender = gender;
            Birthday = birthday;
            StarSign = starSign;
            Quote = quote;
            Phrase = phrase;
            PastPhrases = pastPhrases;
            Clothing = clothing;
            Islander = islander;
            Debut = debut;
            Appearances = appearances;
            NHDetails = nHDetails;
        }

        [Required, JsonPropertyName("name")] public string Name { get; }
        [Required, JsonPropertyName("url")] public string URL { get; }
        [Required, JsonPropertyName("image_url")] public string ImageURL { get; }
        [Required, JsonPropertyName("alt_name")] public string AlternativeName { get; init; }
        [Required, JsonPropertyName("title_color")] public string TitleColor { get; set; }
        [Required, JsonPropertyName("text_color")] public string TextColor { get; init; }
        [Required, JsonPropertyName("id")] public string GameID { get; init; }
        [Required, JsonPropertyName("species")] public Species Species { get; init; }
        [Required, JsonPropertyName("personality")] public Personality Personality { get; init; }
        [Required, JsonPropertyName("gender")] public Gender Gender { get; init; }
        [Required, JsonPropertyName("birthday")] public string Birthday { get; init; }
        [Required, JsonPropertyName("sign")] public StarSign StarSign { get; init; }
        [Required, JsonPropertyName("quote")] public string Quote { get; init; }
        [Required, JsonPropertyName("phrase")] public string Phrase { get; init; }
        [Required, JsonPropertyName("prev_phrases")] public string[] PastPhrases { get; init; }
        [Required, JsonPropertyName("clothing")] public string Clothing { get; init; }
        [Required, JsonPropertyName("islander")] public bool Islander { get; init; }
        [Required, JsonPropertyName("debut")] public Game Debut { get; init; }
        [Required, JsonPropertyName("appearances")] public Game[] Appearances { get; init; }
        [JsonPropertyName("nh_details")] public NewHorizonsInfo? NHDetails { get; init; }

        public static string Endpoint() => "villagers";
        public record NewHorizonsInfo
        {
            [JsonConstructor]
            public NewHorizonsInfo(
                string imageURL, string photoURL, string iconURL, string quote, SubPersonality subPersonality,
                string catchphrase, string clothing, string clothingVariation, string[] favoriteClothingStyles,
                string[] favoriteColors, Hobby hobby, string houseInteriorURL, string houseExteriorURL,
                string houseWallpaper, string houseFlooring, string houseMusic, string houseMusicNote)
            {
                ImageURL = imageURL;
                PhotoURL = photoURL;
                IconURL = iconURL;
                Quote = quote;
                SubPersonality = subPersonality;
                Catchphrase = catchphrase;
                Clothing = clothing;
                ClothingVariation = clothingVariation;
                FavoriteClothingStyles = favoriteClothingStyles;
                FavoriteColors = favoriteColors;
                Hobby = hobby;
                HouseInteriorURL = houseInteriorURL;
                HouseExteriorURL = houseExteriorURL;
                HouseWallpaper = houseWallpaper;
                HouseFlooring = houseFlooring;
                HouseMusic = houseMusic;
                HouseMusicNote = houseMusicNote;
            }

            [Required, JsonPropertyName("image_url")] public string ImageURL { get; }
            [Required, JsonPropertyName("photo_url")] public string PhotoURL { get; }
            [Required, JsonPropertyName("icon_url")] public string IconURL { get; }
            [Required, JsonPropertyName("quote")] public string Quote { get; }
            [Required, JsonPropertyName("sub-personality")] public SubPersonality SubPersonality { get; }
            [Required, JsonPropertyName("catchphrase")] public string Catchphrase { get; }
            [Required, JsonPropertyName("clothing")] public string Clothing { get; }
            [Required, JsonPropertyName("clothing_variation")] public string ClothingVariation { get; }
            [Required, JsonPropertyName("fav_styles")] public string[] FavoriteClothingStyles { get; }
            [Required, JsonPropertyName("fav_colors")] public string[] FavoriteColors { get; }
            [Required, JsonPropertyName("hobby")] public Hobby Hobby { get; }
            [Required, JsonPropertyName("house_interior_url")] public string HouseInteriorURL { get; }
            [Required, JsonPropertyName("house_exterior_url")] public string HouseExteriorURL { get; }
            [Required, JsonPropertyName("house_wallpaper")] public string HouseWallpaper { get; }
            [Required, JsonPropertyName("house_flooring")] public string HouseFlooring { get; }
            [Required, JsonPropertyName("house_music")] public string HouseMusic { get; }
            [Required, JsonPropertyName("house_music_note")] public string HouseMusicNote { get; }
        }
    }
}
