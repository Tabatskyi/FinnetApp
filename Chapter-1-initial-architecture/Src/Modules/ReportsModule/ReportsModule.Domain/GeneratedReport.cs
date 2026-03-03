namespace EvolutionaryArchitecture.Fitnet.Modules.ReportsModule.Domain;

public sealed class GeneratedReport
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public int Year { get; init; }
    public DateTimeOffset GeneratedAt { get; init; }

    public static GeneratedReport Create(int year, DateTimeOffset generatedAt) =>
        new() { Year = year, GeneratedAt = generatedAt };
}
