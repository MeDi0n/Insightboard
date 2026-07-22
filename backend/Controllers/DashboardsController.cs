using backend.Models;
using Microsoft.AspNetCore.Mvc;
using backend.Storage;
using backend.Ai;
using backend.Generation;

namespace backend.Controllers;



[ApiController]
[Route("[controller]")]
public class DashboardsController : ControllerBase
{
    private readonly DashboardStore _store;
    private readonly DashboardGenerator _generator;

    public DashboardsController(DashboardStore store, DashboardGenerator generator)
    {
    _store = store;
    _generator = generator;
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
        var id = Guid.NewGuid();
        var job = new DashboardJob();

        job.Status = "done";
        job.Spec =  await _generator.Generate(file);

        _store.Save(id, job);
        return Accepted(new {id});
    }
}
