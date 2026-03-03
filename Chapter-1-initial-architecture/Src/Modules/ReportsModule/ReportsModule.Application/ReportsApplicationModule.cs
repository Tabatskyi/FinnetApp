namespace EvolutionaryArchitecture.Fitnet.Modules.ReportsModule.Application;

using Microsoft.Extensions.DependencyInjection;
using Saga;

public static class ReportsApplicationModule
{
    public static IServiceCollection AddReportsApplication(this IServiceCollection services)
    {
        services.AddScoped<GenerateReportSaga>();
        services.AddScoped<IReportsService, ReportsService>();
        return services;
    }
}
