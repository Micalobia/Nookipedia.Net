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
    public sealed class NookipediaClient : IDisposable
    {
        private readonly HttpClient _client;
        private HttpClient Client => _client;
        private readonly bool _shouldDispose;
        private static JsonSerializerOptions Options { get; }

        static NookipediaClient()
        {
            Options = new();
            Options.Converters.Add<RegionedListJsonConverter<string>>();
            Options.Converters.Add<RegionedListJsonConverter<Bug>>();
            Options.Converters.Add<RegionedListJsonConverter<Fish>>();
            Options.Converters.Add<RegionedListJsonConverter<SeaCreature>>();
        }

        public NookipediaClient(string apikey, HttpClient? client = null, bool disposeClient = true)
        {
            _shouldDispose = disposeClient;
            _client = client ?? new HttpClient();
            Client.BaseAddress ??= new Uri("https://api.nookipedia.com");
            Client.DefaultRequestHeaders.Add("Accept-Version", NookipediaAPIVersion.ToString(3));
            Client.DefaultRequestHeaders.Add("X-API-KEY", apikey);
        }

        public Task<Bug> GetBugAsync(string name) => FetchAsync<Bug>(Bug.Endpoint(name));
        public Task<Bug[]> GetBugsAsync() => FetchAsync<Bug[]>(Bug.Endpoint());
        public Task<RegionedList<Bug>> GetBugsAsync(Month month) => FetchAsync<RegionedList<Bug>>(Bug.Endpoint(), ("month", month.EnumMember()));
        public Task<string[]> GetBugNamesAsync() => FetchAsync<string[]>(Bug.Endpoint(), ("excludedetails", true));
        public Task<RegionedList<string>> GetBugNamesAsync(Month month) => FetchAsync<RegionedList<string>>(Bug.Endpoint(), ("month", month.EnumMember()), ("excludedetails", true));

        public Task<Fish> GetFishAsync(string name) => FetchAsync<Fish>(Fish.Endpoint(name));
        public Task<Fish[]> GetFishAsync() => FetchAsync<Fish[]>(Fish.Endpoint());
        public Task<RegionedList<Fish>> GetFishAsync(Month month) => FetchAsync<RegionedList<Fish>>(Fish.Endpoint(), ("month", month.EnumMember()));
        public Task<string[]> GetFishNamesAsync() => FetchAsync<string[]>(Fish.Endpoint(), ("excludedetails", true));
        public Task<RegionedList<string>> GetFishNamesAsync(Month month) => FetchAsync<RegionedList<string>>(Fish.Endpoint(), ("month", month.EnumMember()), ("excludedetails", true));

        public Task<SeaCreature> GetSeaCreatureAsync(string name) => FetchAsync<SeaCreature>(SeaCreature.Endpoint(name));
        public Task<SeaCreature[]> GetSeaCreaturesAsync() => FetchAsync<SeaCreature[]>(SeaCreature.Endpoint());
        public Task<RegionedList<SeaCreature>> GetSeaCreaturesAsync(Month month) => FetchAsync<RegionedList<SeaCreature>>(SeaCreature.Endpoint(), ("month", month.EnumMember()));
        public Task<string[]> GetSeaCreatureNamesAsync() => FetchAsync<string[]>(SeaCreature.Endpoint(), ("excludedetails", true));
        public Task<RegionedList<string>> GetSeaCreatureNamesAsync(Month month) => FetchAsync<RegionedList<string>>(SeaCreature.Endpoint(), ("month", month.EnumMember()), ("excludedetails", true));

        public Task<Recipe> GetRecipeAsync(string name) => FetchAsync<Recipe>(Recipe.Endpoint(name));
        public Task<Recipe[]> GetRecipesAsync(params string[] materials)
            => FetchAsync<Recipe[]>(Recipe.Endpoint(), materials.Select(x => new NamedValue("material", x)).ToArray());
        public Task<string[]> GetRecipeNamesAsync(params string[] materials)
            => FetchAsync<string[]>(Recipe.Endpoint(), materials.Select(x => new NamedValue("material", x)).Extend(("excludedetails", true)).ToArray());

        public Task<Artwork> GetArtworkAsync(string name) => FetchAsync<Artwork>(Artwork.Endpoint(name));
        public Task<Artwork[]> GetArtworksAsync() => FetchAsync<Artwork[]>(Artwork.Endpoint());
        public Task<Artwork[]> GetArtworksAsync(bool hasFake) => FetchAsync<Artwork[]>(Artwork.Endpoint(), ("hasfake", hasFake));
        public Task<string[]> GetArtworkNamesAsync() => FetchAsync<string[]>(Artwork.Endpoint(), ("excludedetails", true));
        public Task<string[]> GetArtworkNamesAsync(bool hasFake) => FetchAsync<string[]>(Artwork.Endpoint(), ("hasfake", hasFake), ("excludedetails", true));

        public Task<Villager[]> GetVillagersAsync() => FetchAsync<Villager[]>(Villager.Endpoint());
        public Task<Villager[]> GetVillagersAsync(string name) => FetchAsync<Villager[]>(Villager.Endpoint(), ("name", name));
        public Task<Villager[]> GetVillagersAsync(string? name = null, Species species = Species.None, Personality personality = Personality.None, string? birthmonth = null, int birthday = -1, bool includeNHDetails = false, params Game[] games)
            => FetchAsync<Villager[]>(Villager.Endpoint(), BuildVillagerQuery(name, species, personality, birthmonth, birthday, includeNHDetails, games).ToArray());
        public Task<string[]> GetVillagerNamesAsync() => FetchAsync<string[]>(Villager.Endpoint());
        public Task<string[]> GetVillagerNamesAsync(string name) => FetchAsync<string[]>(Villager.Endpoint(), ("name", name));
        public Task<string[]> GetVillagerNamesAsync(string? name = null, Species species = Species.None, Personality personality = Personality.None, string? birthmonth = null, int birthday = -1, bool includeNHDetails = false, params Game[] games)
            => FetchAsync<string[]>(Villager.Endpoint(), BuildVillagerQuery(name, species, personality, birthmonth, birthday, includeNHDetails, games).Extend(("excludedetails", true)).ToArray());

        private static IEnumerable<NamedValue> BuildVillagerQuery(string? name = null, Species species = Species.None, Personality personality = Personality.None, string? birthmonth = null, int birthday = -1, bool includeNHDetails = false, params Game[] games)
        {
            if (name.Exists()) yield return ("name", name);
            if (personality != Personality.None) yield return ("personality", personality.EnumMember());
            if (birthmonth.Exists()) yield return ("birthmonth", birthmonth);
            if (birthday > 0 && birthday <= 31) yield return ("birthday", birthday);
            if (includeNHDetails) yield return ("nhdetails", "true");
            if (species != Species.None) yield return ("species", species.EnumMember());
            foreach (Game game in games) yield return ("game", game.EnumMember());
        }

        #region Fetch
        private Task<T> FetchAsync<T>(string endpoint, params NamedValue[] parameters) => FetchAsync<T>(endpoint, default, parameters);
        private async Task<T> FetchAsync<T>(string endpoint, CancellationToken token, params NamedValue[] parameters)
        {
            using Stream stream = await GetResponseStreamAsync(endpoint, token, parameters);
            return (await JsonSerializer.DeserializeAsync<T>(stream, Options, token)) ?? throw new NookipediaException("Return was null; Report to Nookipedia.Net");
        }

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
        #endregion

        public void Dispose()
        {
            if (_shouldDispose)
                Client.Dispose();
            GC.SuppressFinalize(this);
        }

        ~NookipediaClient()
        {
            if (_shouldDispose)
                Client.Dispose();
        }
    }
}
