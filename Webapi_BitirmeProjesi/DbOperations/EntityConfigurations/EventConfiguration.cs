using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webapi_BitirmeProjesi.Entities;

namespace Webapi_BitirmeProjesi.DbOperations.EntityConfigurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e=> e.Id);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(50);
            builder.Property(e => e.EventDate).HasColumnType("datetime");
            builder.Property(e => e.LastApplicationDate).HasColumnType("datetime");
            builder.Property(e => e.Description).IsRequired().HasMaxLength(500);
            builder.Property(e => e.Address).IsRequired().HasMaxLength(250);
            builder.Property(e => e.IsItPaid).IsRequired();
            builder.Property(e => e.EventStatus).IsRequired().HasDefaultValue(true);
            builder.Property(e => e.TicketPrice).HasPrecision(14,2);

            builder.HasOne(e=>e.Organizer)
                .WithMany(o=>o.Events)
                .HasForeignKey(e=>e.OrganizerId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
