namespace RichillCapital.Api.Endpoints;

internal static class TestEndpoint
{
    internal static void MapTestEndpoint(
        this IEndpointRouteBuilder builder,
        string path = "/test")
    {
        builder
            .MapGet(path, () =>
            {
                return Results.Ok();
            }).AllowAnonymous();
    }
}