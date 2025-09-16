using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SkillExchange.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialFeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ContentItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 16, 9, 18, 10, 480, DateTimeKind.Utc).AddTicks(6284), new DateTime(2025, 9, 16, 9, 18, 10, 480, DateTimeKind.Utc).AddTicks(6285) });

            migrationBuilder.UpdateData(
                table: "ContentItems",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 16, 9, 18, 10, 480, DateTimeKind.Utc).AddTicks(6287), new DateTime(2025, 9, 16, 9, 18, 10, 480, DateTimeKind.Utc).AddTicks(6288) });

            migrationBuilder.InsertData(
                table: "Feedbacks",
                columns: new[] { "Id", "Comment", "ContentId", "CreatedAt", "Rating", "UserId" },
                values: new object[,]
                {
                    { 1, "Good content!", 1, new DateTime(2025, 9, 16, 9, 18, 10, 480, DateTimeKind.Utc).AddTicks(6311), 4, 3 },
                    { 2, "Not a better content!", 2, new DateTime(2025, 9, 16, 9, 18, 10, 480, DateTimeKind.Utc).AddTicks(6313), 3, 2 }
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 16, 9, 18, 10, 480, DateTimeKind.Utc).AddTicks(6067));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 16, 9, 18, 10, 480, DateTimeKind.Utc).AddTicks(6069));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 16, 9, 18, 10, 480, DateTimeKind.Utc).AddTicks(6206));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 16, 9, 18, 10, 480, DateTimeKind.Utc).AddTicks(6209));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 16, 9, 18, 10, 480, DateTimeKind.Utc).AddTicks(6210));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "ContentItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 16, 9, 12, 35, 752, DateTimeKind.Utc).AddTicks(8853), new DateTime(2025, 9, 16, 9, 12, 35, 752, DateTimeKind.Utc).AddTicks(8853) });

            migrationBuilder.UpdateData(
                table: "ContentItems",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 16, 9, 12, 35, 752, DateTimeKind.Utc).AddTicks(8856), new DateTime(2025, 9, 16, 9, 12, 35, 752, DateTimeKind.Utc).AddTicks(8857) });

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
    }
}
