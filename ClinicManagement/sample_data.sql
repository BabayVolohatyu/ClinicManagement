-- Sample Data for Clinic Management Database
-- Execute this script to populate your database with test data
-- NOTE: Auth tables (Users, Roles, PromotionRequests) are NOT included

-- ============================================
-- FIRST WAVE: Base tables (no dependencies)
-- ============================================

-- Addresses
INSERT INTO "Addresses" ("Country", "State", "Locality", "StreetName", "StreetNumber") VALUES
('USA', 'California', 'Los Angeles', 'Main Street', 123),
('USA', 'New York', 'New York City', 'Broadway', 456),
('USA', 'Texas', 'Houston', 'Oak Avenue', 789),
('USA', 'Florida', 'Miami', 'Beach Road', 321),
('USA', 'Illinois', 'Chicago', 'Michigan Avenue', 654);

-- CabinetTypes
INSERT INTO "CabinetTypes" ("Type") VALUES
('Examination'),
('Consultation'),
('Surgery'),
('Emergency'),
('Laboratory');

-- Specialties
INSERT INTO "Specialties" ("Name") VALUES
('Cardiology'),
('Dermatology'),
('Pediatrics'),
('Orthopedics'),
('Neurology'),
('General Practice');

-- Symptoms
INSERT INTO "Symptoms" ("Name") VALUES
('Fever'),
('Cough'),
('Headache'),
('Nausea'),
('Fatigue'),
('Chest Pain'),
('Shortness of Breath'),
('Joint Pain'),
('Rash'),
('Dizziness');

-- Sicknesses
INSERT INTO "Sicknesses" ("Name") VALUES
('Common Cold'),
('Influenza'),
('Hypertension'),
('Diabetes'),
('Asthma'),
('Arthritis'),
('Migraine'),
('Pneumonia'),
('Bronchitis'),
('Allergies');

-- Treatments
INSERT INTO "Treatments" ("Name") VALUES
('Antibiotics'),
('Pain Relief'),
('Rest and Hydration'),
('Physical Therapy'),
('Medication'),
('Surgery'),
('Therapy'),
('Dietary Changes'),
('Exercise Program'),
('Monitoring');

-- Procedures
INSERT INTO "Procedures" ("Name", "Description", "Price") VALUES
('Blood Test', 'Complete blood count and analysis', 150.00),
('X-Ray', 'Chest X-ray examination', 200.00),
('ECG', 'Electrocardiogram test', 180.00),
('Ultrasound', 'Ultrasound imaging', 250.00),
('MRI Scan', 'Magnetic resonance imaging', 800.00),
('CT Scan', 'Computed tomography scan', 600.00),
('Physical Examination', 'General physical checkup', 100.00),
('Vaccination', 'Immunization procedure', 75.00),
('Biopsy', 'Tissue sample collection', 400.00),
('Endoscopy', 'Internal examination procedure', 500.00);

-- People
INSERT INTO "People" ("FirstName", "LastName", "Patronymic") VALUES
('John', 'Smith', 'Michael'),
('Sarah', 'Johnson', 'Elizabeth'),
('Michael', 'Williams', 'David'),
('Emily', 'Brown', 'Anne'),
('David', 'Jones', 'Robert'),
('Jessica', 'Garcia', 'Maria'),
('Robert', 'Miller', 'James'),
('Amanda', 'Davis', 'Rose'),
('James', 'Rodriguez', 'Carlos'),
('Lisa', 'Martinez', 'Patricia'),
('William', 'Hernandez', 'Jose'),
('Mary', 'Lopez', 'Carmen'),
('Richard', 'Wilson', 'Thomas'),
('Patricia', 'Anderson', 'Jane'),
('Joseph', 'Thomas', 'Paul'),
('Jennifer', 'Taylor', 'Susan'),
('Thomas', 'Moore', 'Charles'),
('Linda', 'Jackson', 'Karen'),
('Charles', 'Martin', 'Edward'),
('Barbara', 'Lee', 'Nancy');

-- ============================================
-- SECOND WAVE: Dependent on first wave
-- ============================================

-- Cabinets (using subqueries to find CabinetTypes by name)
INSERT INTO "Cabinets" ("Building", "Floor", "Number", "TypeId") VALUES
(1, 1, 101, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Examination' LIMIT 1)),
(1, 1, 102, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Consultation' LIMIT 1)),
(1, 1, 103, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Surgery' LIMIT 1)),
(1, 2, 201, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Examination' LIMIT 1)),
(1, 2, 202, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Consultation' LIMIT 1)),
(2, 1, 101, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Emergency' LIMIT 1)),
(2, 1, 102, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Laboratory' LIMIT 1)),
(2, 2, 201, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Examination' LIMIT 1)),
(2, 2, 202, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Surgery' LIMIT 1)),
(2, 3, 301, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Consultation' LIMIT 1));

-- Patients (using subqueries to find People and Addresses by name/location)
INSERT INTO "Patients" ("PersonId", "AddressId") VALUES
((SELECT "Id" FROM "People" WHERE "FirstName" = 'John' AND "LastName" = 'Smith' LIMIT 1), 
 (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Los Angeles' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Sarah' AND "LastName" = 'Johnson' LIMIT 1), 
 (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'New York City' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Michael' AND "LastName" = 'Williams' LIMIT 1), 
 (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Houston' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Emily' AND "LastName" = 'Brown' LIMIT 1), 
 (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Miami' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'David' AND "LastName" = 'Jones' LIMIT 1), 
 (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Chicago' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Jessica' AND "LastName" = 'Garcia' LIMIT 1), 
 (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Los Angeles' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Robert' AND "LastName" = 'Miller' LIMIT 1), 
 (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'New York City' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Amanda' AND "LastName" = 'Davis' LIMIT 1), 
 (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Houston' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'James' AND "LastName" = 'Rodriguez' LIMIT 1), 
 (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Miami' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Lisa' AND "LastName" = 'Martinez' LIMIT 1), 
 (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Chicago' LIMIT 1));

-- Doctors (using subqueries to find People and Specialties by name)
INSERT INTO "Doctors" ("PersonId", "SpecialtyId") VALUES
((SELECT "Id" FROM "People" WHERE "FirstName" = 'William' AND "LastName" = 'Hernandez' LIMIT 1), 
 (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Cardiology' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Mary' AND "LastName" = 'Lopez' LIMIT 1), 
 (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Dermatology' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Richard' AND "LastName" = 'Wilson' LIMIT 1), 
 (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Pediatrics' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Patricia' AND "LastName" = 'Anderson' LIMIT 1), 
 (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Orthopedics' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Joseph' AND "LastName" = 'Thomas' LIMIT 1), 
 (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Neurology' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Jennifer' AND "LastName" = 'Taylor' LIMIT 1), 
 (SELECT "Id" FROM "Specialties" WHERE "Name" = 'General Practice' LIMIT 1));

-- ============================================
-- THIRD WAVE: Dependent on second wave
-- ============================================

-- DistrictDoctors (using subqueries to find Doctors by person name)
INSERT INTO "DistrictDoctors" ("DoctorId") VALUES
((SELECT d."Id" FROM "Doctors" d 
  JOIN "People" p ON d."PersonId" = p."Id" 
  WHERE p."FirstName" = 'William' AND p."LastName" = 'Hernandez' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d 
  JOIN "People" p ON d."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Mary' AND p."LastName" = 'Lopez' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d 
  JOIN "People" p ON d."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Richard' AND p."LastName" = 'Wilson' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d 
  JOIN "People" p ON d."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Patricia' AND p."LastName" = 'Anderson' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d 
  JOIN "People" p ON d."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Joseph' AND p."LastName" = 'Thomas' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d 
  JOIN "People" p ON d."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Jennifer' AND p."LastName" = 'Taylor' LIMIT 1));

-- DoctorOnCallStatus (using subqueries)
INSERT INTO "DoctorOnCallStatuses" ("DoctorId", "AddressId", "StartTime", "EndTime") VALUES
((SELECT d."Id" FROM "Doctors" d 
  JOIN "People" p ON d."PersonId" = p."Id" 
  WHERE p."FirstName" = 'William' AND p."LastName" = 'Hernandez' LIMIT 1),
 (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Los Angeles' LIMIT 1),
 '2024-01-15 08:00:00+00', '2024-01-15 20:00:00+00'),
((SELECT d."Id" FROM "Doctors" d 
  JOIN "People" p ON d."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Mary' AND p."LastName" = 'Lopez' LIMIT 1),
 (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'New York City' LIMIT 1),
 '2024-01-15 09:00:00+00', '2024-01-15 17:00:00+00'),
((SELECT d."Id" FROM "Doctors" d 
  JOIN "People" p ON d."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Richard' AND p."LastName" = 'Wilson' LIMIT 1),
 (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Houston' LIMIT 1),
 '2024-01-16 08:00:00+00', '2024-01-16 20:00:00+00'),
((SELECT d."Id" FROM "Doctors" d 
  JOIN "People" p ON d."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Patricia' AND p."LastName" = 'Anderson' LIMIT 1),
 (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Miami' LIMIT 1),
 '2024-01-16 09:00:00+00', '2024-01-16 17:00:00+00'),
((SELECT d."Id" FROM "Doctors" d 
  JOIN "People" p ON d."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Joseph' AND p."LastName" = 'Thomas' LIMIT 1),
 (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Chicago' LIMIT 1),
 '2024-01-17 08:00:00+00', '2024-01-17 20:00:00+00'),
((SELECT d."Id" FROM "Doctors" d 
  JOIN "People" p ON d."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Jennifer' AND p."LastName" = 'Taylor' LIMIT 1),
 (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Los Angeles' LIMIT 1),
 '2024-01-17 09:00:00+00', '2024-01-17 17:00:00+00');

-- DoctorProcedures (using subqueries)
INSERT INTO "DoctorProcedures" ("DoctorId", "ProcedureId") VALUES
((SELECT d."Id" FROM "Doctors" d 
  JOIN "People" p ON d."PersonId" = p."Id" 
  WHERE p."FirstName" = 'William' AND p."LastName" = 'Hernandez' LIMIT 1),
 (SELECT "Id" FROM "Procedures" WHERE "Name" = 'ECG' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d 
  JOIN "People" p ON d."PersonId" = p."Id" 
  WHERE p."FirstName" = 'William' AND p."LastName" = 'Hernandez' LIMIT 1),
 (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Ultrasound' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d 
  JOIN "People" p ON d."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Mary' AND p."LastName" = 'Lopez' LIMIT 1),
 (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Ultrasound' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d 
  JOIN "People" p ON d."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Mary' AND p."LastName" = 'Lopez' LIMIT 1),
 (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Biopsy' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d 
  JOIN "People" p ON d."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Richard' AND p."LastName" = 'Wilson' LIMIT 1),
 (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Blood Test' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d 
  JOIN "People" p ON d."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Richard' AND p."LastName" = 'Wilson' LIMIT 1),
 (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Physical Examination' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d 
  JOIN "People" p ON d."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Richard' AND p."LastName" = 'Wilson' LIMIT 1),
 (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Vaccination' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d 
  JOIN "People" p ON d."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Patricia' AND p."LastName" = 'Anderson' LIMIT 1),
 (SELECT "Id" FROM "Procedures" WHERE "Name" = 'X-Ray' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d 
  JOIN "People" p ON d."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Patricia' AND p."LastName" = 'Anderson' LIMIT 1),
 (SELECT "Id" FROM "Procedures" WHERE "Name" = 'MRI Scan' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d 
  JOIN "People" p ON d."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Joseph' AND p."LastName" = 'Thomas' LIMIT 1),
 (SELECT "Id" FROM "Procedures" WHERE "Name" = 'MRI Scan' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d 
  JOIN "People" p ON d."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Joseph' AND p."LastName" = 'Thomas' LIMIT 1),
 (SELECT "Id" FROM "Procedures" WHERE "Name" = 'CT Scan' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d 
  JOIN "People" p ON d."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Jennifer' AND p."LastName" = 'Taylor' LIMIT 1),
 (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Blood Test' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d 
  JOIN "People" p ON d."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Jennifer' AND p."LastName" = 'Taylor' LIMIT 1),
 (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Physical Examination' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d 
  JOIN "People" p ON d."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Jennifer' AND p."LastName" = 'Taylor' LIMIT 1),
 (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Vaccination' LIMIT 1));

-- Schedules (using subqueries for DoctorId and CabinetId)
INSERT INTO "Schedules" ("DoctorId", "CabinetId", "StartTime", "EndTime") VALUES
((SELECT d."Id" FROM "Doctors" d 
  JOIN "People" p ON d."PersonId" = p."Id" 
  WHERE p."FirstName" = 'William' AND p."LastName" = 'Hernandez' LIMIT 1),
 (SELECT "Id" FROM "Cabinets" WHERE "Building" = 1 AND "Floor" = 1 AND "Number" = 101 LIMIT 1),
 '2024-01-15 09:00:00+00', '2024-01-15 17:00:00+00'),
((SELECT d."Id" FROM "Doctors" d 
  JOIN "People" p ON d."PersonId" = p."Id" 
  WHERE p."FirstName" = 'William' AND p."LastName" = 'Hernandez' LIMIT 1),
 (SELECT "Id" FROM "Cabinets" WHERE "Building" = 1 AND "Floor" = 1 AND "Number" = 101 LIMIT 1),
 '2024-01-16 09:00:00+00', '2024-01-16 17:00:00+00'),
((SELECT d."Id" FROM "Doctors" d 
  JOIN "People" p ON d."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Mary' AND p."LastName" = 'Lopez' LIMIT 1),
 (SELECT "Id" FROM "Cabinets" WHERE "Building" = 1 AND "Floor" = 1 AND "Number" = 102 LIMIT 1),
 '2024-01-15 10:00:00+00', '2024-01-15 18:00:00+00'),
((SELECT d."Id" FROM "Doctors" d 
  JOIN "People" p ON d."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Mary' AND p."LastName" = 'Lopez' LIMIT 1),
 (SELECT "Id" FROM "Cabinets" WHERE "Building" = 1 AND "Floor" = 1 AND "Number" = 102 LIMIT 1),
 '2024-01-16 10:00:00+00', '2024-01-16 18:00:00+00'),
((SELECT d."Id" FROM "Doctors" d 
  JOIN "People" p ON d."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Richard' AND p."LastName" = 'Wilson' LIMIT 1),
 (SELECT "Id" FROM "Cabinets" WHERE "Building" = 1 AND "Floor" = 1 AND "Number" = 103 LIMIT 1),
 '2024-01-15 08:00:00+00', '2024-01-15 16:00:00+00'),
((SELECT d."Id" FROM "Doctors" d 
  JOIN "People" p ON d."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Richard' AND p."LastName" = 'Wilson' LIMIT 1),
 (SELECT "Id" FROM "Cabinets" WHERE "Building" = 1 AND "Floor" = 1 AND "Number" = 103 LIMIT 1),
 '2024-01-16 08:00:00+00', '2024-01-16 16:00:00+00'),
((SELECT d."Id" FROM "Doctors" d 
  JOIN "People" p ON d."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Patricia' AND p."LastName" = 'Anderson' LIMIT 1),
 (SELECT "Id" FROM "Cabinets" WHERE "Building" = 1 AND "Floor" = 2 AND "Number" = 201 LIMIT 1),
 '2024-01-15 09:30:00+00', '2024-01-15 17:30:00+00'),
((SELECT d."Id" FROM "Doctors" d 
  JOIN "People" p ON d."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Joseph' AND p."LastName" = 'Thomas' LIMIT 1),
 (SELECT "Id" FROM "Cabinets" WHERE "Building" = 1 AND "Floor" = 2 AND "Number" = 202 LIMIT 1),
 '2024-01-15 10:00:00+00', '2024-01-15 18:00:00+00'),
((SELECT d."Id" FROM "Doctors" d 
  JOIN "People" p ON d."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Jennifer' AND p."LastName" = 'Taylor' LIMIT 1),
 (SELECT "Id" FROM "Cabinets" WHERE "Building" = 1 AND "Floor" = 1 AND "Number" = 101 LIMIT 1),
 '2024-01-15 08:00:00+00', '2024-01-15 16:00:00+00'),
((SELECT d."Id" FROM "Doctors" d 
  JOIN "People" p ON d."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Jennifer' AND p."LastName" = 'Taylor' LIMIT 1),
 (SELECT "Id" FROM "Cabinets" WHERE "Building" = 1 AND "Floor" = 1 AND "Number" = 101 LIMIT 1),
 '2024-01-16 08:00:00+00', '2024-01-16 16:00:00+00');

-- SicknessSymptoms (using subqueries)
INSERT INTO "SicknessSymptoms" ("SicknessId", "SymptomId") VALUES
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Common Cold' LIMIT 1), 
 (SELECT "Id" FROM "Symptoms" WHERE "Name" = 'Fever' LIMIT 1)),
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Common Cold' LIMIT 1), 
 (SELECT "Id" FROM "Symptoms" WHERE "Name" = 'Cough' LIMIT 1)),
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Influenza' LIMIT 1), 
 (SELECT "Id" FROM "Symptoms" WHERE "Name" = 'Fever' LIMIT 1)),
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Influenza' LIMIT 1), 
 (SELECT "Id" FROM "Symptoms" WHERE "Name" = 'Cough' LIMIT 1)),
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Influenza' LIMIT 1), 
 (SELECT "Id" FROM "Symptoms" WHERE "Name" = 'Headache' LIMIT 1)),
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Hypertension' LIMIT 1), 
 (SELECT "Id" FROM "Symptoms" WHERE "Name" = 'Chest Pain' LIMIT 1)),
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Diabetes' LIMIT 1), 
 (SELECT "Id" FROM "Symptoms" WHERE "Name" = 'Fatigue' LIMIT 1)),
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Asthma' LIMIT 1), 
 (SELECT "Id" FROM "Symptoms" WHERE "Name" = 'Shortness of Breath' LIMIT 1)),
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Asthma' LIMIT 1), 
 (SELECT "Id" FROM "Symptoms" WHERE "Name" = 'Cough' LIMIT 1)),
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Arthritis' LIMIT 1), 
 (SELECT "Id" FROM "Symptoms" WHERE "Name" = 'Joint Pain' LIMIT 1)),
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Migraine' LIMIT 1), 
 (SELECT "Id" FROM "Symptoms" WHERE "Name" = 'Headache' LIMIT 1)),
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Migraine' LIMIT 1), 
 (SELECT "Id" FROM "Symptoms" WHERE "Name" = 'Dizziness' LIMIT 1)),
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Pneumonia' LIMIT 1), 
 (SELECT "Id" FROM "Symptoms" WHERE "Name" = 'Fever' LIMIT 1)),
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Pneumonia' LIMIT 1), 
 (SELECT "Id" FROM "Symptoms" WHERE "Name" = 'Cough' LIMIT 1)),
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Pneumonia' LIMIT 1), 
 (SELECT "Id" FROM "Symptoms" WHERE "Name" = 'Shortness of Breath' LIMIT 1)),
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Bronchitis' LIMIT 1), 
 (SELECT "Id" FROM "Symptoms" WHERE "Name" = 'Cough' LIMIT 1)),
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Allergies' LIMIT 1), 
 (SELECT "Id" FROM "Symptoms" WHERE "Name" = 'Cough' LIMIT 1)),
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Allergies' LIMIT 1), 
 (SELECT "Id" FROM "Symptoms" WHERE "Name" = 'Rash' LIMIT 1));

-- SicknessTreatments (using subqueries)
INSERT INTO "SicknessTreatments" ("SicknessId", "TreatmentId") VALUES
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Common Cold' LIMIT 1), 
 (SELECT "Id" FROM "Treatments" WHERE "Name" = 'Rest and Hydration' LIMIT 1)),
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Influenza' LIMIT 1), 
 (SELECT "Id" FROM "Treatments" WHERE "Name" = 'Antibiotics' LIMIT 1)),
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Influenza' LIMIT 1), 
 (SELECT "Id" FROM "Treatments" WHERE "Name" = 'Rest and Hydration' LIMIT 1)),
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Hypertension' LIMIT 1), 
 (SELECT "Id" FROM "Treatments" WHERE "Name" = 'Medication' LIMIT 1)),
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Hypertension' LIMIT 1), 
 (SELECT "Id" FROM "Treatments" WHERE "Name" = 'Monitoring' LIMIT 1)),
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Diabetes' LIMIT 1), 
 (SELECT "Id" FROM "Treatments" WHERE "Name" = 'Medication' LIMIT 1)),
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Diabetes' LIMIT 1), 
 (SELECT "Id" FROM "Treatments" WHERE "Name" = 'Dietary Changes' LIMIT 1)),
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Diabetes' LIMIT 1), 
 (SELECT "Id" FROM "Treatments" WHERE "Name" = 'Exercise Program' LIMIT 1)),
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Asthma' LIMIT 1), 
 (SELECT "Id" FROM "Treatments" WHERE "Name" = 'Medication' LIMIT 1)),
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Arthritis' LIMIT 1), 
 (SELECT "Id" FROM "Treatments" WHERE "Name" = 'Physical Therapy' LIMIT 1)),
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Arthritis' LIMIT 1), 
 (SELECT "Id" FROM "Treatments" WHERE "Name" = 'Medication' LIMIT 1)),
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Migraine' LIMIT 1), 
 (SELECT "Id" FROM "Treatments" WHERE "Name" = 'Pain Relief' LIMIT 1)),
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Pneumonia' LIMIT 1), 
 (SELECT "Id" FROM "Treatments" WHERE "Name" = 'Antibiotics' LIMIT 1)),
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Bronchitis' LIMIT 1), 
 (SELECT "Id" FROM "Treatments" WHERE "Name" = 'Antibiotics' LIMIT 1)),
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Allergies' LIMIT 1), 
 (SELECT "Id" FROM "Treatments" WHERE "Name" = 'Medication' LIMIT 1));

-- SicknessProcedures (using subqueries)
INSERT INTO "SicknessProcedures" ("SicknessId", "ProcedureId") VALUES
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Hypertension' LIMIT 1), 
 (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Blood Test' LIMIT 1)),
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Hypertension' LIMIT 1), 
 (SELECT "Id" FROM "Procedures" WHERE "Name" = 'ECG' LIMIT 1)),
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Diabetes' LIMIT 1), 
 (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Blood Test' LIMIT 1)),
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Asthma' LIMIT 1), 
 (SELECT "Id" FROM "Procedures" WHERE "Name" = 'X-Ray' LIMIT 1)),
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Arthritis' LIMIT 1), 
 (SELECT "Id" FROM "Procedures" WHERE "Name" = 'X-Ray' LIMIT 1)),
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Arthritis' LIMIT 1), 
 (SELECT "Id" FROM "Procedures" WHERE "Name" = 'MRI Scan' LIMIT 1)),
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Migraine' LIMIT 1), 
 (SELECT "Id" FROM "Procedures" WHERE "Name" = 'CT Scan' LIMIT 1)),
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Pneumonia' LIMIT 1), 
 (SELECT "Id" FROM "Procedures" WHERE "Name" = 'X-Ray' LIMIT 1)),
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Pneumonia' LIMIT 1), 
 (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Blood Test' LIMIT 1)),
((SELECT "Id" FROM "Sicknesses" WHERE "Name" = 'Bronchitis' LIMIT 1), 
 (SELECT "Id" FROM "Procedures" WHERE "Name" = 'X-Ray' LIMIT 1));

-- ============================================
-- FOURTH WAVE: Dependent on third wave
-- ============================================

-- Appointments (using subqueries for PatientId, DoctorProcedureId, CabinetId)
INSERT INTO "Appointments" ("PatientId", "DoctorProcedureId", "CabinetId", "StartTime", "EndTime", "DidItHappen") VALUES
((SELECT pt."Id" FROM "Patients" pt 
  JOIN "People" p ON pt."PersonId" = p."Id" 
  WHERE p."FirstName" = 'John' AND p."LastName" = 'Smith' LIMIT 1),
 (SELECT dp."Id" FROM "DoctorProcedures" dp 
  JOIN "Doctors" d ON dp."DoctorId" = d."Id" 
  JOIN "People" p ON d."PersonId" = p."Id" 
  JOIN "Procedures" pr ON dp."ProcedureId" = pr."Id" 
  WHERE p."FirstName" = 'William' AND p."LastName" = 'Hernandez' AND pr."Name" = 'ECG' LIMIT 1),
 (SELECT "Id" FROM "Cabinets" WHERE "Building" = 1 AND "Floor" = 1 AND "Number" = 101 LIMIT 1),
 '2024-01-15 10:00:00+00', '2024-01-15 10:30:00+00', true),
((SELECT pt."Id" FROM "Patients" pt 
  JOIN "People" p ON pt."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Sarah' AND p."LastName" = 'Johnson' LIMIT 1),
 (SELECT dp."Id" FROM "DoctorProcedures" dp 
  JOIN "Doctors" d ON dp."DoctorId" = d."Id" 
  JOIN "People" p ON d."PersonId" = p."Id" 
  JOIN "Procedures" pr ON dp."ProcedureId" = pr."Id" 
  WHERE p."FirstName" = 'Mary' AND p."LastName" = 'Lopez' AND pr."Name" = 'Ultrasound' LIMIT 1),
 (SELECT "Id" FROM "Cabinets" WHERE "Building" = 1 AND "Floor" = 1 AND "Number" = 102 LIMIT 1),
 '2024-01-15 11:00:00+00', '2024-01-15 11:30:00+00', true),
((SELECT pt."Id" FROM "Patients" pt 
  JOIN "People" p ON pt."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Michael' AND p."LastName" = 'Williams' LIMIT 1),
 (SELECT dp."Id" FROM "DoctorProcedures" dp 
  JOIN "Doctors" d ON dp."DoctorId" = d."Id" 
  JOIN "People" p ON d."PersonId" = p."Id" 
  JOIN "Procedures" pr ON dp."ProcedureId" = pr."Id" 
  WHERE p."FirstName" = 'Richard' AND p."LastName" = 'Wilson' AND pr."Name" = 'Blood Test' LIMIT 1),
 (SELECT "Id" FROM "Cabinets" WHERE "Building" = 1 AND "Floor" = 1 AND "Number" = 103 LIMIT 1),
 '2024-01-15 14:00:00+00', '2024-01-15 14:30:00+00', false),
((SELECT pt."Id" FROM "Patients" pt 
  JOIN "People" p ON pt."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Emily' AND p."LastName" = 'Brown' LIMIT 1),
 (SELECT dp."Id" FROM "DoctorProcedures" dp 
  JOIN "Doctors" d ON dp."DoctorId" = d."Id" 
  JOIN "People" p ON d."PersonId" = p."Id" 
  JOIN "Procedures" pr ON dp."ProcedureId" = pr."Id" 
  WHERE p."FirstName" = 'Patricia' AND p."LastName" = 'Anderson' AND pr."Name" = 'X-Ray' LIMIT 1),
 (SELECT "Id" FROM "Cabinets" WHERE "Building" = 1 AND "Floor" = 2 AND "Number" = 201 LIMIT 1),
 '2024-01-16 10:00:00+00', '2024-01-16 10:30:00+00', true),
((SELECT pt."Id" FROM "Patients" pt 
  JOIN "People" p ON pt."PersonId" = p."Id" 
  WHERE p."FirstName" = 'David' AND p."LastName" = 'Jones' LIMIT 1),
 (SELECT dp."Id" FROM "DoctorProcedures" dp 
  JOIN "Doctors" d ON dp."DoctorId" = d."Id" 
  JOIN "People" p ON d."PersonId" = p."Id" 
  JOIN "Procedures" pr ON dp."ProcedureId" = pr."Id" 
  WHERE p."FirstName" = 'Joseph' AND p."LastName" = 'Thomas' AND pr."Name" = 'MRI Scan' LIMIT 1),
 (SELECT "Id" FROM "Cabinets" WHERE "Building" = 1 AND "Floor" = 2 AND "Number" = 202 LIMIT 1),
 '2024-01-16 11:00:00+00', '2024-01-16 11:30:00+00', true),
((SELECT pt."Id" FROM "Patients" pt 
  JOIN "People" p ON pt."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Jessica' AND p."LastName" = 'Garcia' LIMIT 1),
 (SELECT dp."Id" FROM "DoctorProcedures" dp 
  JOIN "Doctors" d ON dp."DoctorId" = d."Id" 
  JOIN "People" p ON d."PersonId" = p."Id" 
  JOIN "Procedures" pr ON dp."ProcedureId" = pr."Id" 
  WHERE p."FirstName" = 'Jennifer' AND p."LastName" = 'Taylor' AND pr."Name" = 'Blood Test' LIMIT 1),
 (SELECT "Id" FROM "Cabinets" WHERE "Building" = 1 AND "Floor" = 1 AND "Number" = 101 LIMIT 1),
 '2024-01-16 14:00:00+00', '2024-01-16 14:30:00+00', false),
((SELECT pt."Id" FROM "Patients" pt 
  JOIN "People" p ON pt."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Robert' AND p."LastName" = 'Miller' LIMIT 1),
 (SELECT dp."Id" FROM "DoctorProcedures" dp 
  JOIN "Doctors" d ON dp."DoctorId" = d."Id" 
  JOIN "People" p ON d."PersonId" = p."Id" 
  JOIN "Procedures" pr ON dp."ProcedureId" = pr."Id" 
  WHERE p."FirstName" = 'Richard' AND p."LastName" = 'Wilson' AND pr."Name" = 'Physical Examination' LIMIT 1),
 (SELECT "Id" FROM "Cabinets" WHERE "Building" = 1 AND "Floor" = 1 AND "Number" = 102 LIMIT 1),
 '2024-01-16 15:00:00+00', '2024-01-16 15:30:00+00', true),
((SELECT pt."Id" FROM "Patients" pt 
  JOIN "People" p ON pt."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Amanda' AND p."LastName" = 'Davis' LIMIT 1),
 (SELECT dp."Id" FROM "DoctorProcedures" dp 
  JOIN "Doctors" d ON dp."DoctorId" = d."Id" 
  JOIN "People" p ON d."PersonId" = p."Id" 
  JOIN "Procedures" pr ON dp."ProcedureId" = pr."Id" 
  WHERE p."FirstName" = 'Patricia' AND p."LastName" = 'Anderson' AND pr."Name" = 'MRI Scan' LIMIT 1),
 (SELECT "Id" FROM "Cabinets" WHERE "Building" = 1 AND "Floor" = 1 AND "Number" = 103 LIMIT 1),
 '2024-01-17 09:00:00+00', '2024-01-17 09:30:00+00', true),
((SELECT pt."Id" FROM "Patients" pt 
  JOIN "People" p ON pt."PersonId" = p."Id" 
  WHERE p."FirstName" = 'James' AND p."LastName" = 'Rodriguez' LIMIT 1),
 (SELECT dp."Id" FROM "DoctorProcedures" dp 
  JOIN "Doctors" d ON dp."DoctorId" = d."Id" 
  JOIN "People" p ON d."PersonId" = p."Id" 
  JOIN "Procedures" pr ON dp."ProcedureId" = pr."Id" 
  WHERE p."FirstName" = 'Joseph' AND p."LastName" = 'Thomas' AND pr."Name" = 'CT Scan' LIMIT 1),
 (SELECT "Id" FROM "Cabinets" WHERE "Building" = 1 AND "Floor" = 2 AND "Number" = 201 LIMIT 1),
 '2024-01-17 10:00:00+00', '2024-01-17 10:30:00+00', false),
((SELECT pt."Id" FROM "Patients" pt 
  JOIN "People" p ON pt."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Lisa' AND p."LastName" = 'Martinez' LIMIT 1),
 (SELECT dp."Id" FROM "DoctorProcedures" dp 
  JOIN "Doctors" d ON dp."DoctorId" = d."Id" 
  JOIN "People" p ON d."PersonId" = p."Id" 
  JOIN "Procedures" pr ON dp."ProcedureId" = pr."Id" 
  WHERE p."FirstName" = 'Jennifer' AND p."LastName" = 'Taylor' AND pr."Name" = 'Physical Examination' LIMIT 1),
 (SELECT "Id" FROM "Cabinets" WHERE "Building" = 1 AND "Floor" = 2 AND "Number" = 202 LIMIT 1),
 '2024-01-17 11:00:00+00', '2024-01-17 11:30:00+00', true);

-- HomeCallLogs (using subqueries)
INSERT INTO "HomeCallLogs" ("DoctorId", "AddressId", "DateTime") VALUES
((SELECT d."Id" FROM "Doctors" d 
  JOIN "People" p ON d."PersonId" = p."Id" 
  WHERE p."FirstName" = 'William' AND p."LastName" = 'Hernandez' LIMIT 1),
 (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Los Angeles' LIMIT 1),
 '2024-01-15 08:00:00+00'),
((SELECT d."Id" FROM "Doctors" d 
  JOIN "People" p ON d."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Mary' AND p."LastName" = 'Lopez' LIMIT 1),
 (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'New York City' LIMIT 1),
 '2024-01-15 12:00:00+00'),
((SELECT d."Id" FROM "Doctors" d 
  JOIN "People" p ON d."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Richard' AND p."LastName" = 'Wilson' LIMIT 1),
 (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Houston' LIMIT 1),
 '2024-01-16 08:00:00+00'),
((SELECT d."Id" FROM "Doctors" d 
  JOIN "People" p ON d."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Patricia' AND p."LastName" = 'Anderson' LIMIT 1),
 (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Miami' LIMIT 1),
 '2024-01-16 14:00:00+00'),
((SELECT d."Id" FROM "Doctors" d 
  JOIN "People" p ON d."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Joseph' AND p."LastName" = 'Thomas' LIMIT 1),
 (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Chicago' LIMIT 1),
 '2024-01-17 08:00:00+00');

-- ============================================
-- FIFTH WAVE: Dependent on fourth wave
-- ============================================

-- Diagnoses (using subqueries to find Appointments)
INSERT INTO "Diagnoses" ("AppointmentId", "Prescription") VALUES
((SELECT a."Id" FROM "Appointments" a 
  JOIN "Patients" pt ON a."PatientId" = pt."Id" 
  JOIN "People" p ON pt."PersonId" = p."Id" 
  WHERE p."FirstName" = 'John' AND p."LastName" = 'Smith' 
  AND a."StartTime" = '2024-01-15 10:00:00+00' LIMIT 1),
 'Monitor blood pressure daily. Take medication as prescribed.'),
((SELECT a."Id" FROM "Appointments" a 
  JOIN "Patients" pt ON a."PatientId" = pt."Id" 
  JOIN "People" p ON pt."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Sarah' AND p."LastName" = 'Johnson' 
  AND a."StartTime" = '2024-01-15 11:00:00+00' LIMIT 1),
 'Rest, fluids, and over-the-counter medication for symptoms.'),
((SELECT a."Id" FROM "Appointments" a 
  JOIN "Patients" pt ON a."PatientId" = pt."Id" 
  JOIN "People" p ON pt."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Emily' AND p."LastName" = 'Brown' 
  AND a."StartTime" = '2024-01-16 10:00:00+00' LIMIT 1),
 'Monitor blood sugar levels. Follow dietary guidelines.'),
((SELECT a."Id" FROM "Appointments" a 
  JOIN "Patients" pt ON a."PatientId" = pt."Id" 
  JOIN "People" p ON pt."PersonId" = p."Id" 
  WHERE p."FirstName" = 'David' AND p."LastName" = 'Jones' 
  AND a."StartTime" = '2024-01-16 11:00:00+00' LIMIT 1),
 'Use inhaler as needed. Avoid triggers.'),
((SELECT a."Id" FROM "Appointments" a 
  JOIN "Patients" pt ON a."PatientId" = pt."Id" 
  JOIN "People" p ON pt."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Robert' AND p."LastName" = 'Miller' 
  AND a."StartTime" = '2024-01-16 15:00:00+00' LIMIT 1),
 'Rest and hydration. Over-the-counter cold medication.'),
((SELECT a."Id" FROM "Appointments" a 
  JOIN "Patients" pt ON a."PatientId" = pt."Id" 
  JOIN "People" p ON pt."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Amanda' AND p."LastName" = 'Davis' 
  AND a."StartTime" = '2024-01-17 09:00:00+00' LIMIT 1),
 'Physical therapy recommended. Pain management medication.'),
((SELECT a."Id" FROM "Appointments" a 
  JOIN "Patients" pt ON a."PatientId" = pt."Id" 
  JOIN "People" p ON pt."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Lisa' AND p."LastName" = 'Martinez' 
  AND a."StartTime" = '2024-01-17 11:00:00+00' LIMIT 1),
 'Prescribed migraine medication. Rest in dark room when needed.');
