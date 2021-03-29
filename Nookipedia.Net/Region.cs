using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Nookipedia.Net
{
    public record Region
    {
        [JsonPropertyName("availability_array")] public Availability[]? Availabilities { get; init; }
        [JsonPropertyName("times_by_month")] public TimesByMonth? Times { get; init; }
        [JsonPropertyName("months")] public string? Months { get; init; }
        [JsonPropertyName("months_array")] public int[]? ValidMonths { get; init; }

        public record Availability
        {
            [JsonPropertyName("months")] public string? Months { get; init; }
            [JsonPropertyName("time")] public string? Time { get; init; }
        }

        public record TimesByMonth
        {
            [JsonPropertyName("1")] public string? January { get; init; }
            [JsonPropertyName("2")] public string? February { get; init; }
            [JsonPropertyName("3")] public string? March { get; init; }
            [JsonPropertyName("4")] public string? April { get; init; }
            [JsonPropertyName("5")] public string? May { get; init; }
            [JsonPropertyName("6")] public string? June { get; init; }
            [JsonPropertyName("7")] public string? July { get; init; }
            [JsonPropertyName("8")] public string? August { get; init; }
            [JsonPropertyName("9")] public string? September { get; init; }
            [JsonPropertyName("10")] public string? October { get; init; }
            [JsonPropertyName("11")] public string? November { get; init; }
            [JsonPropertyName("12")] public string? December { get; init; }

            [JsonIgnore]
            public string? this[int index]
            {
                get
                {
                    string? ret = index switch
                    {
                        0 => January,
                        1 => February,
                        2 => March,
                        3 => April,
                        4 => May,
                        5 => June,
                        6 => July,
                        7 => August,
                        8 => September,
                        9 => October,
                        10 => November,
                        11 => December,
                        _ => throw new IndexOutOfRangeException(),
                    };
                    return "NA".Equals(ret) ? "Not available this month" : ret;
                }
            }
        }
    }

    public record BaseNookObject
    {
        [JsonPropertyName("name")] public string? Name { get; init; }
        [JsonPropertyName("url")] public string? URL { get; init; }
        [JsonPropertyName("image_url")] public string? ImageURL { get; init; }
    }
}
