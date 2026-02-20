namespace Fitnet.Modules.ReportsModule.Application;

using DataRetrieval;
using Dtos;

internal sealed class ReportsService(INewPassesRegistrationPerMonthReportDataRetriever dataRetriever) : IReportsService
{
    public async Task<NewPassesRegistrationsPerMonthResponse> GenerateNewPassesPerMonthReportAsync(CancellationToken cancellationToken = default)
    {
        var reportData = await dataRetriever.GetReportDataAsync(cancellationToken);
        return NewPassesRegistrationsPerMonthResponse.Create(reportData);
    }
}
