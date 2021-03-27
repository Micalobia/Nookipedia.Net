using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using static Nookipedia.Net.NookipediaConstants;

namespace Nookipedia.Net
{
    public sealed class NookipediaAsyncClient : IDisposable
    {
        private static readonly JsonSerializerOptions _options;
        private static JsonSerializerOptions Options => _options;

        static NookipediaAsyncClient() => _options = new JsonSerializerOptions();

        private readonly HttpClient _client;
        private HttpClient Client => _client;

        public NookipediaAsyncClient(string apikey, HttpClient client = null)
        {
            _client = client ?? new HttpClient();
            Client.BaseAddress = new Uri("https://api.nookipedia.com");
            Client.DefaultRequestHeaders.Add("Accept-Version", NookipediaAPIVersion.ToString(3));
            Client.DefaultRequestHeaders.Add("X-API-KEY", apikey);
        }

        public Task<T> GetCritterAsync<T>(string name) where T : Critter, new() => FetchSingleAsync<T>(name);
        public Task<T[]> GetCrittersAsync<T>() where T : Critter, new() => FetchListAsync<T>();
        public Task<T[]> GetCrittersAsync<T>(string month) where T : Critter, new() => FetchListAsync<T>(("month", month));
        public Task<string[]> GetCritterNamesAsync<T>() where T : Critter, new() => FetchNamesAsync<T>();
        public Task<string[]> GetCritterNamesAsync<T>(string month) where T : Critter, new() => FetchNamesAsync<T>(("month", month));

        public Task<Artwork> GetArtworkAsync(string name) => FetchSingleAsync<Artwork>(name);
        public Task<Artwork[]> GetArtworksAsync() => FetchListAsync<Artwork>();
        public Task<Artwork[]> GetArtworksAsync(bool hasfake) => FetchListAsync<Artwork>(("hasfake", hasfake));
        public Task<string[]> GetArtworkNamesAsync() => FetchNamesAsync<Artwork>();
        public Task<string[]> GetArtworkNamesAsync(bool hasfake) => FetchNamesAsync<Artwork>(("hasfake", hasfake));

        public Task<Recipe> GetRecipeAsync(string name) => FetchSingleAsync<Recipe>(name);
        public Task<Recipe[]> GetRecipesAsync(params string[] materials) => FetchListAsync<Recipe>(materials.Select(x => new NamedValue("material", x)).ToArray());
        public Task<string[]> GetRecipeNamesAsync(params string[] materials) => FetchNamesAsync<Recipe>(materials.Select(x => new NamedValue("material", x)).ToArray());

        public Task<Villager[]> GetVillagersAsync() => FetchListAsync<Villager>();
        public Task<Villager[]> GetVillagersAsync(string name) => FetchListAsync<Villager>(("name", name));
        public Task<Villager[]> GetVillagersAsync(string name = null, Personality personality = Personality.None, string birthmonth = null, int birthday = -1, bool includeNHDetails = false, params Game[] games)
            => FetchListAsync<Villager>(BuildVillagerQuery(name, personality, birthmonth, birthday, includeNHDetails, games));
        public Task<string[]> GetVillagerNamesAsync() => FetchNamesAsync<Villager>();
        public Task<string[]> GetVillagerNamesAsync(string name) => FetchNamesAsync<Villager>(("name", name));
        public Task<string[]> GetVillagerNamesAsync(string name = null, Personality personality = Personality.None, string birthmonth = null, int birthday = -1, params Game[] games)
            => FetchNamesAsync<Villager>(BuildVillagerQuery(name, personality, birthmonth, birthday, false, games));

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

        private Task<string[]> FetchNamesAsync<T>(params NamedValue[] parameters) where T : IListEndpoint, new() => FetchAsync<string[]>(ListEndpoint<T>.Endpoint(), parameters.Concat(("excludedetails", "true")));
        private Task<T[]> FetchListAsync<T>(params NamedValue[] parameters) where T : IListEndpoint, new() => FetchAsync<T[]>(ListEndpoint<T>.Endpoint(), parameters);
        private Task<T> FetchSingleAsync<T>(string name, params NamedValue[] parameters) where T : ISingleEndpoint, new() => FetchAsync<T>(SingleEndpoint<T>.Endpoint(name), parameters);
        private Task<T> FetchAsync<T>(string endpoint, params NamedValue[] parameters) => FetchAsync<T>(endpoint, default, parameters);
        private async Task<T> FetchAsync<T>(string endpoint, CancellationToken token, params NamedValue[] parameters)
        {
            using Stream stream = await GetResponseStream(endpoint, token, parameters);
            return await JsonSerializer.DeserializeAsync<T>(stream, Options, token);
        }
        private Task<Stream> GetResponseStream(string endpoint, CancellationToken cancellationToken, params NamedValue[] parameters) => Client.GetStreamAsync(BuildEndpoint(endpoint, parameters));

        private static string BuildEndpoint(string endpoint, params NamedValue[] parameters) => $"{endpoint}?{parameters.QueryString()}";

        public void Dispose()
        {
            Client.Dispose();
            GC.SuppressFinalize(this);
        }

        ~NookipediaAsyncClient() => Client.Dispose();
    }
}
