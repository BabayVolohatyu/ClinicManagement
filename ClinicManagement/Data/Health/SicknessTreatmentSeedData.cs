using ClinicManagement.Models.Health;

namespace ClinicManagement.Data.Health
{
    public static class SicknessTreatmentSeedData
    {
        public static List<SicknessTreatment> GetSeedData()
        {
            return new List<SicknessTreatment>
            {
                // Common Cold
                new SicknessTreatment { SicknessId = 1, TreatmentId = 1 }, // Rest and Hydration
                new SicknessTreatment { SicknessId = 1, TreatmentId = 4 }, // Pain Relief Medication
                new SicknessTreatment { SicknessId = 1, TreatmentId = 5 }, // Antihistamine
                
                // Influenza
                new SicknessTreatment { SicknessId = 2, TreatmentId = 1 }, // Rest and Hydration
                new SicknessTreatment { SicknessId = 2, TreatmentId = 3 }, // Antiviral Medication
                new SicknessTreatment { SicknessId = 2, TreatmentId = 4 }, // Pain Relief Medication
                
                // Hypertension
                new SicknessTreatment { SicknessId = 3, TreatmentId = 8 }, // Blood Pressure Medication
                new SicknessTreatment { SicknessId = 3, TreatmentId = 11 }, // Diet Modification
                new SicknessTreatment { SicknessId = 3, TreatmentId = 12 }, // Lifestyle Changes
                
                // Bronchitis
                new SicknessTreatment { SicknessId = 4, TreatmentId = 2 }, // Antibiotic Therapy
                new SicknessTreatment { SicknessId = 4, TreatmentId = 6 }, // Bronchodilator
                new SicknessTreatment { SicknessId = 4, TreatmentId = 4 }, // Pain Relief Medication
                
                // Pneumonia
                new SicknessTreatment { SicknessId = 5, TreatmentId = 2 }, // Antibiotic Therapy
                new SicknessTreatment { SicknessId = 5, TreatmentId = 1 }, // Rest and Hydration
                new SicknessTreatment { SicknessId = 5, TreatmentId = 4 }, // Pain Relief Medication
                
                // Gastritis
                new SicknessTreatment { SicknessId = 6, TreatmentId = 7 }, // Anti-inflammatory Medication
                new SicknessTreatment { SicknessId = 6, TreatmentId = 11 }, // Diet Modification
                new SicknessTreatment { SicknessId = 6, TreatmentId = 4 }, // Pain Relief Medication
                
                // Migraine
                new SicknessTreatment { SicknessId = 7, TreatmentId = 4 }, // Pain Relief Medication
                new SicknessTreatment { SicknessId = 7, TreatmentId = 1 }, // Rest and Hydration
                
                // Arthritis
                new SicknessTreatment { SicknessId = 8, TreatmentId = 7 }, // Anti-inflammatory Medication
                new SicknessTreatment { SicknessId = 8, TreatmentId = 10 }, // Physical Therapy
                
                // Dermatitis
                new SicknessTreatment { SicknessId = 9, TreatmentId = 13 }, // Topical Cream
                new SicknessTreatment { SicknessId = 9, TreatmentId = 5 }, // Antihistamine
                
                // Sinusitis
                new SicknessTreatment { SicknessId = 12, TreatmentId = 2 }, // Antibiotic Therapy
                new SicknessTreatment { SicknessId = 12, TreatmentId = 15 }, // Nasal Spray
                new SicknessTreatment { SicknessId = 12, TreatmentId = 4 }  // Pain Relief Medication
            };
        }
    }
}

