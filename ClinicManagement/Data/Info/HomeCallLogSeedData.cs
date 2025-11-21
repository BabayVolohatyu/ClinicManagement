using ClinicManagement.Models.Info;

namespace ClinicManagement.Data.Info
{
    public static class HomeCallLogSeedData
    {
        public static List<HomeCallLog> GetSeedData()
        {
            var baseDate = DateTimeOffset.UtcNow.Date;
            
            return new List<HomeCallLog>
            {
                // District doctor home calls
                new HomeCallLog 
                { 
                    Id = 1, 
                    DoctorId = 1, 
                    AddressId = 8, 
                    DateTime = baseDate.AddDays(-3).AddHours(14).AddMinutes(30) 
                },
                new HomeCallLog 
                { 
                    Id = 2, 
                    DoctorId = 1, 
                    AddressId = 9, 
                    DateTime = baseDate.AddDays(-2).AddHours(16) 
                },
                new HomeCallLog 
                { 
                    Id = 3, 
                    DoctorId = 2, 
                    AddressId = 10, 
                    DateTime = baseDate.AddDays(-1).AddHours(10).AddMinutes(15) 
                },
                new HomeCallLog 
                { 
                    Id = 4, 
                    DoctorId = 3, 
                    AddressId = 11, 
                    DateTime = baseDate.AddDays(-1).AddHours(18).AddMinutes(45) 
                },
                new HomeCallLog 
                { 
                    Id = 5, 
                    DoctorId = 4, 
                    AddressId = 12, 
                    DateTime = baseDate.AddHours(9).AddMinutes(30) 
                },
                new HomeCallLog 
                { 
                    Id = 6, 
                    DoctorId = 1, 
                    AddressId = 13, 
                    DateTime = baseDate.AddHours(11) 
                },
                new HomeCallLog 
                { 
                    Id = 7, 
                    DoctorId = 2, 
                    AddressId = 14, 
                    DateTime = baseDate.AddHours(15).AddMinutes(20) 
                }
            };
        }
    }
}

