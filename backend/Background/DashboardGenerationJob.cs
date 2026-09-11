using Insightboard.Api.Parsing;

namespace Insightboard.Api.Background;

public class DashboardGenerationJob
{
    public required Guid DashboardId { get; set; }
    public required TableData Table { get; set; }
}
