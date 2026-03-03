namespace EvolutionaryArchitecture.Fitnet.Modules.ReportsModule.Domain.Events;

public sealed record ReportGeneratedEvent(Guid ReportId, DateTimeOffset GeneratedAt, int Year);
