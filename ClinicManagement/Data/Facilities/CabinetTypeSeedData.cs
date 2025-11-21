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
                new CabinetType { Id = 10, Type = "Administration" }
            };
        }
    }
}

