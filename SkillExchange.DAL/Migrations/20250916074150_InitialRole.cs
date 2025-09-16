using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SkillExchange.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "RoleName", "RoleType" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 9, 16, 7, 41, 50, 556, DateTimeKind.Utc).AddTicks(1487), "Admin", 1 },
                    { 2, new DateTime(2025, 9, 16, 7, 41, 50, 556, DateTimeKind.Utc).AddTicks(1489), "User", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
