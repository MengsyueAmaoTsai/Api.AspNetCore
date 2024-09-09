using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using RichillCapital.Infrastructure.Persistence;

using Testcontainers.MsSql;

namespace RichillCapital.Api.AcceptanceTests;

public sealed class EndToEndTestWebApplicationFactory :
    WebApplicationFactory<Program>,
    IAsyncLifetime
{
    private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder()
         .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
         .WithEnvironment("ACCEPT_EULA", "Y")
         .WithEnvironment("SA_PASSWORD", "Pa55w0rd!")
         .WithPassword("Pa55w0rd!")
         .Build();

    public Task InitializeAsync()
    {
        return _msSqlContainer.StartAsync();
    }

    public new Task DisposeAsync()
    {
        return _msSqlContainer.StopAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptor = services
                .SingleOrDefault(descriptor =>
                    descriptor.ServiceType == typeof(DbContextOptions<EFCoreDbContext>));

            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<EFCoreDbContext>(options =>
                options.UseSqlServer(_msSqlContainer.GetConnectionString(), options =>
                {
                    options.EnableRetryOnFailure(3);
                    options.CommandTimeout(30);
                }));

            using var scope = services
                .BuildServiceProvider()
                .CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<EFCoreDbContext>();

            context.Database.EnsureCreated();
        });
    }
}