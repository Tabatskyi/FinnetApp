namespace Fitnet.Modules.ReportsModule.Application.DataRetrieval;

using Dtos;

public interface INewPassesRegistrationPerMonthReportDataRetriever
{
    Task<IReadOnlyCollection<NewPassesRegistrationsPerMonthDto>> GetReportDataAsync(CancellationToken cancellationToken = default);
}
