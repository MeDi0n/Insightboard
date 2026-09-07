using System.Collections.Concurrent;
using Insightboard.Api.Models.Dashboards;

namespace Insightboard.Api.Storage;

public class DashboardStore
{
    private readonly ConcurrentDictionary<Guid, DashboardModel> _jobs = new();

    public void Save(Guid id, DashboardModel job)
    {
        _jobs[id] = job;
    }

    public DashboardModel? Get(Guid id)
    {
       _jobs.TryGetValue(id, out var job);
       return job;
    }
}
