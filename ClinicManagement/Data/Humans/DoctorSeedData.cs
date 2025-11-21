using ClinicManagement.Models.Humans;

namespace ClinicManagement.Data.Humans
{
    public static class DoctorSeedData
    {
        public static List<Doctor> GetSeedData()
        {
            return new List<Doctor>
            {
                // Doctors (PersonId 1-8, SpecialtyId 1-10)
                new Doctor { Id = 1, PersonId = 1, SpecialtyId = 1 }, // General Practitioner
                new Doctor { Id = 2, PersonId = 2, SpecialtyId = 2 }, // Cardiologist
                new Doctor { Id = 3, PersonId = 3, SpecialtyId = 3 }, // Pediatrician
                new Doctor { Id = 4, PersonId = 4, SpecialtyId = 4 }, // Neurologist
                new Doctor { Id = 5, PersonId = 5, SpecialtyId = 5 }, // Dermatologist
                new Doctor { Id = 6, PersonId = 6, SpecialtyId = 6 }, // Orthopedist
                new Doctor { Id = 7, PersonId = 7, SpecialtyId = 7 }, // Ophthalmologist
                new Doctor { Id = 8, PersonId = 8, SpecialtyId = 8 }  // ENT Specialist
            };
        }
    }
}

