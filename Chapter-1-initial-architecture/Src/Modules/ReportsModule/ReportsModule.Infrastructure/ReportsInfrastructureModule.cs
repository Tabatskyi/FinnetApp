namespace EvolutionaryArchitecture.Fitnet.Modules.ReportsModule.Infrastructure;

using Application;
using Application.DataRetrieval;
using Application.GeneratedReports;
using Application.Outbox;
using DataAccess;
using DataRetrieval;
using GeneratedReports;
using Microsoft.EntityFrameworkCore;
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
        services.AddScoped<INewPassesRegistrationPerMonthReportDataRetriever, NewPassesRegistrationPerMonthReportDataRetriever>();

        services.AddDbContext<ReportsPersistence>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Reports")));

        services.AddScoped<IOutboxRepository, OutboxRepository>();
        services.AddScoped<IGeneratedReportRepository, GeneratedReportRepository>();
        services.AddScoped<IReportsUnitOfWork>(provider => provider.GetRequiredService<ReportsPersistence>());

        services.AddHostedService<OutboxProcessor>();

        return services;
    }
}
