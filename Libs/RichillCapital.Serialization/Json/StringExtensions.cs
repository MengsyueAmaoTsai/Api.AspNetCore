using System.Text.Json;

namespace RichillCapital.Serialization.Json;

public static class StringExtensions
{
    public static bool IsValidJson(this string json)
    {
        try
        {
            _ = JsonDocument.Parse(json);
            return true;
        }
        catch (JsonException)
        {
            return false;
        }
    }
}