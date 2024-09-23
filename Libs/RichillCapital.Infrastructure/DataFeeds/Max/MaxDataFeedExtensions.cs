using Microsoft.Extensions.DependencyInjection;

using RichillCapital.Max;

namespace RichillCapital.Infrastructure.DataFeeds.Max;

public static class MaxDataFeedExtensions
{
    public static IServiceCollection AddMaxDataFeed(this IServiceCollection services)
    {
        services.AddMaxRestClient("https://max-api.maicoin.com");

        return services;
    }
}