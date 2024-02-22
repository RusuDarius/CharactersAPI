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
            },
            new Character
            {
                Id = 3,
                Name = "Bigdan",
                HitPoints = 200,
                Defense = 10,
                Strength = 500,
                Intelligence = 300,
                Class = RpgClass.Barbarian
            }
        };

        private readonly IMapper _mapper;

        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        //* Add a character with POST request
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(
            AddCharacterDto newCharacter
        )
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var characterToAdd = _mapper.Map<Character>(newCharacter);
            var DbCharacters = await _context.Characters.ToListAsync();

            characterToAdd.Id = DbCharacters.Max(c => c.Id) + 1;
            DbCharacters.Add(characterToAdd);
            serviceResponse.Data = DbCharacters
                .Select(c => _mapper.Map<GetCharacterDto>(c))
                .ToList();
            serviceResponse.Message = "Successfully added the new character!";
            return (serviceResponse);
        }

        //* Delete 1 single character by id and return the rest of the characters in a list wit DELETE request
        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var DbCharacters = await _context.Characters.ToListAsync();
            try
            {
                var character = DbCharacters.FirstOrDefault(c => c.Id == id);

                if (character is null)
                    throw new Exception($"Character with id {id} not found.");

                DbCharacters.Remove(character);

                serviceResponse.Data = DbCharacters
                    .Select(c => _mapper.Map<GetCharacterDto>(c))
                    .ToList();
                serviceResponse.Message = "Your character has been removed successfully!";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return (serviceResponse);
        }

        //* Get all existing characters with GET request
        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var DbCharacters = await _context.Characters.ToListAsync();
            serviceResponse.Data = DbCharacters
                .Select(c => _mapper.Map<GetCharacterDto>(c))
                .ToList();
            return (serviceResponse);
        }

        //* Get a single character with GET request
        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var DbCharacters = await _context.Characters.ToListAsync();
            var character = DbCharacters.FirstOrDefault(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            return (serviceResponse);
        }

        //* Updating a character with PUT request
        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(
            UpdateCharacterDto updatedCharacter
        )
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var DbCharacters = await _context.Characters.ToListAsync();
            try
            {
                var character = DbCharacters.FirstOrDefault(c => c.Id == updatedCharacter.Id);

                if (character is null)
                    throw new Exception($"Character with id {updatedCharacter.Id} not found.");

                // _mapper.Map(updatedCharacter, character); & new config
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

            return (serviceResponse);
        }
    }
}
