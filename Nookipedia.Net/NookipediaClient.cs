using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using static Nookipedia.Net.NookipediaConstants;

namespace Nookipedia.Net
{
    public class NookipediaClient : IDisposable
    {
        private static readonly JsonSerializerOptions _options;
        private static JsonSerializerOptions Options => _options;

        static NookipediaClient() => _options = new JsonSerializerOptions();

        private readonly HttpClient _client;

        private HttpClient Client => _client;

        public NookipediaClient(string apikey, HttpClient? client = null)
        {
            _client = client ?? new HttpClient();
            Client.BaseAddress ??= new Uri("https://api.nookipedia.com");
            Client.DefaultRequestHeaders.Add("Accept-Version", NookipediaAPIVersion.ToString(3));
            Client.DefaultRequestHeaders.Add("X-API-KEY", apikey);
        }

        public T GetCritter<T>(string name) where T : Critter, new() => FetchSingle<T>(name);
        public T[] GetCritters<T>() where T : Critter, new() => FetchList<T>();
        public T[] GetCritters<T>(string month) where T : Critter, new() => FetchList<T>(("month", month));
        public string[] GetCritterNames<T>() where T : Critter, new() => FetchNames<T>();
        public string[] GetCritterNames<T>(string month) where T : Critter, new() => FetchNames<T>(("month", month));

        public Optional<T> TryGetCritter<T>(string name) where T : Critter, new() => TryFetchSingle<T>(name);
        public Optional<T[]> TryGetCritters<T>() where T : Critter, new() => TryFetchList<T>();
        public Optional<T[]> TryGetCritters<T>(string month) where T : Critter, new() => TryFetchList<T>(("month", month));
        public Optional<string[]> TryGetCritterNames<T>() where T : Critter, new() => TryFetchNames<T>();
        public Optional<string[]> TryGetCritterNames<T>(string month) where T : Critter, new() => TryFetchNames<T>(("month", month));

        public Artwork GetArtwork(string name) => FetchSingle<Artwork>(name);
        public Artwork[] GetArtworks() => FetchList<Artwork>();
        public Artwork[] GetArtworks(bool hasfake) => FetchList<Artwork>(("hasfake", hasfake));
        public string[] GetArtworkNames() => FetchNames<Artwork>();
        public string[] GetArtworkNames(bool hasfake) => FetchNames<Artwork>(("hasfake", hasfake));

        public Optional<Artwork> TryGetArtwork(string name) => TryFetchSingle<Artwork>(name);
        public Optional<Artwork[]> TryGetArtworks() => TryFetchList<Artwork>();
        public Optional<Artwork[]> TryGetArtworks(bool hasfake) => TryFetchList<Artwork>(("hasfake", hasfake));
        public Optional<string[]> TryGetArtworkNames() => TryFetchNames<Artwork>();
        public Optional<string[]> TryGetArtworkNames(bool hasfake) => TryFetchNames<Artwork>(("hasfake", hasfake));

        public Recipe GetRecipe(string name) => FetchSingle<Recipe>(name);
        public Recipe[] GetRecipes() => FetchList<Recipe>();
        public Recipe[] GetRecipes(params string[] materials) => FetchList<Recipe>(materials.Select(x => new NamedValue("material", x)).ToArray());
        public string[] GetRecipeNames() => FetchNames<Recipe>();
        public string[] GetRecipeNames(params string[] materials) => FetchNames<Recipe>(materials.Select(x => new NamedValue("material", x)).ToArray());

        public Optional<Recipe> TryGetRecipe(string name) => TryFetchSingle<Recipe>(name);
        public Optional<Recipe[]> TryGetRecipes() => TryFetchList<Recipe>();
        public Optional<Recipe[]> TryGetRecipes(params string[] materials) => TryFetchList<Recipe>(materials.Select(x => new NamedValue("material", x)).ToArray());
        public Optional<string[]> TryGetRecipeNames() => TryFetchNames<Recipe>();
        public Optional<string[]> TryGetRecipeNames(params string[] materials) => TryFetchNames<Recipe>(materials.Select(x => new NamedValue("material", x)).ToArray());

        public Villager[] GetVillagers() => FetchList<Villager>();
        public Villager[] GetVillagers(string name) => FetchList<Villager>(("name", name));
        public Villager[] GetVillagers(string? name = null, Personality personality = Personality.None, string? birthmonth = null, int birthday = -1, bool includeNHDetails = false, params Game[] games)
            => FetchList<Villager>(BuildVillagerQuery(name, personality, birthmonth, birthday, includeNHDetails, games));
        public string[] GetVillagerNames() => FetchNames<Villager>();
        public string[] GetVillagerNames(string name) => FetchNames<Villager>(("name", name));
        public string[] GetVillagerNames(string? name = null, Personality personality = Personality.None, string? birthmonth = null, int birthday = -1, params Game[] games)
            => FetchNames<Villager>(BuildVillagerQuery(name, personality, birthmonth, birthday, false, games));

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

        private string[] FetchNames<T>(params NamedValue[] parameters) where T : IListEndpoint, new() => Fetch<string[]>(ListEndpoint<T>.Endpoint(), parameters.Concat(("excludedetails", "true")));
        private T[] FetchList<T>(params NamedValue[] parameters) where T : IListEndpoint, new() => Fetch<T[]>(ListEndpoint<T>.Endpoint(), parameters);
        private T FetchSingle<T>(string name, params NamedValue[] parameters) where T : ISingleEndpoint, new() => Fetch<T>(SingleEndpoint<T>.Endpoint(name), parameters);
        private T Fetch<T>(string endpoint, params NamedValue[] parameters)
        {
            using Stream stream = GetResponseStream(endpoint, parameters);
            return JsonSerializer.Deserialize<T>(stream.ReadBytes(), Options) ?? throw new JsonException("Deserialized to null; Report to Nookipedia.Net");
        }

        private Optional<string[]> TryFetchNames<T>(params NamedValue[] parameters) where T : IListEndpoint, new() => TryFetch<string[]>(ListEndpoint<T>.Endpoint(), parameters.Concat(("excludedetails", "true")));
        private Optional<T[]> TryFetchList<T>(params NamedValue[] parameters) where T : IListEndpoint, new() => TryFetch<T[]>(ListEndpoint<T>.Endpoint(), parameters);
        private Optional<T> TryFetchSingle<T>(string name, params NamedValue[] parameters) where T : ISingleEndpoint, new() => TryFetch<T>(SingleEndpoint<T>.Endpoint(name), parameters);
        private Optional<T> TryFetch<T>(string endpoint, params NamedValue[] parameters)
        {
            try
            {
                return Fetch<T>(endpoint, parameters);
            }
            catch (ArgumentNullException err) { throw new NookipediaException("Something went horribly wrong; Report to Nookipedia.Net", err); }
            catch (JsonException err) { throw new NookipediaException("Something went horribly wrong; Report to Nookipedia.Net", err); }
            catch (NotSupportedException err) { throw new NookipediaException("Something went horribly wrong; Report to Nookipedia.Net", err); }
            catch (OutOfMemoryException) { throw; }
            catch
            {
                return Optional<T>.Empty();
            }
        }

        private Stream GetResponseStream(string endpoint, params NamedValue[] parameters) => Client.GetStreamAsync(BuildEndpoint(endpoint, parameters)).Result();

        private static string BuildEndpoint(string endpoint, params NamedValue[] parameters) => $"{endpoint}?{parameters.QueryString()}";

        public void Dispose() => Client.Dispose();

        ~NookipediaClient() => Dispose();
    }
}
