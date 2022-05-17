using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webapi_BitirmeProjesi.Entities;

namespace Webapi_BitirmeProjesi.DbOperations.EntityConfigurations
{
    public class ParticipantConfiguration : IEntityTypeConfiguration<Participant>
    {
        public void Configure(EntityTypeBuilder<Participant> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(p=>p.User)
                .WithMany(u=>u.Participants)
                .HasForeignKey(p=>p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
