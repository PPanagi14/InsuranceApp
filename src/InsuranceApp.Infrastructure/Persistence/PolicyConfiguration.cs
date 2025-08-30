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

        builder.HasIndex(p => p.PolicyNumber);
    }
}
