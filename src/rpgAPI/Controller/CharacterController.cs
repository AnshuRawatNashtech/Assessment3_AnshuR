using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace rpgAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet]
        [ExcludeFromCodeCoverage]
        public ActionResult<ServiceResponse<List<Character>>> GetCharacter()
        {
            return Ok(_characterService.GetAllCharacter());
        }


        [HttpGet("id")]
        [ExcludeFromCodeCoverage]
        public ActionResult<ServiceResponse<Character>> GetId(int id)
        {
            return Ok(_characterService.GetCharacterById(id));
        }

        [HttpPost]
        [ExcludeFromCodeCoverage]
        public ActionResult<ServiceResponse<List<Character>>> PostCharacter(Character newCharacter)
        {
            return Ok(_characterService.AddCharacter(newCharacter));
        }

        [HttpPut("{id}")]
        public ActionResult<ServiceResponse<List<Character>>> PutCharacter(int id, [FromBody] Character updatedCharacter)
        {
            return Ok(_characterService.UpdateCharacter(id,updatedCharacter));
        }

        [HttpDelete("{id}")]
        public ActionResult<ServiceResponse<List<Character>>> DeleteCharacter(int id)
        {
            return Ok(_characterService.DeleteCharacter(id));
        }
    }
}