namespace EvolutionaryArchitecture.Fitnet.Modules.ReportsModule.Infrastructure.DataAccess;

using System.Data;

internal interface IDatabaseConnectionFactory : IDisposable
{
    IDbConnection Create();
}
