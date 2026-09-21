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

    public async Task<UsageSummary> GetUsageSummaryAsync()
    {
        using var db = _factory.CreateDbContext();

        var logs = db.AiCallLogs;
        var invalid = db.AiCallLogs.Where(l => !l.IsValid);

        return new UsageSummary
        {
            TotalCalls = await logs.CountAsync(),
            InvalidCalls = await invalid.CountAsync(),
            TotalInputTokens = await logs.SumAsync(l => l.InputTokens),
            TotalOutputTokens = await logs.SumAsync(l => l.OutputTokens),
            WastedTokens = await invalid.SumAsync(l => l.InputTokens + l.OutputTokens),
        };
    }
}
