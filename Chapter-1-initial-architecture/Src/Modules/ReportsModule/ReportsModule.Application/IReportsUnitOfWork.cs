namespace EvolutionaryArchitecture.Fitnet.Modules.ReportsModule.Application;

public interface IReportsUnitOfWork
{
    Task SaveAsync(CancellationToken cancellationToken = default);
}
