using ClinicManagement.Models.Info;

namespace ClinicManagement.Data.Info
{
    public static class AppointmentSeedData
    {
        public static List<Appointment> GetSeedData()
        {
            var baseDate = DateTimeOffset.UtcNow.Date;
            var data = new List<Appointment>();
            int id = 1;
            var random = new Random(42); // Fixed seed for reproducibility
            
            // Generate appointments for the past 2 weeks, current week, and next 2 weeks
            // Total: 5 weeks of appointments
            
            // Past appointments (completed)
            for (int week = -2; week < 0; week++)
            {
                for (int day = 0; day < 7; day++)
                {
                    var appointmentDate = baseDate.AddDays(week * 7 + day);
                    
                    // 8-12 appointments per day
                    for (int i = 0; i < 10; i++)
                    {
                        var hour = 8 + (i * 2) % 8; // 8 AM to 4 PM
                        var minute = (i % 2) * 30; // :00 or :30
                        
                        var doctorProcedureId = random.Next(1, 200); // Random doctor procedure
                        var cabinetId = random.Next(1, 81); // Random cabinet
                        var patientId = random.Next(1, 201); // Random patient
                        
                        data.Add(new Appointment 
                        { 
                            Id = id++, 
                            DoctorProcedureId = doctorProcedureId, 
                            CabinetId = cabinetId, 
                            PatientId = patientId, 
                            StartTime = appointmentDate.AddHours(hour).AddMinutes(minute), 
                            EndTime = appointmentDate.AddHours(hour).AddMinutes(minute + 30), 
                            DidItHappen = true 
                        });
                    }
                }
            }
            
            // Current week appointments (mix of completed and pending)
            for (int day = 0; day < 7; day++)
            {
                var appointmentDate = baseDate.AddDays(day);
                var isPast = day < DateTimeOffset.UtcNow.DayOfWeek;
                
                // 8-12 appointments per day
                for (int i = 0; i < 10; i++)
                {
                    var hour = 8 + (i * 2) % 8;
                    var minute = (i % 2) * 30;
                    
                    var doctorProcedureId = random.Next(1, 200);
                    var cabinetId = random.Next(1, 81);
                    var patientId = random.Next(1, 201);
                    
                    data.Add(new Appointment 
                    { 
                        Id = id++, 
                        DoctorProcedureId = doctorProcedureId, 
                        CabinetId = cabinetId, 
                        PatientId = patientId, 
                        StartTime = appointmentDate.AddHours(hour).AddMinutes(minute), 
                        EndTime = appointmentDate.AddHours(hour).AddMinutes(minute + 30), 
                        DidItHappen = isPast 
                    });
                }
            }
            
            // Future appointments (pending)
            for (int week = 1; week <= 2; week++)
            {
                for (int day = 0; day < 7; day++)
                {
                    var appointmentDate = baseDate.AddDays(week * 7 + day);
                    
                    // 8-12 appointments per day
                    for (int i = 0; i < 10; i++)
                    {
                        var hour = 8 + (i * 2) % 8;
                        var minute = (i % 2) * 30;
                        
                        var doctorProcedureId = random.Next(1, 200);
                        var cabinetId = random.Next(1, 81);
                        var patientId = random.Next(1, 201);
                        
                        data.Add(new Appointment 
                        { 
                            Id = id++, 
                            DoctorProcedureId = doctorProcedureId, 
                            CabinetId = cabinetId, 
                            PatientId = patientId, 
                            StartTime = appointmentDate.AddHours(hour).AddMinutes(minute), 
                            EndTime = appointmentDate.AddHours(hour).AddMinutes(minute + 30), 
                            DidItHappen = false 
                        });
                    }
                }
            }
            
            return data;
        }
    }
}
