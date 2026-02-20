namespace EvolutionaryArchitecture.Fitnet.Reports;

using EvolutionaryArchitecture.Fitnet.Modules.ReportsModule.Application;
using EvolutionaryArchitecture.Fitnet.Modules.ReportsModule.Infrastructure;

internal static class ReportsModule
{
    internal static IServiceCollection AddReports(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddReportsApplication();
        services.AddReportsInfrastructure(configuration);

        return services;
    }

    internal static IApplicationBuilder UseReports(this IApplicationBuilder applicationBuilder) =>
        applicationBuilder;
}
