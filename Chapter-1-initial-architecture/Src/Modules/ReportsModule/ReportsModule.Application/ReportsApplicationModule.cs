namespace Fitnet.Modules.ReportsModule.Application;

using Microsoft.Extensions.DependencyInjection;

public static class ReportsApplicationModule
{
    public static IServiceCollection AddReportsApplication(this IServiceCollection services)
    {
        services.AddScoped<IReportsService, ReportsService>();
        return services;
    }
}
