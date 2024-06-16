using Microsoft.Extensions.DependencyInjection;

namespace RichillCapital.BackgroundJobs;

public static class BackgroundJobsExtensions
{
    public static IServiceCollection AddBackgroundJobs(this IServiceCollection services)
    {
        return services;
    }
}
