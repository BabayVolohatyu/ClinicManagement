using ClinicManagement.Models.Humans;

namespace ClinicManagement.Data.Humans
{
    public static class DistrictDoctorSeedData
    {
        public static List<DistrictDoctor> GetSeedData()
        {
            return new List<DistrictDoctor>
            {
                // Some doctors are district doctors (DoctorId 1-4)
                new DistrictDoctor { DoctorId = 1 }, // General Practitioner
                new DistrictDoctor { DoctorId = 2 }, // Cardiologist
                new DistrictDoctor { DoctorId = 3 }, // Pediatrician
                new DistrictDoctor { DoctorId = 4 }  // Neurologist
            };
        }
    }
}

