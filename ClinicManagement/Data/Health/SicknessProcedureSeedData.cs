using ClinicManagement.Models.Health;

namespace ClinicManagement.Data.Health
{
    public static class SicknessProcedureSeedData
    {
        public static List<SicknessProcedure> GetSeedData()
        {
            var data = new List<SicknessProcedure>();
            
            // Common Cold (1)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 1, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 1, ProcedureId = 8 } // Throat Culture
            });
            
            // Influenza (2)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 2, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 2, ProcedureId = 6 } // Complete Blood Count
            });
            
            // Hypertension (3)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 3, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 3, ProcedureId = 2 }, // Blood Pressure Measurement
                new SicknessProcedure { SicknessId = 3, ProcedureId = 3 }, // ECG
                new SicknessProcedure { SicknessId = 3, ProcedureId = 6 }, // Complete Blood Count
                new SicknessProcedure { SicknessId = 3, ProcedureId = 32 } // Lipid Panel
            });
            
            // Bronchitis (4)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 4, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 4, ProcedureId = 4 }, // Chest X-Ray
                new SicknessProcedure { SicknessId = 4, ProcedureId = 11 } // Spirometry
            });
            
            // Pneumonia (5)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 5, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 5, ProcedureId = 4 }, // Chest X-Ray
                new SicknessProcedure { SicknessId = 5, ProcedureId = 6 }, // Complete Blood Count
                new SicknessProcedure { SicknessId = 5, ProcedureId = 8 } // Throat Culture
            });
            
            // Gastritis (6)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 6, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 6, ProcedureId = 5 }, // Abdominal Ultrasound
                new SicknessProcedure { SicknessId = 6, ProcedureId = 6 }, // Complete Blood Count
                new SicknessProcedure { SicknessId = 6, ProcedureId = 23 } // Endoscopy
            });
            
            // Migraine (7)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 7, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 7, ProcedureId = 6 }, // Complete Blood Count
                new SicknessProcedure { SicknessId = 7, ProcedureId = 42 } // EEG
            });
            
            // Osteoarthritis (8)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 8, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 8, ProcedureId = 4 }, // X-Ray
                new SicknessProcedure { SicknessId = 8, ProcedureId = 25 } // Bone Density Scan
            });
            
            // Dermatitis (9)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 9, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 9, ProcedureId = 26 } // Skin Biopsy
            });
            
            // Type 2 Diabetes (10)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 10, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 10, ProcedureId = 2 }, // Blood Pressure Measurement
                new SicknessProcedure { SicknessId = 10, ProcedureId = 6 }, // Complete Blood Count
                new SicknessProcedure { SicknessId = 10, ProcedureId = 33 }, // Hemoglobin A1C
                new SicknessProcedure { SicknessId = 10, ProcedureId = 9 } // Comprehensive Eye Examination
            });
            
            // Asthma (11)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 11, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 11, ProcedureId = 11 }, // Spirometry
                new SicknessProcedure { SicknessId = 11, ProcedureId = 40 }, // Pulmonary Function Test
                new SicknessProcedure { SicknessId = 11, ProcedureId = 39 } // Allergy Testing
            });
            
            // Sinusitis (12)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 12, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 12, ProcedureId = 4 }, // X-Ray
                new SicknessProcedure { SicknessId = 12, ProcedureId = 19 } // CT Scan
            });
            
            // Otitis Media (13)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 13, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 13, ProcedureId = 10 }, // Otoscopic Examination
                new SicknessProcedure { SicknessId = 13, ProcedureId = 62 } // Tympanometry
            });
            
            // Conjunctivitis (14)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 14, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 14, ProcedureId = 9 } // Comprehensive Eye Examination
            });
            
            // UTI (15)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 15, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 15, ProcedureId = 7 }, // Urinalysis
                new SicknessProcedure { SicknessId = 15, ProcedureId = 6 } // Complete Blood Count
            });
            
            // Gastroenteritis (16)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 16, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 16, ProcedureId = 6 }, // Complete Blood Count
                new SicknessProcedure { SicknessId = 16, ProcedureId = 7 } // Urinalysis
            });
            
            // Seasonal Allergies (17)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 17, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 17, ProcedureId = 39 } // Allergy Testing
            });
            
            // Rheumatoid Arthritis (18)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 18, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 18, ProcedureId = 6 }, // Complete Blood Count
                new SicknessProcedure { SicknessId = 18, ProcedureId = 4 } // X-Ray
            });
            
            // Fibromyalgia (19)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 19, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 19, ProcedureId = 6 }, // Complete Blood Count
                new SicknessProcedure { SicknessId = 19, ProcedureId = 36 } // Vitamin D Test
            });
            
            // Chronic Fatigue Syndrome (20)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 20, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 20, ProcedureId = 6 }, // Complete Blood Count
                new SicknessProcedure { SicknessId = 20, ProcedureId = 31 } // Thyroid Function Test
            });
            
            // Sleep Apnea (21)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 21, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 21, ProcedureId = 41 } // Sleep Study
            });
            
            // Depression (22)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 22, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 22, ProcedureId = 6 }, // Complete Blood Count
                new SicknessProcedure { SicknessId = 22, ProcedureId = 31 } // Thyroid Function Test
            });
            
            // Anxiety Disorder (23)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 23, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 23, ProcedureId = 2 }, // Blood Pressure Measurement
                new SicknessProcedure { SicknessId = 23, ProcedureId = 3 } // ECG
            });
            
            // High Cholesterol (24)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 24, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 24, ProcedureId = 32 } // Lipid Panel
            });
            
            // Coronary Artery Disease (25)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 25, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 25, ProcedureId = 2 }, // Blood Pressure Measurement
                new SicknessProcedure { SicknessId = 25, ProcedureId = 3 }, // ECG
                new SicknessProcedure { SicknessId = 25, ProcedureId = 16 }, // Echocardiogram
                new SicknessProcedure { SicknessId = 25, ProcedureId = 17 }, // Stress Test
                new SicknessProcedure { SicknessId = 25, ProcedureId = 54 } // Cardiac Catheterization
            });
            
            // Heart Failure (26)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 26, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 26, ProcedureId = 3 }, // ECG
                new SicknessProcedure { SicknessId = 26, ProcedureId = 16 }, // Echocardiogram
                new SicknessProcedure { SicknessId = 26, ProcedureId = 4 }, // Chest X-Ray
                new SicknessProcedure { SicknessId = 26, ProcedureId = 6 } // Complete Blood Count
            });
            
            // Atrial Fibrillation (27)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 27, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 27, ProcedureId = 3 }, // ECG
                new SicknessProcedure { SicknessId = 27, ProcedureId = 18 }, // Holter Monitor
                new SicknessProcedure { SicknessId = 27, ProcedureId = 16 } // Echocardiogram
            });
            
            // COPD (28)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 28, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 28, ProcedureId = 4 }, // Chest X-Ray
                new SicknessProcedure { SicknessId = 28, ProcedureId = 11 }, // Spirometry
                new SicknessProcedure { SicknessId = 28, ProcedureId = 40 } // Pulmonary Function Test
            });
            
            // Emphysema (29)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 29, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 29, ProcedureId = 4 }, // Chest X-Ray
                new SicknessProcedure { SicknessId = 29, ProcedureId = 19 }, // CT Scan
                new SicknessProcedure { SicknessId = 29, ProcedureId = 40 } // Pulmonary Function Test
            });
            
            // Chronic Kidney Disease (30)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 30, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 30, ProcedureId = 6 }, // Complete Blood Count
                new SicknessProcedure { SicknessId = 30, ProcedureId = 7 }, // Urinalysis
                new SicknessProcedure { SicknessId = 30, ProcedureId = 35 } // Kidney Function Test
            });
            
            // Kidney Stones (31)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 31, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 31, ProcedureId = 7 }, // Urinalysis
                new SicknessProcedure { SicknessId = 31, ProcedureId = 5 }, // Abdominal Ultrasound
                new SicknessProcedure { SicknessId = 31, ProcedureId = 19 } // CT Scan
            });
            
            // Gout (32)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 32, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 32, ProcedureId = 6 }, // Complete Blood Count
                new SicknessProcedure { SicknessId = 32, ProcedureId = 7 } // Urinalysis
            });
            
            // Osteoporosis (33)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 33, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 33, ProcedureId = 25 }, // Bone Density Scan
                new SicknessProcedure { SicknessId = 33, ProcedureId = 36 } // Vitamin D Test
            });
            
            // Hypothyroidism (34)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 34, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 34, ProcedureId = 31 }, // Thyroid Function Test
                new SicknessProcedure { SicknessId = 34, ProcedureId = 6 } // Complete Blood Count
            });
            
            // Hyperthyroidism (35)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 35, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 35, ProcedureId = 31 }, // Thyroid Function Test
                new SicknessProcedure { SicknessId = 35, ProcedureId = 3 } // ECG
            });
            
            // IBS (36)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 36, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 36, ProcedureId = 6 }, // Complete Blood Count
                new SicknessProcedure { SicknessId = 36, ProcedureId = 22 } // Colonoscopy
            });
            
            // Crohn's Disease (37)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 37, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 37, ProcedureId = 22 }, // Colonoscopy
                new SicknessProcedure { SicknessId = 37, ProcedureId = 19 }, // CT Scan
                new SicknessProcedure { SicknessId = 37, ProcedureId = 6 } // Complete Blood Count
            });
            
            // Ulcerative Colitis (38)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 38, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 38, ProcedureId = 22 }, // Colonoscopy
                new SicknessProcedure { SicknessId = 38, ProcedureId = 6 } // Complete Blood Count
            });
            
            // Peptic Ulcer Disease (39)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 39, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 39, ProcedureId = 23 }, // Endoscopy
                new SicknessProcedure { SicknessId = 39, ProcedureId = 6 } // Complete Blood Count
            });
            
            // GERD (40)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 40, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 40, ProcedureId = 23 }, // Endoscopy
            });
            
            // Add more relationships for remaining sicknesses (41-100)
            // Hepatitis (41)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 41, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 41, ProcedureId = 6 }, // Complete Blood Count
                new SicknessProcedure { SicknessId = 41, ProcedureId = 34 } // Liver Function Test
            });
            
            // Cirrhosis (42)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 42, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 42, ProcedureId = 34 }, // Liver Function Test
                new SicknessProcedure { SicknessId = 42, ProcedureId = 5 }, // Abdominal Ultrasound
                new SicknessProcedure { SicknessId = 42, ProcedureId = 52 } // Biopsy
            });
            
            // Gallstones (43)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 43, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 43, ProcedureId = 5 }, // Abdominal Ultrasound
                new SicknessProcedure { SicknessId = 43, ProcedureId = 19 } // CT Scan
            });
            
            // Pancreatitis (44)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 44, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 44, ProcedureId = 6 }, // Complete Blood Count
                new SicknessProcedure { SicknessId = 44, ProcedureId = 34 }, // Liver Function Test
                new SicknessProcedure { SicknessId = 44, ProcedureId = 19 } // CT Scan
            });
            
            // Appendicitis (45)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 45, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 45, ProcedureId = 6 }, // Complete Blood Count
                new SicknessProcedure { SicknessId = 45, ProcedureId = 19 } // CT Scan
            });
            
            // Diverticulitis (46)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 46, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 46, ProcedureId = 19 }, // CT Scan
                new SicknessProcedure { SicknessId = 46, ProcedureId = 22 } // Colonoscopy
            });
            
            // Hemorrhoids (47)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 47, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 47, ProcedureId = 24 } // Sigmoidoscopy
            });
            
            // Eczema (48)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 48, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 48, ProcedureId = 26 } // Skin Biopsy
            });
            
            // Psoriasis (49)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 49, ProcedureId = 1 }, // General Physical Examination
                new SicknessProcedure { SicknessId = 49, ProcedureId = 26 } // Skin Biopsy
            });
            
            // Acne Vulgaris (50)
            data.AddRange(new[] {
                new SicknessProcedure { SicknessId = 50, ProcedureId = 1 } // General Physical Examination
            });
            
            // Continue with more relationships for remaining sicknesses...
            
            return data;
        }
    }
}
