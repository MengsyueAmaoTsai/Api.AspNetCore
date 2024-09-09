using System.Diagnostics;

namespace RichillCapital.Infrastructure.Logging;

public static class ActivityExtensions
{
    public static string GetTraceId(this Activity activity) =>
        activity.IdFormat switch
        {
            ActivityIdFormat.Hierarchical => activity.RootId,
            ActivityIdFormat.W3C => activity.TraceId.ToHexString(),
            _ => null,
        } ?? string.Empty;
}