using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceApp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ExtendClientsAndPolicies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PremiumAmount",
                table: "Policies",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<decimal>(
                name: "BrokerCommission",
                table: "Policies",
                type: "numeric(5,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CoverageAmount",
                table: "Policies",
                type: "numeric(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentFrequency",
                table: "Policies",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "Policies",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RenewalDate",
                table: "Policies",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Clients",
                type: "character varying(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Clients",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Clients",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Clients",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Clients",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VatNumber",
                table: "Clients",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "RoleTypes",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "CreatedAtUtc", "UpdatedAtUtc" },
                values: new object[] { new DateTime(2025, 9, 13, 11, 9, 14, 898, DateTimeKind.Utc).AddTicks(139), new DateTime(2025, 9, 13, 11, 9, 14, 898, DateTimeKind.Utc).AddTicks(140) });

            migrationBuilder.UpdateData(
                table: "RoleTypes",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                columns: new[] { "CreatedAtUtc", "UpdatedAtUtc" },
                values: new object[] { new DateTime(2025, 9, 13, 11, 9, 14, 898, DateTimeKind.Utc).AddTicks(142), new DateTime(2025, 9, 13, 11, 9, 14, 898, DateTimeKind.Utc).AddTicks(142) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAtUtc", "UpdatedAtUtc" },
                values: new object[] { new DateTime(2025, 9, 13, 11, 9, 14, 897, DateTimeKind.Utc).AddTicks(7708), new DateTime(2025, 9, 13, 11, 9, 14, 897, DateTimeKind.Utc).AddTicks(7711) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAtUtc", "UpdatedAtUtc" },
                values: new object[] { new DateTime(2025, 9, 13, 11, 9, 14, 897, DateTimeKind.Utc).AddTicks(7715), new DateTime(2025, 9, 13, 11, 9, 14, 897, DateTimeKind.Utc).AddTicks(7715) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAtUtc", "PasswordHash", "UpdatedAtUtc" },
                values: new object[] { new DateTime(2025, 9, 13, 11, 9, 14, 958, DateTimeKind.Utc).AddTicks(9753), "$2b$10$W1xv8qnN5SocWN1HvvuiwucyBcyVI2AFfjA7aEwkSGDoIiPHEg7J.", new DateTime(2025, 9, 13, 11, 9, 14, 958, DateTimeKind.Utc).AddTicks(9755) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAtUtc", "PasswordHash", "UpdatedAtUtc" },
                values: new object[] { new DateTime(2025, 9, 13, 11, 9, 15, 21, DateTimeKind.Utc).AddTicks(5490), "$2b$10$GdMRWW0GIOmJq2T9HjxXv.DQEjGhTJvDXbfsEtPbi4gtDQV8IMBse", new DateTime(2025, 9, 13, 11, 9, 15, 21, DateTimeKind.Utc).AddTicks(5493) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrokerCommission",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "CoverageAmount",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "PaymentFrequency",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "RenewalDate",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "VatNumber",
                table: "Clients");

            migrationBuilder.AlterColumn<decimal>(
                name: "PremiumAmount",
                table: "Policies",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.UpdateData(
                table: "RoleTypes",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "CreatedAtUtc", "UpdatedAtUtc" },
                values: new object[] { new DateTime(2025, 8, 30, 21, 50, 48, 384, DateTimeKind.Utc).AddTicks(6584), new DateTime(2025, 8, 30, 21, 50, 48, 384, DateTimeKind.Utc).AddTicks(6585) });

            migrationBuilder.UpdateData(
                table: "RoleTypes",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                columns: new[] { "CreatedAtUtc", "UpdatedAtUtc" },
                values: new object[] { new DateTime(2025, 8, 30, 21, 50, 48, 384, DateTimeKind.Utc).AddTicks(6587), new DateTime(2025, 8, 30, 21, 50, 48, 384, DateTimeKind.Utc).AddTicks(6588) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAtUtc", "UpdatedAtUtc" },
                values: new object[] { new DateTime(2025, 8, 30, 21, 50, 48, 384, DateTimeKind.Utc).AddTicks(4546), new DateTime(2025, 8, 30, 21, 50, 48, 384, DateTimeKind.Utc).AddTicks(4548) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAtUtc", "UpdatedAtUtc" },
                values: new object[] { new DateTime(2025, 8, 30, 21, 50, 48, 384, DateTimeKind.Utc).AddTicks(4551), new DateTime(2025, 8, 30, 21, 50, 48, 384, DateTimeKind.Utc).AddTicks(4551) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAtUtc", "PasswordHash", "UpdatedAtUtc" },
                values: new object[] { new DateTime(2025, 8, 30, 21, 50, 48, 528, DateTimeKind.Utc).AddTicks(4857), "$2b$10$hN8zBjZ7FMacXOsiwO.gJu2WR4zux2AsbHycZZyNzmglLf0p1S6YK", new DateTime(2025, 8, 30, 21, 50, 48, 528, DateTimeKind.Utc).AddTicks(4863) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAtUtc", "PasswordHash", "UpdatedAtUtc" },
                values: new object[] { new DateTime(2025, 8, 30, 21, 50, 48, 593, DateTimeKind.Utc).AddTicks(3186), "$2b$10$bov1zY/meP6X8vI2w9K0X.4RseuDuejhjXXCNReHN4JoB3cq4TY72", new DateTime(2025, 8, 30, 21, 50, 48, 593, DateTimeKind.Utc).AddTicks(3190) });
        }
    }
}
