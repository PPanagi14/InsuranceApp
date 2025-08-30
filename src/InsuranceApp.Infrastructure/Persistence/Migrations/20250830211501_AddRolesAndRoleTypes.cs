using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InsuranceApp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesAndRoleTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "Role",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "RoleTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_RoleTypes_RoleTypeId",
                        column: x => x.RoleTypeId,
                        principalTable: "RoleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    RolesId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.RolesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "RoleTypes",
                columns: new[] { "Id", "Code", "CreatedAtUtc", "DeletedAtUtc", "Description", "UpdatedAtUtc" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "ADMIN", new DateTime(2025, 8, 30, 21, 15, 0, 689, DateTimeKind.Utc).AddTicks(6302), null, "System administrator", new DateTime(2025, 8, 30, 21, 15, 0, 689, DateTimeKind.Utc).AddTicks(6303) },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "BROKER", new DateTime(2025, 8, 30, 21, 15, 0, 689, DateTimeKind.Utc).AddTicks(6306), null, "Insurance broker", new DateTime(2025, 8, 30, 21, 15, 0, 689, DateTimeKind.Utc).AddTicks(6307) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAtUtc", "DeletedAtUtc", "PasswordHash", "RefreshToken", "RefreshTokenExpiryTime", "UpdatedAtUtc", "Username" },
                values: new object[,]
                {
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 8, 30, 21, 15, 0, 801, DateTimeKind.Utc).AddTicks(2852), null, "$2b$10$gYuSBI73eIwYhCk4Hnh8Fe/lff0CSy3Aljrlt0w187FTIA9EmgbyG", null, null, new DateTime(2025, 8, 30, 21, 15, 0, 801, DateTimeKind.Utc).AddTicks(2859), "admin" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 8, 30, 21, 15, 0, 895, DateTimeKind.Utc).AddTicks(5614), null, "$2b$10$6xVtuSa5ajeOo2fDmnSwFON5Af3/i/5g2WwM3gQSMnxSDyx4QVDgS", null, null, new DateTime(2025, 8, 30, 21, 15, 0, 895, DateTimeKind.Utc).AddTicks(5628), "broker" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAtUtc", "DeletedAtUtc", "RoleTypeId", "UpdatedAtUtc" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 30, 21, 15, 0, 689, DateTimeKind.Utc).AddTicks(2984), null, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2025, 8, 30, 21, 15, 0, 689, DateTimeKind.Utc).AddTicks(2988) },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 8, 30, 21, 15, 0, 689, DateTimeKind.Utc).AddTicks(2991), null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2025, 8, 30, 21, 15, 0, 689, DateTimeKind.Utc).AddTicks(2992) }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RolesId", "UsersId" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("44444444-4444-4444-4444-444444444444") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Roles_RoleTypeId",
                table: "Roles",
                column: "RoleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleTypes_Code",
                table: "RoleTypes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UsersId",
                table: "UserRoles",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "RoleTypes");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAtUtc", "DeletedAtUtc", "PasswordHash", "RefreshToken", "RefreshTokenExpiryTime", "Role", "UpdatedAtUtc", "Username" },
                values: new object[,]
                {
                    { new Guid("3274cdcb-1997-40a6-b5f8-8bf59589c23a"), new DateTime(2025, 8, 30, 20, 9, 41, 459, DateTimeKind.Utc).AddTicks(9211), null, "$2b$10$Tq3MEEURceJi3ZSh04m9TezDjWVg188BQjA1GO34ikEa3Q5nuo41S", null, null, "Admin", new DateTime(2025, 8, 30, 20, 9, 41, 459, DateTimeKind.Utc).AddTicks(9216), "admin" },
                    { new Guid("8b415691-ea52-4b0b-a41e-eb50a1865f39"), new DateTime(2025, 8, 30, 20, 9, 41, 531, DateTimeKind.Utc).AddTicks(1738), null, "$2b$10$n.zseRI8UZEKbo6w0W9zjO7XOFWYA.svPlR9SlyPRQ1MQglcoCJpm", null, null, "Broker", new DateTime(2025, 8, 30, 20, 9, 41, 531, DateTimeKind.Utc).AddTicks(1741), "broker" }
                });
        }
    }
}
