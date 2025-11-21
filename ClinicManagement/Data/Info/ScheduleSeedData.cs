using ClinicManagement.Models.Info;

namespace ClinicManagement.Data.Info
{
    public static class ScheduleSeedData
    {
        public static List<Schedule> GetSeedData()
        {
            var baseDate = DateTimeOffset.UtcNow.Date;
            var today = baseDate;
            var tomorrow = baseDate.AddDays(1);
            var nextWeek = baseDate.AddDays(7);
            
            return new List<Schedule>
            {
                // Doctor 1 (General Practitioner) - Today
                new Schedule { Id = 1, DoctorId = 1, CabinetId = 2, StartTime = today.AddHours(9), EndTime = today.AddHours(13) },
                new Schedule { Id = 2, DoctorId = 1, CabinetId = 2, StartTime = today.AddHours(14), EndTime = today.AddHours(18) },
                
                // Doctor 2 (Cardiologist) - Today
                new Schedule { Id = 3, DoctorId = 2, CabinetId = 4, StartTime = today.AddHours(9), EndTime = today.AddHours(12) },
                new Schedule { Id = 4, DoctorId = 2, CabinetId = 4, StartTime = today.AddHours(13), EndTime = today.AddHours(17) },
                
                // Doctor 3 (Pediatrician) - Today
                new Schedule { Id = 5, DoctorId = 3, CabinetId = 3, StartTime = today.AddHours(8), EndTime = today.AddHours(12) },
                new Schedule { Id = 6, DoctorId = 3, CabinetId = 3, StartTime = today.AddHours(13), EndTime = today.AddHours(17) },
                
                // Doctor 4 (Neurologist) - Tomorrow
                new Schedule { Id = 7, DoctorId = 4, CabinetId = 5, StartTime = tomorrow.AddHours(10), EndTime = tomorrow.AddHours(14) },
                new Schedule { Id = 8, DoctorId = 4, CabinetId = 5, StartTime = tomorrow.AddHours(15), EndTime = tomorrow.AddHours(19) },
                
                // Doctor 5 (Dermatologist) - Tomorrow
                new Schedule { Id = 9, DoctorId = 5, CabinetId = 12, StartTime = tomorrow.AddHours(9), EndTime = tomorrow.AddHours(13) },
                
                // Doctor 6 (Orthopedist) - Next Week
                new Schedule { Id = 10, DoctorId = 6, CabinetId = 13, StartTime = nextWeek.AddHours(9), EndTime = nextWeek.AddHours(13) },
                
                // Doctor 7 (Ophthalmologist) - Today
                new Schedule { Id = 11, DoctorId = 7, CabinetId = 6, StartTime = today.AddHours(10), EndTime = today.AddHours(14) },
                
                // Doctor 8 (ENT Specialist) - Tomorrow
                new Schedule { Id = 12, DoctorId = 8, CabinetId = 7, StartTime = tomorrow.AddHours(9), EndTime = tomorrow.AddHours(13) }
            };
        }
    }
}

