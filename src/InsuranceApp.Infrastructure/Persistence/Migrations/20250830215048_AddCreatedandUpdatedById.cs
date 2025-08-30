using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceApp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedandUpdatedById : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "RoleTypes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "RoleTypes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "RoleTypes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Roles",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "Roles",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "Roles",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Policies",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "Policies",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "Policies",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Clients",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "Clients",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "Clients",
                type: "uuid",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "RoleTypes",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "CreatedAtUtc", "CreatedBy", "DeletedBy", "UpdatedAtUtc", "UpdatedBy" },
                values: new object[] { new DateTime(2025, 8, 30, 21, 50, 48, 384, DateTimeKind.Utc).AddTicks(6584), null, null, new DateTime(2025, 8, 30, 21, 50, 48, 384, DateTimeKind.Utc).AddTicks(6585), null });

            migrationBuilder.UpdateData(
                table: "RoleTypes",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                columns: new[] { "CreatedAtUtc", "CreatedBy", "DeletedBy", "UpdatedAtUtc", "UpdatedBy" },
                values: new object[] { new DateTime(2025, 8, 30, 21, 50, 48, 384, DateTimeKind.Utc).AddTicks(6587), null, null, new DateTime(2025, 8, 30, 21, 50, 48, 384, DateTimeKind.Utc).AddTicks(6588), null });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAtUtc", "CreatedBy", "DeletedBy", "UpdatedAtUtc", "UpdatedBy" },
                values: new object[] { new DateTime(2025, 8, 30, 21, 50, 48, 384, DateTimeKind.Utc).AddTicks(4546), null, null, new DateTime(2025, 8, 30, 21, 50, 48, 384, DateTimeKind.Utc).AddTicks(4548), null });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAtUtc", "CreatedBy", "DeletedBy", "UpdatedAtUtc", "UpdatedBy" },
                values: new object[] { new DateTime(2025, 8, 30, 21, 50, 48, 384, DateTimeKind.Utc).AddTicks(4551), null, null, new DateTime(2025, 8, 30, 21, 50, 48, 384, DateTimeKind.Utc).AddTicks(4551), null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAtUtc", "CreatedBy", "DeletedBy", "PasswordHash", "UpdatedAtUtc", "UpdatedBy" },
                values: new object[] { new DateTime(2025, 8, 30, 21, 50, 48, 528, DateTimeKind.Utc).AddTicks(4857), null, null, "$2b$10$hN8zBjZ7FMacXOsiwO.gJu2WR4zux2AsbHycZZyNzmglLf0p1S6YK", new DateTime(2025, 8, 30, 21, 50, 48, 528, DateTimeKind.Utc).AddTicks(4863), null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAtUtc", "CreatedBy", "DeletedBy", "PasswordHash", "UpdatedAtUtc", "UpdatedBy" },
                values: new object[] { new DateTime(2025, 8, 30, 21, 50, 48, 593, DateTimeKind.Utc).AddTicks(3186), null, null, "$2b$10$bov1zY/meP6X8vI2w9K0X.4RseuDuejhjXXCNReHN4JoB3cq4TY72", new DateTime(2025, 8, 30, 21, 50, 48, 593, DateTimeKind.Utc).AddTicks(3190), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "RoleTypes");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "RoleTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "RoleTypes");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Clients");

            migrationBuilder.UpdateData(
                table: "RoleTypes",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "CreatedAtUtc", "UpdatedAtUtc" },
                values: new object[] { new DateTime(2025, 8, 30, 21, 15, 0, 689, DateTimeKind.Utc).AddTicks(6302), new DateTime(2025, 8, 30, 21, 15, 0, 689, DateTimeKind.Utc).AddTicks(6303) });

            migrationBuilder.UpdateData(
                table: "RoleTypes",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                columns: new[] { "CreatedAtUtc", "UpdatedAtUtc" },
                values: new object[] { new DateTime(2025, 8, 30, 21, 15, 0, 689, DateTimeKind.Utc).AddTicks(6306), new DateTime(2025, 8, 30, 21, 15, 0, 689, DateTimeKind.Utc).AddTicks(6307) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAtUtc", "UpdatedAtUtc" },
                values: new object[] { new DateTime(2025, 8, 30, 21, 15, 0, 689, DateTimeKind.Utc).AddTicks(2984), new DateTime(2025, 8, 30, 21, 15, 0, 689, DateTimeKind.Utc).AddTicks(2988) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAtUtc", "UpdatedAtUtc" },
                values: new object[] { new DateTime(2025, 8, 30, 21, 15, 0, 689, DateTimeKind.Utc).AddTicks(2991), new DateTime(2025, 8, 30, 21, 15, 0, 689, DateTimeKind.Utc).AddTicks(2992) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAtUtc", "PasswordHash", "UpdatedAtUtc" },
                values: new object[] { new DateTime(2025, 8, 30, 21, 15, 0, 801, DateTimeKind.Utc).AddTicks(2852), "$2b$10$gYuSBI73eIwYhCk4Hnh8Fe/lff0CSy3Aljrlt0w187FTIA9EmgbyG", new DateTime(2025, 8, 30, 21, 15, 0, 801, DateTimeKind.Utc).AddTicks(2859) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAtUtc", "PasswordHash", "UpdatedAtUtc" },
                values: new object[] { new DateTime(2025, 8, 30, 21, 15, 0, 895, DateTimeKind.Utc).AddTicks(5614), "$2b$10$6xVtuSa5ajeOo2fDmnSwFON5Af3/i/5g2WwM3gQSMnxSDyx4QVDgS", new DateTime(2025, 8, 30, 21, 15, 0, 895, DateTimeKind.Utc).AddTicks(5628) });
        }
    }
}
