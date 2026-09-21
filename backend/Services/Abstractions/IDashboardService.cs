using Insightboard.Api.Models.Dashboards;
using Insightboard.Api.Parsing;

namespace Insightboard.Api.Services.Abstractions;

public interface IDashboardService
{
    Task<DashboardSpec?> GenerateAsync(Guid DashboardId, TableData parsed);
    Task<CreateDashboardResult> CreateAsync(Stream stream, string filename);
    Task<IReadOnlyCollection<DashboardListItem>> GetAll();
}
