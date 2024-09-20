using System.Reflection;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Sinks.SystemConsole.Themes;

namespace RichillCapital.Infrastructure.Logging;

public static class LoggingExtensions
{
    private const string ConsoleTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext} - {Message:lj}{NewLine}{Exception}{NewLine}";
    private const string FileTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] [{SourceContext}] [TraceId: {TraceId}] [MachineName: {MachineName}] [ProcessId: {ProcessId}] {Message:lj}{NewLine}{Exception}";

    private const int MaxLogFileSize = 10 * 1024 * 1024;
    private const string LogDirectory = "Logs";
    private const string LogFileName = "log-.log";

    public static IWebHostBuilder UseCustomLogger(this IWebHostBuilder builder)
    {
        builder.ConfigureLogging((context, logging) =>
        {
            logging.ClearProviders();
            logging.AddSerilog();

            var assemblyName = Assembly.GetEntryAssembly()?.GetName().Name;
            var environment = context.HostingEnvironment;

            var logsPath = Path.Combine(environment.ContentRootPath, LogDirectory);
            Directory.CreateDirectory(logsPath);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithEnvironmentUserName()
                .Enrich.WithProperty("ProcessId", Environment.ProcessId)
                .Enrich.WithProperty("Assembly", assemblyName)
                .Enrich.WithProperty("Application", environment.ApplicationName)
                .Enrich.WithProperty("EnvironmentName", environment.EnvironmentName)
                .Enrich.WithProperty("ContentRootPath", environment.ContentRootPath)
                .Enrich.WithProperty("WebRootPath", environment.WebRootPath)
                .Enrich.WithExceptionDetails()
                .WriteTo.Console(
                    restrictedToMinimumLevel: LogEventLevel.Information,
                    outputTemplate: ConsoleTemplate,
                    theme: AnsiConsoleTheme.Code)
                .WriteTo.File(
                    Path.Combine(logsPath, LogFileName),
                    fileSizeLimitBytes: MaxLogFileSize,
                    rollOnFileSizeLimit: true,
                    rollingInterval: RollingInterval.Day,
                    shared: true,
                    flushToDiskInterval: TimeSpan.FromSeconds(1),
                    restrictedToMinimumLevel: LogEventLevel.Information,
                    outputTemplate: FileTemplate)
                .CreateLogger();
        });

        return builder;
    }
}
