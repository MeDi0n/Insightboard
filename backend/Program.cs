using System.Text.Json;
using System.Text.Json.Serialization;
using Insightboard.Api.Ai;
using Insightboard.Api.Background;
using Insightboard.Api.Building;
using Insightboard.Api.Parsing;
using Insightboard.Api.Services;
using Insightboard.Api.Services.Abstractions;
using Insightboard.Api.Storage;
using Insightboard.Api.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder
    .Services.AddControllers()
    .AddJsonOptions(
        (options) =>
        {
            options.JsonSerializerOptions.Converters.Add(
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
            );
        }
    );

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "frontend",
        policy =>
            policy
                .WithOrigins("http://localhost:5173") // кому разрешаем
                .AllowAnyHeader()
                .AllowAnyMethod()
    );
});

builder.Services.AddSingleton<IAiProvider, AnthropicAiProvider>();

builder.Services.AddSingleton<PromptBuilder>();

builder.Services.AddSingleton<IFileParser, CsvParser>();

builder.Services.AddSingleton<IFileParser, ExcelParser>();

builder.Services.AddSingleton<IFileParser, PdfParser>();

builder.Services.AddSingleton<Validator>();

builder.Services.AddSingleton<DashboardStore>();
builder.Services.AddSingleton<IDashboardService, DashboardService>();

builder.Services.AddSingleton<DashboardGenerationQueue>();

builder.Services.AddHostedService<DashboardGenerationWorker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseCors("frontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
