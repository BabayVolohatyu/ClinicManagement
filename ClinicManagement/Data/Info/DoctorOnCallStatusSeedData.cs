using ClinicManagement.Models.Info;

namespace ClinicManagement.Data.Info
{
    public static class DoctorOnCallStatusSeedData
    {
        public static List<DoctorOnCallStatus> GetSeedData()
        {
            var baseDate = DateTimeOffset.UtcNow.Date;
            
            return new List<DoctorOnCallStatus>
            {
                // District doctors on call status
                new DoctorOnCallStatus 
                { 
                    Id = 1, 
                    DoctorId = 1, 
                    AddressId = 4, 
                    StartTime = baseDate.AddDays(-2).AddHours(8), 
                    EndTime = baseDate.AddDays(-2).AddHours(20) 
                },
                new DoctorOnCallStatus 
                { 
                    Id = 2, 
                    DoctorId = 2, 
                    AddressId = 5, 
                    StartTime = baseDate.AddDays(-1).AddHours(8), 
                    EndTime = baseDate.AddDays(-1).AddHours(20) 
                },
                new DoctorOnCallStatus 
                { 
                    Id = 3, 
                    DoctorId = 3, 
                    AddressId = 6, 
                    StartTime = baseDate.AddHours(8), 
                    EndTime = baseDate.AddHours(20) 
                },
                new DoctorOnCallStatus 
                { 
                    Id = 4, 
                    DoctorId = 4, 
                    AddressId = 7, 
                    StartTime = baseDate.AddDays(1).AddHours(8), 
                    EndTime = baseDate.AddDays(1).AddHours(20) 
                }
            };
        }
    }
}

