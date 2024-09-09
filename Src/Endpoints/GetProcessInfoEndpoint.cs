using System.Diagnostics;

using RichillCapital.Contracts;

namespace RichillCapital.Api.Endpoints;

internal static class GetProcessInfoEndpoint
{
    internal static void MapProcessInfoEndpoint(
        this IEndpointRouteBuilder builder,
        string path = "/process-info")
    {
        builder
            .MapGet(path, () => Results.Ok(Process.GetCurrentProcess().ToResponse()))
            .AllowAnonymous();
    }
}
