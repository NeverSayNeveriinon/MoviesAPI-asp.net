using System.Text.Json.Serialization;

namespace Core.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum JobOptions : byte
{
    Artist,
    Director,
    Writer
}