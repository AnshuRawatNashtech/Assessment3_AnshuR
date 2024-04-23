using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace rpgAPI.Service
{
    public class CharacterService : ICharacterService
    {


        private static List<Character> _characterList = new List<Character>()
        {
            new Character(),
            new Character(){Name = "Gollum", Id = 1},
            new Character(){Name = "Aron Galvin", Id = 2, HitPoint = 20, Defense = 40, CharacterClass=RPGClass.Cleric},
            new Character(){Name = "Katy Perry", Id = 3, Intelligence =80, Strength=20},
        };

        [ExcludeFromCodeCoverage]
        public ServiceResponse<List<Character>> GetAllCharacter()
        {
            var serviceResponse = new ServiceResponse<List<Character>>()
            {
                Data = _characterList
            };
            return serviceResponse;
        }

        [ExcludeFromCodeCoverage]
        public ServiceResponse<List<Character>> AddCharacter(Character newCharacter)
        { 
             _characterList.Add(newCharacter);
            
            var serviceResponse = new ServiceResponse<List<Character>>()
            {
                Data = _characterList
            };
            return serviceResponse;

        }

        [ExcludeFromCodeCoverage]
        public ServiceResponse<Character> GetCharacterById(int id)
        {
            var character = _characterList.FirstOrDefault(c=>c.Id==id);

            var serviceResponse = new ServiceResponse<Character>();

            if (character == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Id Doesn't Exist";

                return serviceResponse;
            }

            serviceResponse.Data = character;

            return serviceResponse;
        }

        public ServiceResponse<List<Character>> UpdateCharacter(int id, Character updatedCharacter)
        {
            var existingCharacter = _characterList.FirstOrDefault(c => c.Id == id);
            var serviceResponse = new ServiceResponse<List<Character>>();
            if (existingCharacter != null)
            {
                existingCharacter.Name = updatedCharacter.Name;
                existingCharacter.HitPoint = updatedCharacter.HitPoint;
                existingCharacter.Strength = updatedCharacter.Strength;
                existingCharacter.Defense = updatedCharacter.Defense;
                existingCharacter.Intelligence = updatedCharacter.Intelligence;
                existingCharacter.CharacterClass = updatedCharacter.CharacterClass;
                serviceResponse.Data = _characterList;
                serviceResponse.Success = true;
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Record not found with specified Id";
            }
            return serviceResponse;
        }

        public ServiceResponse<List<Character>> DeleteCharacter(int id)
        {
            var characterToRemove = _characterList.FirstOrDefault(c => c.Id == id);
            var serviceResponse = new ServiceResponse<List<Character>>();
            if (characterToRemove != null)
            {
                _characterList.Remove(characterToRemove);
                serviceResponse.Data = _characterList;
                serviceResponse.Success = true;
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Record does not exists with specified Id";
            }
            return serviceResponse;
        }

    }
}