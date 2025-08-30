using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InsuranceApp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokenToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b01c3e35-1626-40ff-abe3-9355a80067ed"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f53cd837-3c91-4e50-9b30-d07965bd930c"));

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAtUtc", "DeletedAtUtc", "PasswordHash", "RefreshToken", "RefreshTokenExpiryTime", "Role", "UpdatedAtUtc", "Username" },
                values: new object[,]
                {
                    { new Guid("3274cdcb-1997-40a6-b5f8-8bf59589c23a"), new DateTime(2025, 8, 30, 20, 9, 41, 459, DateTimeKind.Utc).AddTicks(9211), null, "$2b$10$Tq3MEEURceJi3ZSh04m9TezDjWVg188BQjA1GO34ikEa3Q5nuo41S", null, null, "Admin", new DateTime(2025, 8, 30, 20, 9, 41, 459, DateTimeKind.Utc).AddTicks(9216), "admin" },
                    { new Guid("8b415691-ea52-4b0b-a41e-eb50a1865f39"), new DateTime(2025, 8, 30, 20, 9, 41, 531, DateTimeKind.Utc).AddTicks(1738), null, "$2b$10$n.zseRI8UZEKbo6w0W9zjO7XOFWYA.svPlR9SlyPRQ1MQglcoCJpm", null, null, "Broker", new DateTime(2025, 8, 30, 20, 9, 41, 531, DateTimeKind.Utc).AddTicks(1741), "broker" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3274cdcb-1997-40a6-b5f8-8bf59589c23a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8b415691-ea52-4b0b-a41e-eb50a1865f39"));

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAtUtc", "DeletedAtUtc", "PasswordHash", "Role", "UpdatedAtUtc", "Username" },
                values: new object[,]
                {
                    { new Guid("b01c3e35-1626-40ff-abe3-9355a80067ed"), new DateTime(2025, 8, 30, 12, 43, 17, 51, DateTimeKind.Utc).AddTicks(4253), null, "$2b$10$emp0qTVybeQVkjbjsrDtJOKmO6nURGcSmgeb6tOwqCZJDaDXmQZim", "Admin", new DateTime(2025, 8, 30, 12, 43, 17, 51, DateTimeKind.Utc).AddTicks(4256), "admin" },
                    { new Guid("f53cd837-3c91-4e50-9b30-d07965bd930c"), new DateTime(2025, 8, 30, 12, 43, 17, 127, DateTimeKind.Utc).AddTicks(2706), null, "$2b$10$PxqpHuEzJZPDJgpNa5uI3.ASAcKomBJO6V1bsmZZ9PDJL9zZqtOCG", "Broker", new DateTime(2025, 8, 30, 12, 43, 17, 127, DateTimeKind.Utc).AddTicks(2713), "broker" }
                });
        }
    }
}
