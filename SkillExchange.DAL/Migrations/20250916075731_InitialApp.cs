using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SkillExchange.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialApp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 16, 7, 57, 30, 767, DateTimeKind.Utc).AddTicks(6556));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 16, 7, 57, 30, 767, DateTimeKind.Utc).AddTicks(6558));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FullName", "Password", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 9, 16, 7, 57, 30, 767, DateTimeKind.Utc).AddTicks(6711), "admin@skillportal.com", "Admin User", "hashed_admin_pw", 1 },
                    { 2, new DateTime(2025, 9, 16, 7, 57, 30, 767, DateTimeKind.Utc).AddTicks(6714), "john@skillportal.com", "John Doe", "hashed_john_pw", 1 },
                    { 3, new DateTime(2025, 9, 16, 7, 57, 30, 767, DateTimeKind.Utc).AddTicks(6716), "jane@skillportal.com", "Jane Smith", "hashed_jane_pw", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 16, 7, 41, 50, 556, DateTimeKind.Utc).AddTicks(1487));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 16, 7, 41, 50, 556, DateTimeKind.Utc).AddTicks(1489));
        }
    }
}
