namespace EvolutionaryArchitecture.Fitnet.Modules.ReportsModule.Infrastructure.DataAccess;

internal sealed class ReportsPersistenceOptions
{
    internal const string SectionName = "Modules:Reports";
    public string ConnectionString { get; init; } = string.Empty;
}
