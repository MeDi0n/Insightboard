using Insightboard.Api.Models.Dashboards;
using Insightboard.Api.Parsing;

namespace Insightboard.Api.Services.Abstractions;

public interface IDashboardService
{
    Task<DashboardSpec?> GenerateAsync(TableData parsed);
    Task<CreateDashboardResult> CreateAsync(IFormFile file);
}
