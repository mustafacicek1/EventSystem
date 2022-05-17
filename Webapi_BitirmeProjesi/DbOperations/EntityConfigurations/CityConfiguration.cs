using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webapi_BitirmeProjesi.Entities;

namespace Webapi_BitirmeProjesi.DbOperations.EntityConfigurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(30);

            builder.HasMany(c=>c.Events)
                .WithOne(c => c.City)
                .HasForeignKey(e=>e.CityId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
