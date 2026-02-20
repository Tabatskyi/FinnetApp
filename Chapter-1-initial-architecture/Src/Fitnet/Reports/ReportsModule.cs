namespace EvolutionaryArchitecture.Fitnet.Reports;

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
