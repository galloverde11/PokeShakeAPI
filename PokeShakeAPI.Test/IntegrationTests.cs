using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PokeShakeAPI.Controllers;
using PokeShakeAPI.Models;
using PokeShakeAPI.Services;
using System.Threading.Tasks;
using Xunit;

namespace PokeShakeAPI.Test
{
    public class IntegrationTests
    {
        [Theory]
        [InlineData("charizard", "CHARIZARD flies around the sky in search of powerful opponents. It breathes fire of such great heat that it melts anything. However, it never turns its fiery breath on any opponent weaker than itself.", "Charizard flies 'round the sky in search of powerful opponents. 't breathes fire of such most wondrous heat yond 't melts aught. However,  't nev'r turns its fiery breath on any opponent weaker than itself.")]
        [InlineData("123", "123", "2343")]
        [InlineData("red ronnie", "charlie chaplin", "tom cruise")]
        [InlineData("&%/()$£^", "&%/()$£^", "£%$&$££")]
        public async Task GetTranslatedDescription(string inputName, string description, string translatedDesc)
        {
            // Arrange
            var mockLogger = new Mock<ILogger<PokemonController>>();
            var mockPokeService = new Mock<IPokeService>();
            mockPokeService.Setup(repo => repo.GetDescription(inputName))
                .Returns(Task.FromResult(description));
            var mockShakeService = new Mock<IShakeService>();
            mockShakeService.Setup(repo => repo.GetTranslation(description))
                .Returns(Task.FromResult(translatedDesc));

            var controller = new PokemonController(mockLogger.Object, mockPokeService.Object, mockShakeService.Object);

            // Act
            var result = await controller.Get(inputName);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ReturnModel>>(result);
            var returnValue = Assert.IsType<ReturnModel>(actionResult.Value);
            Assert.NotNull(returnValue);
            Assert.NotEmpty(returnValue.Description);
        }

        [Theory]
        [InlineData("", "", "")]
        public async Task GetTranslatedDescription_empty(string inputName, string description, string translatedDesc)
        {
            // Arrange
            var mockLogger = new Mock<ILogger<PokemonController>>();
            var mockPokeService = new Mock<IPokeService>();
            mockPokeService.Setup(repo => repo.GetDescription(inputName))
                .Returns(Task.FromResult(description));
            var mockShakeService = new Mock<IShakeService>();
            mockShakeService.Setup(repo => repo.GetTranslation(description))
                .Returns(Task.FromResult(translatedDesc));

            var controller = new PokemonController(mockLogger.Object, mockPokeService.Object, mockShakeService.Object);

            // Act
            var result = await controller.Get(inputName);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ReturnModel>>(result);
            var returnValue = Assert.IsType<ReturnModel>(actionResult.Value);
            Assert.NotNull(returnValue);
            Assert.Empty(returnValue.Description);
        }
    }
}
