using Insightboard.Api.Ai;
using Insightboard.Api.Ai.Prompts;
using Insightboard.Api.Ai.Validation;
using Insightboard.Api.Background;
using Insightboard.Api.Models.Dashboards;
using Insightboard.Api.Parsing;
using Insightboard.Api.Services.Abstractions;
using Insightboard.Api.Storage;

namespace Insightboard.Api.Services;

public class DashboardService : IDashboardService
{
    private readonly IAiProvider _ai;
    private readonly DashboardSpecValidator _validator;
    private readonly DashboardPromptBuilder _builder;
    private readonly IEnumerable<IFileParser> _parsers;
    private readonly DashboardStore _store;
    private readonly DashboardGenerationQueue _queue;

    public DashboardService(
        IAiProvider ai,
        DashboardSpecValidator validator,
        DashboardPromptBuilder builder,
        IEnumerable<IFileParser> parsers,
        DashboardStore store,
        DashboardGenerationQueue queue
    )
    {
        _ai = ai;
        _validator = validator;
        _builder = builder;
        _parsers = parsers;
        _store = store;
        _queue = queue;
    }

    public async Task<DashboardSpec?> GenerateAsync(TableData parsed)
    {
        var columns = parsed.Columns;
        var errors = new List<string>();
        for (int i = 0; i < 3; i++)
        {
            var prompt = _builder.Build(columns, errors);
            var response = await _ai.GetResponseAsync(prompt);
            var result = _validator.Validate(response, columns);
            if (result.IsValid)
            {
                result.Spec!.Data = parsed.Rows;
                return result.Spec;
            }
            errors = result.Errors ?? new List<string>();
        }
        return null;
    }

    public async Task<CreateDashboardResult> CreateAsync(Stream stream, string filename)
    {
        if (stream.Length == 0)
        {
            return new CreateDashboardResult
            {
                IsSuccess = false,
                Error = "File is empty or was not provided.",
            };
        }
        var ext = Path.GetExtension(filename);
        var parser = _parsers.FirstOrDefault(p => p.AllowedExtension(ext));
        if (parser == null)
        {
            return new CreateDashboardResult
            {
                IsSuccess = false,
                Error = "Unsupported file type. Upload .csv, .xlsx or .pdf.",
            };
        }

        TableData parsed;
        try
        {
            parsed = parser.Parse(stream);
        }
        catch
        {
            return new CreateDashboardResult { IsSuccess = false, Error = "Сannot read that file" };
        }

        var id = Guid.NewGuid();
        var dashboard = new DashboardModel { Status = DashboardStatus.Processing };
        _store.Save(id, dashboard);
        await _queue.EnqueueAsync(new DashboardGenerationJob { DashboardId = id, Table = parsed });

        return new CreateDashboardResult { IsSuccess = true, Id = id };
    }
}
