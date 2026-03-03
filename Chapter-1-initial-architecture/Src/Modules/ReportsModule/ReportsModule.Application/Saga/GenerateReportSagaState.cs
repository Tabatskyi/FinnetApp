namespace EvolutionaryArchitecture.Fitnet.Modules.ReportsModule.Application.Saga;

public enum GenerateReportSagaStep
{
    RetrievingData,
    PersistingReport,
    PublishingEvent,
    Completed,
    Failed
}

public sealed class GenerateReportSagaState
{
    public Guid SagaId { get; init; } = Guid.NewGuid();
    public int Year { get; init; }
    public GenerateReportSagaStep CurrentStep { get; private set; } = GenerateReportSagaStep.RetrievingData;
    public string? FailureReason { get; private set; }

    public static GenerateReportSagaState Start(int year) => new() { Year = year };

    public void Advance()
    {
        if (CurrentStep is GenerateReportSagaStep.Completed or GenerateReportSagaStep.Failed)
        {
            return;
        }

        if (CurrentStep == GenerateReportSagaStep.RetrievingData)
        {
            CurrentStep = GenerateReportSagaStep.PersistingReport;
        }
        else if (CurrentStep == GenerateReportSagaStep.PersistingReport)
        {
            CurrentStep = GenerateReportSagaStep.PublishingEvent;
        }
        else if (CurrentStep == GenerateReportSagaStep.PublishingEvent)
        {
            CurrentStep = GenerateReportSagaStep.Completed;
        }
    }

    public void Fail(string reason)
    {
        CurrentStep = GenerateReportSagaStep.Failed;
        FailureReason = reason;
    }
}
