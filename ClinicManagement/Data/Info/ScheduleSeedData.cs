using ClinicManagement.Models.Info;

namespace ClinicManagement.Data.Info
{
    public static class ScheduleSeedData
    {
        public static List<Schedule> GetSeedData()
        {
            var baseDate = DateTimeOffset.UtcNow.Date;
            var data = new List<Schedule>();
            int id = 1;
            
            // Generate schedules for all 50 doctors over the next 4 weeks
            for (int doctorId = 1; doctorId <= 50; doctorId++)
            {
                // Each doctor has multiple time slots per week
                for (int week = 0; week < 4; week++)
                {
                    // Monday through Friday
                    for (int day = 0; day < 5; day++)
                    {
                        var scheduleDate = baseDate.AddDays(week * 7 + day);
                        var cabinetId = ((doctorId - 1) % 80) + 1; // Rotate through 80 cabinets
                        
                        // Morning shift (9 AM - 1 PM)
                        data.Add(new Schedule 
                        { 
                            Id = id++, 
                            DoctorId = doctorId, 
                            CabinetId = cabinetId, 
                            StartTime = scheduleDate.AddHours(9), 
                            EndTime = scheduleDate.AddHours(13) 
                        });
                        
                        // Afternoon shift (2 PM - 6 PM)
                        data.Add(new Schedule 
                        { 
                            Id = id++, 
                            DoctorId = doctorId, 
                            CabinetId = cabinetId, 
                            StartTime = scheduleDate.AddHours(14), 
                            EndTime = scheduleDate.AddHours(18) 
                        });
                    }
                }
            }
            
            return data;
        }
    }
}
