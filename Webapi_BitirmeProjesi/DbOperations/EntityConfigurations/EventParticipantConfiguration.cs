using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webapi_BitirmeProjesi.Entities;

namespace Webapi_BitirmeProjesi.DbOperations.EntityConfigurations
{
    public class EventParticipantConfiguration : IEntityTypeConfiguration<EventParticipant>
    {
        public void Configure(EntityTypeBuilder<EventParticipant> builder)
        {
            builder.HasKey(ep => new { ep.EventId, ep.ParticipantId }).IsClustered(false);
            builder.Property(ep => ep.ParticipationStatus).IsRequired();

            builder.HasOne(ep=>ep.Event)
                .WithMany(e=>e.EventParticipants)
                .HasForeignKey(ep=>ep.EventId)
                .HasConstraintName("FK_EventParticipants_EventId");

            builder.HasOne(ep => ep.Participant)
                .WithMany(p => p.EventParticipants)
                .HasForeignKey(ep => ep.ParticipantId)
                .HasConstraintName("FK_EventParticipants_ParticipantId");
        }
    }
}
