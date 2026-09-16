using System.Collections.Concurrent;
using Insightboard.Api.Models.Dashboards;

namespace Insightboard.Api.Storage;

public class DashboardStore
{
    private readonly ConcurrentDictionary<Guid, DashboardModel> _dashboards = new();

    public void Save(Guid id, DashboardModel dashboard)
    {
        _dashboards[id] = dashboard;
    }

    public DashboardModel? Get(Guid id)
    {
        _dashboards.TryGetValue(id, out var dashboard);
        return dashboard;
    }

    public IReadOnlyCollection<DashboardModel> GetAll()
    {
        return _dashboards.Values.ToList();
    }
}
