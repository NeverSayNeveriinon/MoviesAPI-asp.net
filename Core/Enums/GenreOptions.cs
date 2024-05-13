using System.Text.Json.Serialization;

namespace Core.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum GenreOptions : byte
{
    Drama,
    History,
    Thriller,
    Horror,
    Romance,
    Comedy,
    Mystery,
    Crime,
    Sci_Fi,
    Action
}