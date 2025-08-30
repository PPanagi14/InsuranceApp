using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InsuranceApp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAtUtc", "DeletedAtUtc", "PasswordHash", "Role", "UpdatedAtUtc", "Username" },
                values: new object[,]
                {
                    { new Guid("b01c3e35-1626-40ff-abe3-9355a80067ed"), new DateTime(2025, 8, 30, 12, 43, 17, 51, DateTimeKind.Utc).AddTicks(4253), null, "$2b$10$emp0qTVybeQVkjbjsrDtJOKmO6nURGcSmgeb6tOwqCZJDaDXmQZim", "Admin", new DateTime(2025, 8, 30, 12, 43, 17, 51, DateTimeKind.Utc).AddTicks(4256), "admin" },
                    { new Guid("f53cd837-3c91-4e50-9b30-d07965bd930c"), new DateTime(2025, 8, 30, 12, 43, 17, 127, DateTimeKind.Utc).AddTicks(2706), null, "$2b$10$PxqpHuEzJZPDJgpNa5uI3.ASAcKomBJO6V1bsmZZ9PDJL9zZqtOCG", "Broker", new DateTime(2025, 8, 30, 12, 43, 17, 127, DateTimeKind.Utc).AddTicks(2713), "broker" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
