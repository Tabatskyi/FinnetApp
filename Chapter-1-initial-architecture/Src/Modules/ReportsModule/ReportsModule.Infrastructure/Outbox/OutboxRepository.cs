namespace EvolutionaryArchitecture.Fitnet.Modules.ReportsModule.Infrastructure.Outbox;

using System.Collections.Concurrent;
using Application.Outbox;

internal sealed class OutboxRepository : IOutboxRepository
{
    private readonly ConcurrentQueue<OutboxMessage> _store = new();

    public Task AddAsync(OutboxMessage message, CancellationToken cancellationToken = default)
    {
        _store.Enqueue(message);
        return Task.CompletedTask;
    }

    public Task<IReadOnlyCollection<OutboxMessage>> GetPendingAsync(CancellationToken cancellationToken = default)
    {
        IReadOnlyCollection<OutboxMessage> pending = [.. _store.Where(m => !m.IsProcessed)];
        return Task.FromResult(pending);
    }

    public Task MarkProcessedAsync(Guid id, DateTimeOffset processedAt, CancellationToken cancellationToken = default)
    {
        var message = _store.FirstOrDefault(m => m.Id == id);
        message?.MarkProcessed(processedAt);
        return Task.CompletedTask;
    }
}
