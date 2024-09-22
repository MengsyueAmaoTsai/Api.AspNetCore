using Newtonsoft.Json;

namespace RichillCapital.Serialization.JsonConverters;

public sealed class NullableDecimalConverter : JsonConverter<decimal>
{
    public override decimal ReadJson(
        JsonReader reader,
        Type objectType,
        decimal existingValue,
        bool hasExistingValue,
        JsonSerializer serializer) =>
        reader.TokenType switch
        {
            JsonToken.Null => decimal.Zero,
            JsonToken.String => decimal.TryParse((string)reader.Value!, out var value) ? value : decimal.Zero,
            JsonToken.Float or JsonToken.Integer => Convert.ToDecimal(reader.Value),
            _ => throw new JsonSerializationException($"Unexpected token type: {reader.TokenType}"),
        };

    public override void WriteJson(
        JsonWriter writer,
        decimal value,
        JsonSerializer serializer) =>
        throw new NotImplementedException();
}