using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rpgAPI.Service
{

    public interface ICharacterService
    {

        ServiceResponse<List<Character>> GetAllCharacter();

        ServiceResponse<List<Character>> AddCharacter(Character newCharacter);

        ServiceResponse<Character> GetCharacterById(int id);

        ServiceResponse<List<Character>> UpdateCharacter(int id,Character updatedCharacter);

        ServiceResponse<List<Character>> DeleteCharacter(int id);

    }
}