using System.Text.Json;
using backend.Models;
namespace backend.Validation;

public class Validator {

public ValidationResult Validate(string aiResponse, List<string> columns)
{

    DashboardSpec? response;
    try
        {
           response =  JsonSerializer.Deserialize<DashboardSpec>(aiResponse);
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

        var allowedTypes = new List<string>{"bar", "line"};

        foreach (var chart in response.Charts)
        {
            if (!allowedTypes.Contains(chart.Type)) { errors.Add("unsupported chart type"); }
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
