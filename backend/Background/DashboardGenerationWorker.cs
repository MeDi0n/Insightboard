using Insightboard.Api.Models.Dashboards;
using Insightboard.Api.Services.Abstractions;
using Insightboard.Api.Storage;

namespace Insightboard.Api.Background;

public class DashboardGenerationWorker : BackgroundService
{
    private readonly DashboardStore _store;
    private readonly IDashboardService _dashboardService;
    private readonly DashboardGenerationQueue _dashboardGenerationQueue;
    private readonly ILogger<DashboardGenerationWorker> _logger;

    public DashboardGenerationWorker(
        DashboardStore store,
        IDashboardService dashboardService,
        DashboardGenerationQueue dashboardGenerationQueue,
        ILogger<DashboardGenerationWorker> logger
    )
    {
        _store = store;
        _dashboardService = dashboardService;
        _dashboardGenerationQueue = dashboardGenerationQueue;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var job = await _dashboardGenerationQueue.DequeueAsync(stoppingToken);
            var dashboard = _store.Get(job.DashboardId);

            if (dashboard == null)
            {
                continue;
            }

            try
            {
                var spec = await _dashboardService.GenerateAsync(job.Table);

                if (spec == null)
                {
                    dashboard.Status = DashboardStatus.Failed;
                }
                else
                {
                    dashboard.Status = DashboardStatus.Done;
                    dashboard.Spec = spec;
                }
                _store.Update(dashboard);
            }
            catch (Exception ex)
            {
                dashboard.Status = DashboardStatus.Failed;
                _logger.LogError(
                    ex,
                    "Generation failed for dashboard {DashboardId}",
                    job.DashboardId
                );
                _store.Update(dashboard);
            }
        }
    }
}
