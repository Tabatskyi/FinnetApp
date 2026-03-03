namespace EvolutionaryArchitecture.Fitnet.Modules.ReportsModule.Application.Outbox;

public sealed class OutboxMessage
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Type { get; init; } = string.Empty;
    public string Payload { get; init; } = string.Empty;
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset? ProcessedAt { get; private set; }

    public bool IsProcessed => ProcessedAt.HasValue;

    public static OutboxMessage Create(string type, string payload, DateTimeOffset createdAt) =>
        new() { Type = type, Payload = payload, CreatedAt = createdAt };

    public void MarkProcessed(DateTimeOffset processedAt) => ProcessedAt = processedAt;
}
