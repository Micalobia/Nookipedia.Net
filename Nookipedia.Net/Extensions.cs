using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace Nookipedia.Net
{
    internal static class Extensions
    {
        public static NameValueCollection With(this NameValueCollection self, string? name, string? value, bool mutate = true)
        {
            if (mutate)
            {
                self.Add(name, value);
                return self;
            }
            else return new NameValueCollection(self.Count + 1, self).With(name, value);
        }

        public static IEnumerable<T> Extend<T>(this IEnumerable<T> self, params T[] other) => self.Extend(other.AsEnumerable());
        public static IEnumerable<T> Extend<T>(this IEnumerable<T> self, IEnumerable<T> other)
        {
            foreach (T value in self) yield return value;
            foreach (T value in other) yield return value;
        }
        public static IEnumerable<T> Extend<T>(this IEnumerable<T> self, T other)
        {
            foreach (T value in self) yield return value;
            yield return other;
        }
        public static IEnumerable<T> Extend<T>(this IEnumerable<T> self, T other0, T other1)
        {
            foreach (T value in self) yield return value;
            yield return other0;
            yield return other1;
        }

        public static void ReadUntil(ref this Utf8JsonReader reader, JsonTokenType token)
        {
            reader.Read();
            while (reader.TokenType != token) reader.Read();
        }

        //public static IList<T> With<T>(this IList<T> self, T value, bool mutate = true)
        //{
        //    if (mutate)
        //    {
        //        self.Add(value);
        //        return self;
        //    }
        //    else
        //    {
        //        List<T> ret = new(self.Count + 1);
        //        ret.AddRange(self);
        //        ret.Add(value);
        //        return ret;
        //    }
        //}

        //public static IList<T> WithRange<T>(this IList<T> self, IEnumerable<T> elements) => elements.Aggregate(self, (_self, element) => self.With(element));
        //public static IList<T> WithRange<T, K>(this IList<T> self, IEnumerable<K> elements, Func<K, T> func) => elements.Aggregate(self, (_self, element) => self.With(func(element)));

        public static bool Exists<T>(this T? self) where T : class => self is not null;

        public static NameValueCollection Query<T>(this IEnumerable<Tuple<string, T>> self) => self.Aggregate(new NameValueCollection(self.Count()), (query, tuple) => query.With(tuple.Item1, tuple?.Item2?.ToString()));
        public static NameValueCollection Query(this IEnumerable<Tuple<string, string>> self) => self.Aggregate(new NameValueCollection(self.Count()), (query, tuple) => query.With(tuple.Item1, tuple.Item2));

        public static string? Value<T>(this T self) where T : Enum
            => typeof(T)
                .GetTypeInfo()
                .DeclaredMembers
                .SingleOrDefault(x => x.Name == self.ToString())
                ?.GetCustomAttribute<EnumMemberAttribute>(false)
                ?.Value;


        public static T[] Concat<T>(this T[] self, params T[] other)
        {
            if (self is null) throw new ArgumentNullException(nameof(self));
            if (other is null) throw new ArgumentNullException(nameof(other));
            T[] ret = new T[self.Length + other.Length];
            self.CopyTo(ret, 0);
            other.CopyTo(ret, self.Length);
            return ret;
        }

        public static string QueryString(this IEnumerable<NamedValue> self) => string.Join('&', self.Select(x => $"{x.Name}={x.Value}"));
        public static NameValueCollection NameValueCollection(this IEnumerable<NamedValue> self) => self.Where(x => x.Exists()).Aggregate(new NameValueCollection(self.Count()), (ret, value) => ret.With(value.Name, value?.Value?.ToString()));

        public static void Result(this Task self) => self.GetAwaiter().GetResult();
        public static T Result<T>(this Task<T> self) => self.GetAwaiter().GetResult();

        public static byte[] ReadBytes(this Stream self)
        {
            using MemoryStream ms = new();
            self.CopyTo(ms);
            return ms.Capacity == ms.Length ? ms.GetBuffer() : ms.ToArray();
        }

        public static void Add<T>(this IList<JsonConverter> self) where T : JsonConverter, new() => self.Add(new T());
        public static void Add<TConverter, TType>(this IList<JsonConverter> self) where TConverter : JsonConverter<TType>, new() => self.Add(new TConverter());
    }
}
