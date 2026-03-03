namespace EvolutionaryArchitecture.Fitnet.Modules.ReportsModule.Infrastructure.DataAccess.Configurations;

using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal sealed class GeneratedReportEntityConfiguration : IEntityTypeConfiguration<GeneratedReport>
{
    public void Configure(EntityTypeBuilder<GeneratedReport> builder)
    {
        builder.HasKey(report => report.Id);
        builder.Property(report => report.Year).IsRequired();
        builder.Property(report => report.GeneratedAt).IsRequired();
        builder.ToTable("GeneratedReports");
    }
}
