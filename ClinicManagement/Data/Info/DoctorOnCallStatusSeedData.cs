using ClinicManagement.Models.Info;

namespace ClinicManagement.Data.Info
{
    public static class DoctorOnCallStatusSeedData
    {
        public static List<DoctorOnCallStatus> GetSeedData()
        {
            var baseDate = DateTimeOffset.UtcNow.Date;
            var data = new List<DoctorOnCallStatus>();
            
            // District doctors on call status - past week, current week, and next week
            for (int i = 0; i < 20; i++) // 20 district doctors
            {
                var doctorId = i + 1;
                var addressId = 6 + (i % 50); // Rotate through patient addresses
                
                // Past week
                data.Add(new DoctorOnCallStatus 
                { 
                    Id = i * 3 + 1, 
                    DoctorId = doctorId, 
                    AddressId = addressId, 
                    StartTime = baseDate.AddDays(-7 + i % 7).AddHours(8), 
                    EndTime = baseDate.AddDays(-7 + i % 7).AddHours(20) 
                });
                
                // Current week
                data.Add(new DoctorOnCallStatus 
                { 
                    Id = i * 3 + 2, 
                    DoctorId = doctorId, 
                    AddressId = addressId + 1, 
                    StartTime = baseDate.AddDays(i % 7).AddHours(8), 
                    EndTime = baseDate.AddDays(i % 7).AddHours(20) 
                });
                
                // Next week
                data.Add(new DoctorOnCallStatus 
                { 
                    Id = i * 3 + 3, 
                    DoctorId = doctorId, 
                    AddressId = addressId + 2, 
                    StartTime = baseDate.AddDays(7 + i % 7).AddHours(8), 
                    EndTime = baseDate.AddDays(7 + i % 7).AddHours(20) 
                });
            }
            
            return data;
        }
    }
}
