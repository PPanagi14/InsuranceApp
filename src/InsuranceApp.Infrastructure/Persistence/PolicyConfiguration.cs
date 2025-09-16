using InsuranceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InsuranceApp.Infrastructure.Persistence.Configurations;

public class PolicyConfiguration : IEntityTypeConfiguration<Policy>
{
    public void Configure(EntityTypeBuilder<Policy> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Insurer).HasMaxLength(120).IsRequired();
        builder.Property(p => p.PolicyNumber).HasMaxLength(120).IsRequired();

        builder.Property(p => p.Currency).HasMaxLength(3).IsRequired();
        builder.Property(p => p.PremiumAmount).HasColumnType("decimal(18,2)");

        // New fields
        builder.Property(p => p.CoverageAmount).HasColumnType("decimal(18,2)");
        builder.Property(p => p.PaymentMethod).HasMaxLength(50);
        builder.Property(p => p.BrokerCommission).HasColumnType("decimal(5,2)");

        builder.Property(p => p.StartDate).IsRequired();
        builder.Property(p => p.EndDate).IsRequired();
        builder.Property(p => p.RenewalDate);

        // Indexes
        builder.HasIndex(p => p.PolicyNumber).IsUnique(false);
    }
}

