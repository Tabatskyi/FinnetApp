namespace EvolutionaryArchitecture.Fitnet.Modules.ReportsModule.Infrastructure.DataAccess;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

internal sealed class ReportsPersistenceFactory : IDesignTimeDbContextFactory<ReportsPersistence>
{
    public ReportsPersistence CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ReportsPersistence>();
        optionsBuilder.UseNpgsql();

        return new ReportsPersistence(optionsBuilder.Options);
    }
}
