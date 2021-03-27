using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace Nookipedia.Net
{
    internal static class Extensions
    {
        public static NameValueCollection With(this NameValueCollection self, string name, string value, bool mutate = true)
        {
            if (mutate)
            {
                self.Add(name, value);
                return self;
            }
            else return new NameValueCollection(self.Count + 1, self).With(name, value);
        }

        public static IList<T> With<T>(this IList<T> self, T value, bool mutate = true)
        {
            if (mutate)
            {
                self.Add(value);
                return self;
            }
            else
            {
                List<T> ret = new(self.Count + 1);
                ret.AddRange(self);
                ret.Add(value);
                return ret;
            }
        }

        public static bool Exists<T>(this T self) where T : class => !(self is null);

        public static NameValueCollection Query<T>(this IEnumerable<Tuple<string, T>> self) => self.Aggregate(new NameValueCollection(self.Count()), (query, tuple) => query.With(tuple.Item1, tuple.Item2.ToString()));
        public static NameValueCollection Query(this IEnumerable<Tuple<string, string>> self) => self.Aggregate(new NameValueCollection(self.Count()), (query, tuple) => query.With(tuple.Item1, tuple.Item2));

        public static string Value<T>(this T self) where T : Enum
            => typeof(T)
                .GetTypeInfo()
                .DeclaredMembers
                .SingleOrDefault(x => x.Name == self.ToString())
                ?.GetCustomAttribute<EnumMemberAttribute>(false)
                ?.Value;

        public static T[] Concat<T>(this T[] self, params T[] other)
        {
            if (self is null) throw new ArgumentNullException("self");
            if (other is null) throw new ArgumentNullException("other");
            T[] ret = new T[self.Length + other.Length];
            self.CopyTo(ret, 0);
            other.CopyTo(ret, self.Length);
            return ret;
        }

        public static string QueryString(this IEnumerable<NamedValue> self) => string.Join('&', self.Select(x => $"{x.Name}={x.Value}"));
    }
}
