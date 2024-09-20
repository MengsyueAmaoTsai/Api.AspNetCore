using Microsoft.Extensions.DependencyInjection;

using RichillCapital.Domain.Abstractions;

namespace RichillCapital.Infrastructure.Clock;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}

public static class ClockExtensions
{
    public static IServiceCollection AddDateTimeProvider(this IServiceCollection services) =>
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
}