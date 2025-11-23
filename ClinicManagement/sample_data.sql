-- Sample Data for Clinic Management Database
-- Execute this script to populate your database with test data
-- NOTE: Auth tables (Users, Roles, PromotionRequests, PasswordChangeRequests) are NOT touched

-- ============================================
-- CLEANUP: Delete all data except Roles and Users
-- ============================================
-- Delete in reverse dependency order to avoid foreign key violations

-- Fifth wave dependencies
DELETE FROM "Diagnoses";
DELETE FROM "Appointments";
DELETE FROM "HomeCallLogs";

-- Fourth wave dependencies
DELETE FROM "Schedules";
DELETE FROM "DoctorProcedures";
DELETE FROM "DoctorOnCallStatuses";
DELETE FROM "DistrictDoctors";
DELETE FROM "SicknessProcedures";
DELETE FROM "SicknessSymptoms";
DELETE FROM "SicknessTreatments";

-- Third wave dependencies
DELETE FROM "Patients";
DELETE FROM "Doctors";
DELETE FROM "Cabinets";

-- Second wave dependencies (base tables)
DELETE FROM "People";
DELETE FROM "Procedures";
DELETE FROM "Treatments";
DELETE FROM "Sicknesses";
DELETE FROM "Symptoms";
DELETE FROM "Specialties";
DELETE FROM "CabinetTypes";
DELETE FROM "Addresses";

-- ============================================
-- FIRST WAVE: Base tables (no dependencies)
-- ============================================

-- Addresses (50 addresses across multiple states and cities)
INSERT INTO "Addresses" ("Country", "State", "Locality", "StreetName", "StreetNumber") VALUES
('USA', 'California', 'Los Angeles', 'Main Street', 123), ('USA', 'California', 'Los Angeles', 'Sunset Boulevard', 456),
('USA', 'California', 'Los Angeles', 'Hollywood Boulevard', 789), ('USA', 'California', 'Los Angeles', 'Rodeo Drive', 321),
('USA', 'California', 'Los Angeles', 'Wilshire Boulevard', 654), ('USA', 'California', 'San Francisco', 'Market Street', 100),
('USA', 'California', 'San Francisco', 'Mission Street', 200), ('USA', 'California', 'San Diego', 'Ocean Drive', 300),
('USA', 'New York', 'New York City', 'Broadway', 456), ('USA', 'New York', 'New York City', 'Fifth Avenue', 789),
('USA', 'New York', 'New York City', 'Park Avenue', 321), ('USA', 'New York', 'New York City', 'Wall Street', 654),
('USA', 'New York', 'New York City', 'Lexington Avenue', 100), ('USA', 'New York', 'Buffalo', 'Main Street', 200),
('USA', 'Texas', 'Houston', 'Oak Avenue', 789), ('USA', 'Texas', 'Houston', 'Main Street', 123),
('USA', 'Texas', 'Houston', 'Richmond Avenue', 456), ('USA', 'Texas', 'Dallas', 'Commerce Street', 300),
('USA', 'Texas', 'Austin', 'Congress Avenue', 400), ('USA', 'Texas', 'San Antonio', 'Alamo Street', 500),
('USA', 'Florida', 'Miami', 'Beach Road', 321), ('USA', 'Florida', 'Miami', 'Ocean Drive', 654),
('USA', 'Florida', 'Miami', 'Collins Avenue', 123), ('USA', 'Florida', 'Orlando', 'International Drive', 200),
('USA', 'Florida', 'Tampa', 'Bayshore Boulevard', 300), ('USA', 'Illinois', 'Chicago', 'Michigan Avenue', 654),
('USA', 'Illinois', 'Chicago', 'State Street', 123), ('USA', 'Illinois', 'Chicago', 'Wacker Drive', 456),
('USA', 'Illinois', 'Chicago', 'Lake Shore Drive', 789), ('USA', 'Illinois', 'Springfield', 'Monroe Street', 100),
('USA', 'Pennsylvania', 'Philadelphia', 'Market Street', 200), ('USA', 'Pennsylvania', 'Pittsburgh', 'Forbes Avenue', 300),
('USA', 'Arizona', 'Phoenix', 'Central Avenue', 400), ('USA', 'Arizona', 'Tucson', 'Speedway Boulevard', 500),
('USA', 'Washington', 'Seattle', 'Pike Street', 600), ('USA', 'Washington', 'Spokane', 'Riverside Avenue', 700),
('USA', 'Massachusetts', 'Boston', 'Beacon Street', 800), ('USA', 'Massachusetts', 'Cambridge', 'Massachusetts Avenue', 900),
('USA', 'Georgia', 'Atlanta', 'Peachtree Street', 1000), ('USA', 'Georgia', 'Savannah', 'Bull Street', 1100),
('USA', 'Michigan', 'Detroit', 'Woodward Avenue', 1200), ('USA', 'Michigan', 'Grand Rapids', 'Division Avenue', 1300),
('USA', 'Ohio', 'Columbus', 'High Street', 1400), ('USA', 'Ohio', 'Cleveland', 'Euclid Avenue', 1500),
('USA', 'North Carolina', 'Charlotte', 'Tryon Street', 1600), ('USA', 'North Carolina', 'Raleigh', 'Fayetteville Street', 1700),
('USA', 'Colorado', 'Denver', 'Colfax Avenue', 1800), ('USA', 'Colorado', 'Boulder', 'Pearl Street', 1900),
('USA', 'Nevada', 'Las Vegas', 'Strip Boulevard', 2000), ('USA', 'Nevada', 'Reno', 'Virginia Street', 2100),
('USA', 'Tennessee', 'Nashville', 'Broadway', 2200), ('USA', 'Tennessee', 'Memphis', 'Beale Street', 2300),
('USA', 'Missouri', 'St. Louis', 'Market Street', 2400), ('USA', 'Missouri', 'Kansas City', 'Main Street', 2500);

-- CabinetTypes (10 types)
INSERT INTO "CabinetTypes" ("Type") VALUES
('Examination'), ('Consultation'), ('Surgery'), ('Emergency'), ('Laboratory'),
('Radiology'), ('Physical Therapy'), ('Cardiology'), ('Dermatology'), ('Pediatrics');

-- Specialties (25 specialties)
INSERT INTO "Specialties" ("Name") VALUES
('Cardiology'), ('Dermatology'), ('Pediatrics'), ('Orthopedics'), ('Neurology'),
('General Practice'), ('Internal Medicine'), ('Oncology'), ('Psychiatry'), ('Radiology'),
('Anesthesiology'), ('Emergency Medicine'), ('Family Medicine'), ('Gastroenterology'), ('Geriatrics'),
('Gynecology'), ('Hematology'), ('Infectious Disease'), ('Nephrology'), ('Obstetrics'),
('Ophthalmology'), ('Pathology'), ('Pulmonology'), ('Rheumatology'), ('Urology');

-- Symptoms (30 symptoms)
INSERT INTO "Symptoms" ("Name") VALUES
('Fever'), ('Cough'), ('Headache'), ('Nausea'), ('Fatigue'),
('Chest Pain'), ('Shortness of Breath'), ('Joint Pain'), ('Rash'), ('Dizziness'),
('Sore Throat'), ('Runny Nose'), ('Body Aches'), ('Chills'), ('Sweating'),
('Loss of Appetite'), ('Insomnia'), ('Confusion'), ('Blurred Vision'), ('Ear Pain'),
('Abdominal Pain'), ('Diarrhea'), ('Constipation'), ('Vomiting'), ('Back Pain'),
('Muscle Weakness'), ('Tremors'), ('Seizures'), ('Memory Loss'), ('Mood Changes');

-- Sicknesses (35 sicknesses)
INSERT INTO "Sicknesses" ("Name") VALUES
('Common Cold'), ('Influenza'), ('Hypertension'), ('Diabetes'), ('Asthma'),
('Arthritis'), ('Migraine'), ('Pneumonia'), ('Bronchitis'), ('Allergies'),
('COVID-19'), ('Sinusitis'), ('Strep Throat'), ('Urinary Tract Infection'), ('Gastroenteritis'),
('High Cholesterol'), ('Heart Disease'), ('Stroke'), ('Chronic Obstructive Pulmonary Disease'), ('Osteoporosis'),
('Depression'), ('Anxiety'), ('Sleep Apnea'), ('Chronic Fatigue Syndrome'), ('Fibromyalgia'),
('Hypothyroidism'), ('Hyperthyroidism'), ('Epilepsy'), ('Alzheimer''s Disease'), ('Parkinson''s Disease'),
('Hepatitis'), ('Kidney Disease'), ('Liver Disease'), ('Anemia'), ('Cancer');

-- Treatments (25 treatments)
INSERT INTO "Treatments" ("Name") VALUES
('Antibiotics'), ('Pain Relief'), ('Rest and Hydration'), ('Physical Therapy'), ('Medication'),
('Surgery'), ('Therapy'), ('Dietary Changes'), ('Exercise Program'), ('Monitoring'),
('Chemotherapy'), ('Radiation Therapy'), ('Immunotherapy'), ('Counseling'), ('Behavioral Therapy'),
('Occupational Therapy'), ('Speech Therapy'), ('Massage Therapy'), ('Acupuncture'), ('Chiropractic Care'),
('Blood Transfusion'), ('Dialysis'), ('Respiratory Therapy'), ('Psychotherapy'), ('Rehabilitation');

-- Procedures (30 procedures)
INSERT INTO "Procedures" ("Name", "Description", "Price") VALUES
('Blood Test', 'Complete blood count and analysis', 150.00), ('X-Ray', 'Chest X-ray examination', 200.00),
('ECG', 'Electrocardiogram test', 180.00), ('Ultrasound', 'Ultrasound imaging', 250.00),
('MRI Scan', 'Magnetic resonance imaging', 800.00), ('CT Scan', 'Computed tomography scan', 600.00),
('Physical Examination', 'General physical checkup', 100.00), ('Vaccination', 'Immunization procedure', 75.00),
('Biopsy', 'Tissue sample collection', 400.00), ('Endoscopy', 'Internal examination procedure', 500.00),
('Colonoscopy', 'Colon examination procedure', 1200.00), ('Mammography', 'Breast cancer screening', 350.00),
('Bone Density Scan', 'Osteoporosis screening', 300.00), ('Stress Test', 'Cardiac stress test', 450.00),
('Echocardiogram', 'Heart ultrasound', 550.00), ('Holter Monitor', '24-hour heart monitoring', 400.00),
('Pulmonary Function Test', 'Lung function analysis', 350.00), ('Allergy Test', 'Allergen sensitivity testing', 280.00),
('Skin Test', 'Dermatological examination', 150.00), ('Eye Examination', 'Comprehensive eye exam', 200.00),
('Hearing Test', 'Audiological assessment', 180.00), ('Dental Cleaning', 'Routine dental hygiene', 120.00),
('Cataract Surgery', 'Eye lens replacement', 3000.00), ('Arthroscopy', 'Joint examination surgery', 2500.00),
('Angioplasty', 'Blood vessel repair', 5000.00), ('Appendectomy', 'Appendix removal surgery', 4500.00),
('Knee Replacement', 'Knee joint replacement', 15000.00), ('Hip Replacement', 'Hip joint replacement', 16000.00),
('Gallbladder Removal', 'Laparoscopic cholecystectomy', 6000.00), ('Heart Bypass', 'Coronary artery bypass', 25000.00),
('Fluorography', 'Chest fluorography examination', 220.00);

-- People (150 people - mix of patients and doctors)
INSERT INTO "People" ("FirstName", "LastName", "Patronymic") VALUES
-- Patient names (100 people)
('John', 'Smith', 'Michael'), ('Sarah', 'Johnson', 'Elizabeth'), ('Michael', 'Williams', 'David'),
('Emily', 'Brown', 'Anne'), ('David', 'Jones', 'Robert'), ('Jessica', 'Garcia', 'Maria'),
('Robert', 'Miller', 'James'), ('Amanda', 'Davis', 'Rose'), ('James', 'Rodriguez', 'Carlos'),
('Lisa', 'Martinez', 'Patricia'), ('Daniel', 'Anderson', 'Joseph'), ('Michelle', 'White', 'Marie'),
('Christopher', 'Harris', 'Lee'), ('Ashley', 'Clark', 'Nicole'), ('Matthew', 'Lewis', 'Scott'),
('Amanda', 'Robinson', 'Grace'), ('Joshua', 'Walker', 'Ryan'), ('Stephanie', 'Young', 'Michelle'),
('Andrew', 'King', 'James'), ('Melissa', 'Wright', 'Ann'), ('Ryan', 'Lopez', 'Carlos'),
('Nicole', 'Hill', 'Elizabeth'), ('Brandon', 'Scott', 'Michael'), ('Heather', 'Green', 'Marie'),
('Justin', 'Adams', 'William'), ('Amy', 'Baker', 'Jane'), ('Kevin', 'Nelson', 'Thomas'),
('Rachel', 'Carter', 'Susan'), ('Eric', 'Mitchell', 'Paul'), ('Lauren', 'Perez', 'Rose'),
('Brian', 'Roberts', 'Edward'), ('Samantha', 'Turner', 'Karen'), ('Jonathan', 'Phillips', 'Charles'),
('Megan', 'Campbell', 'Patricia'), ('Jason', 'Parker', 'Robert'), ('Kimberly', 'Evans', 'Nancy'),
('Nicholas', 'Edwards', 'Richard'), ('Angela', 'Collins', 'Carol'), ('Tyler', 'Stewart', 'Daniel'),
('Brittany', 'Sanchez', 'Maria'), ('Jacob', 'Morris', 'Andrew'), ('Christina', 'Rogers', 'Jennifer'),
('Nathan', 'Reed', 'Kevin'), ('Danielle', 'Cook', 'Amanda'), ('Jordan', 'Morgan', 'Brian'),
('Rebecca', 'Bell', 'Lisa'), ('Benjamin', 'Murphy', 'Steven'), ('Laura', 'Bailey', 'Sarah'),
('Alexander', 'Rivera', 'Jose'), ('Victoria', 'Cooper', 'Catherine'), ('Noah', 'Richardson', 'Mark'),
('Olivia', 'Cox', 'Patricia'), ('Samuel', 'Howard', 'John'), ('Sophia', 'Ward', 'Elizabeth'),
('Ethan', 'Torres', 'David'), ('Isabella', 'Peterson', 'Michelle'), ('Mason', 'Gray', 'Michael'),
('Ava', 'Ramirez', 'Jessica'), ('Logan', 'James', 'Thomas'), ('Emma', 'Watson', 'Emily'),
('Lucas', 'Brooks', 'Daniel'), ('Mia', 'Kelly', 'Nicole'), ('Aiden', 'Sanders', 'Ryan'),
('Charlotte', 'Price', 'Ashley'), ('Jackson', 'Bennett', 'Christopher'), ('Harper', 'Wood', 'Stephanie'),
('Elijah', 'Barnes', 'Matthew'), ('Amelia', 'Ross', 'Amanda'), ('Liam', 'Henderson', 'Joshua'),
('Evelyn', 'Coleman', 'Melissa'), ('Henry', 'Jenkins', 'Brandon'), ('Abigail', 'Perry', 'Heather'),
('Owen', 'Powell', 'Justin'), ('Emily', 'Long', 'Amy'), ('Sebastian', 'Patterson', 'Kevin'),
('Madison', 'Hughes', 'Rachel'), ('Oliver', 'Flores', 'Eric'), ('Chloe', 'Washington', 'Lauren'),
('Carter', 'Butler', 'Brian'), ('Layla', 'Simmons', 'Samantha'), ('Wyatt', 'Foster', 'Jonathan'),
('Grace', 'Gonzales', 'Megan'), ('Maya', 'Bryant', 'Jason'), ('Levi', 'Alexander', 'Nicholas'),
('Zoe', 'Russell', 'Kimberly'), ('Lincoln', 'Griffin', 'Tyler'), ('Nora', 'Diaz', 'Brittany'),
('Jack', 'Hayes', 'Jacob'), ('Lily', 'Myers', 'Christina'), ('Luke', 'Ford', 'Nathan'),
('Hannah', 'Hamilton', 'Danielle'), ('Grayson', 'Graham', 'Jordan'), ('Addison', 'Sullivan', 'Rebecca'),
-- Doctor names (50 people)
('William', 'Hernandez', 'Jose'), ('Mary', 'Lopez', 'Carmen'), ('Richard', 'Wilson', 'Thomas'),
('Patricia', 'Anderson', 'Jane'), ('Joseph', 'Thomas', 'Paul'), ('Jennifer', 'Taylor', 'Susan'),
('Thomas', 'Moore', 'Charles'), ('Linda', 'Jackson', 'Karen'), ('Charles', 'Martin', 'Edward'),
('Barbara', 'Lee', 'Nancy'), ('Daniel', 'Thompson', 'Robert'), ('Susan', 'Garcia', 'Maria'),
('Mark', 'Martinez', 'Carlos'), ('Karen', 'Robinson', 'Patricia'), ('Steven', 'Clark', 'James'),
('Nancy', 'Rodriguez', 'Carmen'), ('Paul', 'Lewis', 'William'), ('Betty', 'Lee', 'Elizabeth'),
('Andrew', 'Walker', 'Michael'), ('Helen', 'Hall', 'Marie'), ('Kenneth', 'Allen', 'Thomas'),
('Sandra', 'Young', 'Susan'), ('Edward', 'King', 'Charles'), ('Donna', 'Wright', 'Karen'),
('Joshua', 'Lopez', 'Jose'), ('Carol', 'Hill', 'Patricia'), ('Ronald', 'Scott', 'Robert'),
('Sharon', 'Green', 'Nancy'), ('Jerry', 'Adams', 'Michael'), ('Michelle', 'Baker', 'Marie'),
('Frank', 'Nelson', 'William'), ('Emma', 'Carter', 'Elizabeth'), ('Raymond', 'Mitchell', 'Thomas'),
('Cynthia', 'Perez', 'Susan'), ('Gregory', 'Roberts', 'Charles'), ('Kathleen', 'Turner', 'Karen'),
('Lawrence', 'Phillips', 'Robert'), ('Deborah', 'Campbell', 'Patricia'), ('Sean', 'Parker', 'Michael'),
('Rachel', 'Evans', 'Nancy'), ('Patrick', 'Edwards', 'William'), ('Carolyn', 'Collins', 'Marie'),
('Keith', 'Stewart', 'Thomas'), ('Janet', 'Sanchez', 'Susan'), ('Adam', 'Morris', 'Charles'),
('Maria', 'Rogers', 'Karen'), ('Bryan', 'Reed', 'Robert'), ('Frances', 'Cook', 'Patricia'),
('Ralph', 'Morgan', 'Michael'), ('Janice', 'Bell', 'Marie'), ('Eugene', 'Murphy', 'Thomas');

-- ============================================
-- SECOND WAVE: Dependent on first wave
-- ============================================

-- Cabinets (80 cabinets across 3 buildings)
INSERT INTO "Cabinets" ("Building", "Floor", "Number", "TypeId") VALUES
-- Building 1
(1, 1, 101, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Examination' LIMIT 1)), (1, 1, 102, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Consultation' LIMIT 1)),
(1, 1, 103, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Surgery' LIMIT 1)), (1, 1, 104, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Examination' LIMIT 1)),
(1, 1, 105, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Laboratory' LIMIT 1)), (1, 1, 106, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Radiology' LIMIT 1)),
(1, 1, 107, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Physical Therapy' LIMIT 1)), (1, 1, 108, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Cardiology' LIMIT 1)),
(1, 2, 201, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Examination' LIMIT 1)), (1, 2, 202, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Consultation' LIMIT 1)),
(1, 2, 203, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Surgery' LIMIT 1)), (1, 2, 204, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Emergency' LIMIT 1)),
(1, 2, 205, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Examination' LIMIT 1)), (1, 2, 206, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Dermatology' LIMIT 1)),
(1, 2, 207, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Pediatrics' LIMIT 1)), (1, 2, 208, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Consultation' LIMIT 1)),
(1, 3, 301, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Consultation' LIMIT 1)), (1, 3, 302, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Examination' LIMIT 1)),
(1, 3, 303, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Surgery' LIMIT 1)), (1, 3, 304, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Radiology' LIMIT 1)),
(1, 3, 305, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Laboratory' LIMIT 1)), (1, 3, 306, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Physical Therapy' LIMIT 1)),
(1, 3, 307, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Cardiology' LIMIT 1)), (1, 3, 308, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Emergency' LIMIT 1)),
-- Building 2
(2, 1, 101, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Emergency' LIMIT 1)), (2, 1, 102, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Laboratory' LIMIT 1)),
(2, 1, 103, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Examination' LIMIT 1)), (2, 1, 104, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Consultation' LIMIT 1)),
(2, 1, 105, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Surgery' LIMIT 1)), (2, 1, 106, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Radiology' LIMIT 1)),
(2, 1, 107, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Physical Therapy' LIMIT 1)), (2, 1, 108, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Cardiology' LIMIT 1)),
(2, 2, 201, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Examination' LIMIT 1)), (2, 2, 202, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Surgery' LIMIT 1)),
(2, 2, 203, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Consultation' LIMIT 1)), (2, 2, 204, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Dermatology' LIMIT 1)),
(2, 2, 205, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Pediatrics' LIMIT 1)), (2, 2, 206, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Emergency' LIMIT 1)),
(2, 2, 207, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Laboratory' LIMIT 1)), (2, 2, 208, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Examination' LIMIT 1)),
(2, 3, 301, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Consultation' LIMIT 1)), (2, 3, 302, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Surgery' LIMIT 1)),
(2, 3, 303, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Radiology' LIMIT 1)), (2, 3, 304, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Physical Therapy' LIMIT 1)),
(2, 3, 305, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Cardiology' LIMIT 1)), (2, 3, 306, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Examination' LIMIT 1)),
(2, 3, 307, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Emergency' LIMIT 1)), (2, 3, 308, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Laboratory' LIMIT 1)),
-- Building 3
(3, 1, 101, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Examination' LIMIT 1)), (3, 1, 102, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Consultation' LIMIT 1)),
(3, 1, 103, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Surgery' LIMIT 1)), (3, 1, 104, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Emergency' LIMIT 1)),
(3, 1, 105, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Laboratory' LIMIT 1)), (3, 1, 106, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Radiology' LIMIT 1)),
(3, 1, 107, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Physical Therapy' LIMIT 1)), (3, 1, 108, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Cardiology' LIMIT 1)),
(3, 2, 201, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Examination' LIMIT 1)), (3, 2, 202, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Consultation' LIMIT 1)),
(3, 2, 203, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Surgery' LIMIT 1)), (3, 2, 204, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Dermatology' LIMIT 1)),
(3, 2, 205, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Pediatrics' LIMIT 1)), (3, 2, 206, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Emergency' LIMIT 1)),
(3, 2, 207, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Laboratory' LIMIT 1)), (3, 2, 208, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Examination' LIMIT 1)),
(3, 3, 301, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Consultation' LIMIT 1)), (3, 3, 302, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Surgery' LIMIT 1)),
(3, 3, 303, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Radiology' LIMIT 1)), (3, 3, 304, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Physical Therapy' LIMIT 1)),
(3, 3, 305, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Cardiology' LIMIT 1)), (3, 3, 306, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Examination' LIMIT 1)),
(3, 3, 307, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Emergency' LIMIT 1)), (3, 3, 308, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Laboratory' LIMIT 1)),
(3, 4, 401, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Examination' LIMIT 1)), (3, 4, 402, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Consultation' LIMIT 1)),
(3, 4, 403, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Surgery' LIMIT 1)), (3, 4, 404, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Emergency' LIMIT 1)),
(3, 4, 405, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Laboratory' LIMIT 1)), (3, 4, 406, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Radiology' LIMIT 1)),
(3, 4, 407, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Physical Therapy' LIMIT 1)), (3, 4, 408, (SELECT "Id" FROM "CabinetTypes" WHERE "Type" = 'Cardiology' LIMIT 1));

-- Patients (90 patients distributed across various cities)
INSERT INTO "Patients" ("PersonId", "AddressId") VALUES
((SELECT "Id" FROM "People" WHERE "FirstName" = 'John' AND "LastName" = 'Smith' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Los Angeles' ORDER BY "Id" LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Sarah' AND "LastName" = 'Johnson' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'New York City' ORDER BY "Id" LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Michael' AND "LastName" = 'Williams' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Houston' ORDER BY "Id" LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Emily' AND "LastName" = 'Brown' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Miami' ORDER BY "Id" LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'David' AND "LastName" = 'Jones' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Chicago' ORDER BY "Id" LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Jessica' AND "LastName" = 'Garcia' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Los Angeles' ORDER BY "Id" OFFSET 1 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Robert' AND "LastName" = 'Miller' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'New York City' ORDER BY "Id" OFFSET 1 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Amanda' AND "LastName" = 'Davis' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Houston' ORDER BY "Id" OFFSET 1 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'James' AND "LastName" = 'Rodriguez' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Miami' ORDER BY "Id" OFFSET 1 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Lisa' AND "LastName" = 'Martinez' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Chicago' ORDER BY "Id" OFFSET 1 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Daniel' AND "LastName" = 'Anderson' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'San Francisco' ORDER BY "Id" LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Michelle' AND "LastName" = 'White' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Philadelphia' ORDER BY "Id" LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Christopher' AND "LastName" = 'Harris' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Phoenix' ORDER BY "Id" LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Ashley' AND "LastName" = 'Clark' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Seattle' ORDER BY "Id" LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Matthew' AND "LastName" = 'Lewis' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Boston' ORDER BY "Id" LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Amanda' AND "LastName" = 'Robinson' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Atlanta' ORDER BY "Id" LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Joshua' AND "LastName" = 'Walker' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Detroit' ORDER BY "Id" LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Stephanie' AND "LastName" = 'Young' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Columbus' ORDER BY "Id" LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Andrew' AND "LastName" = 'King' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Charlotte' ORDER BY "Id" LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Melissa' AND "LastName" = 'Wright' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Denver' ORDER BY "Id" LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Ryan' AND "LastName" = 'Lopez' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Las Vegas' ORDER BY "Id" LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Nicole' AND "LastName" = 'Hill' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Nashville' ORDER BY "Id" LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Brandon' AND "LastName" = 'Scott' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'St. Louis' ORDER BY "Id" LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Heather' AND "LastName" = 'Green' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Buffalo' ORDER BY "Id" LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Justin' AND "LastName" = 'Adams' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Dallas' ORDER BY "Id" LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Amy' AND "LastName" = 'Baker' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Austin' ORDER BY "Id" LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Kevin' AND "LastName" = 'Nelson' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'San Antonio' ORDER BY "Id" LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Rachel' AND "LastName" = 'Carter' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Orlando' ORDER BY "Id" LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Eric' AND "LastName" = 'Mitchell' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Tampa' ORDER BY "Id" LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Lauren' AND "LastName" = 'Perez' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Springfield' ORDER BY "Id" LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Brian' AND "LastName" = 'Roberts' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Pittsburgh' ORDER BY "Id" LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Samantha' AND "LastName" = 'Turner' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Tucson' ORDER BY "Id" LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Jonathan' AND "LastName" = 'Phillips' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Spokane' ORDER BY "Id" LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Megan' AND "LastName" = 'Campbell' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Cambridge' ORDER BY "Id" LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Jason' AND "LastName" = 'Parker' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Savannah' ORDER BY "Id" LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Kimberly' AND "LastName" = 'Evans' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Grand Rapids' ORDER BY "Id" LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Nicholas' AND "LastName" = 'Edwards' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Cleveland' ORDER BY "Id" LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Angela' AND "LastName" = 'Collins' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Raleigh' ORDER BY "Id" LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Tyler' AND "LastName" = 'Stewart' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Boulder' ORDER BY "Id" LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Brittany' AND "LastName" = 'Sanchez' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Reno' ORDER BY "Id" LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Jacob' AND "LastName" = 'Morris' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Memphis' ORDER BY "Id" LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Christina' AND "LastName" = 'Rogers' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Kansas City' ORDER BY "Id" LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Nathan' AND "LastName" = 'Reed' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Los Angeles' ORDER BY "Id" OFFSET 2 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Danielle' AND "LastName" = 'Cook' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'New York City' ORDER BY "Id" OFFSET 2 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Jordan' AND "LastName" = 'Morgan' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Houston' ORDER BY "Id" OFFSET 2 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Rebecca' AND "LastName" = 'Bell' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Miami' ORDER BY "Id" OFFSET 2 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Benjamin' AND "LastName" = 'Murphy' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Chicago' ORDER BY "Id" OFFSET 2 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Laura' AND "LastName" = 'Bailey' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'San Francisco' ORDER BY "Id" OFFSET 1 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Alexander' AND "LastName" = 'Rivera' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Philadelphia' ORDER BY "Id" OFFSET 1 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Victoria' AND "LastName" = 'Cooper' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Phoenix' ORDER BY "Id" OFFSET 1 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Noah' AND "LastName" = 'Richardson' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Seattle' ORDER BY "Id" OFFSET 1 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Olivia' AND "LastName" = 'Cox' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Boston' ORDER BY "Id" OFFSET 1 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Samuel' AND "LastName" = 'Howard' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Atlanta' ORDER BY "Id" OFFSET 1 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Sophia' AND "LastName" = 'Ward' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Detroit' ORDER BY "Id" OFFSET 1 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Ethan' AND "LastName" = 'Torres' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Columbus' ORDER BY "Id" OFFSET 1 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Isabella' AND "LastName" = 'Peterson' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Charlotte' ORDER BY "Id" OFFSET 1 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Mason' AND "LastName" = 'Gray' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Denver' ORDER BY "Id" OFFSET 1 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Ava' AND "LastName" = 'Ramirez' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Las Vegas' ORDER BY "Id" OFFSET 1 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Logan' AND "LastName" = 'James' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Nashville' ORDER BY "Id" OFFSET 1 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Emma' AND "LastName" = 'Watson' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'St. Louis' ORDER BY "Id" OFFSET 1 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Lucas' AND "LastName" = 'Brooks' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Los Angeles' ORDER BY "Id" OFFSET 3 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Mia' AND "LastName" = 'Kelly' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'New York City' ORDER BY "Id" OFFSET 3 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Aiden' AND "LastName" = 'Sanders' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Houston' ORDER BY "Id" OFFSET 3 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Charlotte' AND "LastName" = 'Price' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Miami' ORDER BY "Id" OFFSET 3 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Jackson' AND "LastName" = 'Bennett' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Chicago' ORDER BY "Id" OFFSET 3 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Harper' AND "LastName" = 'Wood' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'San Francisco' ORDER BY "Id" OFFSET 2 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Elijah' AND "LastName" = 'Barnes' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Philadelphia' ORDER BY "Id" OFFSET 2 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Amelia' AND "LastName" = 'Ross' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Phoenix' ORDER BY "Id" OFFSET 2 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Liam' AND "LastName" = 'Henderson' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Seattle' ORDER BY "Id" OFFSET 2 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Evelyn' AND "LastName" = 'Coleman' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Boston' ORDER BY "Id" OFFSET 2 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Henry' AND "LastName" = 'Jenkins' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Atlanta' ORDER BY "Id" OFFSET 2 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Abigail' AND "LastName" = 'Perry' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Detroit' ORDER BY "Id" OFFSET 2 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Owen' AND "LastName" = 'Powell' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Columbus' ORDER BY "Id" OFFSET 2 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Emily' AND "LastName" = 'Long' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Charlotte' ORDER BY "Id" OFFSET 2 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Sebastian' AND "LastName" = 'Patterson' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Denver' ORDER BY "Id" OFFSET 2 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Madison' AND "LastName" = 'Hughes' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Las Vegas' ORDER BY "Id" OFFSET 2 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Oliver' AND "LastName" = 'Flores' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Nashville' ORDER BY "Id" OFFSET 2 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Chloe' AND "LastName" = 'Washington' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'St. Louis' ORDER BY "Id" OFFSET 2 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Carter' AND "LastName" = 'Butler' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Buffalo' ORDER BY "Id" OFFSET 1 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Layla' AND "LastName" = 'Simmons' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Dallas' ORDER BY "Id" OFFSET 1 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Wyatt' AND "LastName" = 'Foster' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Austin' ORDER BY "Id" OFFSET 1 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Grace' AND "LastName" = 'Gonzales' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'San Antonio' ORDER BY "Id" OFFSET 1 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Maya' AND "LastName" = 'Bryant' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Orlando' ORDER BY "Id" OFFSET 1 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Levi' AND "LastName" = 'Alexander' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Tampa' ORDER BY "Id" OFFSET 1 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Zoe' AND "LastName" = 'Russell' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Springfield' ORDER BY "Id" OFFSET 1 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Lincoln' AND "LastName" = 'Griffin' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Pittsburgh' ORDER BY "Id" OFFSET 1 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Nora' AND "LastName" = 'Diaz' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Tucson' ORDER BY "Id" OFFSET 1 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Jack' AND "LastName" = 'Hayes' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Spokane' ORDER BY "Id" OFFSET 1 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Lily' AND "LastName" = 'Myers' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Cambridge' ORDER BY "Id" OFFSET 1 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Luke' AND "LastName" = 'Ford' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Savannah' ORDER BY "Id" OFFSET 1 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Hannah' AND "LastName" = 'Hamilton' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Grand Rapids' ORDER BY "Id" OFFSET 1 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Grayson' AND "LastName" = 'Graham' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Cleveland' ORDER BY "Id" OFFSET 1 LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Addison' AND "LastName" = 'Sullivan' LIMIT 1), (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Raleigh' ORDER BY "Id" OFFSET 1 LIMIT 1));

-- Doctors (50 doctors across various specialties)
INSERT INTO "Doctors" ("PersonId", "SpecialtyId") VALUES
((SELECT "Id" FROM "People" WHERE "FirstName" = 'William' AND "LastName" = 'Hernandez' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Cardiology' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Mary' AND "LastName" = 'Lopez' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Dermatology' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Richard' AND "LastName" = 'Wilson' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Pediatrics' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Patricia' AND "LastName" = 'Anderson' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Orthopedics' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Joseph' AND "LastName" = 'Thomas' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Neurology' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Jennifer' AND "LastName" = 'Taylor' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'General Practice' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Thomas' AND "LastName" = 'Moore' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Internal Medicine' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Linda' AND "LastName" = 'Jackson' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Oncology' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Charles' AND "LastName" = 'Martin' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Psychiatry' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Barbara' AND "LastName" = 'Lee' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Radiology' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Daniel' AND "LastName" = 'Thompson' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Anesthesiology' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Susan' AND "LastName" = 'Garcia' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Emergency Medicine' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Mark' AND "LastName" = 'Martinez' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Family Medicine' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Karen' AND "LastName" = 'Robinson' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Gastroenterology' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Steven' AND "LastName" = 'Clark' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Geriatrics' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Nancy' AND "LastName" = 'Rodriguez' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Gynecology' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Paul' AND "LastName" = 'Lewis' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Hematology' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Betty' AND "LastName" = 'Lee' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Infectious Disease' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Andrew' AND "LastName" = 'Walker' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Nephrology' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Helen' AND "LastName" = 'Hall' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Obstetrics' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Kenneth' AND "LastName" = 'Allen' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Ophthalmology' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Sandra' AND "LastName" = 'Young' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Pathology' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Edward' AND "LastName" = 'King' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Pulmonology' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Donna' AND "LastName" = 'Wright' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Rheumatology' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Joshua' AND "LastName" = 'Lopez' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Urology' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Carol' AND "LastName" = 'Hill' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Cardiology' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Ronald' AND "LastName" = 'Scott' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Dermatology' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Sharon' AND "LastName" = 'Green' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Pediatrics' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Jerry' AND "LastName" = 'Adams' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Orthopedics' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Michelle' AND "LastName" = 'Baker' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Neurology' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Frank' AND "LastName" = 'Nelson' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'General Practice' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Emma' AND "LastName" = 'Carter' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Internal Medicine' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Raymond' AND "LastName" = 'Mitchell' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Oncology' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Cynthia' AND "LastName" = 'Perez' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Psychiatry' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Gregory' AND "LastName" = 'Roberts' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Radiology' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Kathleen' AND "LastName" = 'Turner' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Anesthesiology' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Lawrence' AND "LastName" = 'Phillips' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Emergency Medicine' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Deborah' AND "LastName" = 'Campbell' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Family Medicine' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Sean' AND "LastName" = 'Parker' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Gastroenterology' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Rachel' AND "LastName" = 'Evans' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Geriatrics' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Patrick' AND "LastName" = 'Edwards' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Gynecology' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Carolyn' AND "LastName" = 'Collins' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Hematology' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Keith' AND "LastName" = 'Stewart' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Infectious Disease' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Janet' AND "LastName" = 'Sanchez' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Nephrology' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Adam' AND "LastName" = 'Morris' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Obstetrics' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Maria' AND "LastName" = 'Rogers' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Ophthalmology' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Bryan' AND "LastName" = 'Reed' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Pathology' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Frances' AND "LastName" = 'Cook' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Pulmonology' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Ralph' AND "LastName" = 'Morgan' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Rheumatology' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Janice' AND "LastName" = 'Bell' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Urology' LIMIT 1)),
((SELECT "Id" FROM "People" WHERE "FirstName" = 'Eugene' AND "LastName" = 'Murphy' LIMIT 1), (SELECT "Id" FROM "Specialties" WHERE "Name" = 'Cardiology' LIMIT 1));

-- ============================================
-- THIRD WAVE: Dependent on second wave
-- ============================================

-- Health junction tables (only depend on first wave - can be inserted early in third wave)
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

-- Doctor-related tables (depend on Doctors from second wave)
-- DistrictDoctors (30 district doctors)
INSERT INTO "DistrictDoctors" ("DoctorId") VALUES
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'William' AND p."LastName" = 'Hernandez' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Mary' AND p."LastName" = 'Lopez' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Richard' AND p."LastName" = 'Wilson' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Patricia' AND p."LastName" = 'Anderson' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Joseph' AND p."LastName" = 'Thomas' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Jennifer' AND p."LastName" = 'Taylor' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Thomas' AND p."LastName" = 'Moore' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Linda' AND p."LastName" = 'Jackson' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Charles' AND p."LastName" = 'Martin' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Barbara' AND p."LastName" = 'Lee' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Daniel' AND p."LastName" = 'Thompson' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Susan' AND p."LastName" = 'Garcia' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Mark' AND p."LastName" = 'Martinez' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Karen' AND p."LastName" = 'Robinson' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Steven' AND p."LastName" = 'Clark' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Nancy' AND p."LastName" = 'Rodriguez' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Paul' AND p."LastName" = 'Lewis' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Betty' AND p."LastName" = 'Lee' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Andrew' AND p."LastName" = 'Walker' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Helen' AND p."LastName" = 'Hall' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Kenneth' AND p."LastName" = 'Allen' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Sandra' AND p."LastName" = 'Young' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Edward' AND p."LastName" = 'King' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Donna' AND p."LastName" = 'Wright' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Joshua' AND p."LastName" = 'Lopez' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Carol' AND p."LastName" = 'Hill' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Ronald' AND p."LastName" = 'Scott' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Sharon' AND p."LastName" = 'Green' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Jerry' AND p."LastName" = 'Adams' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Michelle' AND p."LastName" = 'Baker' LIMIT 1));

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
 (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Vaccination' LIMIT 1)),
-- Additional DoctorProcedures for expanded doctors (assigning 2-5 procedures per doctor)
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Thomas' AND p."LastName" = 'Moore' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Blood Test' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Thomas' AND p."LastName" = 'Moore' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Physical Examination' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Thomas' AND p."LastName" = 'Moore' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'X-Ray' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Linda' AND p."LastName" = 'Jackson' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Biopsy' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Linda' AND p."LastName" = 'Jackson' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'CT Scan' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Linda' AND p."LastName" = 'Jackson' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Chemotherapy' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Charles' AND p."LastName" = 'Martin' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Physical Examination' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Charles' AND p."LastName" = 'Martin' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Psychotherapy' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Barbara' AND p."LastName" = 'Lee' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'X-Ray' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Barbara' AND p."LastName" = 'Lee' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'CT Scan' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Barbara' AND p."LastName" = 'Lee' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'MRI Scan' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Daniel' AND p."LastName" = 'Thompson' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Physical Examination' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Susan' AND p."LastName" = 'Garcia' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Physical Examination' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Mark' AND p."LastName" = 'Martinez' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Physical Examination' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Mark' AND p."LastName" = 'Martinez' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Blood Test' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Mark' AND p."LastName" = 'Martinez' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Vaccination' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Karen' AND p."LastName" = 'Robinson' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Endoscopy' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Karen' AND p."LastName" = 'Robinson' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Colonoscopy' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Steven' AND p."LastName" = 'Clark' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Physical Examination' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Steven' AND p."LastName" = 'Clark' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Bone Density Scan' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Nancy' AND p."LastName" = 'Rodriguez' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Physical Examination' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Nancy' AND p."LastName" = 'Rodriguez' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Ultrasound' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Paul' AND p."LastName" = 'Lewis' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Blood Test' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Paul' AND p."LastName" = 'Lewis' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Blood Transfusion' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Betty' AND p."LastName" = 'Lee' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Blood Test' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Betty' AND p."LastName" = 'Lee' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Physical Examination' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Betty' AND p."LastName" = 'Lee' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Biopsy' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Andrew' AND p."LastName" = 'Walker' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Blood Test' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Andrew' AND p."LastName" = 'Walker' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Dialysis' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Helen' AND p."LastName" = 'Hall' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Ultrasound' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Helen' AND p."LastName" = 'Hall' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Physical Examination' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Kenneth' AND p."LastName" = 'Allen' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Eye Examination' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Kenneth' AND p."LastName" = 'Allen' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Cataract Surgery' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Sandra' AND p."LastName" = 'Young' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Biopsy' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Sandra' AND p."LastName" = 'Young' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Blood Test' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Edward' AND p."LastName" = 'King' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'X-Ray' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Edward' AND p."LastName" = 'King' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Pulmonary Function Test' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Donna' AND p."LastName" = 'Wright' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Blood Test' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Donna' AND p."LastName" = 'Wright' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'X-Ray' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Joshua' AND p."LastName" = 'Lopez' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Physical Examination' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Joshua' AND p."LastName" = 'Lopez' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Ultrasound' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Carol' AND p."LastName" = 'Hill' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'ECG' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Carol' AND p."LastName" = 'Hill' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Echocardiogram' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Carol' AND p."LastName" = 'Hill' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Stress Test' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Ronald' AND p."LastName" = 'Scott' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Skin Test' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Ronald' AND p."LastName" = 'Scott' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Biopsy' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Sharon' AND p."LastName" = 'Green' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Blood Test' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Sharon' AND p."LastName" = 'Green' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Physical Examination' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Sharon' AND p."LastName" = 'Green' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Vaccination' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Jerry' AND p."LastName" = 'Adams' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'X-Ray' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Jerry' AND p."LastName" = 'Adams' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'MRI Scan' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Jerry' AND p."LastName" = 'Adams' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Arthroscopy' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Michelle' AND p."LastName" = 'Baker' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'CT Scan' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Michelle' AND p."LastName" = 'Baker' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'MRI Scan' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Frank' AND p."LastName" = 'Nelson' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Physical Examination' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Frank' AND p."LastName" = 'Nelson' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Blood Test' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Frank' AND p."LastName" = 'Nelson' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'X-Ray' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Frank' AND p."LastName" = 'Nelson' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Vaccination' LIMIT 1)),
-- Additional procedures for query testing
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Barbara' AND p."LastName" = 'Lee' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Fluorography' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Barbara' AND p."LastName" = 'Lee' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'X-Ray' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Richard' AND p."LastName" = 'Wilson' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Vaccination' LIMIT 1)),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Jennifer' AND p."LastName" = 'Taylor' LIMIT 1), (SELECT "Id" FROM "Procedures" WHERE "Name" = 'Vaccination' LIMIT 1));

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
 '2024-01-16 08:00:00+00', '2024-01-16 16:00:00+00'),
-- Additional schedules for query testing (recent dates)
-- Schedules in last 7 days (satisfies physio-doctors-count query for physical therapy)
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Thomas' AND p."LastName" = 'Moore' LIMIT 1),
 (SELECT "Id" FROM "Cabinets" WHERE "Building" = 1 AND "Floor" = 1 AND "Number" = 107 LIMIT 1),
 CURRENT_DATE - INTERVAL '4 days' + INTERVAL '9 hours', CURRENT_DATE - INTERVAL '4 days' + INTERVAL '17 hours'),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Mark' AND p."LastName" = 'Martinez' LIMIT 1),
 (SELECT "Id" FROM "Cabinets" WHERE "Building" = 3 AND "Floor" = 3 AND "Number" = 304 LIMIT 1),
 CURRENT_DATE - INTERVAL '2 days' + INTERVAL '8 hours', CURRENT_DATE - INTERVAL '2 days' + INTERVAL '16 hours'),
-- Schedules in current month
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Carol' AND p."LastName" = 'Hill' LIMIT 1),
 (SELECT "Id" FROM "Cabinets" WHERE "Building" = 1 AND "Floor" = 3 AND "Number" = 307 LIMIT 1),
 DATE_TRUNC('month', CURRENT_DATE) + INTERVAL '3 days' + INTERVAL '9 hours', DATE_TRUNC('month', CURRENT_DATE) + INTERVAL '3 days' + INTERVAL '17 hours'),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Carol' AND p."LastName" = 'Hill' LIMIT 1),
 (SELECT "Id" FROM "Cabinets" WHERE "Building" = 1 AND "Floor" = 3 AND "Number" = 307 LIMIT 1),
 DATE_TRUNC('month', CURRENT_DATE) + INTERVAL '10 days' + INTERVAL '9 hours', DATE_TRUNC('month', CURRENT_DATE) + INTERVAL '10 days' + INTERVAL '17 hours');

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
 '2024-01-17 11:00:00+00', '2024-01-17 11:30:00+00', true),
-- Additional Appointments for query testing (satisfies and doesn't satisfy various queries)
-- Recent appointments in last 7 days (satisfies patients-per-week query)
((SELECT pt."Id" FROM "Patients" pt JOIN "People" p ON pt."PersonId" = p."Id" WHERE p."FirstName" = 'Daniel' AND p."LastName" = 'Anderson' LIMIT 1),
 (SELECT dp."Id" FROM "DoctorProcedures" dp JOIN "Doctors" d ON dp."DoctorId" = d."Id" JOIN "People" p ON d."PersonId" = p."Id" JOIN "Procedures" pr ON dp."ProcedureId" = pr."Id" WHERE p."FirstName" = 'William' AND p."LastName" = 'Hernandez' AND pr."Name" = 'ECG' LIMIT 1),
 (SELECT "Id" FROM "Cabinets" WHERE "Building" = 1 AND "Floor" = 1 AND "Number" = 101 LIMIT 1),
 CURRENT_DATE - INTERVAL '3 days' + INTERVAL '10 hours', CURRENT_DATE - INTERVAL '3 days' + INTERVAL '10 hours 30 minutes', true),
((SELECT pt."Id" FROM "Patients" pt JOIN "People" p ON pt."PersonId" = p."Id" WHERE p."FirstName" = 'Michelle' AND p."LastName" = 'White' LIMIT 1),
 (SELECT dp."Id" FROM "DoctorProcedures" dp JOIN "Doctors" d ON dp."DoctorId" = d."Id" JOIN "People" p ON d."PersonId" = p."Id" JOIN "Procedures" pr ON dp."ProcedureId" = pr."Id" WHERE p."FirstName" = 'Mary' AND p."LastName" = 'Lopez' AND pr."Name" = 'Ultrasound' LIMIT 1),
 (SELECT "Id" FROM "Cabinets" WHERE "Building" = 1 AND "Floor" = 1 AND "Number" = 102 LIMIT 1),
 CURRENT_DATE - INTERVAL '5 days' + INTERVAL '14 hours', CURRENT_DATE - INTERVAL '5 days' + INTERVAL '14 hours 30 minutes', true),
((SELECT pt."Id" FROM "Patients" pt JOIN "People" p ON pt."PersonId" = p."Id" WHERE p."FirstName" = 'Christopher' AND p."LastName" = 'Harris' LIMIT 1),
 (SELECT dp."Id" FROM "DoctorProcedures" dp JOIN "Doctors" d ON dp."DoctorId" = d."Id" JOIN "People" p ON d."PersonId" = p."Id" JOIN "Procedures" pr ON dp."ProcedureId" = pr."Id" WHERE p."FirstName" = 'Richard' AND p."LastName" = 'Wilson' AND pr."Name" = 'Blood Test' LIMIT 1),
 (SELECT "Id" FROM "Cabinets" WHERE "Building" = 1 AND "Floor" = 1 AND "Number" = 103 LIMIT 1),
 CURRENT_DATE - INTERVAL '2 days' + INTERVAL '11 hours', CURRENT_DATE - INTERVAL '2 days' + INTERVAL '11 hours 30 minutes', false),
-- Appointments for patients with multiple doctors (>2) in last week
((SELECT pt."Id" FROM "Patients" pt JOIN "People" p ON pt."PersonId" = p."Id" WHERE p."FirstName" = 'Ashley' AND p."LastName" = 'Clark' LIMIT 1),
 (SELECT dp."Id" FROM "DoctorProcedures" dp JOIN "Doctors" d ON dp."DoctorId" = d."Id" JOIN "People" p ON d."PersonId" = p."Id" JOIN "Procedures" pr ON dp."ProcedureId" = pr."Id" WHERE p."FirstName" = 'William' AND p."LastName" = 'Hernandez' AND pr."Name" = 'ECG' LIMIT 1),
 (SELECT "Id" FROM "Cabinets" WHERE "Building" = 1 AND "Floor" = 1 AND "Number" = 101 LIMIT 1),
 CURRENT_DATE - INTERVAL '4 days' + INTERVAL '9 hours', CURRENT_DATE - INTERVAL '4 days' + INTERVAL '9 hours 30 minutes', true),
((SELECT pt."Id" FROM "Patients" pt JOIN "People" p ON pt."PersonId" = p."Id" WHERE p."FirstName" = 'Ashley' AND p."LastName" = 'Clark' LIMIT 1),
 (SELECT dp."Id" FROM "DoctorProcedures" dp JOIN "Doctors" d ON dp."DoctorId" = d."Id" JOIN "People" p ON d."PersonId" = p."Id" JOIN "Procedures" pr ON dp."ProcedureId" = pr."Id" WHERE p."FirstName" = 'Mary' AND p."LastName" = 'Lopez' AND pr."Name" = 'Biopsy' LIMIT 1),
 (SELECT "Id" FROM "Cabinets" WHERE "Building" = 1 AND "Floor" = 1 AND "Number" = 102 LIMIT 1),
 CURRENT_DATE - INTERVAL '3 days' + INTERVAL '15 hours', CURRENT_DATE - INTERVAL '3 days' + INTERVAL '15 hours 30 minutes', true),
((SELECT pt."Id" FROM "Patients" pt JOIN "People" p ON pt."PersonId" = p."Id" WHERE p."FirstName" = 'Ashley' AND p."LastName" = 'Clark' LIMIT 1),
 (SELECT dp."Id" FROM "DoctorProcedures" dp JOIN "Doctors" d ON dp."DoctorId" = d."Id" JOIN "People" p ON d."PersonId" = p."Id" JOIN "Procedures" pr ON dp."ProcedureId" = pr."Id" WHERE p."FirstName" = 'Patricia' AND p."LastName" = 'Anderson' AND pr."Name" = 'X-Ray' LIMIT 1),
 (SELECT "Id" FROM "Cabinets" WHERE "Building" = 1 AND "Floor" = 2 AND "Number" = 201 LIMIT 1),
 CURRENT_DATE - INTERVAL '6 days' + INTERVAL '10 hours', CURRENT_DATE - INTERVAL '6 days' + INTERVAL '10 hours 30 minutes', true),
((SELECT pt."Id" FROM "Patients" pt JOIN "People" p ON pt."PersonId" = p."Id" WHERE p."FirstName" = 'Ashley' AND p."LastName" = 'Clark' LIMIT 1),
 (SELECT dp."Id" FROM "DoctorProcedures" dp JOIN "Doctors" d ON dp."DoctorId" = d."Id" JOIN "People" p ON d."PersonId" = p."Id" JOIN "Procedures" pr ON dp."ProcedureId" = pr."Id" WHERE p."FirstName" = 'Thomas' AND p."LastName" = 'Moore' AND pr."Name" = 'Blood Test' LIMIT 1),
 (SELECT "Id" FROM "Cabinets" WHERE "Building" = 1 AND "Floor" = 2 AND "Number" = 202 LIMIT 1),
 CURRENT_DATE - INTERVAL '1 days' + INTERVAL '13 hours', CURRENT_DATE - INTERVAL '1 days' + INTERVAL '13 hours 30 minutes', true),
-- Appointments in current month (satisfies monthly queries)
((SELECT pt."Id" FROM "Patients" pt JOIN "People" p ON pt."PersonId" = p."Id" WHERE p."FirstName" = 'Matthew' AND p."LastName" = 'Lewis' LIMIT 1),
 (SELECT dp."Id" FROM "DoctorProcedures" dp JOIN "Doctors" d ON dp."DoctorId" = d."Id" JOIN "People" p ON d."PersonId" = p."Id" JOIN "Procedures" pr ON dp."ProcedureId" = pr."Id" WHERE p."FirstName" = 'Carol' AND p."LastName" = 'Hill' AND pr."Name" = 'ECG' LIMIT 1),
 (SELECT "Id" FROM "Cabinets" WHERE "Building" = 1 AND "Floor" = 3 AND "Number" = 307 LIMIT 1),
 DATE_TRUNC('month', CURRENT_DATE) + INTERVAL '5 days' + INTERVAL '10 hours', DATE_TRUNC('month', CURRENT_DATE) + INTERVAL '5 days' + INTERVAL '10 hours 30 minutes', true),
-- Fluorography appointments (satisfies fluorography-patients query) - need to add doctor who can do fluorography first
-- Adding fluorography procedure for a doctor, then appointment
((SELECT pt."Id" FROM "Patients" pt JOIN "People" p ON pt."PersonId" = p."Id" WHERE p."FirstName" = 'Amanda' AND p."LastName" = 'Robinson' LIMIT 1),
 (SELECT dp."Id" FROM "DoctorProcedures" dp JOIN "Doctors" d ON dp."DoctorId" = d."Id" JOIN "People" p ON d."PersonId" = p."Id" JOIN "Procedures" pr ON dp."ProcedureId" = pr."Id" WHERE p."FirstName" = 'Barbara' AND p."LastName" = 'Lee' AND pr."Name" = 'X-Ray' LIMIT 1),
 (SELECT "Id" FROM "Cabinets" WHERE "Building" = 2 AND "Floor" = 1 AND "Number" = 106 LIMIT 1),
 CURRENT_DATE + INTERVAL '2 days' + INTERVAL '9 hours', CURRENT_DATE + INTERVAL '2 days' + INTERVAL '9 hours 30 minutes', true),
-- Fluorography appointments (satisfies fluorography-patients query)
((SELECT pt."Id" FROM "Patients" pt JOIN "People" p ON pt."PersonId" = p."Id" WHERE p."FirstName" = 'Joshua' AND p."LastName" = 'Walker' LIMIT 1),
 (SELECT dp."Id" FROM "DoctorProcedures" dp JOIN "Doctors" d ON dp."DoctorId" = d."Id" JOIN "People" p ON d."PersonId" = p."Id" JOIN "Procedures" pr ON dp."ProcedureId" = pr."Id" WHERE p."FirstName" = 'Barbara' AND p."LastName" = 'Lee' AND pr."Name" = 'Fluorography' LIMIT 1),
 (SELECT "Id" FROM "Cabinets" WHERE "Building" = 2 AND "Floor" = 1 AND "Number" = 106 LIMIT 1),
 CURRENT_DATE + INTERVAL '1 day' + INTERVAL '10 hours', CURRENT_DATE + INTERVAL '1 day' + INTERVAL '10 hours 30 minutes', true),
((SELECT pt."Id" FROM "Patients" pt JOIN "People" p ON pt."PersonId" = p."Id" WHERE p."FirstName" = 'Stephanie' AND p."LastName" = 'Young' LIMIT 1),
 (SELECT dp."Id" FROM "DoctorProcedures" dp JOIN "Doctors" d ON dp."DoctorId" = d."Id" JOIN "People" p ON d."PersonId" = p."Id" JOIN "Procedures" pr ON dp."ProcedureId" = pr."Id" WHERE p."FirstName" = 'Barbara' AND p."LastName" = 'Lee' AND pr."Name" = 'Fluorography' LIMIT 1),
 (SELECT "Id" FROM "Cabinets" WHERE "Building" = 2 AND "Floor" = 1 AND "Number" = 106 LIMIT 1),
 CURRENT_DATE + INTERVAL '3 days' + INTERVAL '14 hours', CURRENT_DATE + INTERVAL '3 days' + INTERVAL '14 hours 30 minutes', true),
-- Vaccination appointments that were missed (DidItHappen=false) - satisfies missed-vaccination query
((SELECT pt."Id" FROM "Patients" pt JOIN "People" p ON pt."PersonId" = p."Id" WHERE p."FirstName" = 'Lucas' AND p."LastName" = 'Brooks' LIMIT 1),
 (SELECT dp."Id" FROM "DoctorProcedures" dp JOIN "Doctors" d ON dp."DoctorId" = d."Id" JOIN "People" p ON d."PersonId" = p."Id" JOIN "Procedures" pr ON dp."ProcedureId" = pr."Id" WHERE p."FirstName" = 'Richard' AND p."LastName" = 'Wilson' AND pr."Name" = 'Vaccination' LIMIT 1),
 (SELECT "Id" FROM "Cabinets" WHERE "Building" = 1 AND "Floor" = 1 AND "Number" = 103 LIMIT 1),
 CURRENT_DATE - INTERVAL '30 days' + INTERVAL '11 hours', CURRENT_DATE - INTERVAL '30 days' + INTERVAL '11 hours 30 minutes', false),
((SELECT pt."Id" FROM "Patients" pt JOIN "People" p ON pt."PersonId" = p."Id" WHERE p."FirstName" = 'Mia' AND p."LastName" = 'Kelly' LIMIT 1),
 (SELECT dp."Id" FROM "DoctorProcedures" dp JOIN "Doctors" d ON dp."DoctorId" = d."Id" JOIN "People" p ON d."PersonId" = p."Id" JOIN "Procedures" pr ON dp."ProcedureId" = pr."Id" WHERE p."FirstName" = 'Jennifer' AND p."LastName" = 'Taylor' AND pr."Name" = 'Vaccination' LIMIT 1),
 (SELECT "Id" FROM "Cabinets" WHERE "Building" = 1 AND "Floor" = 1 AND "Number" = 101 LIMIT 1),
 CURRENT_DATE - INTERVAL '45 days' + INTERVAL '14 hours', CURRENT_DATE - INTERVAL '45 days' + INTERVAL '14 hours 30 minutes', false),
-- More appointments in current month for monthly queries
((SELECT pt."Id" FROM "Patients" pt JOIN "People" p ON pt."PersonId" = p."Id" WHERE p."FirstName" = 'Amelia' AND p."LastName" = 'Ross' LIMIT 1),
 (SELECT dp."Id" FROM "DoctorProcedures" dp JOIN "Doctors" d ON dp."DoctorId" = d."Id" JOIN "People" p ON d."PersonId" = p."Id" JOIN "Procedures" pr ON dp."ProcedureId" = pr."Id" WHERE p."FirstName" = 'William' AND p."LastName" = 'Hernandez' AND pr."Name" = 'Echocardiogram' LIMIT 1),
 (SELECT "Id" FROM "Cabinets" WHERE "Building" = 1 AND "Floor" = 3 AND "Number" = 307 LIMIT 1),
 DATE_TRUNC('month', CURRENT_DATE) + INTERVAL '7 days' + INTERVAL '11 hours', DATE_TRUNC('month', CURRENT_DATE) + INTERVAL '7 days' + INTERVAL '11 hours 30 minutes', true),
((SELECT pt."Id" FROM "Patients" pt JOIN "People" p ON pt."PersonId" = p."Id" WHERE p."FirstName" = 'Liam' AND p."LastName" = 'Henderson' LIMIT 1),
 (SELECT dp."Id" FROM "DoctorProcedures" dp JOIN "Doctors" d ON dp."DoctorId" = d."Id" JOIN "People" p ON d."PersonId" = p."Id" JOIN "Procedures" pr ON dp."ProcedureId" = pr."Id" WHERE p."FirstName" = 'Carol' AND p."LastName" = 'Hill' AND pr."Name" = 'Stress Test' LIMIT 1),
 (SELECT "Id" FROM "Cabinets" WHERE "Building" = 1 AND "Floor" = 3 AND "Number" = 307 LIMIT 1),
 DATE_TRUNC('month', CURRENT_DATE) + INTERVAL '12 days' + INTERVAL '10 hours', DATE_TRUNC('month', CURRENT_DATE) + INTERVAL '12 days' + INTERVAL '10 hours 30 minutes', true);

-- HomeCallLogs (using subqueries)
-- Mix: some match patient addresses (satisfy home-call-patients query), some don't
INSERT INTO "HomeCallLogs" ("DoctorId", "AddressId", "DateTime") VALUES
-- HomeCallLogs that MATCH patient addresses (satisfy home-call-patients query)
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'William' AND p."LastName" = 'Hernandez' LIMIT 1),
 (SELECT pt."AddressId" FROM "Patients" pt JOIN "People" p ON pt."PersonId" = p."Id" WHERE p."FirstName" = 'John' AND p."LastName" = 'Smith' LIMIT 1),
 '2024-01-15 08:00:00+00'),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Mary' AND p."LastName" = 'Lopez' LIMIT 1),
 (SELECT pt."AddressId" FROM "Patients" pt JOIN "People" p ON pt."PersonId" = p."Id" WHERE p."FirstName" = 'Sarah' AND p."LastName" = 'Johnson' LIMIT 1),
 '2024-01-15 12:00:00+00'),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Richard' AND p."LastName" = 'Wilson' LIMIT 1),
 (SELECT pt."AddressId" FROM "Patients" pt JOIN "People" p ON pt."PersonId" = p."Id" WHERE p."FirstName" = 'Michael' AND p."LastName" = 'Williams' LIMIT 1),
 '2024-01-16 08:00:00+00'),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Patricia' AND p."LastName" = 'Anderson' LIMIT 1),
 (SELECT pt."AddressId" FROM "Patients" pt JOIN "People" p ON pt."PersonId" = p."Id" WHERE p."FirstName" = 'Emily' AND p."LastName" = 'Brown' LIMIT 1),
 '2024-01-16 14:00:00+00'),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Joseph' AND p."LastName" = 'Thomas' LIMIT 1),
 (SELECT pt."AddressId" FROM "Patients" pt JOIN "People" p ON pt."PersonId" = p."Id" WHERE p."FirstName" = 'David' AND p."LastName" = 'Jones' LIMIT 1),
 '2024-01-17 08:00:00+00'),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Thomas' AND p."LastName" = 'Moore' LIMIT 1),
 (SELECT pt."AddressId" FROM "Patients" pt JOIN "People" p ON pt."PersonId" = p."Id" WHERE p."FirstName" = 'Daniel' AND p."LastName" = 'Anderson' LIMIT 1),
 CURRENT_DATE - INTERVAL '2 days' + INTERVAL '10 hours'),
-- HomeCallLogs that DON'T match patient addresses (don't satisfy home-call-patients query)
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Linda' AND p."LastName" = 'Jackson' LIMIT 1),
 (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'San Francisco' AND "StreetName" = 'Market Street' LIMIT 1),
 '2024-01-18 09:00:00+00'),
((SELECT d."Id" FROM "Doctors" d JOIN "People" p ON d."PersonId" = p."Id" WHERE p."FirstName" = 'Charles' AND p."LastName" = 'Martin' LIMIT 1),
 (SELECT "Id" FROM "Addresses" WHERE "Locality" = 'Philadelphia' AND "StreetName" = 'Market Street' LIMIT 1),
 '2024-01-19 14:00:00+00');

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
 'Prescribed migraine medication. Rest in dark room when needed.'),
-- Additional diagnoses for query testing
-- Diagnoses with "angina" in prescription (satisfies angina-count query) - in current month
((SELECT a."Id" FROM "Appointments" a 
  JOIN "Patients" pt ON a."PatientId" = pt."Id" 
  JOIN "People" p ON pt."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Daniel' AND p."LastName" = 'Anderson' 
  AND a."StartTime" >= CURRENT_DATE - INTERVAL '3 days' LIMIT 1),
 'Diagnosis: Stable angina. Prescribed nitroglycerin. Monitor symptoms.'),
((SELECT a."Id" FROM "Appointments" a 
  JOIN "Patients" pt ON a."PatientId" = pt."Id" 
  JOIN "People" p ON pt."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Michelle' AND p."LastName" = 'White' 
  AND a."StartTime" >= CURRENT_DATE - INTERVAL '5 days' LIMIT 1),
 'Chest pain consistent with angina. EKG normal. Recommend stress test.'),
((SELECT a."Id" FROM "Appointments" a 
  JOIN "Patients" pt ON a."PatientId" = pt."Id" 
  JOIN "People" p ON pt."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Christopher' AND p."LastName" = 'Harris' 
  AND a."StartTime" >= CURRENT_DATE - INTERVAL '2 days' LIMIT 1),
 'Unstable angina detected. Immediate cardiology consultation recommended.'),
-- Diagnoses WITHOUT angina (don't satisfy angina-count query)
((SELECT a."Id" FROM "Appointments" a 
  JOIN "Patients" pt ON a."PatientId" = pt."Id" 
  JOIN "People" p ON pt."PersonId" = p."Id" 
  WHERE p."FirstName" = 'Matthew' AND p."LastName" = 'Lewis' 
  AND a."StartTime" >= DATE_TRUNC('month', CURRENT_DATE) LIMIT 1),
 'Hypertension managed. Blood pressure stable. Continue current medication.');
