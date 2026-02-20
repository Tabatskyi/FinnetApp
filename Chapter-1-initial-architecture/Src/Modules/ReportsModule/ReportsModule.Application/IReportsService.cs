namespace EvolutionaryArchitecture.Fitnet.Modules.ReportsModule.Application;

using Dtos;

public interface IReportsService
{
    Task<NewPassesRegistrationsPerMonthResponse> GenerateNewPassesPerMonthReportAsync(CancellationToken cancellationToken = default);
}
