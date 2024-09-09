using RichillCapital.Api.Endpoints;
using RichillCapital.Api.OpenApi;
using RichillCapital.Infrastructure.Logging;
using RichillCapital.UseCases;

using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Application
builder.Services.AddUseCases();

// Infrastructure - Logging
builder.WebHost.UseCustomLogger();
builder.Services.AddSerilog();

// Infrastructure - Identity
// builder.Services.AddApiIdentity();

// Infrastructure - Persistence
// builder.Services.AddDatabase();

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
// builder.Services.AddMiddlewares();
builder.Services.AddEndpoints();
builder.Services.AddOpenApi();


var app = builder.Build();

// app.ResetDatabase();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseExceptionHandler(options =>
{
});

app.UseRouting();

app.UseSwaggerDoc();

app.MapEndpoints();

await app.RunAsync();


public partial class Program;