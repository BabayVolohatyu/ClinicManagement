using ClinicManagement.Models.Facilities;

namespace ClinicManagement.Data.Facilities
{
    public static class CabinetSeedData
    {
        public static List<Cabinet> GetSeedData()
        {
            return new List<Cabinet>
            {
                // Building 1, Floor 1
                new Cabinet { Id = 1, Building = 1, Floor = 1, Number = 101, TypeId = 8 }, // Reception
                new Cabinet { Id = 2, Building = 1, Floor = 1, Number = 102, TypeId = 1 }, // Examination Room
                new Cabinet { Id = 3, Building = 1, Floor = 1, Number = 103, TypeId = 1 }, // Examination Room
                new Cabinet { Id = 4, Building = 1, Floor = 1, Number = 104, TypeId = 2 }, // Consultation Room
                new Cabinet { Id = 5, Building = 1, Floor = 1, Number = 105, TypeId = 2 }, // Consultation Room
                
                // Building 1, Floor 2
                new Cabinet { Id = 6, Building = 1, Floor = 2, Number = 201, TypeId = 3 }, // Procedure Room
                new Cabinet { Id = 7, Building = 1, Floor = 2, Number = 202, TypeId = 3 }, // Procedure Room
                new Cabinet { Id = 8, Building = 1, Floor = 2, Number = 203, TypeId = 4 }, // Surgery Room
                new Cabinet { Id = 9, Building = 1, Floor = 2, Number = 204, TypeId = 5 }, // Laboratory
                new Cabinet { Id = 10, Building = 1, Floor = 2, Number = 205, TypeId = 6 }, // X-Ray Room
                
                // Building 1, Floor 3
                new Cabinet { Id = 11, Building = 1, Floor = 3, Number = 301, TypeId = 7 }, // Ultrasound Room
                new Cabinet { Id = 12, Building = 1, Floor = 3, Number = 302, TypeId = 1 }, // Examination Room
                new Cabinet { Id = 13, Building = 1, Floor = 3, Number = 303, TypeId = 1 }, // Examination Room
                new Cabinet { Id = 14, Building = 1, Floor = 3, Number = 304, TypeId = 9 }, // Waiting Room
                new Cabinet { Id = 15, Building = 1, Floor = 3, Number = 305, TypeId = 10 } // Administration
            };
        }
    }
}

