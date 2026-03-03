namespace EvolutionaryArchitecture.Fitnet.Modules.ReportsModule.Infrastructure.Saga;

using Application.Saga;
using DataAccess;
using Microsoft.EntityFrameworkCore;

internal sealed class SagaStateRepository(ReportsPersistence context) : ISagaStateRepository
{
    public Task AddAsync(GenerateReportSagaState state, CancellationToken cancellationToken = default)
    {
        context.SagaStates.Add(state);
        return Task.CompletedTask;
    }

    public Task<GenerateReportSagaState?> FindBySagaIdAsync(Guid sagaId, CancellationToken cancellationToken = default) =>
        context.SagaStates.FirstOrDefaultAsync(s => s.SagaId == sagaId, cancellationToken);
}
