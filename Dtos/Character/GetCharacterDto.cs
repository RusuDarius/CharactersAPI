using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatrickAPI.Dtos.Character
{
    // Other good naming convention GetCharacterResponseDto
    public class GetCharacterDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Frodo";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public double Intelligence { get; set; } = 40;
        public RpgClass Class { get; set; } = RpgClass.Warlock;
    }
}
