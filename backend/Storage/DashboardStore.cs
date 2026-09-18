using Insightboard.Api.Models.Dashboards;
using Microsoft.EntityFrameworkCore;

namespace Insightboard.Api.Storage;

public class DashboardStore
{
    private readonly IDbContextFactory<AppDbContext> _factory;

    public DashboardStore(IDbContextFactory<AppDbContext> factory)
    {
        _factory = factory;
    }

    public void Add(DashboardModel dashboard)
    {
        using var db = _factory.CreateDbContext();
        db.Dashboards.Add(dashboard);
        db.SaveChanges();
    }

    public void Update(DashboardModel dashboard)
    {
        using var db = _factory.CreateDbContext();
        db.Dashboards.Update(dashboard);
        db.SaveChanges();
    }

    public DashboardModel? Get(Guid id)
    {
        using var db = _factory.CreateDbContext();
        return db.Dashboards.Find(id);
    }

    public IReadOnlyCollection<DashboardModel> GetAll()
    {
        using var db = _factory.CreateDbContext();
        return db.Dashboards.ToList();
    }
}
