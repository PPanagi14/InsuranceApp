using InsuranceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InsuranceApp.Infrastructure.Persistence.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(c => c.Id);

        // Person
        builder.Property(c => c.FirstName).HasMaxLength(120);
        builder.Property(c => c.LastName).HasMaxLength(120);

        // Company
        builder.Property(c => c.CompanyName).HasMaxLength(200);
        builder.Property(c => c.VatNumber).HasMaxLength(50);

        // Contact
        builder.Property(c => c.Email).HasMaxLength(200).IsRequired();
        builder.Property(c => c.PhoneMobile).HasMaxLength(30);
        builder.Property(c => c.Street).HasMaxLength(200);
        builder.Property(c => c.City).HasMaxLength(120);
        builder.Property(c => c.PostalCode).HasMaxLength(20);
        builder.Property(c => c.Country).HasMaxLength(120);

        // Notes
        builder.Property(c => c.Notes).HasMaxLength(1000);


        // Relationships
        builder.HasMany(c => c.Policies)
               .WithOne(p => p.Client)
               .HasForeignKey(p => p.ClientId);
    }
}

