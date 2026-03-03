namespace EvolutionaryArchitecture.Fitnet.Modules.ReportsModule.Infrastructure.DataAccess;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class AutomaticMigrationsExtensions
{
    public static void UseReportsMigrations(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ReportsPersistence>();
        context.Database.Migrate();
    }
}
