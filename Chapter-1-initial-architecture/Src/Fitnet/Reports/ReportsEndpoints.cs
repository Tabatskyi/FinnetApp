namespace EvolutionaryArchitecture.Fitnet.Reports;

internal static class ReportsEndpoints
{
    internal static void MapReports(this IEndpointRouteBuilder app) => app.MapGet(ReportsApiPaths.GenerateNewReport, async (
                                                                                    IReportsService reportsService,
                                                                                    CancellationToken cancellationToken) =>
                                                                                {
                                                                                    var report = await reportsService.GenerateNewPassesPerMonthReportAsync(cancellationToken);
                                                                                    return Results.Ok(report);
                                                                                })
            .WithSummary("Returns report of all passes registered in a month")
            .WithDescription("This endpoint is used to retrieve all passes that were registered in a given month.")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError);
}
