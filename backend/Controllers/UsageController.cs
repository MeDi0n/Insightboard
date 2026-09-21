using Insightboard.Api.Storage;
using Microsoft.AspNetCore.Mvc;

namespace Insightboard.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsageController : ControllerBase
{
    private readonly AiCallLogStore _aiCallLogStore;

    public UsageController(AiCallLogStore aiCallLogStore)
    {
        _aiCallLogStore = aiCallLogStore;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetSummary()
    {
        var getSummaryUsage = await _aiCallLogStore.GetUsageSummaryAsync();
        return Ok(getSummaryUsage);
    }
}
