using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ClinicManagement.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "Country", "Locality", "State", "StreetName", "StreetNumber" },
                values: new object[,]
                {
                    { 1, "Russia", "Moscow", "Moscow Oblast", "Lenin Avenue", 15 },
                    { 2, "Russia", "Moscow", "Moscow Oblast", "Pushkin Street", 42 },
                    { 3, "Russia", "Moscow", "Moscow Oblast", "Gorky Street", 28 },
                    { 4, "Russia", "Moscow", "Moscow Oblast", "Tverskaya Street", 10 },
                    { 5, "Russia", "Moscow", "Moscow Oblast", "Arbat Street", 25 },
                    { 6, "Russia", "Moscow", "Moscow Oblast", "Kutuzovsky Avenue", 8 },
                    { 7, "Russia", "Moscow", "Moscow Oblast", "Leningradsky Avenue", 33 },
                    { 8, "Russia", "Moscow", "Moscow Oblast", "Spartakovskaya Street", 17 },
                    { 9, "Russia", "Moscow", "Moscow Oblast", "Novy Arbat", 12 },
                    { 10, "Russia", "Moscow", "Moscow Oblast", "Sadovaya Street", 5 },
                    { 11, "Russia", "Moscow", "Moscow Oblast", "Prechistenka Street", 20 },
                    { 12, "Russia", "Moscow", "Moscow Oblast", "Ostozhenka Street", 14 },
                    { 13, "Russia", "Moscow", "Moscow Oblast", "Znamenka Street", 7 },
                    { 14, "Russia", "Moscow", "Moscow Oblast", "Volkhonka Street", 19 },
                    { 15, "Russia", "Moscow", "Moscow Oblast", "Nikitsky Boulevard", 11 },
                    { 16, "Russia", "Moscow", "Moscow Oblast", "Tverskoy Boulevard", 22 }
                });

            migrationBuilder.InsertData(
                table: "CabinetTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Examination Room" },
                    { 2, "Consultation Room" },
                    { 3, "Procedure Room" },
                    { 4, "Surgery Room" },
                    { 5, "Laboratory" },
                    { 6, "X-Ray Room" },
                    { 7, "Ultrasound Room" },
                    { 8, "Reception" },
                    { 9, "Waiting Room" },
                    { 10, "Administration" }
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "FirstName", "LastName", "Patronymic" },
                values: new object[,]
                {
                    { 1, "Ivan", "Petrov", "Sergeevich" },
                    { 2, "Maria", "Ivanova", "Viktorovna" },
                    { 3, "Alexander", "Sidorov", "Petrovich" },
                    { 4, "Elena", "Kozlova", "Ivanovna" },
                    { 5, "Dmitry", "Volkov", "Alexandrovich" },
                    { 6, "Olga", "Novikova", "Dmitrievna" },
                    { 7, "Sergey", "Morozov", "Vladimirovich" },
                    { 8, "Anna", "Pavlova", "Sergeevna" },
                    { 9, "Nikolay", "Fedorov", "Ivanovich" },
                    { 10, "Tatiana", "Sokolova", "Petrovna" },
                    { 11, "Vladimir", "Lebedev", "Nikolaevich" },
                    { 12, "Svetlana", "Orlova", "Vladimirovna" },
                    { 13, "Andrey", "Popov", "Andreevich" },
                    { 14, "Natalia", "Kuznetsova", "Andreevna" },
                    { 15, "Mikhail", "Semenov", "Mikhailovich" },
                    { 16, "Yulia", "Vasilyeva", "Mikhailovna" },
                    { 17, "Pavel", "Zakharov", "Pavlovich" },
                    { 18, "Ekaterina", "Stepanova", "Pavlovna" },
                    { 19, "Igor", "Makarov", "Igorevich" },
                    { 20, "Larisa", "Fomina", "Igorevna" },
                    { 21, "Roman", "Romanov", "Romanovich" },
                    { 22, "Galina", "Voronova", "Romanovna" },
                    { 23, "Viktor", "Alekseev", "Viktorovich" },
                    { 24, "Raisa", "Grigorieva", "Viktorovna" }
                });

            migrationBuilder.InsertData(
                table: "Procedures",
                columns: new[] { "Id", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Complete physical examination including vital signs, heart, lungs, and abdomen", "General Examination", 1500f },
                    { 2, "Measurement of systolic and diastolic blood pressure", "Blood Pressure Measurement", 300f },
                    { 3, "Recording of electrical activity of the heart", "ECG (Electrocardiogram)", 2500f },
                    { 4, "Radiographic examination of the chest", "X-Ray Chest", 2000f },
                    { 5, "Ultrasound examination of abdominal organs", "Ultrasound Abdomen", 3000f },
                    { 6, "Complete blood count and basic biochemistry", "Blood Test (Complete)", 1800f },
                    { 7, "Laboratory analysis of urine sample", "Urine Analysis", 500f },
                    { 8, "Collection and analysis of throat culture", "Throat Swab", 800f },
                    { 9, "Comprehensive eye examination including vision test", "Eye Examination", 1200f },
                    { 10, "Otoscopic examination of the ear", "Ear Examination", 700f },
                    { 11, "Lung function test to measure breathing capacity", "Spirometry", 1500f },
                    { 12, "Administration of vaccine", "Vaccination", 1000f },
                    { 13, "Cleaning and dressing of wounds", "Wound Dressing", 600f },
                    { 14, "Intramuscular or intravenous injection", "Injection", 400f },
                    { 15, "Minor surgical procedure", "Minor Surgery", 5000f }
                });

            migrationBuilder.InsertData(
                table: "Sicknesses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Common Cold" },
                    { 2, "Influenza" },
                    { 3, "Hypertension" },
                    { 4, "Bronchitis" },
                    { 5, "Pneumonia" },
                    { 6, "Gastritis" },
                    { 7, "Migraine" },
                    { 8, "Arthritis" },
                    { 9, "Dermatitis" },
                    { 10, "Diabetes Type 2" },
                    { 11, "Asthma" },
                    { 12, "Sinusitis" },
                    { 13, "Otitis Media" },
                    { 14, "Conjunctivitis" },
                    { 15, "Urinary Tract Infection" }
                });

            migrationBuilder.InsertData(
                table: "Specialties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "General Practitioner" },
                    { 2, "Cardiologist" },
                    { 3, "Pediatrician" },
                    { 4, "Neurologist" },
                    { 5, "Dermatologist" },
                    { 6, "Orthopedist" },
                    { 7, "Ophthalmologist" },
                    { 8, "ENT Specialist" },
                    { 9, "Gynecologist" },
                    { 10, "Surgeon" }
                });

            migrationBuilder.InsertData(
                table: "Symptoms",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Fever" },
                    { 2, "Headache" },
                    { 3, "Cough" },
                    { 4, "Sore Throat" },
                    { 5, "Runny Nose" },
                    { 6, "Chest Pain" },
                    { 7, "Shortness of Breath" },
                    { 8, "Nausea" },
                    { 9, "Vomiting" },
                    { 10, "Diarrhea" },
                    { 11, "Abdominal Pain" },
                    { 12, "Dizziness" },
                    { 13, "Fatigue" },
                    { 14, "Joint Pain" },
                    { 15, "Muscle Pain" },
                    { 16, "Rash" },
                    { 17, "Itching" },
                    { 18, "Back Pain" },
                    { 19, "Insomnia" },
                    { 20, "Loss of Appetite" }
                });

            migrationBuilder.InsertData(
                table: "Treatments",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Rest and Hydration" },
                    { 2, "Antibiotic Therapy" },
                    { 3, "Antiviral Medication" },
                    { 4, "Pain Relief Medication" },
                    { 5, "Antihistamine" },
                    { 6, "Bronchodilator" },
                    { 7, "Anti-inflammatory Medication" },
                    { 8, "Blood Pressure Medication" },
                    { 9, "Insulin Therapy" },
                    { 10, "Physical Therapy" },
                    { 11, "Diet Modification" },
                    { 12, "Lifestyle Changes" },
                    { 13, "Topical Cream" },
                    { 14, "Eye Drops" },
                    { 15, "Nasal Spray" }
                });

            migrationBuilder.InsertData(
                table: "Cabinets",
                columns: new[] { "Id", "Building", "Floor", "Number", "TypeId" },
                values: new object[,]
                {
                    { 1, 1, 1, 101, 8 },
                    { 2, 1, 1, 102, 1 },
                    { 3, 1, 1, 103, 1 },
                    { 4, 1, 1, 104, 2 },
                    { 5, 1, 1, 105, 2 },
                    { 6, 1, 2, 201, 3 },
                    { 7, 1, 2, 202, 3 },
                    { 8, 1, 2, 203, 4 },
                    { 9, 1, 2, 204, 5 },
                    { 10, 1, 2, 205, 6 },
                    { 11, 1, 3, 301, 7 },
                    { 12, 1, 3, 302, 1 },
                    { 13, 1, 3, 303, 1 },
                    { 14, 1, 3, 304, 9 },
                    { 15, 1, 3, 305, 10 }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "PersonId", "SpecialtyId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 3, 3 },
                    { 4, 4, 4 },
                    { 5, 5, 5 },
                    { 6, 6, 6 },
                    { 7, 7, 7 },
                    { 8, 8, 8 }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "AddressId", "PersonId" },
                values: new object[,]
                {
                    { 1, 4, 9 },
                    { 2, 5, 10 },
                    { 3, 6, 11 },
                    { 4, 7, 12 },
                    { 5, 8, 13 },
                    { 6, 9, 14 },
                    { 7, 10, 15 },
                    { 8, 11, 16 },
                    { 9, 12, 17 },
                    { 10, 13, 18 },
                    { 11, 14, 19 },
                    { 12, 15, 20 },
                    { 13, 16, 21 },
                    { 14, 4, 22 },
                    { 15, 5, 23 },
                    { 16, 6, 24 }
                });

            migrationBuilder.InsertData(
                table: "SicknessProcedures",
                columns: new[] { "ProcedureId", "SicknessId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 8, 1 },
                    { 1, 2 },
                    { 6, 2 },
                    { 1, 3 },
                    { 2, 3 },
                    { 3, 3 },
                    { 6, 3 },
                    { 1, 4 },
                    { 4, 4 },
                    { 11, 4 },
                    { 1, 5 },
                    { 4, 5 },
                    { 6, 5 },
                    { 1, 6 },
                    { 5, 6 },
                    { 6, 6 },
                    { 1, 7 },
                    { 6, 7 },
                    { 1, 8 },
                    { 4, 8 },
                    { 1, 9 },
                    { 1, 12 },
                    { 10, 12 }
                });

            migrationBuilder.InsertData(
                table: "SicknessSymptoms",
                columns: new[] { "SicknessId", "SymptomId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 },
                    { 1, 5 },
                    { 2, 1 },
                    { 2, 2 },
                    { 2, 3 },
                    { 2, 13 },
                    { 2, 15 },
                    { 3, 2 },
                    { 3, 12 },
                    { 4, 1 },
                    { 4, 3 },
                    { 4, 7 },
                    { 5, 1 },
                    { 5, 3 },
                    { 5, 6 },
                    { 5, 7 },
                    { 6, 8 },
                    { 6, 11 },
                    { 6, 20 },
                    { 7, 2 },
                    { 7, 8 },
                    { 7, 12 },
                    { 8, 14 },
                    { 8, 15 },
                    { 9, 16 },
                    { 9, 17 },
                    { 12, 2 },
                    { 12, 4 },
                    { 12, 5 }
                });

            migrationBuilder.InsertData(
                table: "SicknessTreatments",
                columns: new[] { "SicknessId", "TreatmentId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 4 },
                    { 1, 5 },
                    { 2, 1 },
                    { 2, 3 },
                    { 2, 4 },
                    { 3, 8 },
                    { 3, 11 },
                    { 3, 12 },
                    { 4, 2 },
                    { 4, 4 },
                    { 4, 6 },
                    { 5, 1 },
                    { 5, 2 },
                    { 5, 4 },
                    { 6, 4 },
                    { 6, 7 },
                    { 6, 11 },
                    { 7, 1 },
                    { 7, 4 },
                    { 8, 7 },
                    { 8, 10 },
                    { 9, 5 },
                    { 9, 13 },
                    { 12, 2 },
                    { 12, 4 },
                    { 12, 15 }
                });

            migrationBuilder.InsertData(
                table: "DistrictDoctors",
                column: "DoctorId",
                values: new object[]
                {
                    1,
                    2,
                    3,
                    4
                });

            migrationBuilder.InsertData(
                table: "DoctorOnCallStatuses",
                columns: new[] { "Id", "AddressId", "DoctorId", "EndTime", "StartTime" },
                values: new object[,]
                {
                    { 1, 4, 1, new DateTimeOffset(new DateTime(2025, 11, 19, 20, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 11, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)) },
                    { 2, 5, 2, new DateTimeOffset(new DateTime(2025, 11, 20, 20, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 11, 20, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)) },
                    { 3, 6, 3, new DateTimeOffset(new DateTime(2025, 11, 21, 20, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 11, 21, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)) },
                    { 4, 7, 4, new DateTimeOffset(new DateTime(2025, 11, 22, 20, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 11, 22, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "DoctorProcedures",
                columns: new[] { "Id", "DoctorId", "ProcedureId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 1, 6 },
                    { 4, 1, 7 },
                    { 5, 1, 12 },
                    { 6, 1, 14 },
                    { 7, 2, 1 },
                    { 8, 2, 2 },
                    { 9, 2, 3 },
                    { 10, 2, 6 },
                    { 11, 3, 1 },
                    { 12, 3, 6 },
                    { 13, 3, 12 },
                    { 14, 3, 14 },
                    { 15, 4, 1 },
                    { 16, 4, 6 },
                    { 17, 5, 1 },
                    { 18, 6, 1 },
                    { 19, 6, 4 },
                    { 20, 7, 9 },
                    { 21, 8, 10 },
                    { 22, 8, 8 }
                });

            migrationBuilder.InsertData(
                table: "HomeCallLogs",
                columns: new[] { "Id", "AddressId", "DateTime", "DoctorId" },
                values: new object[,]
                {
                    { 1, 8, new DateTimeOffset(new DateTime(2025, 11, 18, 14, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 1 },
                    { 2, 9, new DateTimeOffset(new DateTime(2025, 11, 19, 16, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 1 },
                    { 3, 10, new DateTimeOffset(new DateTime(2025, 11, 20, 10, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 2 },
                    { 4, 11, new DateTimeOffset(new DateTime(2025, 11, 20, 18, 45, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 3 },
                    { 5, 12, new DateTimeOffset(new DateTime(2025, 11, 21, 9, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 4 },
                    { 6, 13, new DateTimeOffset(new DateTime(2025, 11, 21, 11, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 1 },
                    { 7, 14, new DateTimeOffset(new DateTime(2025, 11, 21, 15, 20, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 2 }
                });

            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "Id", "CabinetId", "DoctorId", "EndTime", "StartTime" },
                values: new object[,]
                {
                    { 1, 2, 1, new DateTimeOffset(new DateTime(2025, 11, 21, 13, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 11, 21, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)) },
                    { 2, 2, 1, new DateTimeOffset(new DateTime(2025, 11, 21, 18, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 11, 21, 14, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)) },
                    { 3, 4, 2, new DateTimeOffset(new DateTime(2025, 11, 21, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 11, 21, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)) },
                    { 4, 4, 2, new DateTimeOffset(new DateTime(2025, 11, 21, 17, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 11, 21, 13, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)) },
                    { 5, 3, 3, new DateTimeOffset(new DateTime(2025, 11, 21, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 11, 21, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)) },
                    { 6, 3, 3, new DateTimeOffset(new DateTime(2025, 11, 21, 17, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 11, 21, 13, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)) },
                    { 7, 5, 4, new DateTimeOffset(new DateTime(2025, 11, 22, 14, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 11, 22, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)) },
                    { 8, 5, 4, new DateTimeOffset(new DateTime(2025, 11, 22, 19, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 11, 22, 15, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)) },
                    { 9, 12, 5, new DateTimeOffset(new DateTime(2025, 11, 22, 13, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 11, 22, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)) },
                    { 10, 13, 6, new DateTimeOffset(new DateTime(2025, 11, 28, 13, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 11, 28, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)) },
                    { 11, 6, 7, new DateTimeOffset(new DateTime(2025, 11, 21, 14, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 11, 21, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)) },
                    { 12, 7, 8, new DateTimeOffset(new DateTime(2025, 11, 22, 13, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 11, 22, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "CabinetId", "DidItHappen", "DoctorProcedureId", "EndTime", "PatientId", "StartTime" },
                values: new object[,]
                {
                    { 1, 2, true, 1, new DateTimeOffset(new DateTime(2025, 11, 14, 10, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 1, new DateTimeOffset(new DateTime(2025, 11, 14, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)) },
                    { 2, 4, true, 7, new DateTimeOffset(new DateTime(2025, 11, 15, 11, 45, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2025, 11, 15, 11, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)) },
                    { 3, 3, true, 11, new DateTimeOffset(new DateTime(2025, 11, 16, 9, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 3, new DateTimeOffset(new DateTime(2025, 11, 16, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)) },
                    { 4, 2, true, 1, new DateTimeOffset(new DateTime(2025, 11, 20, 10, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 4, new DateTimeOffset(new DateTime(2025, 11, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)) },
                    { 5, 5, true, 15, new DateTimeOffset(new DateTime(2025, 11, 20, 14, 40, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 5, new DateTimeOffset(new DateTime(2025, 11, 20, 14, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)) },
                    { 6, 2, false, 1, new DateTimeOffset(new DateTime(2025, 11, 21, 10, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 6, new DateTimeOffset(new DateTime(2025, 11, 21, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)) },
                    { 7, 4, false, 7, new DateTimeOffset(new DateTime(2025, 11, 21, 11, 45, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 7, new DateTimeOffset(new DateTime(2025, 11, 21, 11, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)) },
                    { 8, 3, false, 11, new DateTimeOffset(new DateTime(2025, 11, 21, 14, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 8, new DateTimeOffset(new DateTime(2025, 11, 21, 14, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)) },
                    { 9, 5, false, 15, new DateTimeOffset(new DateTime(2025, 11, 22, 10, 40, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 9, new DateTimeOffset(new DateTime(2025, 11, 22, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)) },
                    { 10, 12, false, 17, new DateTimeOffset(new DateTime(2025, 11, 22, 11, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 10, new DateTimeOffset(new DateTime(2025, 11, 22, 11, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)) },
                    { 11, 2, false, 1, new DateTimeOffset(new DateTime(2025, 11, 22, 15, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 11, new DateTimeOffset(new DateTime(2025, 11, 22, 15, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)) },
                    { 12, 6, false, 20, new DateTimeOffset(new DateTime(2025, 11, 22, 10, 45, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 12, new DateTimeOffset(new DateTime(2025, 11, 22, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Diagnoses",
                columns: new[] { "Id", "AppointmentId", "Prescription" },
                values: new object[,]
                {
                    { 1, 1, "Rest for 3-5 days. Take paracetamol 500mg every 6 hours if fever above 38°C. Drink plenty of fluids. Return if symptoms worsen." },
                    { 2, 2, "Hypertension stage 1. Prescribed: Lisinopril 10mg once daily. Monitor blood pressure twice daily. Reduce salt intake. Follow-up in 2 weeks." },
                    { 3, 3, "Common cold in child. Symptomatic treatment: nasal saline drops, paracetamol syrup if fever. Ensure adequate hydration. Monitor temperature." },
                    { 4, 4, "Acute bronchitis. Prescribed: Amoxicillin 500mg three times daily for 7 days. Expectorant syrup. Rest and avoid cold air. Follow-up if no improvement in 5 days." },
                    { 5, 5, "Tension headache. Prescribed: Ibuprofen 400mg as needed. Stress management techniques. Adequate sleep. Return if headaches persist or worsen." }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Cabinets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cabinets",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Cabinets",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Cabinets",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Cabinets",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Cabinets",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Cabinets",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Diagnoses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Diagnoses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Diagnoses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Diagnoses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Diagnoses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "DistrictDoctors",
                keyColumn: "DoctorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DistrictDoctors",
                keyColumn: "DoctorId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DistrictDoctors",
                keyColumn: "DoctorId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DistrictDoctors",
                keyColumn: "DoctorId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DoctorOnCallStatuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DoctorOnCallStatuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DoctorOnCallStatuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DoctorOnCallStatuses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DoctorProcedures",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DoctorProcedures",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DoctorProcedures",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DoctorProcedures",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "DoctorProcedures",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "DoctorProcedures",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "DoctorProcedures",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "DoctorProcedures",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "DoctorProcedures",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "DoctorProcedures",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "DoctorProcedures",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "DoctorProcedures",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "DoctorProcedures",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "DoctorProcedures",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "DoctorProcedures",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "DoctorProcedures",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "HomeCallLogs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "HomeCallLogs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "HomeCallLogs",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "HomeCallLogs",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "HomeCallLogs",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "HomeCallLogs",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "HomeCallLogs",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Procedures",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Procedures",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "SicknessProcedures",
                keyColumns: new[] { "ProcedureId", "SicknessId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "SicknessProcedures",
                keyColumns: new[] { "ProcedureId", "SicknessId" },
                keyValues: new object[] { 8, 1 });

            migrationBuilder.DeleteData(
                table: "SicknessProcedures",
                keyColumns: new[] { "ProcedureId", "SicknessId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "SicknessProcedures",
                keyColumns: new[] { "ProcedureId", "SicknessId" },
                keyValues: new object[] { 6, 2 });

            migrationBuilder.DeleteData(
                table: "SicknessProcedures",
                keyColumns: new[] { "ProcedureId", "SicknessId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "SicknessProcedures",
                keyColumns: new[] { "ProcedureId", "SicknessId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "SicknessProcedures",
                keyColumns: new[] { "ProcedureId", "SicknessId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "SicknessProcedures",
                keyColumns: new[] { "ProcedureId", "SicknessId" },
                keyValues: new object[] { 6, 3 });

            migrationBuilder.DeleteData(
                table: "SicknessProcedures",
                keyColumns: new[] { "ProcedureId", "SicknessId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "SicknessProcedures",
                keyColumns: new[] { "ProcedureId", "SicknessId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "SicknessProcedures",
                keyColumns: new[] { "ProcedureId", "SicknessId" },
                keyValues: new object[] { 11, 4 });

            migrationBuilder.DeleteData(
                table: "SicknessProcedures",
                keyColumns: new[] { "ProcedureId", "SicknessId" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "SicknessProcedures",
                keyColumns: new[] { "ProcedureId", "SicknessId" },
                keyValues: new object[] { 4, 5 });

            migrationBuilder.DeleteData(
                table: "SicknessProcedures",
                keyColumns: new[] { "ProcedureId", "SicknessId" },
                keyValues: new object[] { 6, 5 });

            migrationBuilder.DeleteData(
                table: "SicknessProcedures",
                keyColumns: new[] { "ProcedureId", "SicknessId" },
                keyValues: new object[] { 1, 6 });

            migrationBuilder.DeleteData(
                table: "SicknessProcedures",
                keyColumns: new[] { "ProcedureId", "SicknessId" },
                keyValues: new object[] { 5, 6 });

            migrationBuilder.DeleteData(
                table: "SicknessProcedures",
                keyColumns: new[] { "ProcedureId", "SicknessId" },
                keyValues: new object[] { 6, 6 });

            migrationBuilder.DeleteData(
                table: "SicknessProcedures",
                keyColumns: new[] { "ProcedureId", "SicknessId" },
                keyValues: new object[] { 1, 7 });

            migrationBuilder.DeleteData(
                table: "SicknessProcedures",
                keyColumns: new[] { "ProcedureId", "SicknessId" },
                keyValues: new object[] { 6, 7 });

            migrationBuilder.DeleteData(
                table: "SicknessProcedures",
                keyColumns: new[] { "ProcedureId", "SicknessId" },
                keyValues: new object[] { 1, 8 });

            migrationBuilder.DeleteData(
                table: "SicknessProcedures",
                keyColumns: new[] { "ProcedureId", "SicknessId" },
                keyValues: new object[] { 4, 8 });

            migrationBuilder.DeleteData(
                table: "SicknessProcedures",
                keyColumns: new[] { "ProcedureId", "SicknessId" },
                keyValues: new object[] { 1, 9 });

            migrationBuilder.DeleteData(
                table: "SicknessProcedures",
                keyColumns: new[] { "ProcedureId", "SicknessId" },
                keyValues: new object[] { 1, 12 });

            migrationBuilder.DeleteData(
                table: "SicknessProcedures",
                keyColumns: new[] { "ProcedureId", "SicknessId" },
                keyValues: new object[] { 10, 12 });

            migrationBuilder.DeleteData(
                table: "SicknessSymptoms",
                keyColumns: new[] { "SicknessId", "SymptomId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "SicknessSymptoms",
                keyColumns: new[] { "SicknessId", "SymptomId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "SicknessSymptoms",
                keyColumns: new[] { "SicknessId", "SymptomId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "SicknessSymptoms",
                keyColumns: new[] { "SicknessId", "SymptomId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "SicknessSymptoms",
                keyColumns: new[] { "SicknessId", "SymptomId" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "SicknessSymptoms",
                keyColumns: new[] { "SicknessId", "SymptomId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "SicknessSymptoms",
                keyColumns: new[] { "SicknessId", "SymptomId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "SicknessSymptoms",
                keyColumns: new[] { "SicknessId", "SymptomId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "SicknessSymptoms",
                keyColumns: new[] { "SicknessId", "SymptomId" },
                keyValues: new object[] { 2, 13 });

            migrationBuilder.DeleteData(
                table: "SicknessSymptoms",
                keyColumns: new[] { "SicknessId", "SymptomId" },
                keyValues: new object[] { 2, 15 });

            migrationBuilder.DeleteData(
                table: "SicknessSymptoms",
                keyColumns: new[] { "SicknessId", "SymptomId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "SicknessSymptoms",
                keyColumns: new[] { "SicknessId", "SymptomId" },
                keyValues: new object[] { 3, 12 });

            migrationBuilder.DeleteData(
                table: "SicknessSymptoms",
                keyColumns: new[] { "SicknessId", "SymptomId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "SicknessSymptoms",
                keyColumns: new[] { "SicknessId", "SymptomId" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "SicknessSymptoms",
                keyColumns: new[] { "SicknessId", "SymptomId" },
                keyValues: new object[] { 4, 7 });

            migrationBuilder.DeleteData(
                table: "SicknessSymptoms",
                keyColumns: new[] { "SicknessId", "SymptomId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "SicknessSymptoms",
                keyColumns: new[] { "SicknessId", "SymptomId" },
                keyValues: new object[] { 5, 3 });

            migrationBuilder.DeleteData(
                table: "SicknessSymptoms",
                keyColumns: new[] { "SicknessId", "SymptomId" },
                keyValues: new object[] { 5, 6 });

            migrationBuilder.DeleteData(
                table: "SicknessSymptoms",
                keyColumns: new[] { "SicknessId", "SymptomId" },
                keyValues: new object[] { 5, 7 });

            migrationBuilder.DeleteData(
                table: "SicknessSymptoms",
                keyColumns: new[] { "SicknessId", "SymptomId" },
                keyValues: new object[] { 6, 8 });

            migrationBuilder.DeleteData(
                table: "SicknessSymptoms",
                keyColumns: new[] { "SicknessId", "SymptomId" },
                keyValues: new object[] { 6, 11 });

            migrationBuilder.DeleteData(
                table: "SicknessSymptoms",
                keyColumns: new[] { "SicknessId", "SymptomId" },
                keyValues: new object[] { 6, 20 });

            migrationBuilder.DeleteData(
                table: "SicknessSymptoms",
                keyColumns: new[] { "SicknessId", "SymptomId" },
                keyValues: new object[] { 7, 2 });

            migrationBuilder.DeleteData(
                table: "SicknessSymptoms",
                keyColumns: new[] { "SicknessId", "SymptomId" },
                keyValues: new object[] { 7, 8 });

            migrationBuilder.DeleteData(
                table: "SicknessSymptoms",
                keyColumns: new[] { "SicknessId", "SymptomId" },
                keyValues: new object[] { 7, 12 });

            migrationBuilder.DeleteData(
                table: "SicknessSymptoms",
                keyColumns: new[] { "SicknessId", "SymptomId" },
                keyValues: new object[] { 8, 14 });

            migrationBuilder.DeleteData(
                table: "SicknessSymptoms",
                keyColumns: new[] { "SicknessId", "SymptomId" },
                keyValues: new object[] { 8, 15 });

            migrationBuilder.DeleteData(
                table: "SicknessSymptoms",
                keyColumns: new[] { "SicknessId", "SymptomId" },
                keyValues: new object[] { 9, 16 });

            migrationBuilder.DeleteData(
                table: "SicknessSymptoms",
                keyColumns: new[] { "SicknessId", "SymptomId" },
                keyValues: new object[] { 9, 17 });

            migrationBuilder.DeleteData(
                table: "SicknessSymptoms",
                keyColumns: new[] { "SicknessId", "SymptomId" },
                keyValues: new object[] { 12, 2 });

            migrationBuilder.DeleteData(
                table: "SicknessSymptoms",
                keyColumns: new[] { "SicknessId", "SymptomId" },
                keyValues: new object[] { 12, 4 });

            migrationBuilder.DeleteData(
                table: "SicknessSymptoms",
                keyColumns: new[] { "SicknessId", "SymptomId" },
                keyValues: new object[] { 12, 5 });

            migrationBuilder.DeleteData(
                table: "SicknessTreatments",
                keyColumns: new[] { "SicknessId", "TreatmentId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "SicknessTreatments",
                keyColumns: new[] { "SicknessId", "TreatmentId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "SicknessTreatments",
                keyColumns: new[] { "SicknessId", "TreatmentId" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "SicknessTreatments",
                keyColumns: new[] { "SicknessId", "TreatmentId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "SicknessTreatments",
                keyColumns: new[] { "SicknessId", "TreatmentId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "SicknessTreatments",
                keyColumns: new[] { "SicknessId", "TreatmentId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "SicknessTreatments",
                keyColumns: new[] { "SicknessId", "TreatmentId" },
                keyValues: new object[] { 3, 8 });

            migrationBuilder.DeleteData(
                table: "SicknessTreatments",
                keyColumns: new[] { "SicknessId", "TreatmentId" },
                keyValues: new object[] { 3, 11 });

            migrationBuilder.DeleteData(
                table: "SicknessTreatments",
                keyColumns: new[] { "SicknessId", "TreatmentId" },
                keyValues: new object[] { 3, 12 });

            migrationBuilder.DeleteData(
                table: "SicknessTreatments",
                keyColumns: new[] { "SicknessId", "TreatmentId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "SicknessTreatments",
                keyColumns: new[] { "SicknessId", "TreatmentId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "SicknessTreatments",
                keyColumns: new[] { "SicknessId", "TreatmentId" },
                keyValues: new object[] { 4, 6 });

            migrationBuilder.DeleteData(
                table: "SicknessTreatments",
                keyColumns: new[] { "SicknessId", "TreatmentId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "SicknessTreatments",
                keyColumns: new[] { "SicknessId", "TreatmentId" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                table: "SicknessTreatments",
                keyColumns: new[] { "SicknessId", "TreatmentId" },
                keyValues: new object[] { 5, 4 });

            migrationBuilder.DeleteData(
                table: "SicknessTreatments",
                keyColumns: new[] { "SicknessId", "TreatmentId" },
                keyValues: new object[] { 6, 4 });

            migrationBuilder.DeleteData(
                table: "SicknessTreatments",
                keyColumns: new[] { "SicknessId", "TreatmentId" },
                keyValues: new object[] { 6, 7 });

            migrationBuilder.DeleteData(
                table: "SicknessTreatments",
                keyColumns: new[] { "SicknessId", "TreatmentId" },
                keyValues: new object[] { 6, 11 });

            migrationBuilder.DeleteData(
                table: "SicknessTreatments",
                keyColumns: new[] { "SicknessId", "TreatmentId" },
                keyValues: new object[] { 7, 1 });

            migrationBuilder.DeleteData(
                table: "SicknessTreatments",
                keyColumns: new[] { "SicknessId", "TreatmentId" },
                keyValues: new object[] { 7, 4 });

            migrationBuilder.DeleteData(
                table: "SicknessTreatments",
                keyColumns: new[] { "SicknessId", "TreatmentId" },
                keyValues: new object[] { 8, 7 });

            migrationBuilder.DeleteData(
                table: "SicknessTreatments",
                keyColumns: new[] { "SicknessId", "TreatmentId" },
                keyValues: new object[] { 8, 10 });

            migrationBuilder.DeleteData(
                table: "SicknessTreatments",
                keyColumns: new[] { "SicknessId", "TreatmentId" },
                keyValues: new object[] { 9, 5 });

            migrationBuilder.DeleteData(
                table: "SicknessTreatments",
                keyColumns: new[] { "SicknessId", "TreatmentId" },
                keyValues: new object[] { 9, 13 });

            migrationBuilder.DeleteData(
                table: "SicknessTreatments",
                keyColumns: new[] { "SicknessId", "TreatmentId" },
                keyValues: new object[] { 12, 2 });

            migrationBuilder.DeleteData(
                table: "SicknessTreatments",
                keyColumns: new[] { "SicknessId", "TreatmentId" },
                keyValues: new object[] { 12, 4 });

            migrationBuilder.DeleteData(
                table: "SicknessTreatments",
                keyColumns: new[] { "SicknessId", "TreatmentId" },
                keyValues: new object[] { 12, 15 });

            migrationBuilder.DeleteData(
                table: "Sicknesses",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Sicknesses",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Sicknesses",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Sicknesses",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Sicknesses",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Symptoms",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Symptoms",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Symptoms",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Symptoms",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Treatments",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Treatments",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CabinetTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CabinetTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CabinetTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "CabinetTypes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "CabinetTypes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "CabinetTypes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "CabinetTypes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Cabinets",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Cabinets",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Cabinets",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Cabinets",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "DoctorProcedures",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "DoctorProcedures",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Procedures",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Procedures",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Procedures",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Procedures",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Procedures",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Procedures",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Procedures",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Procedures",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Procedures",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Procedures",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Procedures",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Sicknesses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sicknesses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sicknesses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Sicknesses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Sicknesses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Sicknesses",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Sicknesses",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Sicknesses",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Sicknesses",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Sicknesses",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Symptoms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Symptoms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Symptoms",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Symptoms",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Symptoms",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Symptoms",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Symptoms",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Symptoms",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Symptoms",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Symptoms",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Symptoms",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Symptoms",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Symptoms",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Symptoms",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Symptoms",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Symptoms",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Treatments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Treatments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Treatments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Treatments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Treatments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Treatments",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Treatments",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Treatments",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Treatments",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Treatments",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Treatments",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Treatments",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Treatments",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "CabinetTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cabinets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cabinets",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cabinets",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cabinets",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "DoctorProcedures",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DoctorProcedures",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "DoctorProcedures",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "DoctorProcedures",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Procedures",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "CabinetTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CabinetTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Procedures",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
