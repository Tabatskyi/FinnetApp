namespace Fitnet.Modules.ReportsModule.Application.Dtos;

public sealed record NewPassesRegistrationsPerMonthDto(int MonthOrder, string MonthName, long RegisteredPasses);
