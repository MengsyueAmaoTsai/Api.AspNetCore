using Microsoft.AspNetCore.HttpOverrides;

using RichillCapital.Api.Endpoints;
using RichillCapital.Api.Middlewares;
using RichillCapital.Api.OpenApi;
using RichillCapital.Domain;
using RichillCapital.Infrastructure.Events;
using RichillCapital.Infrastructure.Identity;
using RichillCapital.Infrastructure.Logging;
using RichillCapital.Infrastructure.Persistence;
using RichillCapital.UseCases;

using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Domain
builder.Services.AddOrderPlacementEvaluator();

// Application
builder.Services.AddUseCases();

// Infrastructure - Logging
builder.WebHost.UseCustomLogger();
builder.Services.AddSerilog();

// Infrastructure - Identity
builder.Services.AddCustomIdentity();

// Infrastructure - Persistence
builder.Services.AddDatabase();

// Infrastructure - Events
builder.Services.AddDomainEvents();

// Infrastructure - Storage
// builder.Services.AddLocalFileStorageManager();

// Infrastructure - Notifications
// builder.Services.AddLineNotification();
// builder.Services.AddDiscordNotification();

// Infrastructure - BackgroundJobs
// builder.Services.AddBackgroundJobs();

// Infrastructure - Brokerages

// Infrastructure - DataFeeds

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
app.UseSignalDebuggingMiddleware();

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