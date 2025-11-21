using ClinicManagement.Models.Info;

namespace ClinicManagement.Data.Info
{
    public static class DoctorProcedureSeedData
    {
        public static List<DoctorProcedure> GetSeedData()
        {
            var data = new List<DoctorProcedure>();
            int id = 1;
            
            // Family Medicine doctors (1, 26) - can do general procedures
            foreach (var doctorId in new[] { 1, 26 })
            {
                data.AddRange(new[] {
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 1 }, // General Examination
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 2 }, // Blood Pressure
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 6 }, // CBC
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 7 }, // Urinalysis
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 12 }, // Vaccination
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 14 }, // Injection
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 13 } // Wound Dressing
                });
            }
            
            // Internal Medicine doctors (2, 27) - comprehensive care
            foreach (var doctorId in new[] { 2, 27 })
            {
                data.AddRange(new[] {
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 1 }, // General Examination
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 2 }, // Blood Pressure
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 6 }, // CBC
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 32 }, // Lipid Panel
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 31 }, // Thyroid Function
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 33 } // Hemoglobin A1C
                });
            }
            
            // Cardiologists (3, 28) - heart procedures
            foreach (var doctorId in new[] { 3, 28 })
            {
                data.AddRange(new[] {
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 1 }, // General Examination
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 2 }, // Blood Pressure
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 3 }, // ECG
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 16 }, // Echocardiogram
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 17 }, // Stress Test
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 18 }, // Holter Monitor
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 54 }, // Cardiac Catheterization
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 55 } // Angioplasty
                });
            }
            
            // Pediatricians (4, 29) - child care
            foreach (var doctorId in new[] { 4, 29 })
            {
                data.AddRange(new[] {
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 1 }, // General Examination
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 6 }, // CBC
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 12 }, // Vaccination
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 14 }, // Injection
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 8 } // Throat Culture
                });
            }
            
            // Orthopedists (5, 30) - bone and joint procedures
            foreach (var doctorId in new[] { 5, 30 })
            {
                data.AddRange(new[] {
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 1 }, // General Examination
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 4 }, // X-Ray
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 19 }, // CT Scan
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 20 }, // MRI
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 25 }, // Bone Density
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 45 }, // Arthroscopy
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 46 }, // Joint Injection
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 74 } // Knee Arthroscopy
                });
            }
            
            // Dermatologists (6, 31) - skin procedures
            foreach (var doctorId in new[] { 6, 31 })
            {
                data.AddRange(new[] {
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 1 }, // General Examination
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 26 }, // Skin Biopsy
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 27 }, // Mole Removal
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 91 }, // Botox
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 93 }, // Chemical Peel
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 94 } // Microdermabrasion
                });
            }
            
            // Neurologists (7, 32) - brain and nerve procedures
            foreach (var doctorId in new[] { 7, 32 })
            {
                data.AddRange(new[] {
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 1 }, // General Examination
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 19 }, // CT Scan
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 20 }, // MRI
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 42 }, // EEG
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 43 }, // EMG
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 44 }, // Nerve Conduction
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 51 } // Lumbar Puncture
                });
            }
            
            // Ophthalmologists (8, 33) - eye procedures
            foreach (var doctorId in new[] { 8, 33 })
            {
                data.AddRange(new[] {
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 9 }, // Eye Examination
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 59 }, // Cataract Surgery
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 60 }, // LASIK
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 93 }, // Glasses
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 94 } // Contact Lenses
                });
            }
            
            // ENT Specialists (9, 34) - ear, nose, throat procedures
            foreach (var doctorId in new[] { 9, 34 })
            {
                data.AddRange(new[] {
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 1 }, // General Examination
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 8 }, // Throat Culture
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 10 }, // Ear Examination
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 61 }, // Hearing Test
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 62 }, // Tympanometry
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 63 }, // Tonsillectomy
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 64 } // Adenoidectomy
                });
            }
            
            // General Surgeons (10, 35) - surgical procedures
            foreach (var doctorId in new[] { 10, 35 })
            {
                data.AddRange(new[] {
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 1 }, // General Examination
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 15 }, // Minor Surgery
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 55 }, // Angioplasty
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 65 }, // Appendectomy
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 66 }, // Cholecystectomy
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 67 }, // Hernia Repair
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 73 } // Laparoscopy
                });
            }
            
            // OB/GYN (11, 36) - women's health
            foreach (var doctorId in new[] { 11, 36 })
            {
                data.AddRange(new[] {
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 1 }, // General Examination
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 21 }, // Mammogram
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 28 }, // Pap Smear
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 71 }, // D&C
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 72 }, // Hysteroscopy
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 70 } // Tubal Ligation
                });
            }
            
            // Psychiatrists (12, 37) - mental health
            foreach (var doctorId in new[] { 12, 37 })
            {
                data.AddRange(new[] {
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 1 }, // General Examination
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 6 }, // CBC
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 31 }, // Thyroid Function
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 41 } // Sleep Study
                });
            }
            
            // Emergency Medicine (13, 38) - emergency procedures
            foreach (var doctorId in new[] { 13, 38 })
            {
                data.AddRange(new[] {
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 1 }, // General Examination
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 2 }, // Blood Pressure
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 3 }, // ECG
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 4 }, // X-Ray
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 13 }, // Wound Dressing
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 14 }, // Injection
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 84 } // IV Therapy
                });
            }
            
            // Radiologists (14, 39) - imaging procedures
            foreach (var doctorId in new[] { 14, 39 })
            {
                data.AddRange(new[] {
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 4 }, // X-Ray
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 5 }, // Ultrasound
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 19 }, // CT Scan
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 20 }, // MRI
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 16 }, // Echocardiogram
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 21 }, // Mammogram
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 22 } // Colonoscopy
                });
            }
            
            // Anesthesiologists (15, 40) - anesthesia procedures
            foreach (var doctorId in new[] { 15, 40 })
            {
                data.AddRange(new[] {
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 1 }, // General Examination
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 2 }, // Blood Pressure
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 6 } // CBC
                });
            }
            
            // Oncologists (16, 41) - cancer procedures
            foreach (var doctorId in new[] { 16, 41 })
            {
                data.AddRange(new[] {
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 1 }, // General Examination
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 6 }, // CBC
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 19 }, // CT Scan
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 20 }, // MRI
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 52 }, // Biopsy
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 87 }, // Chemotherapy
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 88 } // Radiation Therapy
                });
            }
            
            // Endocrinologists (17, 42) - hormone procedures
            foreach (var doctorId in new[] { 17, 42 })
            {
                data.AddRange(new[] {
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 1 }, // General Examination
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 31 }, // Thyroid Function
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 33 }, // Hemoglobin A1C
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 36 }, // Vitamin D
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 37 } // B12 Test
                });
            }
            
            // Gastroenterologists (18, 43) - digestive procedures
            foreach (var doctorId in new[] { 18, 43 })
            {
                data.AddRange(new[] {
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 1 }, // General Examination
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 22 }, // Colonoscopy
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 23 }, // Endoscopy
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 24 }, // Sigmoidoscopy
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 5 }, // Abdominal Ultrasound
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 19 } // CT Scan
                });
            }
            
            // Pulmonologists (19, 44) - lung procedures
            foreach (var doctorId in new[] { 19, 44 })
            {
                data.AddRange(new[] {
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 1 }, // General Examination
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 4 }, // Chest X-Ray
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 11 }, // Spirometry
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 40 }, // Pulmonary Function
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 19 }, // CT Scan
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 41 } // Sleep Study
                });
            }
            
            // Urologists (20, 45) - urinary procedures
            foreach (var doctorId in new[] { 20, 45 })
            {
                data.AddRange(new[] {
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 1 }, // General Examination
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 7 }, // Urinalysis
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 29 }, // Prostate Exam
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 30 }, // PSA Test
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 50 }, // Cystoscopy
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 69 } // Vasectomy
                });
            }
            
            // Rheumatologists (21, 46) - joint and autoimmune procedures
            foreach (var doctorId in new[] { 21, 46 })
            {
                data.AddRange(new[] {
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 1 }, // General Examination
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 6 }, // CBC
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 4 }, // X-Ray
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 46 }, // Joint Injection
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 47 } // Cortisone Injection
                });
            }
            
            // Nephrologists (22, 47) - kidney procedures
            foreach (var doctorId in new[] { 22, 47 })
            {
                data.AddRange(new[] {
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 1 }, // General Examination
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 7 }, // Urinalysis
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 35 }, // Kidney Function
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 5 }, // Abdominal Ultrasound
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 89 }, // Dialysis
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 19 } // CT Scan
                });
            }
            
            // Hematologists (23, 48) - blood procedures
            foreach (var doctorId in new[] { 23, 48 })
            {
                data.AddRange(new[] {
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 1 }, // General Examination
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 6 }, // CBC
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 38 }, // Iron Studies
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 52 }, // Biopsy
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 85 } // Blood Transfusion
                });
            }
            
            // Infectious Disease (24, 49) - infection procedures
            foreach (var doctorId in new[] { 24, 49 })
            {
                data.AddRange(new[] {
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 1 }, // General Examination
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 6 }, // CBC
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 8 }, // Throat Culture
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 52 }, // Biopsy
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 51 } // Lumbar Puncture
                });
            }
            
            // Allergy and Immunology (25, 50) - allergy procedures
            foreach (var doctorId in new[] { 25, 50 })
            {
                data.AddRange(new[] {
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 1 }, // General Examination
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 39 }, // Allergy Testing
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 11 }, // Spirometry
                    new DoctorProcedure { Id = id++, DoctorId = doctorId, ProcedureId = 89 } // Allergy Shots
                });
            }
            
            return data;
        }
    }
}
