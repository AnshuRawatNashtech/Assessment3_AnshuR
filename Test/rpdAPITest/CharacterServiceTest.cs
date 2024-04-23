using rpgAPI.Model;
using rpgAPI.Service;

namespace rpgAPITest;

public class CharacterServiceTest
{
    private readonly CharacterService _characterService;
    public CharacterServiceTest()
    {
        _characterService = new CharacterService();
    }

      [Fact]
    public void UpdateCharacter_GivenValidInput_CharacterUpdated()
    {
        //Arrange
        var character = new Character()
        {
            Name = "New Gollum",
            Id = 2,
            Strength = 10,
            Intelligence =90,
            HitPoint =20,
            Defense = 30
        };
        
        //Act
        var response = _characterService.UpdateCharacter(character.Id,character);

        //Assert
        Assert.NotNull(response);
        Assert.True(response.Success);
        Assert.Equal("New Gollum", response.Data.FirstOrDefault(x => x.Id == 2).Name, ignoreCase: true);
    }

    [Fact]
    public void UpdateCharacter_GivenCharacterInput_CheckNameNotEmpty()
    {
        //Arrange
        var character = new Character()
        {
            Name = "Test Name",
            Id = 0,
        };

        //Act
        var response = _characterService.UpdateCharacter(character.Id, character);

        //Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Data.FirstOrDefault(x => x.Id == 0).Name);
        Assert.False(string.IsNullOrEmpty(response.Data.FirstOrDefault(x => x.Id == 0).Name));

    }

    [Fact]
    public void UpdateCharacter_GivenInvalidId_FailureResponse()
    {
        // Arrange
        var updatedCharacter = new Character
        {
            Id = 10, 
            Name = "Sakura"         
        };

        // Act
        var serviceResponse = _characterService.UpdateCharacter(updatedCharacter.Id,updatedCharacter);

        // Assert
        Assert.False(serviceResponse.Success);
        Assert.Equal("Record not found with specified Id", serviceResponse.Message);
    }

    [Fact]
    public void UpdateCharacter_GivenInputWithNumericProperties_CheckInputRange()
    {
        //Arrange
        var character = new Character()
        {
            Name = "Aron Galvin",
            Id = 2,
            Strength = 30,
            HitPoint = 30,
            Defense = 30,
            Intelligence = 120,
            CharacterClass = RPGClass.Elf
        };

        //Act
        var response = _characterService.UpdateCharacter(character.Id, character);
        var result = response.Data.FirstOrDefault(c => c.Id == 2);
        //Assert
        Assert.True(result.HitPoint > 0);
        Assert.True(result.Defense > 0); 
        Assert.True(result.Strength > 0);
        Assert.InRange(result.Intelligence, 1, 500); //assuming the IQ level max is 500
    }

    [Fact]
    public void DeleteCharacter_GivenValidCharacterId_DeleteCharacterRecord()
    {
        // Arrange
        var characterService = new CharacterService();

        // Act
        var response = characterService.DeleteCharacter(1);

        // Assert
        Assert.True(response.Success);
        Assert.DoesNotContain(response.Data, c => c.Id == 1);
    }

    [Fact]
    public void DeleteCharacter_GivenInvalidCharacterId_ReturnError()
    {
        // Arrange
        var characterService = new CharacterService();

        // Act
        var response = characterService.DeleteCharacter(99);

        // Assert
        Assert.False(response.Success);
        Assert.Equal("Record does not exists with specified Id", response.Message);
    }


}