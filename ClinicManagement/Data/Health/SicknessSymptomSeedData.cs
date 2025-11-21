using ClinicManagement.Models.Health;

namespace ClinicManagement.Data.Health
{
    public static class SicknessSymptomSeedData
    {
        public static List<SicknessSymptom> GetSeedData()
        {
            return new List<SicknessSymptom>
            {
                // Common Cold
                new SicknessSymptom { SicknessId = 1, SymptomId = 1 }, // Fever
                new SicknessSymptom { SicknessId = 1, SymptomId = 2 }, // Headache
                new SicknessSymptom { SicknessId = 1, SymptomId = 3 }, // Cough
                new SicknessSymptom { SicknessId = 1, SymptomId = 4 }, // Sore Throat
                new SicknessSymptom { SicknessId = 1, SymptomId = 5 }, // Runny Nose
                
                // Influenza
                new SicknessSymptom { SicknessId = 2, SymptomId = 1 }, // Fever
                new SicknessSymptom { SicknessId = 2, SymptomId = 2 }, // Headache
                new SicknessSymptom { SicknessId = 2, SymptomId = 3 }, // Cough
                new SicknessSymptom { SicknessId = 2, SymptomId = 13 }, // Fatigue
                new SicknessSymptom { SicknessId = 2, SymptomId = 15 }, // Muscle Pain
                
                // Hypertension
                new SicknessSymptom { SicknessId = 3, SymptomId = 2 }, // Headache
                new SicknessSymptom { SicknessId = 3, SymptomId = 12 }, // Dizziness
                
                // Bronchitis
                new SicknessSymptom { SicknessId = 4, SymptomId = 3 }, // Cough
                new SicknessSymptom { SicknessId = 4, SymptomId = 7 }, // Shortness of Breath
                new SicknessSymptom { SicknessId = 4, SymptomId = 1 }, // Fever
                
                // Pneumonia
                new SicknessSymptom { SicknessId = 5, SymptomId = 1 }, // Fever
                new SicknessSymptom { SicknessId = 5, SymptomId = 3 }, // Cough
                new SicknessSymptom { SicknessId = 5, SymptomId = 6 }, // Chest Pain
                new SicknessSymptom { SicknessId = 5, SymptomId = 7 }, // Shortness of Breath
                
                // Gastritis
                new SicknessSymptom { SicknessId = 6, SymptomId = 8 }, // Nausea
                new SicknessSymptom { SicknessId = 6, SymptomId = 11 }, // Abdominal Pain
                new SicknessSymptom { SicknessId = 6, SymptomId = 20 }, // Loss of Appetite
                
                // Migraine
                new SicknessSymptom { SicknessId = 7, SymptomId = 2 }, // Headache
                new SicknessSymptom { SicknessId = 7, SymptomId = 8 }, // Nausea
                new SicknessSymptom { SicknessId = 7, SymptomId = 12 }, // Dizziness
                
                // Arthritis
                new SicknessSymptom { SicknessId = 8, SymptomId = 14 }, // Joint Pain
                new SicknessSymptom { SicknessId = 8, SymptomId = 15 }, // Muscle Pain
                
                // Dermatitis
                new SicknessSymptom { SicknessId = 9, SymptomId = 16 }, // Rash
                new SicknessSymptom { SicknessId = 9, SymptomId = 17 }, // Itching
                
                // Sinusitis
                new SicknessSymptom { SicknessId = 12, SymptomId = 2 }, // Headache
                new SicknessSymptom { SicknessId = 12, SymptomId = 5 }, // Runny Nose
                new SicknessSymptom { SicknessId = 12, SymptomId = 4 }  // Sore Throat
            };
        }
    }
}

