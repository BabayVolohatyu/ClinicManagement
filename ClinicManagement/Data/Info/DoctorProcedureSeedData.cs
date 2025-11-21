using ClinicManagement.Models.Info;

namespace ClinicManagement.Data.Info
{
    public static class DoctorProcedureSeedData
    {
        public static List<DoctorProcedure> GetSeedData()
        {
            return new List<DoctorProcedure>
            {
                // General Practitioner procedures
                new DoctorProcedure { Id = 1, DoctorId = 1, ProcedureId = 1 }, // General Examination
                new DoctorProcedure { Id = 2, DoctorId = 1, ProcedureId = 2 }, // Blood Pressure Measurement
                new DoctorProcedure { Id = 3, DoctorId = 1, ProcedureId = 6 }, // Blood Test
                new DoctorProcedure { Id = 4, DoctorId = 1, ProcedureId = 7 }, // Urine Analysis
                new DoctorProcedure { Id = 5, DoctorId = 1, ProcedureId = 12 }, // Vaccination
                new DoctorProcedure { Id = 6, DoctorId = 1, ProcedureId = 14 }, // Injection
                
                // Cardiologist procedures
                new DoctorProcedure { Id = 7, DoctorId = 2, ProcedureId = 1 }, // General Examination
                new DoctorProcedure { Id = 8, DoctorId = 2, ProcedureId = 2 }, // Blood Pressure Measurement
                new DoctorProcedure { Id = 9, DoctorId = 2, ProcedureId = 3 }, // ECG
                new DoctorProcedure { Id = 10, DoctorId = 2, ProcedureId = 6 }, // Blood Test
                
                // Pediatrician procedures
                new DoctorProcedure { Id = 11, DoctorId = 3, ProcedureId = 1 }, // General Examination
                new DoctorProcedure { Id = 12, DoctorId = 3, ProcedureId = 6 }, // Blood Test
                new DoctorProcedure { Id = 13, DoctorId = 3, ProcedureId = 12 }, // Vaccination
                new DoctorProcedure { Id = 14, DoctorId = 3, ProcedureId = 14 }, // Injection
                
                // Neurologist procedures
                new DoctorProcedure { Id = 15, DoctorId = 4, ProcedureId = 1 }, // General Examination
                new DoctorProcedure { Id = 16, DoctorId = 4, ProcedureId = 6 }, // Blood Test
                
                // Dermatologist procedures
                new DoctorProcedure { Id = 17, DoctorId = 5, ProcedureId = 1 }, // General Examination
                
                // Orthopedist procedures
                new DoctorProcedure { Id = 18, DoctorId = 6, ProcedureId = 1 }, // General Examination
                new DoctorProcedure { Id = 19, DoctorId = 6, ProcedureId = 4 }, // X-Ray
                
                // Ophthalmologist procedures
                new DoctorProcedure { Id = 20, DoctorId = 7, ProcedureId = 9 }, // Eye Examination
                
                // ENT Specialist procedures
                new DoctorProcedure { Id = 21, DoctorId = 8, ProcedureId = 10 }, // Ear Examination
                new DoctorProcedure { Id = 22, DoctorId = 8, ProcedureId = 8 }  // Throat Swab
            };
        }
    }
}

