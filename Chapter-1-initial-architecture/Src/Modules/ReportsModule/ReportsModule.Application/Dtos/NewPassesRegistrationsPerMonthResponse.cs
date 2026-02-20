namespace EvolutionaryArchitecture.Fitnet.Modules.ReportsModule.Application.Dtos;

public sealed record NewPassesRegistrationsPerMonthResponse(IReadOnlyCollection<NewPassesRegistrationsPerMonthDto> Passes)
{
    public static NewPassesRegistrationsPerMonthResponse Create(IReadOnlyCollection<NewPassesRegistrationsPerMonthDto> passes) =>
        new(passes);
}
