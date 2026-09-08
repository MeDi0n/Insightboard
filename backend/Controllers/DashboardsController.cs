using Insightboard.Api.Services.Abstractions;
using Insightboard.Api.Storage;
using Microsoft.AspNetCore.Mvc;

namespace Insightboard.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DashboardsController : ControllerBase
{
    private readonly DashboardStore _store;
    private readonly IDashboardService _dashboardService;

    public DashboardsController(DashboardStore store, IDashboardService dashboardService)
    {
        _store = store;
        _dashboardService = dashboardService;
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid Id)
    {
        var job = _store.Get(Id);
        if (job == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(job);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(IFormFile file)
    {
        var result = await _dashboardService.CreateAsync(file);
        if (result.IsSuccess == false)
        {
            return BadRequest(new { error = result.Error });
        }
        else
        {
            return Accepted(new { id = result.Id });
        }
    }
}
