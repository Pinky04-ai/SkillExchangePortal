using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillExchange.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialMsg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ContentItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 16, 9, 20, 38, 470, DateTimeKind.Utc).AddTicks(6371), new DateTime(2025, 9, 16, 9, 20, 38, 470, DateTimeKind.Utc).AddTicks(6372) });

            migrationBuilder.UpdateData(
                table: "ContentItems",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 16, 9, 20, 38, 470, DateTimeKind.Utc).AddTicks(6375), new DateTime(2025, 9, 16, 9, 20, 38, 470, DateTimeKind.Utc).AddTicks(6376) });

            migrationBuilder.UpdateData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 16, 9, 20, 38, 470, DateTimeKind.Utc).AddTicks(6395));

            migrationBuilder.UpdateData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 16, 9, 20, 38, 470, DateTimeKind.Utc).AddTicks(6398));

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "AppUserId", "AppUserId1", "Content", "FromUserId", "SentAt", "Status", "ToUserId" },
                values: new object[] { 1, null, null, "Hello Jane! Welcome to the portal.", 2, new DateTime(2025, 9, 16, 9, 20, 38, 470, DateTimeKind.Utc).AddTicks(6419), 0, 3 });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "AppUserId", "AppUserId1", "Content", "FromUserId", "IsRead", "SentAt", "Status", "ToUserId" },
                values: new object[] { 2, null, null, "Hi John, thank you!", 3, true, new DateTime(2025, 9, 16, 9, 20, 38, 470, DateTimeKind.Utc).AddTicks(6422), 2, 2 });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 16, 9, 20, 38, 470, DateTimeKind.Utc).AddTicks(6099));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 16, 9, 20, 38, 470, DateTimeKind.Utc).AddTicks(6101));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 16, 9, 20, 38, 470, DateTimeKind.Utc).AddTicks(6248));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 16, 9, 20, 38, 470, DateTimeKind.Utc).AddTicks(6250));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 16, 9, 20, 38, 470, DateTimeKind.Utc).AddTicks(6252));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 2);

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

            migrationBuilder.UpdateData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 16, 9, 18, 10, 480, DateTimeKind.Utc).AddTicks(6311));

            migrationBuilder.UpdateData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 16, 9, 18, 10, 480, DateTimeKind.Utc).AddTicks(6313));

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
    }
}
