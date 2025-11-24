-- Clean up existing data
DELETE FROM "Diagnoses";
DELETE FROM "Appointments";
DELETE FROM "HomeCallLogs";
DELETE FROM "Schedules";
DELETE FROM "DoctorProcedures";
DELETE FROM "DoctorOnCallStatuses";
DELETE FROM "DistrictDoctors";
DELETE FROM "SicknessProcedures";
DELETE FROM "SicknessSymptoms";
DELETE FROM "SicknessTreatments";
DELETE FROM "Patients";
DELETE FROM "Doctors";
DELETE FROM "Cabinets";
DELETE FROM "People";
DELETE FROM "Procedures";
DELETE FROM "Treatments";
DELETE FROM "Sicknesses";
DELETE FROM "Symptoms";
DELETE FROM "Specialties";
DELETE FROM "CabinetTypes";
DELETE FROM "Addresses";

-- FIRST WAVE: Base tables with explicit IDs
INSERT INTO "Addresses" ("Id", "Country", "State", "Locality", "StreetName", "StreetNumber") VALUES
(1, 'USA', 'California', 'Los Angeles', 'Main Street', 123),
(2, 'USA', 'California', 'Los Angeles', 'Sunset Boulevard', 456),
(3, 'USA', 'California', 'San Francisco', 'Market Street', 100),
(4, 'USA', 'New York', 'New York City', 'Broadway', 456),
(5, 'USA', 'New York', 'New York City', 'Fifth Avenue', 789),
(6, 'USA', 'Texas', 'Houston', 'Main Street', 123),
(7, 'USA', 'Texas', 'Dallas', 'Commerce Street', 300),
(8, 'USA', 'Florida', 'Miami', 'Beach Road', 321),
(9, 'USA', 'Illinois', 'Chicago', 'Michigan Avenue', 654),
(10, 'USA', 'Pennsylvania', 'Philadelphia', 'Market Street', 200);

INSERT INTO "CabinetTypes" ("Id", "Type") VALUES
(1, 'Examination'),
(2, 'Consultation'),
(3, 'Surgery'),
(4, 'Emergency'),
(5, 'Laboratory'),
(6, 'Radiology'),
(7, 'Physical Therapy'),
(8, 'Cardiology'),
(9, 'Dermatology'),
(10, 'Pediatrics');

INSERT INTO "Specialties" ("Id", "Name") VALUES
(1, 'Cardiology'),
(2, 'Dermatology'),
(3, 'Pediatrics'),
(4, 'Orthopedics'),
(5, 'Neurology'),
(6, 'General Practice'),
(7, 'Internal Medicine'),
(8, 'Oncology'),
(9, 'Psychiatry'),
(10, 'Radiology');

INSERT INTO "Symptoms" ("Id", "Name") VALUES
(1, 'Fever'),
(2, 'Cough'),
(3, 'Headache'),
(4, 'Nausea'),
(5, 'Fatigue'),
(6, 'Chest Pain'),
(7, 'Shortness of Breath'),
(8, 'Joint Pain'),
(9, 'Rash'),
(10, 'Dizziness');

INSERT INTO "Sicknesses" ("Id", "Name") VALUES
(1, 'Common Cold'),
(2, 'Influenza'),
(3, 'Hypertension'),
(4, 'Diabetes'),
(5, 'Asthma'),
(6, 'Arthritis'),
(7, 'Migraine'),
(8, 'Pneumonia'),
(9, 'Bronchitis'),
(10, 'Allergies');

INSERT INTO "Treatments" ("Id", "Name") VALUES
(1, 'Antibiotics'),
(2, 'Pain Relief'),
(3, 'Rest and Hydration'),
(4, 'Physical Therapy'),
(5, 'Medication'),
(6, 'Surgery'),
(7, 'Therapy'),
(8, 'Dietary Changes'),
(9, 'Exercise Program'),
(10, 'Monitoring');

INSERT INTO "Procedures" ("Id", "Name", "Description", "Price") VALUES
(1, 'General Checkup', 'Routine physical examination', 150.00),
(2, 'Blood Test', 'Complete blood count and analysis', 75.00),
(3, 'X-Ray', 'Radiographic imaging', 200.00),
(4, 'MRI Scan', 'Magnetic resonance imaging', 800.00),
(5, 'CT Scan', 'Computed tomography scan', 600.00),
(6, 'Ultrasound', 'Ultrasonic imaging', 300.00),
(7, 'EKG', 'Electrocardiogram', 120.00),
(8, 'Echocardiogram', 'Heart ultrasound', 450.00),
(9, 'Colonoscopy', 'Colon examination', 1200.00),
(10, 'Endoscopy', 'Upper GI examination', 900.00);

-- SECOND WAVE: Dependent on first wave
INSERT INTO "People" ("Id", "FirstName", "LastName", "Patronymic") VALUES
(1, 'John', 'Smith', 'Michael'),
(2, 'Sarah', 'Johnson', 'Elizabeth'),
(3, 'Michael', 'Williams', 'David'),
(4, 'Emily', 'Brown', 'Anne'),
(5, 'David', 'Jones', 'Robert'),
(6, 'Jessica', 'Garcia', 'Marie'),
(7, 'Robert', 'Miller', 'James'),
(8, 'Amanda', 'Davis', 'Rose'),
(9, 'James', 'Rodriguez', 'Thomas'),
(10, 'Lisa', 'Martinez', 'Jane'),
(11, 'Christopher', 'Green', 'Daniel'),
(12, 'Melissa', 'Adams', 'Rebecca'),
(13, 'Kevin', 'Nelson', 'Justin'),
(14, 'Stephanie', 'Baker', 'Rachel'),
(15, 'Brian', 'Carter', 'Brandon');

INSERT INTO "Patients" ("Id", "PersonId", "AddressId") VALUES
(1, 11, 1),
(2, 12, 2),
(3, 13, 3),
(4, 14, 4),
(5, 15, 5);

INSERT INTO "Doctors" ("Id", "PersonId", "SpecialtyId") VALUES
(1, 1, 1),
(2, 2, 2),
(3, 3, 3),
(4, 4, 4),
(5, 5, 5),
(6, 6, 6),
(7, 7, 7),
(8, 8, 8),
(9, 9, 9),
(10, 10, 10);

INSERT INTO "Cabinets" ("Id", "Building", "Floor", "Number", "TypeId") VALUES
(1, 1, 1, 101, 1),
(2, 1, 1, 102, 2),
(3, 1, 2, 201, 3),
(4, 1, 2, 202, 4),
(5, 2, 1, 101, 5);

INSERT INTO "SicknessSymptoms" ("SicknessId", "SymptomId") VALUES
(1, 1), (1, 2), (1, 3),
(2, 1), (2, 2), (2, 5),
(3, 3), (3, 10),
(5, 7), (5, 2);

INSERT INTO "SicknessTreatments" ("SicknessId", "TreatmentId") VALUES
(1, 2), (1, 3),
(2, 1), (2, 2),
(3, 5), (3, 8),
(4, 5), (4, 8);

INSERT INTO "SicknessProcedures" ("SicknessId", "ProcedureId") VALUES
(1, 1), (1, 2),
(2, 1), (2, 2),
(3, 1), (3, 7),
(5, 1), (5, 6);

-- THIRD WAVE: Dependent on second wave
INSERT INTO "DistrictDoctors" ("DoctorId") VALUES
(1), (2), (3);

INSERT INTO "DoctorOnCallStatuses" ("Id", "DoctorId", "AddressId", "StartTime", "EndTime") VALUES
(1, 1, 1, '2024-01-15 08:00:00+00', '2024-01-15 20:00:00+00'),
(2, 2, 2, '2024-01-16 08:00:00+00', '2024-01-16 20:00:00+00'),
(3, 3, 3, '2024-01-17 08:00:00+00', '2024-01-17 20:00:00+00');

INSERT INTO "DoctorProcedures" ("Id", "DoctorId", "ProcedureId") VALUES
(1, 1, 1),
(2, 1, 2),
(3, 1, 7),
(4, 2, 1),
(5, 2, 3),
(6, 3, 1),
(7, 3, 2),
(8, 4, 1),
(9, 4, 3),
(10, 5, 1),
(11, 5, 4);

INSERT INTO "Schedules" ("Id", "DoctorId", "CabinetId", "StartTime", "EndTime") VALUES
(1, 1, 1, '2024-01-15 08:00:00+00', '2024-01-15 12:00:00+00'),
(2, 1, 1, '2024-01-15 13:00:00+00', '2024-01-15 17:00:00+00'),
(3, 2, 2, '2024-01-16 08:00:00+00', '2024-01-16 12:00:00+00'),
(4, 3, 3, '2024-01-17 09:00:00+00', '2024-01-17 13:00:00+00'),
(5, 4, 4, '2024-01-18 10:00:00+00', '2024-01-18 14:00:00+00');

-- FOURTH WAVE: Dependent on third wave
INSERT INTO "Appointments" ("Id", "DoctorProcedureId", "CabinetId", "PatientId", "StartTime", "EndTime", "DidItHappen") VALUES
(1, 1, 1, 1, '2024-01-15 08:00:00+00', '2024-01-15 08:30:00+00', true),
(2, 2, 1, 2, '2024-01-15 08:30:00+00', '2024-01-15 09:00:00+00', true),
(3, 4, 2, 3, '2024-01-16 08:00:00+00', '2024-01-16 08:30:00+00', true),
(4, 6, 3, 4, '2024-01-17 09:00:00+00', '2024-01-17 09:30:00+00', false),
(5, 8, 4, 5, '2024-01-18 10:00:00+00', '2024-01-18 10:30:00+00', true);

INSERT INTO "HomeCallLogs" ("Id", "DoctorId", "AddressId", "DateTime") VALUES
(1, 1, 1, '2024-01-15 10:00:00+00'),
(2, 2, 2, '2024-01-16 11:00:00+00'),
(3, 3, 3, '2024-01-17 14:00:00+00');

-- FIFTH WAVE: Dependent on fourth wave
INSERT INTO "Diagnoses" ("Id", "AppointmentId", "Prescription") VALUES
(1, 1, 'Take ibuprofen 200mg twice daily for 5 days. Rest and stay hydrated.'),
(2, 2, 'Antibiotic course: Amoxicillin 500mg three times daily for 7 days.'),
(3, 3, 'Blood pressure medication: Lisinopril 10mg once daily. Monitor BP weekly.'),
(4, 5, 'General checkup: All vital signs normal. Continue healthy lifestyle.');
