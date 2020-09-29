using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PokeShakeAPI.Services;
using System.Threading.Tasks;

namespace PokeShakeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly ILogger<PokemonController> _logger;
        private readonly IPokeService _pokeService;
        private readonly IShakeService _shakeService;

        public PokemonController(ILogger<PokemonController> logger, IPokeService pokeService, IShakeService shakeService)
        {
            _logger = logger;
            _pokeService = pokeService;
            _shakeService = shakeService;
        }

        // GET pokemon/inputName
        [HttpGet("{inputName}")]
        public async Task<string> Get(string inputName)
        {
            string description = await _pokeService.GetDescription(inputName);
            string translation = await _shakeService.GetTranslation(description);
            return translation;
        }
    }
}
