using backend.Models;
using Microsoft.AspNetCore.Mvc;
using backend.Storage;

namespace backend.Controllers;



[ApiController]
[Route("[controller]")]
public class DashboardsController : ControllerBase
{
    private readonly DashboardStore _store;

    public DashboardsController(DashboardStore store)
    {
    _store = store;
    }

    [HttpGet]
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
    public IActionResult Create(IFormFile file)
    {
        var id = Guid.NewGuid();
        var job = new DashboardJob();
        _store.Save(id, job);
        return Accepted(new {id});
    }
}
