using Newtonsoft.Json;

namespace RichillCapital.Serialization.JsonConverters;

public sealed class NullableStringConverter : JsonConverter<string>
{
    public override string? ReadJson(
        JsonReader reader,
        Type objectType,
        string? existingValue,
        bool hasExistingValue,
        JsonSerializer serializer) =>
        reader.TokenType switch
        {
            JsonToken.Null => string.Empty,
            JsonToken.String => reader.Value as string,
            _ => reader.Value?.ToString(),
        };

    public override void WriteJson(
        JsonWriter writer,
        string? value,
        JsonSerializer serializer) =>
        throw new NotImplementedException();
}