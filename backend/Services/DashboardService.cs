using Insightboard.Api.Ai;
using Insightboard.Api.Building;
using Insightboard.Api.Models.Dashboards;
using Insightboard.Api.Parsing;
using Insightboard.Api.Services.Abstractions;
using Insightboard.Api.Storage;
using Insightboard.Api.Validation;

namespace Insightboard.Api.Services;

public class DashboardService : IDashboardService
{
    private readonly IAiProvider _ai;
    private readonly Validator _validator;
    private readonly PromptBuilder _builder;
    private readonly IEnumerable<IFileParser> _parsers;
    private readonly DashboardStore _store;

    public DashboardService(
        IAiProvider ai,
        Validator validator,
        PromptBuilder builder,
        IEnumerable<IFileParser> parsers,
        DashboardStore store
    )
    {
        _ai = ai;
        _validator = validator;
        _builder = builder;
        _parsers = parsers;
        _store = store;
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

    public async Task<CreateDashboardResult> CreateAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return new CreateDashboardResult
            {
                IsSuccess = false,
                Error = "File is empty or was not provided.",
            };
        }
        var ext = Path.GetExtension(file.FileName);
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
            parsed = parser.Parse(file);
        }
        catch
        {
            return new CreateDashboardResult { IsSuccess = false, Error = "Сannot read that file" };
        }

        var id = Guid.NewGuid();
        var dashboard = new DashboardModel { Status = DashboardStatus.Processing };
        _store.Save(id, dashboard);
        _ = Task.Run(async () =>
        {
            var spec = await GenerateAsync(parsed);
            if (spec == null)
            {
                dashboard.Status = DashboardStatus.Failed;
            }
            else
            {
                dashboard.Status = DashboardStatus.Done;
                dashboard.Spec = spec;
            }
        });

        return new CreateDashboardResult { IsSuccess = true, Id = id };
    }
}
