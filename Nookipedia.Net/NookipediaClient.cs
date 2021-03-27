using System;
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

        public Bug GetBug(string name) => FetchSingle<Bug>(name);
        public Bug[] GetBugs() => FetchList<Bug>();
        public Bug[] GetBugs(string month) => FetchList<Bug>(new NameValueCollection().With("month", month));
        public string[] GetBugNames() => FetchNames<Bug>();
        public string[] GetBugNames(string month) => FetchNames<Bug>(new NameValueCollection().With("month", month));

        public Fish GetFish(string name) => FetchSingle<Fish>(name);
        public Fish[] GetFishes() => FetchList<Fish>();
        public Fish[] GetFishes(string month) => FetchList<Fish>(new NameValueCollection().With("month", month));
        public string[] GetFishNames() => FetchNames<Fish>();
        public string[] GetFishNames(string month) => FetchNames<Fish>(new NameValueCollection().With("month", month));

        public SeaCreature GetSeaCreature(string name) => FetchSingle<SeaCreature>(name);
        public SeaCreature[] GetSeaCreatures() => FetchList<SeaCreature>();
        public SeaCreature[] GetSeaCreatures(string month) => FetchList<SeaCreature>(new NameValueCollection().With("month", month));
        public string[] GetSeaCreatureNames() => FetchNames<SeaCreature>();
        public string[] GetSeaCreatureNames(string month) => FetchNames<SeaCreature>(new NameValueCollection().With("month", month));

        public Artwork GetArtwork(string name) => FetchSingle<Artwork>(name);
        public Artwork[] GetArtworks() => FetchList<Artwork>();
        public Artwork[] GetArtworks(bool hasfake) => FetchList<Artwork>(new NameValueCollection().With("hasfake", hasfake.ToString()));
        public string[] GetArtworkNames() => FetchNames<Artwork>();
        public string[] GetArtworkNames(bool hasfake) => FetchNames<Artwork>(new NameValueCollection().With("hasfake", hasfake.ToString()));

        public Recipe GetRecipe(string name) => FetchSingle<Recipe>(name);
        public Recipe[] GetRecipes() => FetchList<Recipe>();
        public Recipe[] GetRecipes(params string[] materials) => FetchList<Recipe>(materials.Aggregate(new NameValueCollection(), (query, material) => query.With("material", material)));
        public string[] GetRecipeNames() => FetchNames<Recipe>();
        public string[] GetRecipeNames(params string[] materials) => FetchNames<Recipe>(materials.Aggregate(new NameValueCollection(), (query, material) => query.With("material", material)));

        public Villager[] GetVillagers() => FetchList<Villager>();
        public Villager[] GetVillagers(string name) => FetchList<Villager>(new NameValueCollection().With("name", name));
        public Villager[] GetVillagers(string name = null, Personality personality = Personality.None, string birthmonth = null, int birthday = -1, bool includeNHDetails = false, params Game[] games)
            => FetchList<Villager>(BuildVillagerQuery(name, personality, birthmonth, birthday, includeNHDetails, games));
        public string[] GetVillagerNames() => FetchNames<Villager>();
        public string[] GetVillagerNames(string name) => FetchNames<Villager>(new NameValueCollection().With("name", name));
        public string[] GetVillagerNames(string name = null, Personality personality = Personality.None, string birthmonth = null, int birthday = -1, params Game[] games)
            => FetchNames<Villager>(BuildVillagerQuery(name, personality, birthmonth, birthday, false, games));

        private static NameValueCollection BuildVillagerQuery(string name = null, Personality personality = Personality.None, string birthmonth = null, int birthday = -1, bool includeNHDetails = false, params Game[] games)
        {
            NameValueCollection query = new();
            if (name.Exists()) query.Add("name", name);
            if (personality != Personality.None) query.Add("personality", personality.Value());
            if (birthmonth.Exists()) query.Add("birthmonth", birthmonth);
            if (birthday > 0 && birthday <= 31) query.Add("birthday", birthday.ToString());
            if (includeNHDetails) query.Add("nhdetails", "true");
            return games.Aggregate(query, (self, game) => self.With("game", game.Value()));
        }

        private string[] FetchNames<T>() where T : IListEndpoint, new() => Fetch<string[]>(ListEndpoint<T>.Endpoint(), new NameValueCollection().With("excludedetails", "true", false));
        private string[] FetchNames<T>(NameValueCollection query) where T : IListEndpoint, new() => Fetch<string[]>(ListEndpoint<T>.Endpoint(), query.With("excludedetails", "true", false));
        private T[] FetchList<T>() where T : IListEndpoint, new() => Fetch<T[]>(ListEndpoint<T>.Endpoint(), new NameValueCollection());
        private T[] FetchList<T>(NameValueCollection query) where T : IListEndpoint, new() => Fetch<T[]>(ListEndpoint<T>.Endpoint(), query);
        private T FetchSingle<T>(string sub) where T : ISingleEndpoint, new() => Fetch<T>(SingleEndpoint<T>.Endpoint(sub), new NameValueCollection());
        private T FetchSingle<T>(string sub, NameValueCollection query) where T : ISingleEndpoint, new() => Fetch<T>(SingleEndpoint<T>.Endpoint(sub), query);
        private T Fetch<T>(string endpoint) => Fetch<T>(endpoint, new NameValueCollection());
        private T Fetch<T>(string endpoint, NameValueCollection query)
        {
            Client.QueryString = query;
            return Deserialize<T>(Client.DownloadString(endpoint));
        }

        private T Deserialize<T>(string json) => JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions(JsonSerializerDefaults.Web));

        public void Dispose() => Client.Dispose();

        ~NookipediaClient() => Dispose();
    }
}
