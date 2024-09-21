
using Newtonsoft.Json;

namespace RichillCapital.Binance.Serialization;

internal sealed class UnixDateTimeOffsetConverter :
    JsonConverter<DateTimeOffset>
{
    public override DateTimeOffset ReadJson(JsonReader reader, Type objectType, DateTimeOffset existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType != JsonToken.Integer)
        {
            throw new JsonSerializationException($"Unexpected token parsing date. Expected Integer, got {reader.TokenType}.");
        }

        var timestamp = (long)reader.Value!;

        return DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
    }

    public override void WriteJson(
        JsonWriter writer,
        DateTimeOffset value,
        JsonSerializer serializer) =>
        throw new NotImplementedException();
}