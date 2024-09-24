using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

using RichillCapital.Extensions.Options;
using RichillCapital.Infrastructure.Storage.Local;

namespace RichillCapital.Infrastructure.Storage;

public static class StorageExtensions
{
    public static IServiceCollection AddStorage(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(
            typeof(StorageExtensions).Assembly,
            includeInternalTypes: true);

        services.AddOptionsWithFluentValidation<StorageOptions>(StorageOptions.SectionKey);

        services.AddLocalFileStorage();

        return services;
    }
}