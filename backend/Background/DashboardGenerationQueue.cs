using System.Threading.Channels;

namespace Insightboard.Api.Background;

public class DashboardGenerationQueue
{
    private readonly Channel<DashboardGenerationJob> _channel =
        Channel.CreateUnbounded<DashboardGenerationJob>();

    public ValueTask EnqueueAsync(DashboardGenerationJob job)
    {
        return _channel.Writer.WriteAsync(job);
    }

    public ValueTask<DashboardGenerationJob> DequeueAsync(CancellationToken cancellationToken)
    {
        return _channel.Reader.ReadAsync(cancellationToken);
    }
}
