namespace RichillCapital.Contracts;

public sealed record ThreadPoolInfoResponse
{
    public required DateTimeOffset Time { get; init; }
    public required string MachineName { get; init; }
    public required int ProcessId { get; init; }
    public required int ThreadCount { get; init; }
    public required long CompletedWorkItemCount { get; init; }
    public required long PendingWorkItemCount { get; init; }
    public required int AvailableWorkerThreads { get; init; }
    public required int AvailableCompletionPortThreads { get; init; }
    public required int MinWorkerThreads { get; init; }
    public required int MinCompletionPortThreads { get; init; }
    public required int MaxWorkerThreads { get; init; }
    public required int MaxCompletionPortThreads { get; init; }
}

