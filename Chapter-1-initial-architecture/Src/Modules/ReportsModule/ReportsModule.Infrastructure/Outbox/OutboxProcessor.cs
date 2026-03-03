namespace EvolutionaryArchitecture.Fitnet.Modules.ReportsModule.Infrastructure.Outbox;

using Application.Outbox;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

internal sealed partial class OutboxProcessor(
    IOutboxRepository outboxRepository,
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
        var pending = await outboxRepository.GetPendingAsync(cancellationToken);

        foreach (var message in pending)
        {
            try
            {
                LogDispatchingMessage(logger, message.Id, message.EventType, message.OccurredAt);
                await outboxRepository.MarkProcessedAsync(message.Id, timeProvider.GetUtcNow(), cancellationToken);
            }
            catch (Exception ex)
            {
                LogDispatchFailed(logger, message.Id, ex);
            }
        }
    }

    [LoggerMessage(Level = LogLevel.Information, Message = "Dispatching outbox message {Id} of type {EventType} occurred at {OccurredAt}")]
    private static partial void LogDispatchingMessage(ILogger logger, Guid id, string eventType, DateTimeOffset occurredAt);

    [LoggerMessage(Level = LogLevel.Error, Message = "Failed to dispatch outbox message {Id}")]
    private static partial void LogDispatchFailed(ILogger logger, Guid id, Exception ex);
}
