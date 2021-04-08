using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Nookipedia.Net
{
    public record RegionedList<T>
    {
        public RegionedList()
        {
            Common = Array.Empty<T>();
            Joined = Array.Empty<T>();
        }

        public string? Month { get; init; }
        public T[]? North { get; init; }
        public T[]? South { get; init; }
        public T[] Common { get; init; }
        public T[] Joined { get; init; }
        public bool IsRegioned { get; init; }
    }

    internal class RegionedListJsonConverter<T> : JsonConverter<RegionedList<T>>
    {
        public override RegionedList<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.StartObject)
            {
                reader.ReadUntil(JsonTokenType.String);
                string? month = reader.GetString();
                HashSet<T> northSet = new();
                HashSet<T> southSet = new();
                reader.ReadUntil(JsonTokenType.StartArray);
                while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                {
                    T? value = JsonSerializer.Deserialize<T>(ref reader, options);
                    if (value is not null) northSet.Add(value);
                    else throw new JsonException("That isn't a valid regioned list; Report to Nookipedia.Net");
                }
                reader.ReadUntil(JsonTokenType.StartArray);
                while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                {
                    T? value = JsonSerializer.Deserialize<T>(ref reader, options);
                    if (value is not null) southSet.Add(value);
                    else throw new JsonException("That isn't a valid regioned list; Report to Nookipedia.Net");
                }
                HashSet<T> commonSet = new(northSet);
                commonSet.IntersectWith(southSet);
                HashSet<T> joinedSet = new(northSet);
                joinedSet.UnionWith(southSet);
                reader.Read();
                return new RegionedList<T>()
                {
                    Common = commonSet.ToArray(),
                    Joined = joinedSet.ToArray(),
                    North = northSet.ToArray(),
                    South = southSet.ToArray(),
                    Month = month,
                    IsRegioned = true
                };
            }
            else
            {
                HashSet<T> set = new();
                while (reader.Read() && reader.TokenType == JsonTokenType.String)
                {
                    T? value = JsonSerializer.Deserialize<T>(ref reader, options);
                    if (value is not null) set.Add(value);
                    else throw new JsonException("That isn't a valid regioned list; Report to Nookipedia.Net");
                }
                return new RegionedList<T>()
                {
                    Common = set.ToArray(),
                    Joined = set.ToArray(),
                    IsRegioned = false
                };
            }
        }

        public override void Write(Utf8JsonWriter writer, RegionedList<T> value, JsonSerializerOptions options) => throw new NotImplementedException();
    }
}
