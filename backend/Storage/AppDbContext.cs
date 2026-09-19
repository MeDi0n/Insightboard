using System.Text.Json;
using Insightboard.Api.Models.AiCalls;
using Insightboard.Api.Models.Dashboards;
using Microsoft.EntityFrameworkCore;

namespace Insightboard.Api.Storage;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<DashboardModel> Dashboards => Set<DashboardModel>();
    public DbSet<AiCallLog> AiCallLogs => Set<AiCallLog>();

    private static readonly JsonSerializerOptions JsonOptions = new();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DashboardModel>().Property(d => d.Status).HasConversion<string>();

        modelBuilder
            .Entity<DashboardModel>()
            .Property(d => d.Spec)
            .HasConversion(
                spec => JsonSerializer.Serialize(spec, JsonOptions),
                json => JsonSerializer.Deserialize<DashboardSpec>(json, JsonOptions)
            );

        modelBuilder
            .Entity<AiCallLog>()
            .HasOne<DashboardModel>()
            .WithMany()
            .HasForeignKey(l => l.DashboardId);
    }
}
