using backend.Models;
using Microsoft.AspNetCore.Mvc;
using backend.Storage;
using backend.Ai;
using backend.Generation;
using backend.Parsing;

namespace backend.Controllers;



[ApiController]
[Route("[controller]")]
public class DashboardsController : ControllerBase
{
    private readonly DashboardStore _store;
    private readonly DashboardGenerator _generator;
    private readonly IEnumerable<IFileParser> _parsers;


    public DashboardsController(DashboardStore store, DashboardGenerator generator, IEnumerable<IFileParser> parser)
    {
    _parsers = parser;
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
    public IActionResult Create(IFormFile file)
    {
        if(file == null || file.Length == 0)
        {
            return BadRequest(new { error = "File is empty or was not provided."});
        }
        var ext = Path.GetExtension(file.FileName);
        var parser = _parsers.FirstOrDefault(p => p.AllowedExtension(ext));
        if(parser == null)
        {
            return BadRequest(new {error = "Unsupported file type. Upload .csv, .xlsx or .pdf."});
        }
        var id = Guid.NewGuid();
        var job = new DashboardJob{Status = "processing"};
        CsvData parsed;
        try
        {
        parsed = parser.Parse(file);
        }
        catch
        {
            return BadRequest(new {error = "Сannot read that file"});
        }
        _store.Save(id, job);

        _ = Task.Run(async () =>
        {
        var spec = await _generator.Generate(parsed);
        if(spec == null)
        {
        job.Status = "failed";
        }
        else
        {
        job.Status = "done";
        job.Spec =  spec;
        }
        });

        return Accepted(new {id});
    }
}
