using ClinicManagement.Models.Humans;

namespace ClinicManagement.Data.Humans
{
    public static class SpecialtySeedData
    {
        public static List<Specialty> GetSeedData()
        {
            return new List<Specialty>
            {
                new Specialty { Id = 1, Name = "Family Medicine" },
                new Specialty { Id = 2, Name = "Internal Medicine" },
                new Specialty { Id = 3, Name = "Cardiology" },
                new Specialty { Id = 4, Name = "Pediatrics" },
                new Specialty { Id = 5, Name = "Orthopedics" },
                new Specialty { Id = 6, Name = "Dermatology" },
                new Specialty { Id = 7, Name = "Neurology" },
                new Specialty { Id = 8, Name = "Ophthalmology" },
                new Specialty { Id = 9, Name = "ENT (Ear, Nose, Throat)" },
                new Specialty { Id = 10, Name = "General Surgery" },
                new Specialty { Id = 11, Name = "Obstetrics and Gynecology" },
                new Specialty { Id = 12, Name = "Psychiatry" },
                new Specialty { Id = 13, Name = "Emergency Medicine" },
                new Specialty { Id = 14, Name = "Radiology" },
                new Specialty { Id = 15, Name = "Anesthesiology" },
                new Specialty { Id = 16, Name = "Oncology" },
                new Specialty { Id = 17, Name = "Endocrinology" },
                new Specialty { Id = 18, Name = "Gastroenterology" },
                new Specialty { Id = 19, Name = "Pulmonology" },
                new Specialty { Id = 20, Name = "Urology" },
                new Specialty { Id = 21, Name = "Rheumatology" },
                new Specialty { Id = 22, Name = "Nephrology" },
                new Specialty { Id = 23, Name = "Hematology" },
                new Specialty { Id = 24, Name = "Infectious Disease" },
                new Specialty { Id = 25, Name = "Allergy and Immunology" }
            };
        }
    }
}
