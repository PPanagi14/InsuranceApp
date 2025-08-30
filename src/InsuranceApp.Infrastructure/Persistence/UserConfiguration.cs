using InsuranceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InsuranceApp.Infrastructure.Persistence;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(u => u.Username).IsUnique();

        builder.Property(u => u.PasswordHash).IsRequired();

        builder.HasMany(u => u.Roles)
               .WithMany(r => r.Users)
               .UsingEntity(j => j.ToTable("UserRoles"));

        // --- Stable GUIDs ---
        var adminUserId = Guid.Parse("33333333-3333-3333-3333-333333333333");
        var brokerUserId = Guid.Parse("44444444-4444-4444-4444-444444444444");

        builder.HasData(
            new User
            {
                Id = adminUserId,
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                CreatedAtUtc = DateTime.UtcNow,
                UpdatedAtUtc = DateTime.UtcNow
            },
            new User
            {
                Id = brokerUserId,
                Username = "broker",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Broker@123"),
                CreatedAtUtc = DateTime.UtcNow,
                UpdatedAtUtc = DateTime.UtcNow
            }
        );

        // --- Seeding UserRoles join table ---
        builder.HasMany(u => u.Roles)
               .WithMany(r => r.Users)
               .UsingEntity(j => j.HasData(
                   // admin → ADMIN
                   new { RolesId = Guid.Parse("11111111-1111-1111-1111-111111111111"), UsersId = adminUserId },
                   // admin → BROKER
                   new { RolesId = Guid.Parse("22222222-2222-2222-2222-222222222222"), UsersId = adminUserId },
                   // broker → BROKER
                   new { RolesId = Guid.Parse("22222222-2222-2222-2222-222222222222"), UsersId = brokerUserId }
               ));
    }
}