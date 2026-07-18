using System.Collections.Concurrent;

namespace backend.Storage;

public class DashboardStore
{
    private readonly ConcurrentDictionary<Guid, DashboardJob> _jobs = new();

    public void Save(Guid id, DashboardJob job)
    {
        _jobs[id] = job;
    }

    public DashboardJob? Get(Guid id)
    {
       _jobs.TryGetValue(id, out var job);
       return job;
    }
}
