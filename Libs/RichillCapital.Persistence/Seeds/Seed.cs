using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using RichillCapital.SharedKernel;

namespace RichillCapital.Persistence.Seeds;

public static partial class Seed
{
    public static WebApplication PopulateSeed(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var services = scope.ServiceProvider;

        var logger = services.GetRequiredService<ILogger<EFCoreDbContext>>();

        try
        {
            var context = services.GetRequiredService<EFCoreDbContext>();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            services.AddInitialData();

            logger.LogInformation("Seed populated successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred seeding the database. {exceptionMessage}", ex.Message);
        }

        return app;
    }

    internal static void AddInitialData(this IServiceProvider serviceProvider)
    {
        using var context = serviceProvider.GetRequiredService<EFCoreDbContext>();

        context.AddEntitiesWithoutDomainEvents(CreateUsers());

        context.AddEntitiesWithoutDomainEvents(CreateSignalSources());

        context.SaveChanges();
    }
}

internal static class DbContextExtensions
{
    internal static void AddEntitiesWithoutDomainEvents<TEntity>(
        this DbContext context,
        IEnumerable<TEntity> entities)
        where TEntity : class, IEntity =>
        context.Set<TEntity>().AddRange(
            entities.Select(entity =>
            {
                entity.ClearDomainEvents();
                return entity;
            }));
}