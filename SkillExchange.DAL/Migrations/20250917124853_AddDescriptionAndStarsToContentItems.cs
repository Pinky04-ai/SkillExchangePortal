using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillExchange.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionAndStarsToContentItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ContentItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Stars",
                table: "ContentItems",
                type: "float",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ContentItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Description", "Stars", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 17, 12, 48, 53, 273, DateTimeKind.Utc).AddTicks(7867), null, null, new DateTime(2025, 9, 17, 12, 48, 53, 273, DateTimeKind.Utc).AddTicks(7868) });

            migrationBuilder.UpdateData(
                table: "ContentItems",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Description", "Stars", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 17, 12, 48, 53, 273, DateTimeKind.Utc).AddTicks(7872), null, null, new DateTime(2025, 9, 17, 12, 48, 53, 273, DateTimeKind.Utc).AddTicks(7872) });

            migrationBuilder.UpdateData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 17, 12, 48, 53, 273, DateTimeKind.Utc).AddTicks(7898));

            migrationBuilder.UpdateData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 17, 12, 48, 53, 273, DateTimeKind.Utc).AddTicks(7901));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1,
                column: "SentAt",
                value: new DateTime(2025, 9, 17, 12, 48, 53, 273, DateTimeKind.Utc).AddTicks(7923));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 2,
                column: "SentAt",
                value: new DateTime(2025, 9, 17, 12, 48, 53, 273, DateTimeKind.Utc).AddTicks(7925));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 17, 12, 48, 53, 273, DateTimeKind.Utc).AddTicks(7594));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 17, 12, 48, 53, 273, DateTimeKind.Utc).AddTicks(7597));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 17, 12, 48, 53, 273, DateTimeKind.Utc).AddTicks(7734));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 17, 12, 48, 53, 273, DateTimeKind.Utc).AddTicks(7737));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 17, 12, 48, 53, 273, DateTimeKind.Utc).AddTicks(7739));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ContentItems");

            migrationBuilder.DropColumn(
                name: "Stars",
                table: "ContentItems");

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

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1,
                column: "SentAt",
                value: new DateTime(2025, 9, 16, 9, 20, 38, 470, DateTimeKind.Utc).AddTicks(6419));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 2,
                column: "SentAt",
                value: new DateTime(2025, 9, 16, 9, 20, 38, 470, DateTimeKind.Utc).AddTicks(6422));

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
    }
}
