using RichillCapital.Api.Endpoints;
using RichillCapital.Api.OpenApi;
using RichillCapital.Identity;
using RichillCapital.Logging;
using RichillCapital.Persistence;
using RichillCapital.Persistence.Seeds;
using RichillCapital.Storage.Local;
using RichillCapital.UseCases;
using RichillCapital.Notifications.Discord;
using RichillCapital.Notifications.Line;

using Serilog;
using RichillCapital.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Application
builder.Services.AddUseCases();

// Infrastructure - Logging
builder.Services.AddSerilog();
builder.WebHost.UseApiLogger();

// Infrastructure - Identity
builder.Services.AddApiIdentity();

// Infrastructure - Persistence
builder.Services.AddDatabase();

// Infrastructure - Storage
builder.Services.AddLocalFileStorageManager();

// Infrastructure - Notifications
builder.Services.AddLineNotification();
builder.Services.AddDiscordNotification();

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

var app = builder.Build();

app.PopulateSeed();

app.UseDebuggingRequestMiddleware();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseExceptionHandler(options =>
{
});

app.UseRouting();

app.UseCors();

app.UseSwaggerDoc();

app.MapTestEndpoint();
app.MapGCInfoEndpoint();
app.MapThreadPoolInfoEndpoint();
app.MapProcessInfoEndpoint();

app.MapEndpoints();

await app.RunAsync();


public partial class Program;