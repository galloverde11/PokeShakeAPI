using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PokeShakeAPI.Models;
using PokeShakeAPI.Services;
using System;
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
        public async Task<ActionResult<ReturnModel>> Get(string inputName)
        {
            string description = null;
            string translation = null;
            try
            {
                description = await _pokeService.GetDescription(inputName);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in retrieving pokemon description");
                return NotFound();
            }

            try
            {
                translation = await _shakeService.GetTranslation(description);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in traslating the description");
                return NotFound();
            }

            var res = new ReturnModel();
            res.Name = inputName;
            res.Description = translation;
            return res;
        }
    }
}
