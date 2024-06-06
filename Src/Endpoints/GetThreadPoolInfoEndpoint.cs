namespace RichillCapital.Api.Endpoints;

internal static class GetThreadPoolInfoEndpoint
{
    internal static void MapThreadPoolInfoEndpoint(
        this IEndpointRouteBuilder builder,
        string path = "/thread-pool-info")
    {
        builder.MapGet(path, () =>
        {
            return Results.Ok();
        });
    }
}