using RichillCapital.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapGCInfoEndpoint();
app.MapThreadPoolInfoEndpoint();
app.MapProcessInfoEndpoint();
app.MapControllers();

await app.RunAsync();

public partial class Program;