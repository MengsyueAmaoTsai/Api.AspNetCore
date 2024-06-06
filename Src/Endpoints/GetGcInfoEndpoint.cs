namespace RichillCapital.Api.Endpoints;

internal static class GetGCInfoEndpoint
{
    internal static void MapGCInfoEndpoint(
        this IEndpointRouteBuilder builder,
        string path = "/gc-info")
    {
        builder.MapGet(path, () =>
        {
            return Results.Ok();
        });
    }
}