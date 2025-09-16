using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SkillExchange.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ContentItems",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "FileUrl", "Status", "Title", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 9, 16, 9, 12, 35, 752, DateTimeKind.Utc).AddTicks(8853), "C:\\Users\\ASUS\\Documents\\Internship\\SKILL EXCHANGE PORTAL\\SkillExchangePortal\\SkillExchange.DAL\\Files\\lecture1424354156.pdf", 1, "Learn C# Basics", new DateTime(2025, 9, 16, 9, 12, 35, 752, DateTimeKind.Utc).AddTicks(8853), 2 },
                    { 2, 2, new DateTime(2025, 9, 16, 9, 12, 35, 752, DateTimeKind.Utc).AddTicks(8856), "C:\\Users\\ASUS\\Documents\\Internship\\SKILL EXCHANGE PORTAL\\SkillExchangePortal\\SkillExchange.DAL\\Files\\09-UX.pdf", 0, "UI/UX Design Principles", new DateTime(2025, 9, 16, 9, 12, 35, 752, DateTimeKind.Utc).AddTicks(8857), 3 }
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 16, 9, 12, 35, 752, DateTimeKind.Utc).AddTicks(8649));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 16, 9, 12, 35, 752, DateTimeKind.Utc).AddTicks(8651));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 16, 9, 12, 35, 752, DateTimeKind.Utc).AddTicks(8779));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 16, 9, 12, 35, 752, DateTimeKind.Utc).AddTicks(8781));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 16, 9, 12, 35, 752, DateTimeKind.Utc).AddTicks(8783));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ContentItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ContentItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 16, 8, 4, 11, 738, DateTimeKind.Utc).AddTicks(2157));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 16, 8, 4, 11, 738, DateTimeKind.Utc).AddTicks(2160));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 16, 8, 4, 11, 738, DateTimeKind.Utc).AddTicks(2267));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 16, 8, 4, 11, 738, DateTimeKind.Utc).AddTicks(2269));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 16, 8, 4, 11, 738, DateTimeKind.Utc).AddTicks(2271));
        }
    }
}
