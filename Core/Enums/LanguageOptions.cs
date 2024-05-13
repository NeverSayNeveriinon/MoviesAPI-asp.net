using System.Text.Json.Serialization;


namespace Core.Enums;

[Flags]
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum LanguageOptions
{
    English = 1,
    Persian = 2,
    French = 4,
    Korean = 8,
    Spanish = 16
}