namespace EvolutionaryArchitecture.Fitnet.Modules.ReportsModule.Infrastructure.GeneratedReports;

using Application.GeneratedReports;
using DataAccess;
using Domain;

internal sealed class GeneratedReportRepository(ReportsPersistence context) : IGeneratedReportRepository
{
    public Task AddAsync(GeneratedReport report, CancellationToken cancellationToken = default)
    {
        context.GeneratedReports.Add(report);
        return Task.CompletedTask;
    }
}
