using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Nookipedia.Net
{
    [JsonConverter(typeof(NameListConverter))]
    public record NameList
    {
        public string? Month { get; init; }
        public string[]? North { get; init; }
        public string[]? South { get; init; }
        public string[]? Common { get; init; }
        public string[]? Joined { get; init; }
        public bool IsRegioned { get; init; }
    }

    internal class NameListConverter : JsonConverter<NameList>
    {
        public override NameList? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            JsonTokenType token = reader.TokenType;
            if (token == JsonTokenType.StartObject)
            {
                reader.ReadUntil(JsonTokenType.String);
                string? month = reader.GetString();
                HashSet<string> northSet = new();
                reader.ReadUntil(JsonTokenType.StartArray);
                while (reader.Read() && reader.TokenType == JsonTokenType.String)
                    northSet.Add(reader.GetString() ?? throw new JsonException("That isn't a valid name list; Report to Nookipedia.Net"));
                reader.ReadUntil(JsonTokenType.StartArray);
                HashSet<string> southSet = new();
                while (reader.Read() && reader.TokenType == JsonTokenType.String)
                    southSet.Add(reader.GetString() ?? throw new JsonException("That isn't a valid name list; Report to Nookipedia.Net"));
                HashSet<string> commonSet = new(northSet);
                commonSet.IntersectWith(southSet);
                HashSet<string> joinedSet = new(northSet);
                joinedSet.UnionWith(southSet);
                reader.Read();
                return new NameList()
                {
                    Month = month,
                    North = northSet.ToArray(),
                    South = southSet.ToArray(),
                    Common = commonSet.ToArray(),
                    Joined = joinedSet.ToArray(),
                    IsRegioned = true
                };
            }
            else
            {
                HashSet<string> set = new();
                while (reader.Read() && reader.TokenType == JsonTokenType.String)
                    set.Add(reader.GetString() ?? "");
                return new NameList()
                {
                    Common = set.ToArray(),
                    Joined = set.ToArray(),
                    IsRegioned = false
                };
            }
        }
        public override void Write(Utf8JsonWriter writer, NameList value, JsonSerializerOptions options) => throw new NotImplementedException();
    }
}
