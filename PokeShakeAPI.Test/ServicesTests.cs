using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Moq;
using PokeShakeAPI.Services;
using System.Threading.Tasks;
using Xunit;

namespace PokeShakeAPI.Test
{
    public class ServicesTests
    {
        [Theory]
        [InlineData("charizard")]
        [InlineData("")]
        [InlineData("123")]
        [InlineData("charlie chaplin")]
        [InlineData("&%/()$£^")]
        public async Task GetDescription(string itemName)
        {
            var pokeOptions = new PokeOptions
            {
                DefaultSiteURL = "https://pokeapi.co",
                SpeciesEndpoint = "/api/v2/pokemon-species/",
                UserAgent = "PokeShakeAPI (https://gitlab.com/galloverde11/PokeShakeAPI or a fork of it)",
                RandomDescription = false,
                UseCache = true,
                CacheSuffix = "pokecache_",
                ResourceNotFoundMsg = "It was not possible to get the description"
            };
            var options = new Mock<IOptions<PokeOptions>>();
            options.Setup(x => x.Value).Returns(pokeOptions);
            var cache = new MemoryCache(new MemoryCacheOptions());

            var pokeService = new PokeService(options.Object, cache);
            string t = await pokeService.GetDescription(itemName);

            // Making multiple calls to check the cache is working
            //t = await pokeService.GetDescription(itemName);
            //t = await pokeService.GetDescription(itemName);
            //t = await pokeService.GetDescription(itemName);
            //t = await pokeService.GetDescription(itemName);
            //t = await pokeService.GetDescription(itemName);
            //t = await pokeService.GetDescription(itemName);
            //t = await pokeService.GetDescription(itemName);
            //t = await pokeService.GetDescription(itemName);
            //t = await pokeService.GetDescription(itemName);
            //t = await pokeService.GetDescription(itemName);
            Assert.NotNull(t);
        }

        [Theory]
        [InlineData("You gave Mr. Tim a hearty meal, but unfortunately what he ate made him die.")]
        [InlineData("")]
        [InlineData("123")]
        [InlineData("charlie chaplin")]
        [InlineData("&%/()$£^")]
        public async Task GetTranslation(string itemName)
        {
            var shakeOptions = new ShakeOptions
            {
                DefaultSiteURL = "https://api.funtranslations.com",
                SpeciesEndpoint = "/translate/shakespeare.json?text=",
                UserAgent = "PokeShakeAPI (https://gitlab.com/galloverde11/PokeShakeAPI or a fork of it)",
                RandomDescription = false,
                UseCache = false,
                CacheSuffix = "shakecache_",
                ResourceNotFoundMsg = "It was not possible to get a translation"
            };
            var options = new Mock<IOptions<ShakeOptions>>();
            options.Setup(x => x.Value).Returns(shakeOptions);
            var cache = new MemoryCache(new MemoryCacheOptions());

            var shakeService = new ShakeService(options.Object, cache);
            string t = await shakeService.GetTranslation(itemName);
            Assert.NotNull(t);
        }
    }
}
