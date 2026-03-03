namespace EvolutionaryArchitecture.Fitnet.Modules.ReportsModule.Infrastructure.Outbox;

using System.Text.Json;
using Application.Outbox;
using Application.Saga;
using Domain.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

internal sealed partial class OutboxProcessor(
    IServiceScopeFactory scopeFactory,
    TimeProvider timeProvider,
    ILogger<OutboxProcessor> logger) : BackgroundService
{
    private static readonly TimeSpan PollingInterval = TimeSpan.FromSeconds(10);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await ProcessPendingMessagesAsync(stoppingToken);
            await Task.Delay(PollingInterval, stoppingToken);
        }
    }

    private async Task ProcessPendingMessagesAsync(CancellationToken cancellationToken)
    {
        using var scope = scopeFactory.CreateScope();
        var outboxRepository = scope.ServiceProvider.GetRequiredService<IOutboxRepository>();
        var sagaStateRepository = scope.ServiceProvider.GetRequiredService<ISagaStateRepository>();
        var pending = await outboxRepository.GetPendingAsync(cancellationToken);

        foreach (var message in pending)
        {
            try
            {
                LogDispatchingMessage(logger, message.Id, message.Type, message.CreatedAt);
                await UpdateSagaStateAsync(message, sagaStateRepository, cancellationToken);
                await outboxRepository.MarkProcessedAsync(message.Id, timeProvider.GetUtcNow(), cancellationToken);
            }
            catch (Exception ex)
            {
                LogDispatchFailed(logger, message.Id, ex);
            }
        }
    }

    private async Task UpdateSagaStateAsync(OutboxMessage message, ISagaStateRepository sagaStateRepository, CancellationToken cancellationToken)
    {
        if (message.Type != nameof(ReportGeneratedEvent))
        {
            return;
        }

        var @event = JsonSerializer.Deserialize<ReportGeneratedEvent>(message.Payload);
        if (@event is null)
        {
            return;
        }

        var sagaState = await sagaStateRepository.FindBySagaIdAsync(@event.ReportId, cancellationToken);
        if (sagaState is null || sagaState.Status == SagaStatus.Completed)
        {
            return;
        }

        sagaState.Complete(timeProvider.GetUtcNow());
    }

    [LoggerMessage(Level = LogLevel.Information, Message = "Dispatching outbox message {Id} of type {Type} occurred at {CreatedAt}")]
    private static partial void LogDispatchingMessage(ILogger logger, Guid id, string type, DateTimeOffset createdAt);

    [LoggerMessage(Level = LogLevel.Error, Message = "Failed to dispatch outbox message {Id}")]
    private static partial void LogDispatchFailed(ILogger logger, Guid id, Exception ex);
}
