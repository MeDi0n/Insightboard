using backend.Ai;
using backend.Generation;
using backend.Storage;

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

builder.Services.AddSingleton<IAiProvider, FakeAiProvider>();

builder.Services.AddSingleton<DashboardStore>();

builder.Services.AddScoped<DashboardGenerator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("frontend");

app.UseAuthorization();

app.MapControllers();

app.Run();

