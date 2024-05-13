using System.Text.Json.Serialization;

namespace Core.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum GenderOptions : byte
{
    Male,
    Female,
    Other
}