using System.Text.Json.Serialization;

namespace PatrickAPI.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RpgClass
    {
        Knight = 1,
        Mage = 2,
        Priest = 3,
        Cleric = 4,
        Warlock = 5,
        Druid = 6,
        Paladin = 7,
        Barbarian = 8
    }
}
