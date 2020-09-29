using LitJson;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace PokeShakeAPI.Services
{
    public class ShakeService : ServiceAPI, IShakeService
    {
        private readonly ShakeOptions _options;
        private readonly IMemoryCache _cache;
        public ShakeService(IOptions<ShakeOptions> options, IMemoryCache cache)
        {
            _options = options.Value;
            _cache = cache;
            //DefaultBaseURL = _options.DefaultSiteURL + _options.SpeciesEndpoint;
            client.DefaultRequestHeaders.UserAgent.ParseAdd(_options.UserAgent);
        }

        public async Task<string> GetTranslation(string inputText)
        {
            if (_options.UseCache)
            {
                return await _cache.GetOrCreateAsync<string>(_options.CacheSuffix + inputText,
                        async cacheEntry =>
                        {
                            return await GetCacheableTranslation(inputText);
                        });
            }
            else
                return await GetCacheableTranslation(inputText);


        }
        public async Task<string> GetCacheableTranslation(string inputText)
        {
            if (!string.IsNullOrEmpty(inputText))
                inputText = RemoveEscapeSequences(inputText);
            JsonData jsonData = JsonMapper.ToObject(await GetStringAsync(_options.DefaultSiteURL + _options.SpeciesEndpoint, inputText));
            if ((int)jsonData["success"]["total"] >= 1)
                return (string)jsonData["contents"]["translated"];
            else
                return _options.TranslationNotFoundMsg;
        }

        private static string RemoveEscapeSequences(string sText)
        {

            sText = sText.Replace("\a", " "); // Warning
            sText = sText.Replace("\b", " "); // BACKSPACE
            sText = sText.Replace("\f", " "); // Form-feed
            sText = sText.Replace("\n", " "); // Line reverse
            sText = sText.Replace("\r", " "); // Carriage return
            sText = sText.Replace("\t", " "); // Horizontal tab
            sText = sText.Replace("\v", " "); // Vertical tab
            sText = sText.Replace("\'", " "); // Single quote
            sText = sText.Replace("\"", " "); // Double quote
            sText = sText.Replace("\\", " "); // Backslash

            return sText;
        }
    }

    public interface IShakeService
    {
        public Task<string> GetTranslation(string inputText);
    }
}
