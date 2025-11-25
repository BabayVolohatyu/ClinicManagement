using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 

namespace ClinicManagement.Migrations
{
    
    public partial class InitialMigration : Migration
    {
        
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Country = table.Column<string>(type: "text", nullable: false),
                    State = table.Column<string>(type: "text", nullable: false),
                    Locality = table.Column<string>(type: "text", nullable: false),
                    StreetName = table.Column<string>(type: "text", nullable: false),
                    StreetNumber = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CabinetTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CabinetTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Patronymic = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Procedures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procedures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
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
                name: "Sicknesses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sicknesses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specialties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Symptoms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Symptoms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Treatments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treatments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cabinets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Building = table.Column<int>(type: "integer", nullable: false),
                    Floor = table.Column<int>(type: "integer", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cabinets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cabinets_CabinetTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "CabinetTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PersonId = table.Column<int>(type: "integer", nullable: false),
                    AddressId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Patients_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SicknessProcedures",
                columns: table => new
                {
                    SicknessId = table.Column<int>(type: "integer", nullable: false),
                    ProcedureId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SicknessProcedures", x => new { x.SicknessId, x.ProcedureId });
                    table.ForeignKey(
                        name: "FK_SicknessProcedures_Procedures_ProcedureId",
                        column: x => x.ProcedureId,
                        principalTable: "Procedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SicknessProcedures_Sicknesses_SicknessId",
                        column: x => x.SicknessId,
                        principalTable: "Sicknesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PersonId = table.Column<int>(type: "integer", nullable: false),
                    SpecialtyId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctors_Specialties_SpecialtyId",
                        column: x => x.SpecialtyId,
                        principalTable: "Specialties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SicknessSymptoms",
                columns: table => new
                {
                    SicknessId = table.Column<int>(type: "integer", nullable: false),
                    SymptomId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SicknessSymptoms", x => new { x.SicknessId, x.SymptomId });
                    table.ForeignKey(
                        name: "FK_SicknessSymptoms_Sicknesses_SicknessId",
                        column: x => x.SicknessId,
                        principalTable: "Sicknesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SicknessSymptoms_Symptoms_SymptomId",
                        column: x => x.SymptomId,
                        principalTable: "Symptoms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SicknessTreatments",
                columns: table => new
                {
                    SicknessId = table.Column<int>(type: "integer", nullable: false),
                    TreatmentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SicknessTreatments", x => new { x.SicknessId, x.TreatmentId });
                    table.ForeignKey(
                        name: "FK_SicknessTreatments_Sicknesses_SicknessId",
                        column: x => x.SicknessId,
                        principalTable: "Sicknesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SicknessTreatments_Treatments_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PasswordChangeRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RequestedPasswordHash = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    RequestedAt = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ProcessedByAdminId = table.Column<int>(type: "integer", nullable: true),
                    ProcessedAt = table.Column<DateTimeOffset>(type: "timestamptz", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordChangeRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PasswordChangeRequests_Users_ProcessedByAdminId",
                        column: x => x.ProcessedByAdminId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PasswordChangeRequests_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    RequestedAt = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ProcessedByAdminId = table.Column<int>(type: "integer", nullable: true),
                    ProcessedAt = table.Column<DateTimeOffset>(type: "timestamptz", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "DistrictDoctors",
                columns: table => new
                {
                    DoctorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistrictDoctors", x => x.DoctorId);
                    table.ForeignKey(
                        name: "FK_DistrictDoctors_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorOnCallStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DoctorId = table.Column<int>(type: "integer", nullable: false),
                    AddressId = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
                    EndTime = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorOnCallStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorOnCallStatuses_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorOnCallStatuses_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorProcedures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DoctorId = table.Column<int>(type: "integer", nullable: false),
                    ProcedureId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorProcedures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorProcedures_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorProcedures_Procedures_ProcedureId",
                        column: x => x.ProcedureId,
                        principalTable: "Procedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HomeCallLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DoctorId = table.Column<int>(type: "integer", nullable: false),
                    AddressId = table.Column<int>(type: "integer", nullable: false),
                    DateTime = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeCallLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeCallLogs_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HomeCallLogs_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DoctorId = table.Column<int>(type: "integer", nullable: false),
                    CabinetId = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
                    EndTime = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Cabinets_CabinetId",
                        column: x => x.CabinetId,
                        principalTable: "Cabinets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedules_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DoctorProcedureId = table.Column<int>(type: "integer", nullable: false),
                    CabinetId = table.Column<int>(type: "integer", nullable: false),
                    PatientId = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
                    EndTime = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
                    DidItHappen = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Cabinets_CabinetId",
                        column: x => x.CabinetId,
                        principalTable: "Cabinets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_DoctorProcedures_DoctorProcedureId",
                        column: x => x.DoctorProcedureId,
                        principalTable: "DoctorProcedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Diagnoses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AppointmentId = table.Column<int>(type: "integer", nullable: false),
                    Prescription = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnoses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diagnoses_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CanRead", "Name", "Type" },
                values: new object[] { 1, true, "Guest", 1 });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CanAskPromotion", "CanCreate", "CanDelete", "CanRead", "CanUpdate", "Name", "Type" },
                values: new object[] { 2, true, true, true, true, true, "Authorized", 2 });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CanAskPromotion", "CanCreate", "CanDelete", "CanDownloadCsv", "CanExecuteRawQueries", "CanRead", "CanUpdate", "Name", "Type" },
                values: new object[] { 3, true, true, true, true, true, true, true, "Operator", 3 });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CanAcceptPromotions", "CanCreate", "CanDelete", "CanDownloadCsv", "CanExecuteRawQueries", "CanManageUsers", "CanRead", "CanUpdate", "CanViewPromotionsList", "CanViewUserData", "Name", "Type" },
                values: new object[] { 4, true, true, true, true, true, true, true, true, true, true, "Admin", 4 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "LastName", "MiddleName", "PasswordHash", "RoleId" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "admin@clinic.local", "Admin", "User", null, "mvFbM25qlhmShTffMLLmojdlafz51+dz7M7eZWBlKaA=", 4 },
                    { 2, new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "authorized@clinic.local", "Authorized", "User", null, "mvFbM25qlhmShTffMLLmojdlafz51+dz7M7eZWBlKaA=", 2 },
                    { 3, new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "operator@clinic.local", "Operator", "User", null, "mvFbM25qlhmShTffMLLmojdlafz51+dz7M7eZWBlKaA=", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CabinetId",
                table: "Appointments",
                column: "CabinetId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorProcedureId",
                table: "Appointments",
                column: "DoctorProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Cabinets_TypeId",
                table: "Cabinets",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnoses_AppointmentId",
                table: "Diagnoses",
                column: "AppointmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorOnCallStatuses_AddressId",
                table: "DoctorOnCallStatuses",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorOnCallStatuses_DoctorId",
                table: "DoctorOnCallStatuses",
                column: "DoctorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorProcedures_DoctorId",
                table: "DoctorProcedures",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorProcedures_ProcedureId",
                table: "DoctorProcedures",
                column: "ProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_PersonId",
                table: "Doctors",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_SpecialtyId",
                table: "Doctors",
                column: "SpecialtyId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeCallLogs_AddressId",
                table: "HomeCallLogs",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeCallLogs_DoctorId",
                table: "HomeCallLogs",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_PasswordChangeRequests_ProcessedByAdminId",
                table: "PasswordChangeRequests",
                column: "ProcessedByAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_PasswordChangeRequests_RequestedAt",
                table: "PasswordChangeRequests",
                column: "RequestedAt");

            migrationBuilder.CreateIndex(
                name: "IX_PasswordChangeRequests_Status",
                table: "PasswordChangeRequests",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_PasswordChangeRequests_UserId",
                table: "PasswordChangeRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_AddressId",
                table: "Patients",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PersonId",
                table: "Patients",
                column: "PersonId",
                unique: true);

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
                name: "IX_Schedules_CabinetId",
                table: "Schedules",
                column: "CabinetId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_DoctorId",
                table: "Schedules",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_SicknessProcedures_ProcedureId",
                table: "SicknessProcedures",
                column: "ProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_SicknessSymptoms_SymptomId",
                table: "SicknessSymptoms",
                column: "SymptomId");

            migrationBuilder.CreateIndex(
                name: "IX_SicknessTreatments_TreatmentId",
                table: "SicknessTreatments",
                column: "TreatmentId");

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

        
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Diagnoses");

            migrationBuilder.DropTable(
                name: "DistrictDoctors");

            migrationBuilder.DropTable(
                name: "DoctorOnCallStatuses");

            migrationBuilder.DropTable(
                name: "HomeCallLogs");

            migrationBuilder.DropTable(
                name: "PasswordChangeRequests");

            migrationBuilder.DropTable(
                name: "PromotionRequests");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "SicknessProcedures");

            migrationBuilder.DropTable(
                name: "SicknessSymptoms");

            migrationBuilder.DropTable(
                name: "SicknessTreatments");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Symptoms");

            migrationBuilder.DropTable(
                name: "Sicknesses");

            migrationBuilder.DropTable(
                name: "Treatments");

            migrationBuilder.DropTable(
                name: "Cabinets");

            migrationBuilder.DropTable(
                name: "DoctorProcedures");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "CabinetTypes");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Procedures");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Specialties");
        }
    }
}
