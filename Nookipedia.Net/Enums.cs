using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Nookipedia.Net
{
    public enum Month
    {
        [EnumMember(Value = "current")] Current,
        [EnumMember(Value = "january")] January,
        [EnumMember(Value = "february")] February,
        [EnumMember(Value = "march")] March,
        [EnumMember(Value = "april")] April,
        [EnumMember(Value = "may")] May,
        [EnumMember(Value = "june")] June,
        [EnumMember(Value = "july")] July,
        [EnumMember(Value = "august")] August,
        [EnumMember(Value = "september")] September,
        [EnumMember(Value = "october")] October,
        [EnumMember(Value = "november")] November,
        [EnumMember(Value = "december")] December
    }

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum ClothingCategory
    {
        [EnumMember(Value = "Other")] Other,
        [EnumMember(Value = "Headwear")] Headwear,
        [EnumMember(Value = "Shoes")] Shoes,
        [EnumMember(Value = "Bags")] Bags,
        [EnumMember(Value = "Tops")] Tops,
        [EnumMember(Value = "Dress-Up")] DressUp,
        [EnumMember(Value = "Socks")] Socks,
        [EnumMember(Value = "Accessories")] Accessories,
        [EnumMember(Value = "Bottoms")] Bottoms,
        [EnumMember(Value = "Umbrellas")] Umbrellas
    }

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum ClothingLabel
    {
        [EnumMember(Value = "Goth")] Goth,
        [EnumMember(Value = "Formal")] Formal,
        [EnumMember(Value = "Party")] Party,
        [EnumMember(Value = "Sporty")] Sporty,
        [EnumMember(Value = "Work")] Work,
        [EnumMember(Value = "Vacation")] Vacation,
        [EnumMember(Value = "Theatrical")] Theatrical,
        [EnumMember(Value = "Fairy Tale")] FairyTale,
        [EnumMember(Value = "Outdoorsy")] Outdoorsy,
        [EnumMember(Value = "Comfy")] Comfy,
        [EnumMember(Value = "Everyday")] Everyday
    }

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum ClothingStyle
    {
        [EnumMember(Value = "Gorgeous")] Gorgeous,
        [EnumMember(Value = "Active")] Active,
        [EnumMember(Value = "Cool")] Cool,
        [EnumMember(Value = "Elegant")] Elegant,
        [EnumMember(Value = "Simple")] Simple,
        [EnumMember(Value = "Cute")] Cute
    }

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum Color
    {
        [EnumMember(Value = "Colorful")] Colorful,
        [EnumMember(Value = "Gray")] Gray,
        [EnumMember(Value = "Aqua")] Aqua,
        [EnumMember(Value = "Pink")] Pink,
        [EnumMember(Value = "Purple")] Purple,
        [EnumMember(Value = "White")] White,
        [EnumMember(Value = "Red")] Red,
        [EnumMember(Value = "Orange")] Orange,
        [EnumMember(Value = "Brown")] Brown,
        [EnumMember(Value = "Green")] Green,
        [EnumMember(Value = "Black")] Black,
        [EnumMember(Value = "Beige")] Beige,
        [EnumMember(Value = "Yellow")] Yellow,
        [EnumMember(Value = "Blue")] Blue
    }

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum PhotoCategory
    {
        [EnumMember(Value = "Posters")] Posters,
        [EnumMember(Value = "Photos")] Photos
    }

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum ClothingGender
    {
        [EnumMember(Value = "")] None,
        [EnumMember(Value = "Manly")] Manly,
        [EnumMember(Value = "Womanly")] Womanly,
        [EnumMember(Value = "Free")] Free
    }

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum FurnitureCategory
    {
        [EnumMember(Value = "Housewares")] Housewares,
        [EnumMember(Value = "Miscellaneous")] Miscellaneous,
        [EnumMember(Value = "Wall-Mounted")] WallMounted
    }

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum FurnitureHHACategory
    {
        [EnumMember(Value = "")] Missing,
        [EnumMember(Value = "Pet")] Pet,
        [EnumMember(Value = "AC")] AC,
        [EnumMember(Value = "Clock")] Clock,
        [EnumMember(Value = "Lighting")] Lighting,
        [EnumMember(Value = "Trash")] Trash,
        [EnumMember(Value = "Audio")] Audio,
        [EnumMember(Value = "Small Goods")] SmallGoods,
        [EnumMember(Value = "Appliance")] Appliance,
        [EnumMember(Value = "None")] None,
        [EnumMember(Value = "Musical Instrument")] MusicalInstrument,
        [EnumMember(Value = "Plant")] Plant,
        [EnumMember(Value = "TV")] TV,
        [EnumMember(Value = "Dresser")] Dresser,
        [EnumMember(Value = "Doll")] Doll
    }

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum Currency
    {
        [EnumMember(Value = "")] None,
        [EnumMember(Value = "Bells")] Bells,
        [EnumMember(Value = "Nook Miles")] NookMiles
    }

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum Species
    {
        [EnumMember(Value = "")] None,
        [EnumMember(Value = "Alligator")] Alligator,
        [EnumMember(Value = "Anteater")] Anteater,
        [EnumMember(Value = "Bear")] Bear,
        [EnumMember(Value = "Bird")] Bird,
        [EnumMember(Value = "Bull")] Bull,
        [EnumMember(Value = "Cat")] Cat,
        [EnumMember(Value = "Cub")] Cub,
        [EnumMember(Value = "Chicken")] Chicken,
        [EnumMember(Value = "Cow")] Cow,
        [EnumMember(Value = "Deer")] Deer,
        [EnumMember(Value = "Dog")] Dog,
        [EnumMember(Value = "Duck")] Duck,
        [EnumMember(Value = "Eagle")] Eagle,
        [EnumMember(Value = "Elephant")] Elephant,
        [EnumMember(Value = "Frog")] Frog,
        [EnumMember(Value = "Goat")] Goat,
        [EnumMember(Value = "Gorilla")] Gorilla,
        [EnumMember(Value = "Hamster")] Hamster,
        [EnumMember(Value = "Hippo")] Hippo,
        [EnumMember(Value = "Horse")] Horse,
        [EnumMember(Value = "Koala")] Koala,
        [EnumMember(Value = "Kangaroo")] Kangaroo,
        [EnumMember(Value = "Lion")] Lion,
        [EnumMember(Value = "Monkey")] Monkey,
        [EnumMember(Value = "Mouse")] Mouse,
        [EnumMember(Value = "Octopus")] Octopus,
        [EnumMember(Value = "Ostrich")] Ostrich,
        [EnumMember(Value = "Penguin")] Penguin,
        [EnumMember(Value = "Pig")] Pig,
        [EnumMember(Value = "Rabbit")] Rabbit,
        [EnumMember(Value = "Rhino")] Rhino,
        [EnumMember(Value = "Sheep")] Sheep,
        [EnumMember(Value = "Squirrel")] Squirrel,
        [EnumMember(Value = "Tiger")] Tiger,
        [EnumMember(Value = "Wolf")] Wolf
    }

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum Personality
    {
        [EnumMember(Value = "")] None,
        [EnumMember(Value = "Cranky")] Cranky,
        [EnumMember(Value = "Jock")] Jock,
        [EnumMember(Value = "Lazy")] Lazy,
        [EnumMember(Value = "Normal")] Normal,
        [EnumMember(Value = "Peppy")] Peppy,
        [EnumMember(Value = "Sisterly")] Sisterly,
        [EnumMember(Value = "Smug")] Smug,
        [EnumMember(Value = "Snooty")] Snooty
    }

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum Gender
    {
        [EnumMember(Value = "")] None,
        [EnumMember(Value = "Male")] Male,
        [EnumMember(Value = "Female")] Female
    }

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum StarSign
    {
        [EnumMember(Value = "")] None,
        [EnumMember(Value = "Aries")] Aries,
        [EnumMember(Value = "Taurus")] Taurus,
        [EnumMember(Value = "Gemini")] Gemini,
        [EnumMember(Value = "Cancer")] Cancer,
        [EnumMember(Value = "Leo")] Leo,
        [EnumMember(Value = "Virgo")] Virgo,
        [EnumMember(Value = "Libra")] Libra,
        [EnumMember(Value = "Scorpio")] Scorpio,
        [EnumMember(Value = "Sagittarius")] Sagittarius,
        [EnumMember(Value = "Capricorn")] Capricorn,
        [EnumMember(Value = "Aquarius")] Aquarius,
        [EnumMember(Value = "Pisces")] Pisces
    }

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum Game
    {
        [EnumMember(Value = "")] None,
        [EnumMember(Value = "DNM")] DoubutsuNoMori,
        [EnumMember(Value = "AC")] AnimalCrossing,
        [EnumMember(Value = "E_PLUS")] DoubutsuNoMoriEPlus,
        [EnumMember(Value = "WW")] WildWorld,
        [EnumMember(Value = "CF")] CityFolk,
        [EnumMember(Value = "NL")] NewLeaf,
        [EnumMember(Value = "WA")] WelcomeAmiibo,
        [EnumMember(Value = "NH")] NewHorizons,
        [EnumMember(Value = "FILM")] DoubutsuNoMoriFilm,
        [EnumMember(Value = "HHD")] HappyHomeDesigner,
        [EnumMember(Value = "PC")] PocketCamp
    }

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum SubPersonality
    {
        [EnumMember(Value = "")] None,
        [EnumMember(Value = "A")] A,
        [EnumMember(Value = "B")] B
    }

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum Hobby
    {
        [EnumMember(Value = "")] None,
        [EnumMember(Value = "Education")] Education,
        [EnumMember(Value = "Fashion")] Fashion,
        [EnumMember(Value = "Fitness")] Fitness,
        [EnumMember(Value = "Music")] Music,
        [EnumMember(Value = "Nature")] Nature,
        [EnumMember(Value = "Play")] Play
    }
}
