using System.Text.Json;
using backend.Models;
namespace backend.Validation;

public class Validator {

public ValidationResult Validate(string aiResponse, List<string> columns)
{


    DashboardSpec? response;
    try
        {
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
           response =  JsonSerializer.Deserialize<DashboardSpec>(aiResponse, options);
        }
    catch(JsonException)
        {
            return new ValidationResult{IsValid = false, Errors = new List<string>{"invalid Json"}};
        }

        if(response == null)
        {
            return new ValidationResult{IsValid = false, Errors = new List<string>{"result is null"}};
        }

        var errors = new List<string>();

        if(response.Charts == null || response.Charts.Count == 0)
        {
            errors.Add("no charts");
        }
        else
        {
            foreach(var chart in response.Charts)
            {
                if (string.IsNullOrWhiteSpace(chart.Type))
                {
                    errors.Add("chart type is empty");
                }

                if (string.IsNullOrWhiteSpace(chart.Title))
                {
                    errors.Add("chart title is empty");
                }

                if (string.IsNullOrWhiteSpace(chart.X))
                {
                    errors.Add("chart x is empty");
                }

                if (string.IsNullOrWhiteSpace(chart.Y))
                {
                    errors.Add("chart y is empty");
                }
            }
        }

         if(errors.Count > 0)
        {
             return new ValidationResult{IsValid = false, Errors = errors};
        }

        foreach (var chart in response.Charts!)
        {
            if (!Enum.TryParse<ChartType>(chart.Type, ignoreCase: true, out var parsed))
            {
                errors.Add("unsupported chart type");
            }
            else
            {
                chart.Type = parsed.ToString().ToLowerInvariant();
            }
            if (!columns.Contains(chart.X))         { errors.Add("x column not found"); }
            if (!columns.Contains(chart.Y))         { errors.Add("y column not found"); }
        }
        if(errors.Count > 0)
        {
             return new ValidationResult{IsValid = false, Errors = errors};
        }

        return new ValidationResult{IsValid = true, Spec = response};
}
}
