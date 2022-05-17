using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webapi_BitirmeProjesi.Entities;

namespace Webapi_BitirmeProjesi.DbOperations.EntityConfigurations
{
    public class OrganizerConfiguration : IEntityTypeConfiguration<Organizer>
    {
        public void Configure(EntityTypeBuilder<Organizer> builder)
        {
            builder.HasKey(o => o.Id);

            builder.HasOne(o=>o.User)
                .WithMany(u=>u.Organizers)
                .HasForeignKey(o=>o.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
