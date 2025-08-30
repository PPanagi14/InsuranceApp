using InsuranceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InsuranceApp.Infrastructure.Persistence;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");
        builder.HasKey(r => r.Id);

        builder.HasOne(r => r.RoleType)
               .WithMany(rt => rt.Roles)
               .HasForeignKey(r => r.RoleTypeId);

        //  Seed Roles (linked to RoleTypes)
        var adminRoleId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var brokerRoleId = Guid.Parse("22222222-2222-2222-2222-222222222222");

        builder.HasData(
            new Role
            {
                Id = adminRoleId,
                RoleTypeId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                CreatedAtUtc = DateTime.UtcNow,
                UpdatedAtUtc = DateTime.UtcNow
            },
            new Role
            {
                Id = brokerRoleId,
                RoleTypeId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                CreatedAtUtc = DateTime.UtcNow,
                UpdatedAtUtc = DateTime.UtcNow
            }
        );
    }
}
