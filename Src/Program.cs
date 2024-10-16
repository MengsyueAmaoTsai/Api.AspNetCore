using Microsoft.AspNetCore.HttpOverrides;

using RichillCapital.Api.Endpoints;
using RichillCapital.Api.Middlewares;
using RichillCapital.Api.OpenApi;
using RichillCapital.Domain;
using RichillCapital.Infrastructure.Brokerages;
using RichillCapital.Infrastructure.Clock;
using RichillCapital.Infrastructure.Events;
using RichillCapital.Infrastructure.Notifications.Line;
using RichillCapital.Infrastructure.Identity;
using RichillCapital.Infrastructure.Logging;
using RichillCapital.Infrastructure.Persistence;
using RichillCapital.UseCases;
using RichillCapital.Infrastructure.DataFeeds;
using RichillCapital.Infrastructure.Storage;

var builder = WebApplication.CreateBuilder(args);

// Domain
builder.Services.AddDomainServices();

// Application
builder.Services.AddUseCases();

// Infrastructure - Logging
builder.WebHost.UseCustomLogger();
// builder.Services.AddSerilog();

// Infrastructure - Identity
builder.Services.AddCustomIdentity();

// Infrastructure - Persistence
builder.Services.AddDatabase();

// Infrastructure - Clock
builder.Services.AddDateTimeProvider();

// Infrastructure - Events
builder.Services.AddDomainEvents();

// Infrastructure - Storage
builder.Services.AddStorage();

// Infrastructure - Notifications
builder.Services.AddLineNotification();
// builder.Services.AddDiscordNotification();

// Infrastructure - BackgroundJobs
// builder.Services.AddBackgroundJobs();

// Infrastructure - Brokerages
builder.Services.AddBrokerages();

// Infrastructure - DataFeeds
builder.Services.AddDataFeeds();

// Infrastructure - Payments
// builder.Services.AddPayments();

// Presentation
builder.Services.AddMiddlewares();
builder.Services.AddEndpoints();
builder.Services.AddOpenApi();

builder.Services.AddCors(builder =>
{
    builder
        .AddDefaultPolicy(policy =>
        {
            policy
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
        });
});

builder.Services
    .Configure<ForwardedHeadersOptions>(options =>
    {
        options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
        options.KnownNetworks.Clear();
        options.KnownProxies.Clear();
    });

var app = builder.Build();

app.ResetDatabase();

app.UseForwardedHeaders();

app.UseRequestDebuggingMiddleware();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseExceptionHandler(options =>
{
});

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors();

app.UseAuthorization();

app.UseSwaggerDoc();
app.MapEndpoints();

await app.RunAsync();


public partial class Program;