namespace EvolutionaryArchitecture.Fitnet.Modules.ReportsModule.Application.Outbox;

public interface IOutboxRepository
{
    Task AddAsync(OutboxMessage message, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<OutboxMessage>> GetPendingAsync(CancellationToken cancellationToken = default);
    Task MarkProcessedAsync(Guid id, DateTimeOffset processedAt, CancellationToken cancellationToken = default);
}
