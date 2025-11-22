using ClinicManagement.Models.Info;

namespace ClinicManagement.Data.Info
{
    public static class DoctorOnCallStatusSeedData
    {
        public static List<DoctorOnCallStatus> GetSeedData()
        {
            var baseDate = DateTimeOffset.UtcNow.Date;
            var data = new List<DoctorOnCallStatus>();
            
            // District doctors on call status - one per doctor (one-to-one relationship)
            // 20 district doctors (DoctorId 1-20)
            for (int i = 0; i < 20; i++)
            {
                var doctorId = i + 1;
                var addressId = 6 + (i % 50); // Rotate through patient addresses
                
                // Current on-call status for each district doctor
                data.Add(new DoctorOnCallStatus 
                { 
                    Id = i + 1, 
                    DoctorId = doctorId, 
                    AddressId = addressId, 
                    StartTime = baseDate.AddDays(i % 7).AddHours(8), 
                    EndTime = baseDate.AddDays(i % 7).AddHours(20) 
                });
            }
            
            return data;
        }
    }
}
