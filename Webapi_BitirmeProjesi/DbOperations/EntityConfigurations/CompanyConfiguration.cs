using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webapi_BitirmeProjesi.Entities;

namespace Webapi_BitirmeProjesi.DbOperations.EntityConfigurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.CompanyName).IsRequired().HasMaxLength(30);
            builder.Property(c => c.Domain).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Mail).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Password).IsRequired().HasMaxLength(30);
            builder.Property(c => c.Role).IsRequired().HasMaxLength(20);

        }
    }
}
