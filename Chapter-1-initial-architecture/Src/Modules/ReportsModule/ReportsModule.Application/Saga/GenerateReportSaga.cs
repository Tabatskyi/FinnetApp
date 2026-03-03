namespace EvolutionaryArchitecture.Fitnet.Modules.ReportsModule.Application.Saga;

using System.Text.Json;
using DataRetrieval;
using Domain;
using Domain.Events;
using Dtos;
using GeneratedReports;
using Outbox;

internal sealed class GenerateReportSaga(
    INewPassesRegistrationPerMonthReportDataRetriever dataRetriever,
    IGeneratedReportRepository generatedReportRepository,
    IOutboxRepository outboxRepository,
    IReportsUnitOfWork unitOfWork,
    TimeProvider timeProvider)
{
    public async Task<(GenerateReportSagaState State, NewPassesRegistrationsPerMonthResponse? Report)> ExecuteAsync(
        CancellationToken cancellationToken = default)
    {
        var state = GenerateReportSagaState.Start(timeProvider.GetUtcNow().Year);

        IReadOnlyCollection<NewPassesRegistrationsPerMonthDto> reportData;
        try
        {
            reportData = await dataRetriever.GetReportDataAsync(cancellationToken);
            state.Advance();
        }
        catch (Exception ex)
        {
            state.Fail($"Data retrieval failed: {ex.Message}");
            return (state, null);
        }

        var generatedReport = GeneratedReport.Create(state.Year, timeProvider.GetUtcNow());
        var @event = new ReportGeneratedEvent(state.SagaId, timeProvider.GetUtcNow(), state.Year);
        var outboxMessage = OutboxMessage.Create(
            nameof(ReportGeneratedEvent),
            JsonSerializer.Serialize(@event),
            timeProvider.GetUtcNow());
        try
        {
            await generatedReportRepository.AddAsync(generatedReport, cancellationToken);
            await outboxRepository.AddAsync(outboxMessage, cancellationToken);
            await unitOfWork.SaveAsync(cancellationToken);
            state.Advance();
        }
        catch (Exception ex)
        {
            state.Fail($"Persisting report failed: {ex.Message}");
            return (state, null);
        }

        state.Advance();

        return (state, NewPassesRegistrationsPerMonthResponse.Create(reportData));
    }
}
