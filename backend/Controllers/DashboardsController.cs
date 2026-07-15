using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class DashboardsController : ControllerBase
{
    [HttpGet(Name = "GetDashboards")]
    public string Get()
    {
        return "hi";
    }
}
