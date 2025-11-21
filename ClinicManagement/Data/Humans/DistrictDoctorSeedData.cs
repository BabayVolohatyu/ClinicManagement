using ClinicManagement.Models.Humans;

namespace ClinicManagement.Data.Humans
{
    public static class DistrictDoctorSeedData
    {
        public static List<DistrictDoctor> GetSeedData()
        {
            return new List<DistrictDoctor>
            {
                // District doctors (20 out of 50 doctors are district doctors)
                new DistrictDoctor { DoctorId = 1 }, // Family Medicine
                new DistrictDoctor { DoctorId = 2 }, // Internal Medicine
                new DistrictDoctor { DoctorId = 3 }, // Cardiology
                new DistrictDoctor { DoctorId = 4 }, // Pediatrics
                new DistrictDoctor { DoctorId = 5 }, // Orthopedics
                new DistrictDoctor { DoctorId = 6 }, // Dermatology
                new DistrictDoctor { DoctorId = 7 }, // Neurology
                new DistrictDoctor { DoctorId = 8 }, // Ophthalmology
                new DistrictDoctor { DoctorId = 9 }, // ENT
                new DistrictDoctor { DoctorId = 10 }, // General Surgery
                new DistrictDoctor { DoctorId = 11 }, // OB/GYN
                new DistrictDoctor { DoctorId = 12 }, // Psychiatry
                new DistrictDoctor { DoctorId = 13 }, // Emergency Medicine
                new DistrictDoctor { DoctorId = 14 }, // Radiology
                new DistrictDoctor { DoctorId = 15 }, // Anesthesiology
                new DistrictDoctor { DoctorId = 16 }, // Oncology
                new DistrictDoctor { DoctorId = 17 }, // Endocrinology
                new DistrictDoctor { DoctorId = 18 }, // Gastroenterology
                new DistrictDoctor { DoctorId = 19 }, // Pulmonology
                new DistrictDoctor { DoctorId = 20 }  // Urology
            };
        }
    }
}
