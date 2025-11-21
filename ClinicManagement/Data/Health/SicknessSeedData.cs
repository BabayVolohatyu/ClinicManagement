using ClinicManagement.Models.Health;

namespace ClinicManagement.Data.Health
{
    public static class SicknessSeedData
    {
        public static List<Sickness> GetSeedData()
        {
            return new List<Sickness>
            {
                new Sickness { Id = 1, Name = "Common Cold" },
                new Sickness { Id = 2, Name = "Influenza" },
                new Sickness { Id = 3, Name = "Hypertension" },
                new Sickness { Id = 4, Name = "Bronchitis" },
                new Sickness { Id = 5, Name = "Pneumonia" },
                new Sickness { Id = 6, Name = "Gastritis" },
                new Sickness { Id = 7, Name = "Migraine" },
                new Sickness { Id = 8, Name = "Arthritis" },
                new Sickness { Id = 9, Name = "Dermatitis" },
                new Sickness { Id = 10, Name = "Diabetes Type 2" },
                new Sickness { Id = 11, Name = "Asthma" },
                new Sickness { Id = 12, Name = "Sinusitis" },
                new Sickness { Id = 13, Name = "Otitis Media" },
                new Sickness { Id = 14, Name = "Conjunctivitis" },
                new Sickness { Id = 15, Name = "Urinary Tract Infection" }
            };
        }
    }
}

