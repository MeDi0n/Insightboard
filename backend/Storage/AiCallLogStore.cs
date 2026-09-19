using Insightboard.Api.Models.AiCalls;
using Microsoft.EntityFrameworkCore;

namespace Insightboard.Api.Storage;

public class AiCallLogStore
{
    private readonly IDbContextFactory<AppDbContext> _factory;

    public AiCallLogStore(IDbContextFactory<AppDbContext> factory)
    {
        _factory = factory;
    }

    public async Task AddAsync(AiCallLog log)
    {
        using var db = _factory.CreateDbContext();
        db.AiCallLogs.Add(log);
        await db.SaveChangesAsync();
    }
}
