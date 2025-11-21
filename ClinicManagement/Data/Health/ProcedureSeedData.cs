using ClinicManagement.Models.Health;

namespace ClinicManagement.Data.Health
{
    public static class ProcedureSeedData
    {
        public static List<Procedure> GetSeedData()
        {
            return new List<Procedure>
            {
                new Procedure { Id = 1, Name = "General Examination", Description = "Complete physical examination including vital signs, heart, lungs, and abdomen", Price = 1500.00f },
                new Procedure { Id = 2, Name = "Blood Pressure Measurement", Description = "Measurement of systolic and diastolic blood pressure", Price = 300.00f },
                new Procedure { Id = 3, Name = "ECG (Electrocardiogram)", Description = "Recording of electrical activity of the heart", Price = 2500.00f },
                new Procedure { Id = 4, Name = "X-Ray Chest", Description = "Radiographic examination of the chest", Price = 2000.00f },
                new Procedure { Id = 5, Name = "Ultrasound Abdomen", Description = "Ultrasound examination of abdominal organs", Price = 3000.00f },
                new Procedure { Id = 6, Name = "Blood Test (Complete)", Description = "Complete blood count and basic biochemistry", Price = 1800.00f },
                new Procedure { Id = 7, Name = "Urine Analysis", Description = "Laboratory analysis of urine sample", Price = 500.00f },
                new Procedure { Id = 8, Name = "Throat Swab", Description = "Collection and analysis of throat culture", Price = 800.00f },
                new Procedure { Id = 9, Name = "Eye Examination", Description = "Comprehensive eye examination including vision test", Price = 1200.00f },
                new Procedure { Id = 10, Name = "Ear Examination", Description = "Otoscopic examination of the ear", Price = 700.00f },
                new Procedure { Id = 11, Name = "Spirometry", Description = "Lung function test to measure breathing capacity", Price = 1500.00f },
                new Procedure { Id = 12, Name = "Vaccination", Description = "Administration of vaccine", Price = 1000.00f },
                new Procedure { Id = 13, Name = "Wound Dressing", Description = "Cleaning and dressing of wounds", Price = 600.00f },
                new Procedure { Id = 14, Name = "Injection", Description = "Intramuscular or intravenous injection", Price = 400.00f },
                new Procedure { Id = 15, Name = "Minor Surgery", Description = "Minor surgical procedure", Price = 5000.00f }
            };
        }
    }
}

