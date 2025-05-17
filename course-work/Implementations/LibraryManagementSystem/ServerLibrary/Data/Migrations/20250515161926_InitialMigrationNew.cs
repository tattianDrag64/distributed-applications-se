using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrationNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalReadBooks",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 15, 16, 19, 25, 765, DateTimeKind.Utc).AddTicks(6507), new DateTime(2025, 5, 15, 16, 19, 25, 765, DateTimeKind.Utc).AddTicks(6508) });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 15, 16, 19, 25, 765, DateTimeKind.Utc).AddTicks(6513), new DateTime(2025, 5, 15, 16, 19, 25, 765, DateTimeKind.Utc).AddTicks(6513) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 15, 16, 19, 25, 765, DateTimeKind.Utc).AddTicks(6537), new DateTime(2025, 5, 15, 16, 19, 25, 765, DateTimeKind.Utc).AddTicks(6537) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 15, 16, 19, 25, 765, DateTimeKind.Utc).AddTicks(6543), new DateTime(2025, 5, 15, 16, 19, 25, 765, DateTimeKind.Utc).AddTicks(6543) });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 15, 16, 19, 25, 765, DateTimeKind.Utc).AddTicks(6236), new DateTime(2025, 5, 15, 16, 19, 25, 765, DateTimeKind.Utc).AddTicks(6239) });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 15, 16, 19, 25, 765, DateTimeKind.Utc).AddTicks(6241), new DateTime(2025, 5, 15, 16, 19, 25, 765, DateTimeKind.Utc).AddTicks(6242) });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 15, 16, 19, 25, 765, DateTimeKind.Utc).AddTicks(6243), new DateTime(2025, 5, 15, 16, 19, 25, 765, DateTimeKind.Utc).AddTicks(6243) });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "DueDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 15, 16, 19, 25, 765, DateTimeKind.Utc).AddTicks(6618), new DateTime(2025, 5, 29, 16, 19, 25, 765, DateTimeKind.Utc).AddTicks(6612), new DateTime(2025, 5, 15, 16, 19, 25, 765, DateTimeKind.Utc).AddTicks(6618) });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "DueDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 15, 16, 19, 25, 765, DateTimeKind.Utc).AddTicks(6622), new DateTime(2025, 5, 29, 16, 19, 25, 765, DateTimeKind.Utc).AddTicks(6621), new DateTime(2025, 5, 15, 16, 19, 25, 765, DateTimeKind.Utc).AddTicks(6622) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "TotalReadBooks", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 15, 16, 19, 25, 765, DateTimeKind.Utc).AddTicks(6577), 0, new DateTime(2025, 5, 15, 16, 19, 25, 765, DateTimeKind.Utc).AddTicks(6574) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "TotalReadBooks", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 15, 16, 19, 25, 765, DateTimeKind.Utc).AddTicks(6581), 0, new DateTime(2025, 5, 15, 16, 19, 25, 765, DateTimeKind.Utc).AddTicks(6579) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalReadBooks",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 14, 10, 25, 39, 668, DateTimeKind.Utc).AddTicks(7110), new DateTime(2025, 5, 14, 10, 25, 39, 668, DateTimeKind.Utc).AddTicks(7110) });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 14, 10, 25, 39, 668, DateTimeKind.Utc).AddTicks(7114), new DateTime(2025, 5, 14, 10, 25, 39, 668, DateTimeKind.Utc).AddTicks(7114) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 14, 10, 25, 39, 668, DateTimeKind.Utc).AddTicks(7134), new DateTime(2025, 5, 14, 10, 25, 39, 668, DateTimeKind.Utc).AddTicks(7135) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 14, 10, 25, 39, 668, DateTimeKind.Utc).AddTicks(7139), new DateTime(2025, 5, 14, 10, 25, 39, 668, DateTimeKind.Utc).AddTicks(7139) });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 14, 10, 25, 39, 668, DateTimeKind.Utc).AddTicks(6930), new DateTime(2025, 5, 14, 10, 25, 39, 668, DateTimeKind.Utc).AddTicks(6933) });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 14, 10, 25, 39, 668, DateTimeKind.Utc).AddTicks(6937), new DateTime(2025, 5, 14, 10, 25, 39, 668, DateTimeKind.Utc).AddTicks(6937) });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 14, 10, 25, 39, 668, DateTimeKind.Utc).AddTicks(6938), new DateTime(2025, 5, 14, 10, 25, 39, 668, DateTimeKind.Utc).AddTicks(6939) });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "DueDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 14, 10, 25, 39, 668, DateTimeKind.Utc).AddTicks(7202), new DateTime(2025, 5, 28, 10, 25, 39, 668, DateTimeKind.Utc).AddTicks(7195), new DateTime(2025, 5, 14, 10, 25, 39, 668, DateTimeKind.Utc).AddTicks(7202) });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "DueDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 14, 10, 25, 39, 668, DateTimeKind.Utc).AddTicks(7209), new DateTime(2025, 5, 28, 10, 25, 39, 668, DateTimeKind.Utc).AddTicks(7208), new DateTime(2025, 5, 14, 10, 25, 39, 668, DateTimeKind.Utc).AddTicks(7209) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 14, 10, 25, 39, 668, DateTimeKind.Utc).AddTicks(7166), new DateTime(2025, 5, 14, 10, 25, 39, 668, DateTimeKind.Utc).AddTicks(7164) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 14, 10, 25, 39, 668, DateTimeKind.Utc).AddTicks(7169), new DateTime(2025, 5, 14, 10, 25, 39, 668, DateTimeKind.Utc).AddTicks(7168) });
        }
    }
}
