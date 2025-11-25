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
(10, 'Endoscopy', 'Upper GI examination', 900.00),
(11, 'Fluorography', 'Chest X-ray screening for tuberculosis and lung diseases', 150.00),
(12, 'Vaccination', 'Immunization procedure', 50.00);

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
(5, 2, 1, 101, 5),
(6, 2, 2, 201, 7),
(7, 2, 2, 202, 7),
(8, 1, 3, 301, 7);

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
(11, 5, 4),
(12, 1, 11),
(13, 2, 11),
(14, 3, 11),
(15, 6, 1),
(16, 6, 2),
(17, 6, 11),
(18, 7, 1),
(19, 7, 3),
(20, 8, 1),
(21, 8, 12),
(22, 9, 1),
(23, 10, 1);

-- Generate schedules for all doctors for 30 days forward (weekdays only, Monday-Friday)
-- Each doctor works 8:00-12:00 (morning) and 13:00-17:00 (afternoon) shifts
DO $$
DECLARE
    schedule_id INTEGER := 1;
    doctor_rec RECORD;
    day_offset INTEGER;
    current_date_val DATE;
    schedule_date DATE;
    cabinet_id INTEGER;
BEGIN
    current_date_val := CURRENT_DATE;
    
    -- Loop through each doctor
    FOR doctor_rec IN SELECT "Id" FROM "Doctors" ORDER BY "Id"
    LOOP
        -- Assign cabinet in round-robin fashion (cabinets 1-5)
        cabinet_id := ((doctor_rec."Id" - 1) % 5) + 1;
        
        -- Loop through next 30 days
        FOR day_offset IN 0..29
        LOOP
            schedule_date := current_date_val + (day_offset * INTERVAL '1 day');
            
            -- Only create schedules for weekdays (Monday=1, Friday=5)
            IF EXTRACT(DOW FROM schedule_date) BETWEEN 1 AND 5 THEN
                -- Morning shift: 08:00-12:00
                INSERT INTO "Schedules" ("Id", "DoctorId", "CabinetId", "StartTime", "EndTime")
                VALUES (
                    schedule_id,
                    doctor_rec."Id",
                    cabinet_id,
                    (schedule_date::timestamp + INTERVAL '8 hours') AT TIME ZONE 'UTC',
                    (schedule_date::timestamp + INTERVAL '12 hours') AT TIME ZONE 'UTC'
                );
                schedule_id := schedule_id + 1;
                
                -- Afternoon shift: 13:00-17:00
                INSERT INTO "Schedules" ("Id", "DoctorId", "CabinetId", "StartTime", "EndTime")
                VALUES (
                    schedule_id,
                    doctor_rec."Id",
                    cabinet_id,
                    (schedule_date::timestamp + INTERVAL '13 hours') AT TIME ZONE 'UTC',
                    (schedule_date::timestamp + INTERVAL '17 hours') AT TIME ZONE 'UTC'
                );
                schedule_id := schedule_id + 1;
            END IF;
        END LOOP;
    END LOOP;
    
    -- Generate additional schedules for Physical Therapy rooms (cabinets 6, 7, 8)
    -- Assign doctors 4, 5, and 6 to physical therapy rooms
    FOR doctor_rec IN SELECT "Id" FROM "Doctors" WHERE "Id" IN (4, 5, 6) ORDER BY "Id"
    LOOP
        -- Assign physical therapy cabinets (6, 7, 8) in round-robin
        cabinet_id := 6 + ((doctor_rec."Id" - 4) % 3);
        
        -- Loop through next 30 days
        FOR day_offset IN 0..29
        LOOP
            schedule_date := current_date_val + (day_offset * INTERVAL '1 day');
            
            -- Only create schedules for weekdays (Monday=1, Friday=5)
            IF EXTRACT(DOW FROM schedule_date) BETWEEN 1 AND 5 THEN
                -- Morning shift: 08:00-12:00
                INSERT INTO "Schedules" ("Id", "DoctorId", "CabinetId", "StartTime", "EndTime")
                VALUES (
                    schedule_id,
                    doctor_rec."Id",
                    cabinet_id,
                    (schedule_date::timestamp + INTERVAL '8 hours') AT TIME ZONE 'UTC',
                    (schedule_date::timestamp + INTERVAL '12 hours') AT TIME ZONE 'UTC'
                );
                schedule_id := schedule_id + 1;
                
                -- Afternoon shift: 13:00-17:00
                INSERT INTO "Schedules" ("Id", "DoctorId", "CabinetId", "StartTime", "EndTime")
                VALUES (
                    schedule_id,
                    doctor_rec."Id",
                    cabinet_id,
                    (schedule_date::timestamp + INTERVAL '13 hours') AT TIME ZONE 'UTC',
                    (schedule_date::timestamp + INTERVAL '17 hours') AT TIME ZONE 'UTC'
                );
                schedule_id := schedule_id + 1;
            END IF;
        END LOOP;
    END LOOP;
END $$;

-- FOURTH WAVE: Dependent on third wave
-- Historical appointments
INSERT INTO "Appointments" ("Id", "DoctorProcedureId", "CabinetId", "PatientId", "StartTime", "EndTime", "DidItHappen") VALUES
(1, 1, 1, 1, '2024-01-15 08:00:00+00', '2024-01-15 08:30:00+00', true),
(2, 2, 1, 2, '2024-01-15 08:30:00+00', '2024-01-15 09:00:00+00', true),
(3, 4, 2, 3, '2024-01-16 08:00:00+00', '2024-01-16 08:30:00+00', true),
(4, 6, 3, 4, '2024-01-17 09:00:00+00', '2024-01-17 09:30:00+00', false),
(5, 8, 4, 5, '2024-01-18 10:00:00+00', '2024-01-18 10:30:00+00', true);

-- Generate recent appointments (last 7 days and current month) for various queries
DO $$
DECLARE
    appointment_id INTEGER := 6;
    current_date_val DATE;
    appointment_date DATE;
    day_offset INTEGER;
    hour_offset INTEGER;
BEGIN
    current_date_val := CURRENT_DATE;
    
    -- Add appointments for last 7 days (for patients-per-week, procedures-total queries)
    FOR day_offset IN 0..6
    LOOP
        appointment_date := current_date_val - (day_offset * INTERVAL '1 day');
        
        -- Only weekdays
        IF EXTRACT(DOW FROM appointment_date) BETWEEN 1 AND 5 THEN
            -- Patient 1 sees multiple doctors (doctor 1, 2, 3) - for patients-multiple-doctors query
            IF day_offset <= 2 THEN
                -- Day 0: Doctor 1
                INSERT INTO "Appointments" ("Id", "DoctorProcedureId", "CabinetId", "PatientId", "StartTime", "EndTime", "DidItHappen")
                VALUES (
                    appointment_id,
                    1,
                    1,
                    1,
                    (appointment_date::timestamp + INTERVAL '9 hours') AT TIME ZONE 'UTC',
                    (appointment_date::timestamp + INTERVAL '9 hours 30 minutes') AT TIME ZONE 'UTC',
                    true
                );
                appointment_id := appointment_id + 1;
                
                -- Day 1: Doctor 2 (same patient)
                IF day_offset = 1 THEN
                    INSERT INTO "Appointments" ("Id", "DoctorProcedureId", "CabinetId", "PatientId", "StartTime", "EndTime", "DidItHappen")
                    VALUES (
                        appointment_id,
                        4,
                        2,
                        1,
                        (appointment_date::timestamp + INTERVAL '10 hours') AT TIME ZONE 'UTC',
                        (appointment_date::timestamp + INTERVAL '10 hours 30 minutes') AT TIME ZONE 'UTC',
                        true
                    );
                    appointment_id := appointment_id + 1;
                END IF;
                
                -- Day 2: Doctor 3 (same patient - now 3 doctors)
                IF day_offset = 2 THEN
                    INSERT INTO "Appointments" ("Id", "DoctorProcedureId", "CabinetId", "PatientId", "StartTime", "EndTime", "DidItHappen")
                    VALUES (
                        appointment_id,
                        6,
                        1,
                        1,
                        (appointment_date::timestamp + INTERVAL '11 hours') AT TIME ZONE 'UTC',
                        (appointment_date::timestamp + INTERVAL '11 hours 30 minutes') AT TIME ZONE 'UTC',
                        true
                    );
                    appointment_id := appointment_id + 1;
                END IF;
            END IF;
            
            -- Add more appointments for other patients and procedures
            -- Patient 2 with doctor 1
            INSERT INTO "Appointments" ("Id", "DoctorProcedureId", "CabinetId", "PatientId", "StartTime", "EndTime", "DidItHappen")
            VALUES (
                appointment_id,
                12,
                1,
                2,
                (appointment_date::timestamp + INTERVAL '14 hours') AT TIME ZONE 'UTC',
                (appointment_date::timestamp + INTERVAL '14 hours 30 minutes') AT TIME ZONE 'UTC',
                true
            );
            appointment_id := appointment_id + 1;
            
            -- Patient 3 with doctor 2
            INSERT INTO "Appointments" ("Id", "DoctorProcedureId", "CabinetId", "PatientId", "StartTime", "EndTime", "DidItHappen")
            VALUES (
                appointment_id,
                13,
                2,
                3,
                (appointment_date::timestamp + INTERVAL '15 hours') AT TIME ZONE 'UTC',
                (appointment_date::timestamp + INTERVAL '15 hours 30 minutes') AT TIME ZONE 'UTC',
                true
            );
            appointment_id := appointment_id + 1;
            
            -- Patient 4 with doctor 3 (fluorography) - only on first day for specific date query
            IF day_offset = 0 THEN
                INSERT INTO "Appointments" ("Id", "DoctorProcedureId", "CabinetId", "PatientId", "StartTime", "EndTime", "DidItHappen")
                VALUES (
                    appointment_id,
                    14,
                    1,
                    4,
                    (appointment_date::timestamp + INTERVAL '16 hours') AT TIME ZONE 'UTC',
                    (appointment_date::timestamp + INTERVAL '16 hours 30 minutes') AT TIME ZONE 'UTC',
                    true
                );
                appointment_id := appointment_id + 1;
            END IF;
        END IF;
    END LOOP;
    
    -- Add appointments for current month (for visit-stats queries)
    -- Add appointments throughout the month
    FOR day_offset IN 0..29
    LOOP
        appointment_date := DATE_TRUNC('month', CURRENT_DATE)::date + (day_offset * INTERVAL '1 day');
        
        -- Only weekdays and only if in current month
        IF EXTRACT(DOW FROM appointment_date) BETWEEN 1 AND 5 
           AND appointment_date >= DATE_TRUNC('month', CURRENT_DATE)::date
           AND appointment_date < (DATE_TRUNC('month', CURRENT_DATE) + INTERVAL '1 month')::date THEN
            
            -- Add appointments for various doctors and specialties
            -- Doctor 1 (Cardiology)
            INSERT INTO "Appointments" ("Id", "DoctorProcedureId", "CabinetId", "PatientId", "StartTime", "EndTime", "DidItHappen")
            VALUES (
                appointment_id,
                1,
                1,
                1,
                (appointment_date::timestamp + INTERVAL '9 hours') AT TIME ZONE 'UTC',
                (appointment_date::timestamp + INTERVAL '9 hours 30 minutes') AT TIME ZONE 'UTC',
                true
            );
            appointment_id := appointment_id + 1;
            
            -- Doctor 2 (Dermatology)
            INSERT INTO "Appointments" ("Id", "DoctorProcedureId", "CabinetId", "PatientId", "StartTime", "EndTime", "DidItHappen")
            VALUES (
                appointment_id,
                4,
                2,
                2,
                (appointment_date::timestamp + INTERVAL '10 hours') AT TIME ZONE 'UTC',
                (appointment_date::timestamp + INTERVAL '10 hours 30 minutes') AT TIME ZONE 'UTC',
                true
            );
            appointment_id := appointment_id + 1;
            
            -- Doctor 3 (Pediatrics)
            INSERT INTO "Appointments" ("Id", "DoctorProcedureId", "CabinetId", "PatientId", "StartTime", "EndTime", "DidItHappen")
            VALUES (
                appointment_id,
                6,
                1,
                3,
                (appointment_date::timestamp + INTERVAL '11 hours') AT TIME ZONE 'UTC',
                (appointment_date::timestamp + INTERVAL '11 hours 30 minutes') AT TIME ZONE 'UTC',
                true
            );
            appointment_id := appointment_id + 1;
        END IF;
    END LOOP;
END $$;

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

-- Generate additional diagnoses for recent appointments, including angina case
DO $$
DECLARE
    diagnosis_id INTEGER := 5;
    appointment_rec RECORD;
    current_month_start DATE;
    current_month_end DATE;
BEGIN
    current_month_start := DATE_TRUNC('month', CURRENT_DATE)::date;
    current_month_end := (DATE_TRUNC('month', CURRENT_DATE) + INTERVAL '1 month')::date;
    
    -- Add diagnoses for recent appointments (last 7 days)
    FOR appointment_rec IN 
        SELECT "Id" FROM "Appointments" 
        WHERE "StartTime" >= CURRENT_DATE - INTERVAL '7 days'
        AND "DidItHappen" = true
        AND "Id" NOT IN (SELECT "AppointmentId" FROM "Diagnoses")
        ORDER BY "Id"
    LOOP
        INSERT INTO "Diagnoses" ("Id", "AppointmentId", "Prescription")
        VALUES (
            diagnosis_id,
            appointment_rec."Id",
            'Follow-up required. Continue current treatment plan.'
        );
        diagnosis_id := diagnosis_id + 1;
    END LOOP;
    
    -- Add angina diagnosis for current month (for angina-count query)
    FOR appointment_rec IN 
        SELECT "Id" FROM "Appointments" 
        WHERE "StartTime" >= current_month_start
        AND "StartTime" < current_month_end
        AND "DidItHappen" = true
        AND "Id" NOT IN (SELECT "AppointmentId" FROM "Diagnoses")
        ORDER BY "Id"
        LIMIT 1
    LOOP
        INSERT INTO "Diagnoses" ("Id", "AppointmentId", "Prescription")
        VALUES (
            diagnosis_id,
            appointment_rec."Id",
            'Angina detected. Prescribed nitroglycerin for chest pain relief. Monitor symptoms closely.'
        );
        diagnosis_id := diagnosis_id + 1;
    END LOOP;
    
    -- Add more diagnoses for current month appointments
    FOR appointment_rec IN 
        SELECT "Id" FROM "Appointments" 
        WHERE "StartTime" >= current_month_start
        AND "StartTime" < current_month_end
        AND "DidItHappen" = true
        AND "Id" NOT IN (SELECT "AppointmentId" FROM "Diagnoses")
        ORDER BY "Id"
        LIMIT 10
    LOOP
        INSERT INTO "Diagnoses" ("Id", "AppointmentId", "Prescription")
        VALUES (
            diagnosis_id,
            appointment_rec."Id",
            'Routine checkup completed. Patient in good health.'
        );
        diagnosis_id := diagnosis_id + 1;
    END LOOP;
END $$;
