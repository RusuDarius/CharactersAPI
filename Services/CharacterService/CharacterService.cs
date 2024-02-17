using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatrickAPI.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>
        {
            new Character(),
            new Character { Name = "Bilbo", Id = 1 },
            new Character
            {
                Id = 2,
                Name = "Gandalf",
                HitPoints = 250,
                Defense = 200,
                Strength = 50,
                Intelligence = 450,
                Class = RpgClass.Mage
            }
        };

        private readonly IMapper _mapper;

        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(
            AddCharacterDto newCharacter
        )
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var characterToAdd = _mapper.Map<Character>(newCharacter);
            characterToAdd.Id = characters.Max(c => c.Id) + 1;
            characters.Add(characterToAdd);
            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            serviceResponse.Message = "Successfully added the new character!";
            return Task.FromResult(serviceResponse);
        }

        public Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return Task.FromResult(serviceResponse);
        }

        public Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var character = characters.FirstOrDefault(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            return Task.FromResult(serviceResponse);
        }

        public Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(
            UpdateCharacterDto updatedCharacter
        )
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                var character = characters.FirstOrDefault(c => c.Id == updatedCharacter.Id);

                if (character is null)
                    throw new Exception($"Character with id {updatedCharacter.Id} not found.");

                // _mapper.Map(updatedCharacter, character); & new map defined inside profile
                // _mapper.Map<Character>(updatedCharacter);

                character.Name = updatedCharacter.Name;
                character.HitPoints = updatedCharacter.HitPoints;
                character.Strength = updatedCharacter.Strength;
                character.Defense = updatedCharacter.Defense;
                character.Class = updatedCharacter.Class;
                character.Intelligence = updatedCharacter.Intelligence;

                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
                serviceResponse.Message = "Your character has been updated successfully!";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return Task.FromResult(serviceResponse);
        }
    }
}
