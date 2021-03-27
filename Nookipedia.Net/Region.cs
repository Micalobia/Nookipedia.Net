using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Nookipedia.Net
{
    [JsonObject]
    public class Region
    {
        [JsonProperty("availability_array")] public Availability[] Availabilities { get; set; }
        [JsonProperty("times_by_month")] public TimesByMonth Times { get; set; }
        [JsonProperty("months")] public string Months { get; set; }
        [JsonProperty("months_array")] public int[] ValidMonths { get; set; }

        [JsonObject]
        public class Availability
        {
            [JsonProperty("months")] public string Months { get; set; }
            [JsonProperty("time")] public string Time { get; set; }
        }

        [JsonObject]
        public class TimesByMonth : IEnumerable<string>
        {
            [JsonProperty("1")] public string January { get; set; } = null;
            [JsonProperty("2")] public string February { get; set; } = null;
            [JsonProperty("3")] public string March { get; set; } = null;
            [JsonProperty("4")] public string April { get; set; } = null;
            [JsonProperty("5")] public string May { get; set; } = null;
            [JsonProperty("6")] public string June { get; set; } = null;
            [JsonProperty("7")] public string July { get; set; } = null;
            [JsonProperty("8")] public string August { get; set; } = null;
            [JsonProperty("9")] public string September { get; set; } = null;
            [JsonProperty("10")] public string October { get; set; } = null;
            [JsonProperty("11")] public string November { get; set; } = null;
            [JsonProperty("12")] public string December { get; set; } = null;

            public string this[int index]
            {
                get
                {
                    string ret;
                    switch (index)
                    {
                        case 0: ret = January; break;
                        case 1: ret = February; break;
                        case 2: ret = March; break;
                        case 3: ret = April; break;
                        case 4: ret = May; break;
                        case 5: ret = June; break;
                        case 6: ret = July; break;
                        case 7: ret = August; break;
                        case 8: ret = September; break;
                        case 9: ret = October; break;
                        case 10: ret = November; break;
                        case 11: ret = December; break;
                        default:
                            throw new IndexOutOfRangeException();
                    }
                    return "NA".Equals(ret) ? "Not available this month" : ret;
                }
                set
                {
                    switch (index)
                    {
                        case 0: January = value; break;
                        case 1: February = value; break;
                        case 2: March = value; break;
                        case 3: April = value; break;
                        case 4: May = value; break;
                        case 5: June = value; break;
                        case 6: July = value; break;
                        case 7: August = value; break;
                        case 8: September = value; break;
                        case 9: October = value; break;
                        case 10: November = value; break;
                        case 11: December = value; break;
                        default:
                            throw new IndexOutOfRangeException();
                    }
                }
            }

            public IEnumerator<string> GetEnumerator() => new Enumerator(this);
            IEnumerator IEnumerable.GetEnumerator() => new Enumerator(this);

            private class Enumerator : IEnumerator<string>
            {
                private TimesByMonth _self;
                private int _index = -1;

                public Enumerator(TimesByMonth self) => _self = self;

                public string Current => _self[_index];

                object IEnumerator.Current => throw new NotImplementedException();

                void IDisposable.Dispose() { }
                public bool MoveNext() => ++_index < 12;
                public void Reset() => _index = -1;
            }
        }
    }

    [JsonObject]
    public class BaseNookObject
    {
        [JsonProperty("name")] public string Name { get; private set; }
        [JsonProperty("url")] public string URL { get; private set; }
        [JsonProperty("image_url")] public string ImageURL { get; private set; }
    }
}
