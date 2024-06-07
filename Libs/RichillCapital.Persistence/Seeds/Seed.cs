using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using RichillCapital.Domain;

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

        context.Set<User>().AddRange(CreateUsers());

        context.SaveChanges();
    }
}
