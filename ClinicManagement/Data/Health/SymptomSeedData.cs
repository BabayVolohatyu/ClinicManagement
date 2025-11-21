using ClinicManagement.Models.Health;

namespace ClinicManagement.Data.Health
{
    public static class SymptomSeedData
    {
        public static List<Symptom> GetSeedData()
        {
            return new List<Symptom>
            {
                new Symptom { Id = 1, Name = "Fever" },
                new Symptom { Id = 2, Name = "Headache" },
                new Symptom { Id = 3, Name = "Cough" },
                new Symptom { Id = 4, Name = "Sore Throat" },
                new Symptom { Id = 5, Name = "Runny Nose" },
                new Symptom { Id = 6, Name = "Chest Pain" },
                new Symptom { Id = 7, Name = "Shortness of Breath" },
                new Symptom { Id = 8, Name = "Nausea" },
                new Symptom { Id = 9, Name = "Vomiting" },
                new Symptom { Id = 10, Name = "Diarrhea" },
                new Symptom { Id = 11, Name = "Abdominal Pain" },
                new Symptom { Id = 12, Name = "Dizziness" },
                new Symptom { Id = 13, Name = "Fatigue" },
                new Symptom { Id = 14, Name = "Joint Pain" },
                new Symptom { Id = 15, Name = "Muscle Pain" },
                new Symptom { Id = 16, Name = "Rash" },
                new Symptom { Id = 17, Name = "Itching" },
                new Symptom { Id = 18, Name = "Back Pain" },
                new Symptom { Id = 19, Name = "Insomnia" },
                new Symptom { Id = 20, Name = "Loss of Appetite" }
            };
        }
    }
}

