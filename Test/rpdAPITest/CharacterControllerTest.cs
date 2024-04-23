using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using rpgAPI.Controller;
using rpgAPI.Model;
using rpgAPI.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpgAPITest
{
    public class CharacterControllerTest
    {
        [Fact]
        public void PutCharacter_InputCharacter_ReturnSuccessResponse()
        {
            // Arrange
            var mockCharacterService = new Mock<ICharacterService>();
            var controller = new CharacterController(mockCharacterService.Object);
            var updatedCharacter = new Character
            {
                Id = 2,
                Name = "Aron Edward",
                HitPoint = 15,
                Strength = 22,
                Defense = 10,
                Intelligence = 130,
                CharacterClass = RPGClass.Knight
            };
            var serviceResponse = new ServiceResponse<List<Character>>
            {
                Success = true,
                Data = new List<Character> { updatedCharacter }
            };
            mockCharacterService.Setup(service => service.UpdateCharacter(updatedCharacter.Id,updatedCharacter)).Returns(serviceResponse);

            // Act
            var result = controller.PutCharacter(updatedCharacter.Id, updatedCharacter);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ServiceResponse<List<Character>>>(okResult.Value);

            // Assert
            Assert.NotNull(result);
            Assert.True(response.Success);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal("Aron Edward", response.Data[0].Name);
        }

        [Fact]
        public void DeleteCharacter_InputCharacterId_ReturnResponse()
        {
            // Arrange
            var mockCharacterService = new Mock<ICharacterService>();
            var controller = new CharacterController(mockCharacterService.Object);
            var Id = 1;
            var serviceResponse = new ServiceResponse<List<Character>>
            {
                Success = true,
                Data = new List<Character>() // Assuming this list contains the characters after deletion
            };
            mockCharacterService.Setup(service => service.DeleteCharacter(Id)).Returns(serviceResponse);

            // Act
            var result = controller.DeleteCharacter(Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedServiceResponse = Assert.IsType<ServiceResponse<List<Character>>>(okResult.Value);
            Assert.True(returnedServiceResponse.Success);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }
    }
}
