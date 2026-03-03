namespace EvolutionaryArchitecture.Fitnet.Modules.ReportsModule.Infrastructure;

using Application.DataRetrieval;
using Application.Outbox;
using DataAccess;
using DataRetrieval;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Outbox;

public static class ReportsInfrastructureModule
{
    public static IServiceCollection AddReportsInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ReportsPersistenceOptions>(configuration.GetSection(ReportsPersistenceOptions.SectionName));
        services.AddOptionsWithValidateOnStart<ReportsPersistenceOptions>();
        services.AddScoped<IDatabaseConnectionFactory, DatabaseConnectionFactory>();
        services.AddSingleton<INewPassesRegistrationPerMonthReportDataRetriever, NewPassesRegistrationPerMonthReportDataRetriever>();

        services.AddSingleton<IOutboxRepository, OutboxRepository>();
        services.AddHostedService<OutboxProcessor>();

        return services;
    }
}
