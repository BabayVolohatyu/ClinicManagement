using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ClinicManagement.Migrations
{
    /// <inheritdoc />
    public partial class Addauthtables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CanCreate = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CanRead = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CanUpdate = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CanDelete = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CanExecuteRawQueries = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CanAskPromotion = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CanAcceptPromotions = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CanViewPromotionsList = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CanManageUsers = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CanViewUserData = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CanDownloadCsv = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RequestedRoleId = table.Column<int>(type: "integer", nullable: false),
                    Reason = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    RequestedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ProcessedByAdminId = table.Column<int>(type: "integer", nullable: true),
                    ProcessedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserId1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromotionRequests_Roles_RequestedRoleId",
                        column: x => x.RequestedRoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromotionRequests_Users_ProcessedByAdminId",
                        column: x => x.ProcessedByAdminId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PromotionRequests_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromotionRequests_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CanAskPromotion", "CanRead", "Name" },
                values: new object[] { 1, true, true, "Guest" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CanAskPromotion", "CanDownloadCsv", "CanRead", "Name" },
                values: new object[] { 2, true, true, true, "Authorized" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CanCreate", "CanDelete", "CanDownloadCsv", "CanExecuteRawQueries", "CanRead", "CanUpdate", "Name" },
                values: new object[] { 3, true, true, true, true, true, true, "Operator" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CanAcceptPromotions", "CanCreate", "CanDelete", "CanDownloadCsv", "CanExecuteRawQueries", "CanManageUsers", "CanRead", "CanUpdate", "CanViewPromotionsList", "CanViewUserData", "Name" },
                values: new object[] { 4, true, true, true, true, true, true, true, true, true, true, "Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "IsActive", "LastName", "MiddleName", "PasswordHash", "RoleId" },
                values: new object[] { 1, new DateTime(2025, 11, 9, 12, 56, 2, 944, DateTimeKind.Utc).AddTicks(7206), "admin@clinic.com", "Admin", true, null, null, "hashed_password_here", 4 });

            migrationBuilder.CreateIndex(
                name: "IX_PromotionRequests_ProcessedByAdminId",
                table: "PromotionRequests",
                column: "ProcessedByAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionRequests_RequestedAt",
                table: "PromotionRequests",
                column: "RequestedAt");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionRequests_RequestedRoleId",
                table: "PromotionRequests",
                column: "RequestedRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionRequests_Status",
                table: "PromotionRequests",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionRequests_UserId",
                table: "PromotionRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionRequests_UserId1",
                table: "PromotionRequests",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PromotionRequests");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
