using LoremIpsumLogistica.Server.Domains.Entities;
using LoremIpsumLogistica.Server.Utils.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoremIpsumLogistica.Server.Infraestructure.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("TB_CUSTOMER", DatabaseSchemas.Backoffice);

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.DateOfBirth)
                .IsRequired();

            builder.Property(c => c.Gender)
                .IsRequired();
        }
    }
}