using InsuranceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InsuranceApp.Infrastructure.Persistence.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.FirstName).HasMaxLength(120);
        builder.Property(c => c.LastName).HasMaxLength(120);
        builder.Property(c => c.CompanyName).HasMaxLength(200);
        builder.Property(c => c.Email).HasMaxLength(200).IsRequired();
        builder.Property(c => c.PhoneMobile).HasMaxLength(30);
        builder.Property(c => c.City).HasMaxLength(120);

        builder.HasMany(c => c.Policies)
               .WithOne(p => p.Client)
               .HasForeignKey(p => p.ClientId);
    }
}
