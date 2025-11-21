using ClinicManagement.Models.Facilities;

namespace ClinicManagement.Data.Facilities
{
    public static class CabinetTypeSeedData
    {
        public static List<CabinetType> GetSeedData()
        {
            return new List<CabinetType>
            {
                new CabinetType { Id = 1, Type = "Examination Room" },
                new CabinetType { Id = 2, Type = "Consultation Room" },
                new CabinetType { Id = 3, Type = "Procedure Room" },
                new CabinetType { Id = 4, Type = "Surgery Room" },
                new CabinetType { Id = 5, Type = "Laboratory" },
                new CabinetType { Id = 6, Type = "X-Ray Room" },
                new CabinetType { Id = 7, Type = "Ultrasound Room" },
                new CabinetType { Id = 8, Type = "Reception" },
                new CabinetType { Id = 9, Type = "Waiting Room" },
                new CabinetType { Id = 10, Type = "Administration" },
                new CabinetType { Id = 11, Type = "Pharmacy" },
                new CabinetType { Id = 12, Type = "Physical Therapy Room" },
                new CabinetType { Id = 13, Type = "Cardiology Lab" },
                new CabinetType { Id = 14, Type = "MRI Room" },
                new CabinetType { Id = 15, Type = "CT Scan Room" },
                new CabinetType { Id = 16, Type = "Emergency Room" },
                new CabinetType { Id = 17, Type = "Intensive Care Unit" },
                new CabinetType { Id = 18, Type = "Maternity Ward" },
                new CabinetType { Id = 19, Type = "Pediatric Ward" },
                new CabinetType { Id = 20, Type = "Medical Records" }
            };
        }
    }
}
