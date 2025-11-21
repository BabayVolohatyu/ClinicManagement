using ClinicManagement.Models.Health;

namespace ClinicManagement.Data.Health
{
    public static class SicknessProcedureSeedData
    {
        public static List<SicknessProcedure> GetSeedData()
        {
            return new List<SicknessProcedure>
            {
                // Common Cold
                new SicknessProcedure { SicknessId = 1, ProcedureId = 1 }, // General Examination
                new SicknessProcedure { SicknessId = 1, ProcedureId = 8 }, // Throat Swab
                
                // Influenza
                new SicknessProcedure { SicknessId = 2, ProcedureId = 1 }, // General Examination
                new SicknessProcedure { SicknessId = 2, ProcedureId = 6 }, // Blood Test
                
                // Hypertension
                new SicknessProcedure { SicknessId = 3, ProcedureId = 1 }, // General Examination
                new SicknessProcedure { SicknessId = 3, ProcedureId = 2 }, // Blood Pressure Measurement
                new SicknessProcedure { SicknessId = 3, ProcedureId = 3 }, // ECG
                new SicknessProcedure { SicknessId = 3, ProcedureId = 6 }, // Blood Test
                
                // Bronchitis
                new SicknessProcedure { SicknessId = 4, ProcedureId = 1 }, // General Examination
                new SicknessProcedure { SicknessId = 4, ProcedureId = 4 }, // X-Ray Chest
                new SicknessProcedure { SicknessId = 4, ProcedureId = 11 }, // Spirometry
                
                // Pneumonia
                new SicknessProcedure { SicknessId = 5, ProcedureId = 1 }, // General Examination
                new SicknessProcedure { SicknessId = 5, ProcedureId = 4 }, // X-Ray Chest
                new SicknessProcedure { SicknessId = 5, ProcedureId = 6 }, // Blood Test
                
                // Gastritis
                new SicknessProcedure { SicknessId = 6, ProcedureId = 1 }, // General Examination
                new SicknessProcedure { SicknessId = 6, ProcedureId = 5 }, // Ultrasound Abdomen
                new SicknessProcedure { SicknessId = 6, ProcedureId = 6 }, // Blood Test
                
                // Migraine
                new SicknessProcedure { SicknessId = 7, ProcedureId = 1 }, // General Examination
                new SicknessProcedure { SicknessId = 7, ProcedureId = 6 }, // Blood Test
                
                // Arthritis
                new SicknessProcedure { SicknessId = 8, ProcedureId = 1 }, // General Examination
                new SicknessProcedure { SicknessId = 8, ProcedureId = 4 }, // X-Ray
                
                // Dermatitis
                new SicknessProcedure { SicknessId = 9, ProcedureId = 1 }, // General Examination
                
                // Sinusitis
                new SicknessProcedure { SicknessId = 12, ProcedureId = 1 }, // General Examination
                new SicknessProcedure { SicknessId = 12, ProcedureId = 10 } // Ear Examination
            };
        }
    }
}

