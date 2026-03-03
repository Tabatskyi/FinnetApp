namespace EvolutionaryArchitecture.Fitnet.Modules.ReportsModule.Infrastructure.Outbox;

using Application.Outbox;
using DataAccess;
using Microsoft.EntityFrameworkCore;

internal sealed class OutboxRepository(ReportsPersistence context) : IOutboxRepository
{
    public Task AddAsync(OutboxMessage message, CancellationToken cancellationToken = default)
    {
        context.OutboxMessages.Add(message);
        return Task.CompletedTask;
    }

    public async Task<IReadOnlyCollection<OutboxMessage>> GetPendingAsync(CancellationToken cancellationToken = default)
    {
        var pending = await context.OutboxMessages
            .Where(m => m.ProcessedAt == null)
            .ToListAsync(cancellationToken);

        return pending;
    }

    public async Task MarkProcessedAsync(Guid id, DateTimeOffset processedAt, CancellationToken cancellationToken = default)
    {
        var message = await context.OutboxMessages.FindAsync([id], cancellationToken);
        message?.MarkProcessed(processedAt);
        await context.SaveChangesAsync(cancellationToken);
    }
}
