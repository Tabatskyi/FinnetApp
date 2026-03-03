namespace EvolutionaryArchitecture.Fitnet.Modules.ReportsModule.Application.Saga;

public enum SagaStatus { Started, Completed, Failed }

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
    public string CorrelationId { get; init; } = string.Empty;
    public SagaStatus Status { get; private set; } = SagaStatus.Started;
    public int Year { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdatedAt { get; private set; }
    public GenerateReportSagaStep CurrentStep { get; private set; } = GenerateReportSagaStep.RetrievingData;
    public string? FailureReason { get; private set; }

    public static GenerateReportSagaState Start(int year, DateTimeOffset now) =>
        new() { Year = year, CorrelationId = year.ToString(System.Globalization.CultureInfo.InvariantCulture), CreatedAt = now, UpdatedAt = now };

    public void Complete(DateTimeOffset now)
    {
        CurrentStep = GenerateReportSagaStep.Completed;
        Status = SagaStatus.Completed;
        UpdatedAt = now;
    }

    public void Advance(DateTimeOffset now)
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

        UpdatedAt = now;
    }

    public void Fail(string reason, DateTimeOffset now)
    {
        CurrentStep = GenerateReportSagaStep.Failed;
        Status = SagaStatus.Failed;
        FailureReason = reason;
        UpdatedAt = now;
    }
}
