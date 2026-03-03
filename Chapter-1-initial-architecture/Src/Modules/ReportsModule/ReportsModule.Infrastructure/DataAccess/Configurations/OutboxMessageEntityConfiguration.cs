namespace EvolutionaryArchitecture.Fitnet.Modules.ReportsModule.Infrastructure.DataAccess.Configurations;

using Application.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal sealed class OutboxMessageEntityConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.HasKey(message => message.Id);
        builder.Property(message => message.Type).IsRequired().HasMaxLength(200);
        builder.Property(message => message.Payload).IsRequired();
        builder.Property(message => message.CreatedAt).IsRequired();
        builder.Property(message => message.ProcessedAt);
        builder.Ignore(message => message.IsProcessed);
        builder.ToTable("OutboxMessages");
    }
}
