using RichillCapital.Api.Endpoints;
using RichillCapital.Api.OpenApi;
using RichillCapital.Logging;
using RichillCapital.Persistence;
using RichillCapital.Persistence.Seeds;
using RichillCapital.UseCases;

using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Application
builder.Services.AddUseCases();

// Infrastructure - Logging
builder.Services.AddSerilog();
builder.WebHost.UseApiLogger();

// Infrastructure - Identity

// Infrastructure - Persistence
builder.Services.AddDatabase();

builder.Services.AddEndpoints();
builder.Services.AddOpenApi();

var app = builder.Build();

app.PopulateSeed();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseExceptionHandler(options =>
{
});

app.UseSwaggerDoc();

app.MapGCInfoEndpoint();
app.MapThreadPoolInfoEndpoint();
app.MapProcessInfoEndpoint();

app.MapEndpoints();

await app.RunAsync();


public partial class Program;