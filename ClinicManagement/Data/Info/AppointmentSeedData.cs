using ClinicManagement.Models.Info;

namespace ClinicManagement.Data.Info
{
    public static class AppointmentSeedData
    {
        public static List<Appointment> GetSeedData()
        {
            var baseDate = DateTimeOffset.UtcNow.Date;
            var today = baseDate;
            var yesterday = baseDate.AddDays(-1);
            var tomorrow = baseDate.AddDays(1);
            var lastWeek = baseDate.AddDays(-7);
            
            return new List<Appointment>
            {
                // Past appointments (completed)
                new Appointment 
                { 
                    Id = 1, 
                    DoctorProcedureId = 1, 
                    CabinetId = 2, 
                    PatientId = 1, 
                    StartTime = lastWeek.AddHours(10), 
                    EndTime = lastWeek.AddHours(10).AddMinutes(30), 
                    DidItHappen = true 
                },
                new Appointment 
                { 
                    Id = 2, 
                    DoctorProcedureId = 7, 
                    CabinetId = 4, 
                    PatientId = 2, 
                    StartTime = lastWeek.AddDays(1).AddHours(11), 
                    EndTime = lastWeek.AddDays(1).AddHours(11).AddMinutes(45), 
                    DidItHappen = true 
                },
                new Appointment 
                { 
                    Id = 3, 
                    DoctorProcedureId = 11, 
                    CabinetId = 3, 
                    PatientId = 3, 
                    StartTime = lastWeek.AddDays(2).AddHours(9), 
                    EndTime = lastWeek.AddDays(2).AddHours(9).AddMinutes(30), 
                    DidItHappen = true 
                },
                
                // Yesterday appointments
                new Appointment 
                { 
                    Id = 4, 
                    DoctorProcedureId = 1, 
                    CabinetId = 2, 
                    PatientId = 4, 
                    StartTime = yesterday.AddHours(10), 
                    EndTime = yesterday.AddHours(10).AddMinutes(30), 
                    DidItHappen = true 
                },
                new Appointment 
                { 
                    Id = 5, 
                    DoctorProcedureId = 15, 
                    CabinetId = 5, 
                    PatientId = 5, 
                    StartTime = yesterday.AddHours(14), 
                    EndTime = yesterday.AddHours(14).AddMinutes(40), 
                    DidItHappen = true 
                },
                
                // Today appointments
                new Appointment 
                { 
                    Id = 6, 
                    DoctorProcedureId = 1, 
                    CabinetId = 2, 
                    PatientId = 6, 
                    StartTime = today.AddHours(10), 
                    EndTime = today.AddHours(10).AddMinutes(30), 
                    DidItHappen = false 
                },
                new Appointment 
                { 
                    Id = 7, 
                    DoctorProcedureId = 7, 
                    CabinetId = 4, 
                    PatientId = 7, 
                    StartTime = today.AddHours(11), 
                    EndTime = today.AddHours(11).AddMinutes(45), 
                    DidItHappen = false 
                },
                new Appointment 
                { 
                    Id = 8, 
                    DoctorProcedureId = 11, 
                    CabinetId = 3, 
                    PatientId = 8, 
                    StartTime = today.AddHours(14), 
                    EndTime = today.AddHours(14).AddMinutes(30), 
                    DidItHappen = false 
                },
                
                // Tomorrow appointments
                new Appointment 
                { 
                    Id = 9, 
                    DoctorProcedureId = 15, 
                    CabinetId = 5, 
                    PatientId = 9, 
                    StartTime = tomorrow.AddHours(10), 
                    EndTime = tomorrow.AddHours(10).AddMinutes(40), 
                    DidItHappen = false 
                },
                new Appointment 
                { 
                    Id = 10, 
                    DoctorProcedureId = 17, 
                    CabinetId = 12, 
                    PatientId = 10, 
                    StartTime = tomorrow.AddHours(11), 
                    EndTime = tomorrow.AddHours(11).AddMinutes(30), 
                    DidItHappen = false 
                },
                new Appointment 
                { 
                    Id = 11, 
                    DoctorProcedureId = 1, 
                    CabinetId = 2, 
                    PatientId = 11, 
                    StartTime = tomorrow.AddHours(15), 
                    EndTime = tomorrow.AddHours(15).AddMinutes(30), 
                    DidItHappen = false 
                },
                new Appointment 
                { 
                    Id = 12, 
                    DoctorProcedureId = 20, 
                    CabinetId = 6, 
                    PatientId = 12, 
                    StartTime = tomorrow.AddHours(10), 
                    EndTime = tomorrow.AddHours(10).AddMinutes(45), 
                    DidItHappen = false 
                }
            };
        }
    }
}

