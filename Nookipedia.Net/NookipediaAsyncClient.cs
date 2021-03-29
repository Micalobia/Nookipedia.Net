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

        public NookipediaAsyncClient(string apikey, HttpClient? client = null)
        {
            _client = client ?? new HttpClient();
            Client.BaseAddress ??= new Uri("https://api.nookipedia.com");
            Client.DefaultRequestHeaders.Add("Accept-Version", NookipediaAPIVersion.ToString(3));
            Client.DefaultRequestHeaders.Add("X-API-KEY", apikey);
        }

        public Task<T?> GetCritterAsync<T>(string name) where T : Critter, new() => FetchSingleAsync<T>(name);
        public Task<T[]?> GetCrittersAsync<T>() where T : Critter, new() => FetchListAsync<T>();
        public Task<T[]?> GetCrittersAsync<T>(string month) where T : Critter, new() => FetchListAsync<T>(("month", month));
        public Task<string[]?> GetCritterNamesAsync<T>() where T : Critter, new() => FetchNamesAsync<T>();
        public Task<string[]?> GetCritterNamesAsync<T>(string month) where T : Critter, new() => FetchNamesAsync<T>(("month", month));

        public Task<Optional<T>> TryGetCritterAsync<T>(string name) where T : Critter, new() => TryFetchSingleAsync<T>(name);
        public Task<Optional<T[]>> TryGetCrittersAsync<T>() where T : Critter, new() => TryFetchListAsync<T>();
        public Task<Optional<T[]>> TryGetCrittersAsync<T>(string month) where T : Critter, new() => TryFetchListAsync<T>(("month", month));
        public Task<Optional<string[]>> TryGetCritterNamesAsync<T>() where T : Critter, new() => TryFetchNamesAsync<T>();
        public Task<Optional<string[]>> TryGetCritterNamesAsync<T>(string month) where T : Critter, new() => TryFetchNamesAsync<T>(("month", month));

        public Task<Artwork?> GetArtworkAsync(string name) => FetchSingleAsync<Artwork>(name);
        public Task<Artwork[]?> GetArtworksAsync() => FetchListAsync<Artwork>();
        public Task<Artwork[]?> GetArtworksAsync(bool hasfake) => FetchListAsync<Artwork>(("hasfake", hasfake));
        public Task<string[]?> GetArtworkNamesAsync() => FetchNamesAsync<Artwork>();
        public Task<string[]?> GetArtworkNamesAsync(bool hasfake) => FetchNamesAsync<Artwork>(("hasfake", hasfake));

        public Task<Optional<Artwork>> TryGetArtworkAsync(string name) => TryFetchSingleAsync<Artwork>(name);
        public Task<Optional<Artwork[]>> TryGetArtworksAsync() => TryFetchListAsync<Artwork>();
        public Task<Optional<Artwork[]>> TryGetArtworksAsync(bool hasfake) => TryFetchListAsync<Artwork>(("hasfake", hasfake));
        public Task<Optional<string[]>> TryGetArtworkNames() => TryFetchNamesAsync<Artwork>();
        public Task<Optional<string[]>> TryGetArtworkNames(bool hasfake) => TryFetchNamesAsync<Artwork>(("hasfake", hasfake));

        public Task<Recipe?> GetRecipeAsync(string name) => FetchSingleAsync<Recipe>(name);
        public Task<Recipe[]?> GetRecipesAsync(params string[] materials) => FetchListAsync<Recipe>(materials.Select(x => new NamedValue("material", x)).ToArray());
        public Task<string[]?> GetRecipeNamesAsync(params string[] materials) => FetchNamesAsync<Recipe>(materials.Select(x => new NamedValue("material", x)).ToArray());

        public Task<Optional<Recipe>> TryGetRecipeAsync(string name) => TryFetchSingleAsync<Recipe>(name);
        public Task<Optional<Recipe[]>> TryGetRecipesAsync(params string[] materials) => TryFetchListAsync<Recipe>(materials.Select(x => new NamedValue("material", x)).ToArray());
        public Task<Optional<string[]>> TryGetRecipeNamesAsync(params string[] materials) => TryFetchNamesAsync<Recipe>(materials.Select(x => new NamedValue("material", x)).ToArray());

        public Task<Villager[]?> GetVillagersAsync() => FetchListAsync<Villager>();
        public Task<Villager[]?> GetVillagersAsync(string name) => FetchListAsync<Villager>(("name", name));
        public Task<Villager[]?> GetVillagersAsync(string? name = null, Personality personality = Personality.None, string? birthmonth = null, int birthday = -1, bool includeNHDetails = false, params Game[] games)
            => FetchListAsync<Villager>(BuildVillagerQuery(name, personality, birthmonth, birthday, includeNHDetails, games));
        public Task<string[]?> GetVillagerNamesAsync() => FetchNamesAsync<Villager>();
        public Task<string[]?> GetVillagerNamesAsync(string name) => FetchNamesAsync<Villager>(("name", name));
        public Task<string[]?> GetVillagerNamesAsync(string? name = null, Personality personality = Personality.None, string? birthmonth = null, int birthday = -1, params Game[] games)
            => FetchNamesAsync<Villager>(BuildVillagerQuery(name, personality, birthmonth, birthday, false, games));

        public Task<Optional<Villager[]>> TryGetVillagersAsync() => TryFetchListAsync<Villager>();
        public Task<Optional<Villager[]>> TryGetVillagersAsync(string name) => TryFetchListAsync<Villager>(("name", name));
        public Task<Optional<Villager[]>> TryGetVillagersAsync(string? name = null, Personality personality = Personality.None, string? birthmonth = null, int birthday = -1, bool includeNHDetails = false, params Game[] games)
            => TryFetchListAsync<Villager>(BuildVillagerQuery(name, personality, birthmonth, birthday, includeNHDetails, games));
        public Task<Optional<string[]>> TryGetVillagerNamesAsync() => TryFetchNamesAsync<Villager>();
        public Task<Optional<string[]>> TryGetVillagerNamesAsync(string name) => TryFetchNamesAsync<Villager>(("name", name));
        public Task<Optional<string[]>> TryGetVillagerNamesAsync(string? name = null, Personality personality = Personality.None, string? birthmonth = null, int birthday = -1, params Game[] games)
            => TryFetchNamesAsync<Villager>(BuildVillagerQuery(name, personality, birthmonth, birthday, false, games));

        private static NamedValue[] BuildVillagerQuery(string? name = null, Personality personality = Personality.None, string? birthmonth = null, int birthday = -1, bool includeNHDetails = false, params Game[] games)
        {
            IList<NamedValue> ret = new List<NamedValue>();
            if (name.Exists()) ret.Add(("name", name));
            if (personality != Personality.None) ret.Add(("personality", personality.Value()));
            if (birthmonth.Exists()) ret.Add(("birthmonth", birthmonth));
            if (birthday > 0 && birthday <= 31) ret.Add(("birthday", birthday));
            if (includeNHDetails) ret.Add(("nhdetails", "true"));
            return ret.WithRange(games, game => ("game", game)).ToArray();
        }

        private Task<string[]?> FetchNamesAsync<T>(params NamedValue[] parameters) where T : IListEndpoint, new() => FetchAsync<string[]>(ListEndpoint<T>.Endpoint(), parameters.Concat(("excludedetails", "true")));
        private Task<T[]?> FetchListAsync<T>(params NamedValue[] parameters) where T : IListEndpoint, new() => FetchAsync<T[]>(ListEndpoint<T>.Endpoint(), parameters);
        private Task<T?> FetchSingleAsync<T>(string name, params NamedValue[] parameters) where T : ISingleEndpoint, new() => FetchAsync<T>(SingleEndpoint<T>.Endpoint(name), parameters);
        private Task<T?> FetchAsync<T>(string endpoint, params NamedValue[] parameters) => FetchAsync<T>(endpoint, default, parameters);
        private async Task<T?> FetchAsync<T>(string endpoint, CancellationToken token, params NamedValue[] parameters)
        {
            using Stream stream = await GetResponseStreamAsync(endpoint, token, parameters);
            return await JsonSerializer.DeserializeAsync<T>(stream, Options, token);
        }

        private Task<Optional<string[]>> TryFetchNamesAsync<T>(params NamedValue[] parameters) where T : IListEndpoint, new() => TryFetchAsync<string[]>(ListEndpoint<T>.Endpoint(), parameters.Concat(("excludedetails", "true")));
        private Task<Optional<T[]>> TryFetchListAsync<T>(params NamedValue[] parameters) where T : IListEndpoint, new() => TryFetchAsync<T[]>(ListEndpoint<T>.Endpoint(), parameters);
        private Task<Optional<T>> TryFetchSingleAsync<T>(string name, params NamedValue[] parameters) where T : ISingleEndpoint, new() => TryFetchAsync<T>(SingleEndpoint<T>.Endpoint(name), parameters);
        private Task<Optional<T>> TryFetchAsync<T>(string endpoint, params NamedValue[] parameters) => TryFetchAsync<T>(endpoint, default, parameters);
        private async Task<Optional<T>> TryFetchAsync<T>(string endpoint, CancellationToken token, params NamedValue[] parameters)
        {
            try
            {
                return await FetchAsync<T>(endpoint, token, parameters);
            }
            catch (ArgumentNullException err) { throw new NookipediaException("Something went horribly wrong; Report to Nookipedia.Net", err); }
            catch (JsonException err) { throw new NookipediaException("Something went horribly wrong; Report to Nookipedia.Net", err); }
            catch (NotSupportedException err) { throw new NookipediaException("Something went horribly wrong; Report to Nookipedia.Net", err); }
            catch (OutOfMemoryException) { throw; }
            catch (TaskCanceledException) { throw; }
            catch { return Optional<T>.Empty(); }
        }

        private Task<Stream> GetResponseStreamAsync(string endpoint, CancellationToken cancellationToken, params NamedValue[] parameters) => Client.GetStreamAsync(BuildEndpoint(endpoint, parameters), cancellationToken);

        private static string BuildEndpoint(string endpoint, params NamedValue[] parameters) => $"{endpoint}?{parameters.QueryString()}";

        public void Dispose()
        {
            Client.Dispose();
            GC.SuppressFinalize(this);
        }

        ~NookipediaAsyncClient() => Client.Dispose();
    }
}
