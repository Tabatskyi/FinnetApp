namespace EvolutionaryArchitecture.Fitnet.Modules.ReportsModule.Infrastructure.DataAccess.Configurations;

using Application.Saga;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal sealed class GenerateReportSagaStateEntityConfiguration : IEntityTypeConfiguration<GenerateReportSagaState>
{
    public void Configure(EntityTypeBuilder<GenerateReportSagaState> builder)
    {
        builder.HasKey(s => s.SagaId);
        builder.Property(s => s.CorrelationId).IsRequired().HasMaxLength(50);
        builder.Property(s => s.Status).IsRequired().HasConversion<string>().HasMaxLength(20);
        builder.Property(s => s.CurrentStep).IsRequired().HasConversion<string>().HasMaxLength(30);
        builder.Property(s => s.Year).IsRequired();
        builder.Property(s => s.CreatedAt).IsRequired();
        builder.Property(s => s.UpdatedAt).IsRequired();
        builder.Property(s => s.FailureReason).HasMaxLength(500);
        builder.HasIndex(s => s.CorrelationId).IsUnique();
        builder.ToTable("SagaStates");
    }
}
