using System.Reflection;

namespace RichillCapital.Api;

internal static class ApplicationInfo
{
    internal static string GetDisplayName() => "RichillCapital API";

    internal static string GetAssemblyVersion()
    {
        var attribute = typeof(Program).Assembly
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>() ??
            throw new InvalidOperationException();

        return attribute.InformationalVersion;
    }

}