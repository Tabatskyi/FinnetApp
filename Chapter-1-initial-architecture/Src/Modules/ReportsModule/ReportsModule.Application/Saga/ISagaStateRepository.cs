namespace EvolutionaryArchitecture.Fitnet.Modules.ReportsModule.Application.Saga;

public interface ISagaStateRepository
{
    Task AddAsync(GenerateReportSagaState state, CancellationToken cancellationToken = default);
    Task<GenerateReportSagaState?> FindBySagaIdAsync(Guid sagaId, CancellationToken cancellationToken = default);
    Task<GenerateReportSagaState?> FindByCorrelationIdAsync(string correlationId, CancellationToken cancellationToken = default);
}
