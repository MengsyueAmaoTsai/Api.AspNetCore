using Newtonsoft.Json;

namespace RichillCapital.Binance.Serialization;

internal sealed class TimestampDateTimeOffsetConverter :
    JsonConverter<DateTimeOffset>
{
    public override DateTimeOffset ReadJson(
        JsonReader reader,
        Type objectType,
        DateTimeOffset existingValue,
        bool hasExistingValue,
        JsonSerializer serializer)
    {
        if (reader.TokenType != JsonToken.Integer)
        {
            throw new JsonSerializationException($"Unexpected token parsing date. Expected Integer, got {reader.TokenType}.");
        }

        var timestamp = (long)reader.Value!;

        return timestamp.ToString().Length == 10 ?
            DateTimeOffset.FromUnixTimeSeconds(timestamp) :
            timestamp.ToString().Length == 13 ?
                DateTimeOffset.FromUnixTimeMilliseconds(timestamp) :
                throw new JsonSerializationException($"Unexpected timestamp length: {timestamp.ToString().Length}");
    }

    public override void WriteJson(
        JsonWriter writer,
        DateTimeOffset value,
        JsonSerializer serializer) =>
        throw new NotImplementedException();
}