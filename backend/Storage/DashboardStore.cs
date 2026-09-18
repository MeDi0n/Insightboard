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

    public async Task Add(DashboardModel dashboard)
    {
        using var db = _factory.CreateDbContext();
        db.Dashboards.Add(dashboard);
        await db.SaveChangesAsync();
    }

    public async Task Update(DashboardModel dashboard)
    {
        using var db = _factory.CreateDbContext();
        db.Dashboards.Update(dashboard);
        await db.SaveChangesAsync();
    }

    public async Task<DashboardModel?> Get(Guid id)
    {
        using var db = _factory.CreateDbContext();
        return await db.Dashboards.FindAsync(id);
    }

    public async Task<IReadOnlyCollection<DashboardModel>> GetAll()
    {
        using var db = _factory.CreateDbContext();
        return await db.Dashboards.ToListAsync();
    }

    public async Task<int> FailUnfinished()
    {
        using var db = _factory.CreateDbContext();
        return await db
            .Dashboards.Where(d => d.Status == DashboardStatus.Processing)
            .ExecuteUpdateAsync(s => s.SetProperty(d => d.Status, DashboardStatus.Failed));
    }
}
