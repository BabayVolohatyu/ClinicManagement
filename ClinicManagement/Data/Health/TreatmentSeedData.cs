using ClinicManagement.Models.Health;

namespace ClinicManagement.Data.Health
{
    public static class TreatmentSeedData
    {
        public static List<Treatment> GetSeedData()
        {
            return new List<Treatment>
            {
                new Treatment { Id = 1, Name = "Rest and Hydration" },
                new Treatment { Id = 2, Name = "Antibiotic Therapy" },
                new Treatment { Id = 3, Name = "Antiviral Medication" },
                new Treatment { Id = 4, Name = "Pain Relief Medication" },
                new Treatment { Id = 5, Name = "Antihistamine" },
                new Treatment { Id = 6, Name = "Bronchodilator" },
                new Treatment { Id = 7, Name = "Anti-inflammatory Medication" },
                new Treatment { Id = 8, Name = "Blood Pressure Medication" },
                new Treatment { Id = 9, Name = "Insulin Therapy" },
                new Treatment { Id = 10, Name = "Physical Therapy" },
                new Treatment { Id = 11, Name = "Diet Modification" },
                new Treatment { Id = 12, Name = "Lifestyle Changes" },
                new Treatment { Id = 13, Name = "Topical Cream" },
                new Treatment { Id = 14, Name = "Eye Drops" },
                new Treatment { Id = 15, Name = "Nasal Spray" }
            };
        }
    }
}

