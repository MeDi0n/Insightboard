using backend.Ai;
using backend.Building;
using backend.Generation;
using backend.Parsing;
using backend.Storage;
using backend.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("frontend", policy =>
        policy.WithOrigins("http://localhost:5173")  // кому разрешаем
              .AllowAnyHeader()
              .AllowAnyMethod());
});

builder.Services.AddSingleton<IAiProvider, AnthropicAiProvider>();

builder.Services.AddSingleton<PromptBuilder>();

builder.Services.AddSingleton<IFileParser, CsvParser>();

builder.Services.AddSingleton<IFileParser, ExcelParser>();

builder.Services.AddSingleton<IFileParser, PdfParser>();

builder.Services.AddSingleton<Validator>();

builder.Services.AddSingleton<DashboardStore>();

builder.Services.AddSingleton<DashboardGenerator>();

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

