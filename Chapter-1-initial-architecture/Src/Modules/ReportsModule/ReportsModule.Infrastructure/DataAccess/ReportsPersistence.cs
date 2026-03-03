namespace EvolutionaryArchitecture.Fitnet.Modules.ReportsModule.Infrastructure.DataAccess;

using Application;
using Application.Outbox;
using Configurations;
using Domain;
using Microsoft.EntityFrameworkCore;

internal sealed class ReportsPersistence(DbContextOptions<ReportsPersistence> options) : DbContext(options), IReportsUnitOfWork
{
    private const string Schema = "Reports";

    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();
    public DbSet<GeneratedReport> GeneratedReports => Set<GeneratedReport>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);
        modelBuilder.ApplyConfiguration(new OutboxMessageEntityConfiguration());
        modelBuilder.ApplyConfiguration(new GeneratedReportEntityConfiguration());
    }

    async Task IReportsUnitOfWork.SaveAsync(CancellationToken cancellationToken) =>
        await SaveChangesAsync(cancellationToken);
}
