using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webapi_BitirmeProjesi.Entities;

namespace Webapi_BitirmeProjesi.DbOperations.EntityConfigurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c=>c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(30);

            builder.HasMany(c=>c.Events)
                .WithOne(e=>e.Category)
                .HasForeignKey(e=>e.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
