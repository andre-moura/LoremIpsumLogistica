using LoremIpsumLogistica.Server.Domains.Entities;
using LoremIpsumLogistica.Server.Utils.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoremIpsumLogistica.Server.Infraestructure.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("TB_ADDRESSES", DatabaseSchemas.Backoffice);

            builder.HasKey(a => a.Id);

            builder.Property(a => a.ZipCode)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Number)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(a => a.Complement)
                .HasMaxLength(100);

            builder.Property(a => a.Neighborhood)
                .HasMaxLength(50);

            builder.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.State)
                .IsRequired()
                .HasMaxLength(2);

            builder.Property(a => a.AddressType)
                .IsRequired();

            builder.HasOne(a => a.Customer)
                .WithMany(c => c.Addresses)
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}