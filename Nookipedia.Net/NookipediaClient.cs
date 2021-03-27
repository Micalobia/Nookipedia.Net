using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text.Json;
using static Nookipedia.Net.NookipediaConstants;

namespace Nookipedia.Net
{
    public class NookipediaClient : IDisposable
    {
        private readonly WebClient _client;

        private WebClient Client => _client;

        public NookipediaClient(string apikey, WebClient client = null)
        {
            _client = client ?? new WebClient();
            Client.BaseAddress = "https://api.nookipedia.com";
            Client.Headers.Add("Accept-Version", NookipediaAPIVersion.ToString(3));
            Client.Headers.Add("X-API-KEY", apikey);
        }

        public T GetCritter<T>(string name) where T : Critter, new() => FetchSingle<T>(name);
        public T[] GetCritters<T>() where T : Critter, new() => FetchList<T>();
        public T[] GetCritters<T>(string month) where T : Critter, new() => FetchList<T>(("month", month));
        public string[] GetCritterNames<T>() where T : Critter, new() => FetchNames<T>();
        public string[] GetCritterNames<T>(string month) where T : Critter, new() => FetchNames<T>(("month", month));

        public Artwork GetArtwork(string name) => FetchSingle<Artwork>(name);
        public Artwork[] GetArtworks() => FetchList<Artwork>();
        public Artwork[] GetArtworks(bool hasfake) => FetchList<Artwork>(("hasfake", hasfake));
        public string[] GetArtworkNames() => FetchNames<Artwork>();
        public string[] GetArtworkNames(bool hasfake) => FetchNames<Artwork>(("hasfake", hasfake));

        public Recipe GetRecipe(string name) => FetchSingle<Recipe>(name);
        public Recipe[] GetRecipes() => FetchList<Recipe>();
        public Recipe[] GetRecipes(params string[] materials) => FetchList<Recipe>(materials.Select(x => new NamedValue("material", x)).ToArray());
        public string[] GetRecipeNames() => FetchNames<Recipe>();
        public string[] GetRecipeNames(params string[] materials) => FetchNames<Recipe>(materials.Select(x => new NamedValue("material", x)).ToArray());

        public Villager[] GetVillagers() => FetchList<Villager>();
        public Villager[] GetVillagers(string name) => FetchList<Villager>(("name", name));
        public Villager[] GetVillagers(string name = null, Personality personality = Personality.None, string birthmonth = null, int birthday = -1, bool includeNHDetails = false, params Game[] games)
            => FetchList<Villager>(BuildVillagerQuery(name, personality, birthmonth, birthday, includeNHDetails, games));
        public string[] GetVillagerNames() => FetchNames<Villager>();
        public string[] GetVillagerNames(string name) => FetchNames<Villager>(("name", name));
        public string[] GetVillagerNames(string name = null, Personality personality = Personality.None, string birthmonth = null, int birthday = -1, params Game[] games)
            => FetchNames<Villager>(BuildVillagerQuery(name, personality, birthmonth, birthday, false, games));

        private static NamedValue[] BuildVillagerQuery(string name = null, Personality personality = Personality.None, string birthmonth = null, int birthday = -1, bool includeNHDetails = false, params Game[] games)
        {
            List<NamedValue> ret = new();
            if (name.Exists()) ret.Add(("name", name));
            if (personality != Personality.None) ret.Add(("personality", personality.Value()));
            if (birthmonth.Exists()) ret.Add(("birthmonth", birthmonth));
            if (birthday > 0 && birthday <= 31) ret.Add(("birthday", birthday));
            if (includeNHDetails) ret.Add(("nhdetails", "true"));
            return games.Aggregate(ret, (self, game) => self.With(("game", game)) as List<NamedValue>).ToArray();
        }

        private string[] FetchNames<T>(params NamedValue[] parameters) where T : IListEndpoint, new() => Fetch<string[]>(ListEndpoint<T>.Endpoint(), parameters.Concat(("excludedetails", "true")));
        private T[] FetchList<T>(params NamedValue[] parameters) where T : IListEndpoint, new() => Fetch<T[]>(ListEndpoint<T>.Endpoint(), parameters);
        private T FetchSingle<T>(string name, params NamedValue[] parameters) where T : ISingleEndpoint, new() => Fetch<T>(SingleEndpoint<T>.Endpoint(name), parameters);
        private T Fetch<T>(string endpoint, params NamedValue[] parameters)
        {
            Client.QueryString = parameters.NameValueCollection();
            return Deserialize<T>(Client.DownloadString(endpoint));
        }

        private T Deserialize<T>(string json) => JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions(JsonSerializerDefaults.Web));

        public void Dispose() => Client.Dispose();

        ~NookipediaClient() => Dispose();
    }
}
