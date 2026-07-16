using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class DashboardsController : ControllerBase
{
    [HttpGet(Name = "GetDashboards")]
    public DashboardSpec Get()
    {
        return new DashboardSpec
        {
            Charts = new List<ChartSpec>
            {
                new ChartSpec{Type = "bar", Title = "result", X = "month", Y = "sales"},
                new ChartSpec{Type = "line", Title = "awards", X = "month", Y = "awards"},
            }
        };
    }
}
