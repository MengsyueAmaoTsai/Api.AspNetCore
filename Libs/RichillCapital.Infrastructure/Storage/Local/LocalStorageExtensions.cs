using Microsoft.Extensions.DependencyInjection;

using RichillCapital.Domain.Abstractions;

namespace RichillCapital.Infrastructure.Storage.Local;

public static class LocalStorageExtensions
{
    public static IServiceCollection AddLocalFileStorage(this IServiceCollection services)
    {
        services.AddSingleton<IFileStorageManager, LocalFileStorageManager>();
        return services;
    }
}