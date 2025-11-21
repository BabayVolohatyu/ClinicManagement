using ClinicManagement.Models.Humans;

namespace ClinicManagement.Data.Humans
{
    public static class SpecialtySeedData
    {
        public static List<Specialty> GetSeedData()
        {
            return new List<Specialty>
            {
                new Specialty { Id = 1, Name = "General Practitioner" },
                new Specialty { Id = 2, Name = "Cardiologist" },
                new Specialty { Id = 3, Name = "Pediatrician" },
                new Specialty { Id = 4, Name = "Neurologist" },
                new Specialty { Id = 5, Name = "Dermatologist" },
                new Specialty { Id = 6, Name = "Orthopedist" },
                new Specialty { Id = 7, Name = "Ophthalmologist" },
                new Specialty { Id = 8, Name = "ENT Specialist" },
                new Specialty { Id = 9, Name = "Gynecologist" },
                new Specialty { Id = 10, Name = "Surgeon" }
            };
        }
    }
}

