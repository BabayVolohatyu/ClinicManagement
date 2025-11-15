using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicManagement.Migrations
{
    /// <inheritdoc />
    public partial class Addauthtablesv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Users",
                type: "timestamptz",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "RequestedAt",
                table: "PromotionRequests",
                type: "timestamptz",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ProcessedAt",
                table: "PromotionRequests",
                type: "timestamptz",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamptz");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RequestedAt",
                table: "PromotionRequests",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamptz");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ProcessedAt",
                table: "PromotionRequests",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamptz",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "IsActive", "LastName", "MiddleName", "PasswordHash", "RoleId" },
                values: new object[] { 1, new DateTime(2025, 11, 9, 12, 56, 2, 944, DateTimeKind.Utc).AddTicks(7206), "admin@clinic.com", "Admin", true, null, null, "hashed_password_here", 4 });
        }
    }
}
