namespace RichillCapital.Domain.Abstractions;

public interface IDateTimeProvider
{
    DateTimeOffset UtcNow { get; }
}