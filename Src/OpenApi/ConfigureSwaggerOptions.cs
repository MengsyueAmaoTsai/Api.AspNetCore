using Asp.Versioning.ApiExplorer;

using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace RichillCapital.Api.OpenApi;

internal sealed class ConfigureSwaggerOptions(
    IApiVersionDescriptionProvider _provider) :
    IConfigureNamedOptions<SwaggerGenOptions>
{
    public void Configure(string? name, SwaggerGenOptions options) => Configure(options);

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(
                description.GroupName,
                CreateOpenApiInfo(description));
        }
    }

    private static OpenApiInfo CreateOpenApiInfo(ApiVersionDescription description)
    {
        var assemblyVersion = typeof(Program).Assembly.GetName().Version;

        var info = new OpenApiInfo
        {
            Title = $"RichillCapital.Api v{description.ApiVersion}",
            Version = description.ApiVersion.ToString(),
            Description = $"Richill Capital Api",
            Contact = new OpenApiContact
            {
                Name = "Mengsyue Amao Tsai",
                Email = "mengsyue.tsai@outlook.com",
                Url = new Uri("https://github.com/MengsyueAmaoTsai"),
            },
        };

        if (description.IsDeprecated)
        {
            info.Description += " This API version has been deprecated.";
        }

        return info;
    }
}