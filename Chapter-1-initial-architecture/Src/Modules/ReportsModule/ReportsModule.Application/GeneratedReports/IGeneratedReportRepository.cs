namespace EvolutionaryArchitecture.Fitnet.Modules.ReportsModule.Application.GeneratedReports;

using Domain;

public interface IGeneratedReportRepository
{
    Task AddAsync(GeneratedReport report, CancellationToken cancellationToken = default);
}
