using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PatrickAPI.Dtos.Character;

namespace PatrickAPI.Controllers
{
    // Api controller attribute
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        // Field to avoid using this.characterService
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get()
        {
            return Ok(await _characterService.GetAllCharacters());
        }

        [HttpGet("id_of_character={id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingle(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));
        }

        [HttpPost("upload_character")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(
            AddCharacterDto newCharacter
        )
        {
            return Ok(await _characterService.AddCharacter(newCharacter));
        }

        [HttpPut("update_character")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> UpdateCharacter(
            UpdateCharacterDto updatedCharacter
        )
        {
            var response = await _characterService.UpdateCharacter(updatedCharacter);
            if (response.Data is null)
                return NotFound(response);
            return Ok(response);
        }
    }
}
