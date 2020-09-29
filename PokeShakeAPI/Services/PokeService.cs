using LitJson;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using PokeAPI;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PokeShakeAPI.Services
{
    public class PokeService : ServiceAPI, IPokeService
    {
        private readonly PokeOptions _options;
        private readonly IMemoryCache _cache;
        public PokeService(IOptions<PokeOptions> options, IMemoryCache cache)
        {
            _options = options.Value;
            _cache = cache;
            client.DefaultRequestHeaders.UserAgent.ParseAdd(_options.UserAgent);
        }
        public async Task<string> GetDescription(string speciesName)
        {
            string flavorText = null;
            if (!_options.RandomDescription)
            {
                if (_options.UseCache)
                {
                    flavorText = await _cache.GetOrCreateAsync<string>(_options.CacheSuffix + speciesName,
                        async cacheEntry =>
                        {
                            return await GetCacheableDescription(speciesName);
                        });
                }
                else
                {
                    flavorText = await GetCacheableDescription(speciesName);
                }
            }
            else
            {
                var species = await GetSpecies(speciesName);
                Random random = new Random();
                var englishFlavors = species.FlavorTexts.Where(f => f.Language.Name == "en");
                flavorText = englishFlavors.ElementAt(random.Next(englishFlavors.Count() - 1)).FlavorText;
            }
            if (flavorText == null)
                throw new Exception(_options.ResourceNotFoundMsg);
            return flavorText;
        }

        private async Task<string> GetCacheableDescription(string speciesName)
        {
            var species = await GetSpecies(speciesName);
            string cachedText = species.FlavorTexts.Where(f => f.Language.Name == "en" && f.Version.Name == "ruby").FirstOrDefault().FlavorText;
            if (cachedText == null)
                cachedText = species.FlavorTexts.Where(f => f.Language.Name == "en").FirstOrDefault().FlavorText;
            return cachedText;
        }

        private async Task<PokemonSpecies> GetSpecies(string speciesName)
        {
            var res = JsonMapper.ToObject<PokemonSpecies>(await GetStringAsync(_options.DefaultSiteURL + _options.SpeciesEndpoint, speciesName));
            return res;
        }

        //private async Task<string> GetStringAsync(string path)
        //{
        //    var url = DefaultBaseURL + path;
        //    if (path.StartsWith(DefaultBaseURL))
        //    {
        //        url = path;
        //    }

        //    return await client.GetStringAsync(url);
        //}
    }

    public interface IPokeService
    {
        public Task<string> GetDescription(string speciesName);
    }
}
