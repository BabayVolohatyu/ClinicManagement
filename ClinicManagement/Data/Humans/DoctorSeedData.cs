using ClinicManagement.Models.Humans;

namespace ClinicManagement.Data.Humans
{
    public static class DoctorSeedData
    {
        public static List<Doctor> GetSeedData()
        {
            return new List<Doctor>
            {
                // Doctors (PersonId 1-50, SpecialtyId 1-25)
                new Doctor { Id = 1, PersonId = 1, SpecialtyId = 1 }, // Family Medicine
                new Doctor { Id = 2, PersonId = 2, SpecialtyId = 2 }, // Internal Medicine
                new Doctor { Id = 3, PersonId = 3, SpecialtyId = 3 }, // Cardiology
                new Doctor { Id = 4, PersonId = 4, SpecialtyId = 4 }, // Pediatrics
                new Doctor { Id = 5, PersonId = 5, SpecialtyId = 5 }, // Orthopedics
                new Doctor { Id = 6, PersonId = 6, SpecialtyId = 6 }, // Dermatology
                new Doctor { Id = 7, PersonId = 7, SpecialtyId = 7 }, // Neurology
                new Doctor { Id = 8, PersonId = 8, SpecialtyId = 8 }, // Ophthalmology
                new Doctor { Id = 9, PersonId = 9, SpecialtyId = 9 }, // ENT
                new Doctor { Id = 10, PersonId = 10, SpecialtyId = 10 }, // General Surgery
                new Doctor { Id = 11, PersonId = 11, SpecialtyId = 11 }, // OB/GYN
                new Doctor { Id = 12, PersonId = 12, SpecialtyId = 12 }, // Psychiatry
                new Doctor { Id = 13, PersonId = 13, SpecialtyId = 13 }, // Emergency Medicine
                new Doctor { Id = 14, PersonId = 14, SpecialtyId = 14 }, // Radiology
                new Doctor { Id = 15, PersonId = 15, SpecialtyId = 15 }, // Anesthesiology
                new Doctor { Id = 16, PersonId = 16, SpecialtyId = 16 }, // Oncology
                new Doctor { Id = 17, PersonId = 17, SpecialtyId = 17 }, // Endocrinology
                new Doctor { Id = 18, PersonId = 18, SpecialtyId = 18 }, // Gastroenterology
                new Doctor { Id = 19, PersonId = 19, SpecialtyId = 19 }, // Pulmonology
                new Doctor { Id = 20, PersonId = 20, SpecialtyId = 20 }, // Urology
                new Doctor { Id = 21, PersonId = 21, SpecialtyId = 21 }, // Rheumatology
                new Doctor { Id = 22, PersonId = 22, SpecialtyId = 22 }, // Nephrology
                new Doctor { Id = 23, PersonId = 23, SpecialtyId = 23 }, // Hematology
                new Doctor { Id = 24, PersonId = 24, SpecialtyId = 24 }, // Infectious Disease
                new Doctor { Id = 25, PersonId = 25, SpecialtyId = 25 }, // Allergy and Immunology
                new Doctor { Id = 26, PersonId = 26, SpecialtyId = 1 }, // Family Medicine
                new Doctor { Id = 27, PersonId = 27, SpecialtyId = 2 }, // Internal Medicine
                new Doctor { Id = 28, PersonId = 28, SpecialtyId = 3 }, // Cardiology
                new Doctor { Id = 29, PersonId = 29, SpecialtyId = 4 }, // Pediatrics
                new Doctor { Id = 30, PersonId = 30, SpecialtyId = 5 }, // Orthopedics
                new Doctor { Id = 31, PersonId = 31, SpecialtyId = 6 }, // Dermatology
                new Doctor { Id = 32, PersonId = 32, SpecialtyId = 7 }, // Neurology
                new Doctor { Id = 33, PersonId = 33, SpecialtyId = 8 }, // Ophthalmology
                new Doctor { Id = 34, PersonId = 34, SpecialtyId = 9 }, // ENT
                new Doctor { Id = 35, PersonId = 35, SpecialtyId = 10 }, // General Surgery
                new Doctor { Id = 36, PersonId = 36, SpecialtyId = 11 }, // OB/GYN
                new Doctor { Id = 37, PersonId = 37, SpecialtyId = 12 }, // Psychiatry
                new Doctor { Id = 38, PersonId = 38, SpecialtyId = 13 }, // Emergency Medicine
                new Doctor { Id = 39, PersonId = 39, SpecialtyId = 14 }, // Radiology
                new Doctor { Id = 40, PersonId = 40, SpecialtyId = 15 }, // Anesthesiology
                new Doctor { Id = 41, PersonId = 41, SpecialtyId = 16 }, // Oncology
                new Doctor { Id = 42, PersonId = 42, SpecialtyId = 17 }, // Endocrinology
                new Doctor { Id = 43, PersonId = 43, SpecialtyId = 18 }, // Gastroenterology
                new Doctor { Id = 44, PersonId = 44, SpecialtyId = 19 }, // Pulmonology
                new Doctor { Id = 45, PersonId = 45, SpecialtyId = 20 }, // Urology
                new Doctor { Id = 46, PersonId = 46, SpecialtyId = 21 }, // Rheumatology
                new Doctor { Id = 47, PersonId = 47, SpecialtyId = 22 }, // Nephrology
                new Doctor { Id = 48, PersonId = 48, SpecialtyId = 23 }, // Hematology
                new Doctor { Id = 49, PersonId = 49, SpecialtyId = 24 }, // Infectious Disease
                new Doctor { Id = 50, PersonId = 50, SpecialtyId = 25 }  // Allergy and Immunology
            };
        }
    }
}
