namespace RichillCapital.Contracts;

public sealed record GCInfoResponse
{
    public required DateTimeOffset Time { get; init; }
    public required string MachineName { get; init; }
    public required int ProcessId { get; init; }
    public required long TotalMemory { get; init; }
    public required int Gen0Collections { get; init; }
    public required int Gen1Collections { get; init; }
    public required int Gen2Collections { get; init; }
    public required TimeSpan TotalPauseDuration { get; init; }
    public required GCMemoryInfoResponse GCMemoryInfo { get; init; }
}



public sealed record GCMemoryInfoResponse
{
    public required long HighMemoryLoadThresholdBytes { get; init; }
    public required long MemoryLoadBytes { get; init; }
    public required long HeapSizeBytes { get; init; }
    public required long FragmentedBytes { get; init; }
    public required double PauseTimePercentage { get; init; }
    public required long PinnedObjectsCount { get; init; }
}