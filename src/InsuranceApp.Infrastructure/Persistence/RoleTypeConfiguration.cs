using InsuranceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InsuranceApp.Infrastructure.Persistence;

public class RoleTypeConfiguration : IEntityTypeConfiguration<RoleTypeEntity>
{
    public void Configure(EntityTypeBuilder<RoleTypeEntity> builder)
    {
        builder.ToTable("RoleTypes");
        builder.HasKey(rt => rt.Id);

        builder.Property(rt => rt.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(rt => rt.Code).IsUnique();

        builder.Property(rt => rt.Description).HasMaxLength(200);

        
        var adminRoleTypeId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
        var brokerRoleTypeId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

        builder.HasData(
            new RoleTypeEntity
            {
                Id = adminRoleTypeId,
                Code = "ADMIN",
                Description = "System administrator",
                CreatedAtUtc = DateTime.UtcNow,
                UpdatedAtUtc = DateTime.UtcNow
            },
            new RoleTypeEntity
            {
                Id = brokerRoleTypeId,
                Code = "BROKER",
                Description = "Insurance broker",
                CreatedAtUtc = DateTime.UtcNow,
                UpdatedAtUtc = DateTime.UtcNow
            }
        );
    }
}
