﻿using Microsoft.Extensions.DependencyInjection;

using RichillCapital.Domain.Common.Storage;

namespace RichillCapital.Storage;

public static class LocalFileStorageExtensions
{
    public static IServiceCollection AddLocalFileStorageManager(this IServiceCollection services)
    {
        services.AddSingleton<IFileStorageManager, LocalFileStorageManager>();

        return services;
    }
}