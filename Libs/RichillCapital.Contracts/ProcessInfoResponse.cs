using System.Diagnostics;

namespace RichillCapital.Contracts;

public sealed record ProcessInfoResponse
{
    public required DateTimeOffset Time { get; init; }
    public required int ProcessId { get; init; }
    public required string MachineName { get; init; }
    public required string UserName { get; init; }
    public required double TotalProcessorTime { get; init; }
    public required long PrivateMemorySize64 { get; init; }
    public required long VirtualMemorySize64 { get; init; }
    public required long WorkingSet64 { get; init; }
    public required long PeakVirtualMemorySize64 { get; init; }
    public required long PeakWorkingSet64 { get; init; }
}

public static class ProcessInfoResponseMapping
{
    public static ProcessInfoResponse ToResponse(this Process process) =>
        new()
        {
            Time = DateTimeOffset.Now,
            ProcessId = Environment.ProcessId,
            MachineName = Environment.MachineName,
            UserName = Environment.UserName,
            TotalProcessorTime = process.TotalProcessorTime.TotalSeconds,
            PrivateMemorySize64 = process.PrivateMemorySize64 / 1024 / 1024,
            VirtualMemorySize64 = process.VirtualMemorySize64 / 1024 / 1024,
            WorkingSet64 = process.WorkingSet64 / 1024 / 1024,
            PeakVirtualMemorySize64 = process.PeakVirtualMemorySize64 / 1024 / 1024,
            PeakWorkingSet64 = process.PeakWorkingSet64 / 1024 / 1024,
        };
}