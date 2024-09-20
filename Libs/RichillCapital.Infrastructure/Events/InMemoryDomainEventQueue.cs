using System.Threading.Channels;

using RichillCapital.SharedKernel;

namespace RichillCapital.Infrastructure.Events;

internal sealed class InMemoryDomainEventQueue
{
    private readonly Channel<IDomainEvent> _channel = Channel.CreateUnbounded<IDomainEvent>();
    public ChannelWriter<IDomainEvent> Writer => _channel.Writer;
    public ChannelReader<IDomainEvent> Reader => _channel.Reader;
}