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

        public async Task<List<Character>> AddCharacter(Character newCharacter)
        {
            characters.Add(newCharacter);
            return characters;
        }

        public async Task<List<Character>> GetAllCharacters()
        {
            return characters;
        }

        public async Task<Character> GetCharacterById(int id)
        {
            var character = characters.FirstOrDefault(c => c.Id == id);
            if (character is not null)
                return character;
            throw new Exception("Character not found!");
        }
    }
}
