using ClinicManagement.Models.Info;

namespace ClinicManagement.Data.Info
{
    public static class HomeCallLogSeedData
    {
        public static List<HomeCallLog> GetSeedData()
        {
            var baseDate = DateTimeOffset.UtcNow.Date;
            var data = new List<HomeCallLog>();
            int id = 1;
            var random = new Random(42);
            
            // Generate home call logs for district doctors over the past month
            // Each district doctor makes 2-3 home calls per week
            
            for (int doctorId = 1; doctorId <= 20; doctorId++) // 20 district doctors
            {
                for (int week = -4; week < 1; week++) // Past 4 weeks and current week
                {
                    // 2-3 calls per week
                    for (int call = 0; call < 3; call++)
                    {
                        var day = random.Next(0, 7);
                        var hour = random.Next(8, 20); // 8 AM to 8 PM
                        var minute = random.Next(0, 60);
                        
                        var callDate = baseDate.AddDays(week * 7 + day).AddHours(hour).AddMinutes(minute);
                        var addressId = random.Next(6, 251); // Random patient address
                        
                        data.Add(new HomeCallLog 
                        { 
                            Id = id++, 
                            DoctorId = doctorId, 
                            AddressId = addressId, 
                            DateTime = callDate 
                        });
                    }
                }
            }
            
            return data;
        }
    }
}
