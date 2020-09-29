using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PokeShakeAPI.Services
{
    public class ServiceAPI
    {
        internal readonly HttpClient client = new HttpClient();
        internal async Task<string> GetStringAsync(string baseUrl, string path)
        {
            string encodedPath = Uri.EscapeDataString(path);
            var url = baseUrl + encodedPath;
            if (path.StartsWith(baseUrl))
            {
                url = encodedPath;
            }

            return await client.GetStringAsync(url);
        }
    }
}
