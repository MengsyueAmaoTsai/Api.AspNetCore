using FluentValidation;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using RichillCapital.Domain.Common.Events;
using RichillCapital.Domain.Common.Repositories;
using RichillCapital.Extensions.Options;
using RichillCapital.SharedKernel.Specifications.Evaluators;

namespace RichillCapital.Persistence;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(
            typeof(DatabaseExtensions).Assembly,
            includeInternalTypes: true);

        services.AddOptionsWithFluentValidation<DatabaseOptions>(DatabaseOptions.SectionKey);

        services.AddMsSql();

        return services;
    }

    private static IServiceCollection AddMsSql(this IServiceCollection services)
    {
        using var scope = services
            .BuildServiceProvider()
            .CreateScope();

        var databaseOptions = scope.ServiceProvider
            .GetRequiredService<IOptions<DatabaseOptions>>()
            .Value;

        services
            .AddDbContext<EFCoreDbContext>(options =>
                options.UseSqlServer(databaseOptions.ConnectionString))
            .AddDbContextFactory<EFCoreDbContext>(
                (Action<DbContextOptionsBuilder>)null!,
                ServiceLifetime.Scoped);

        services.AddScoped(typeof(IRepository<>), typeof(EFCoreRepository<>));
        services.AddScoped(typeof(IReadOnlyRepository<>), typeof(EFCoreRepository<>));

        services.AddScoped<IUnitOfWork>(serviceProvider =>
                serviceProvider.GetRequiredService<EFCoreDbContext>());

        services.AddSpecifications();

        services.AddMediatorDomainEventDispatcher();

        return services;
    }

    private static IServiceCollection AddSpecifications(this IServiceCollection services)
    {
        services.AddScoped<IInMemorySpecificationEvaluator, InMemorySpecificationEvaluator>();

        return services;
    }

    private static IServiceCollection AddMediatorDomainEventDispatcher(this IServiceCollection services)
    {
        services.AddScoped<IDomainEventDispatcher, MediatorDomainEventDispatcher>();

        return services;
    }
}