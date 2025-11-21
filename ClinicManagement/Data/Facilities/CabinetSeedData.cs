using ClinicManagement.Models.Facilities;

namespace ClinicManagement.Data.Facilities
{
    public static class CabinetSeedData
    {
        public static List<Cabinet> GetSeedData()
        {
            return new List<Cabinet>
            {
                // Building 1, Floor 1 (Main Building)
                new Cabinet { Id = 1, Building = 1, Floor = 1, Number = 101, TypeId = 8 }, // Reception
                new Cabinet { Id = 2, Building = 1, Floor = 1, Number = 102, TypeId = 1 }, // Examination Room
                new Cabinet { Id = 3, Building = 1, Floor = 1, Number = 103, TypeId = 1 }, // Examination Room
                new Cabinet { Id = 4, Building = 1, Floor = 1, Number = 104, TypeId = 1 }, // Examination Room
                new Cabinet { Id = 5, Building = 1, Floor = 1, Number = 105, TypeId = 1 }, // Examination Room
                new Cabinet { Id = 6, Building = 1, Floor = 1, Number = 106, TypeId = 2 }, // Consultation Room
                new Cabinet { Id = 7, Building = 1, Floor = 1, Number = 107, TypeId = 2 }, // Consultation Room
                new Cabinet { Id = 8, Building = 1, Floor = 1, Number = 108, TypeId = 2 }, // Consultation Room
                new Cabinet { Id = 9, Building = 1, Floor = 1, Number = 109, TypeId = 9 }, // Waiting Room
                new Cabinet { Id = 10, Building = 1, Floor = 1, Number = 110, TypeId = 10 }, // Administration
                
                // Building 1, Floor 2
                new Cabinet { Id = 11, Building = 1, Floor = 2, Number = 201, TypeId = 3 }, // Procedure Room
                new Cabinet { Id = 12, Building = 1, Floor = 2, Number = 202, TypeId = 3 }, // Procedure Room
                new Cabinet { Id = 13, Building = 1, Floor = 2, Number = 203, TypeId = 3 }, // Procedure Room
                new Cabinet { Id = 14, Building = 1, Floor = 2, Number = 204, TypeId = 4 }, // Surgery Room
                new Cabinet { Id = 15, Building = 1, Floor = 2, Number = 205, TypeId = 4 }, // Surgery Room
                new Cabinet { Id = 16, Building = 1, Floor = 2, Number = 206, TypeId = 5 }, // Laboratory
                new Cabinet { Id = 17, Building = 1, Floor = 2, Number = 207, TypeId = 5 }, // Laboratory
                new Cabinet { Id = 18, Building = 1, Floor = 2, Number = 208, TypeId = 6 }, // X-Ray Room
                new Cabinet { Id = 19, Building = 1, Floor = 2, Number = 209, TypeId = 7 }, // Ultrasound Room
                new Cabinet { Id = 20, Building = 1, Floor = 2, Number = 210, TypeId = 11 }, // Pharmacy
                
                // Building 1, Floor 3
                new Cabinet { Id = 21, Building = 1, Floor = 3, Number = 301, TypeId = 1 }, // Examination Room
                new Cabinet { Id = 22, Building = 1, Floor = 3, Number = 302, TypeId = 1 }, // Examination Room
                new Cabinet { Id = 23, Building = 1, Floor = 3, Number = 303, TypeId = 1 }, // Examination Room
                new Cabinet { Id = 24, Building = 1, Floor = 3, Number = 304, TypeId = 1 }, // Examination Room
                new Cabinet { Id = 25, Building = 1, Floor = 3, Number = 305, TypeId = 1 }, // Examination Room
                new Cabinet { Id = 26, Building = 1, Floor = 3, Number = 306, TypeId = 2 }, // Consultation Room
                new Cabinet { Id = 27, Building = 1, Floor = 3, Number = 307, TypeId = 2 }, // Consultation Room
                new Cabinet { Id = 28, Building = 1, Floor = 3, Number = 308, TypeId = 12 }, // Physical Therapy Room
                new Cabinet { Id = 29, Building = 1, Floor = 3, Number = 309, TypeId = 13 }, // Cardiology Lab
                new Cabinet { Id = 30, Building = 1, Floor = 3, Number = 310, TypeId = 9 }, // Waiting Room
                
                // Building 1, Floor 4
                new Cabinet { Id = 31, Building = 1, Floor = 4, Number = 401, TypeId = 14 }, // MRI Room
                new Cabinet { Id = 32, Building = 1, Floor = 4, Number = 402, TypeId = 15 }, // CT Scan Room
                new Cabinet { Id = 33, Building = 1, Floor = 4, Number = 403, TypeId = 16 }, // Emergency Room
                new Cabinet { Id = 34, Building = 1, Floor = 4, Number = 404, TypeId = 17 }, // ICU
                new Cabinet { Id = 35, Building = 1, Floor = 4, Number = 405, TypeId = 18 }, // Maternity Ward
                new Cabinet { Id = 36, Building = 1, Floor = 4, Number = 406, TypeId = 19 }, // Pediatric Ward
                new Cabinet { Id = 37, Building = 1, Floor = 4, Number = 407, TypeId = 20 }, // Medical Records
                new Cabinet { Id = 38, Building = 1, Floor = 4, Number = 408, TypeId = 10 }, // Administration
                new Cabinet { Id = 39, Building = 1, Floor = 4, Number = 409, TypeId = 5 }, // Laboratory
                new Cabinet { Id = 40, Building = 1, Floor = 4, Number = 410, TypeId = 9 }, // Waiting Room
                
                // Building 2, Floor 1 (Specialty Building)
                new Cabinet { Id = 41, Building = 2, Floor = 1, Number = 101, TypeId = 8 }, // Reception
                new Cabinet { Id = 42, Building = 2, Floor = 1, Number = 102, TypeId = 1 }, // Examination Room
                new Cabinet { Id = 43, Building = 2, Floor = 1, Number = 103, TypeId = 1 }, // Examination Room
                new Cabinet { Id = 44, Building = 2, Floor = 1, Number = 104, TypeId = 1 }, // Examination Room
                new Cabinet { Id = 45, Building = 2, Floor = 1, Number = 105, TypeId = 2 }, // Consultation Room
                new Cabinet { Id = 46, Building = 2, Floor = 1, Number = 106, TypeId = 2 }, // Consultation Room
                new Cabinet { Id = 47, Building = 2, Floor = 1, Number = 107, TypeId = 2 }, // Consultation Room
                new Cabinet { Id = 48, Building = 2, Floor = 1, Number = 108, TypeId = 9 }, // Waiting Room
                new Cabinet { Id = 49, Building = 2, Floor = 1, Number = 109, TypeId = 11 }, // Pharmacy
                new Cabinet { Id = 50, Building = 2, Floor = 1, Number = 110, TypeId = 10 }, // Administration
                
                // Building 2, Floor 2
                new Cabinet { Id = 51, Building = 2, Floor = 2, Number = 201, TypeId = 3 }, // Procedure Room
                new Cabinet { Id = 52, Building = 2, Floor = 2, Number = 202, TypeId = 3 }, // Procedure Room
                new Cabinet { Id = 53, Building = 2, Floor = 2, Number = 203, TypeId = 4 }, // Surgery Room
                new Cabinet { Id = 54, Building = 2, Floor = 2, Number = 204, TypeId = 5 }, // Laboratory
                new Cabinet { Id = 55, Building = 2, Floor = 2, Number = 205, TypeId = 6 }, // X-Ray Room
                new Cabinet { Id = 56, Building = 2, Floor = 2, Number = 206, TypeId = 7 }, // Ultrasound Room
                new Cabinet { Id = 57, Building = 2, Floor = 2, Number = 207, TypeId = 12 }, // Physical Therapy Room
                new Cabinet { Id = 58, Building = 2, Floor = 2, Number = 208, TypeId = 13 }, // Cardiology Lab
                new Cabinet { Id = 59, Building = 2, Floor = 2, Number = 209, TypeId = 1 }, // Examination Room
                new Cabinet { Id = 60, Building = 2, Floor = 2, Number = 210, TypeId = 1 }, // Examination Room
                
                // Building 3, Floor 1 (Outpatient Services)
                new Cabinet { Id = 61, Building = 3, Floor = 1, Number = 101, TypeId = 8 }, // Reception
                new Cabinet { Id = 62, Building = 3, Floor = 1, Number = 102, TypeId = 1 }, // Examination Room
                new Cabinet { Id = 63, Building = 3, Floor = 1, Number = 103, TypeId = 1 }, // Examination Room
                new Cabinet { Id = 64, Building = 3, Floor = 1, Number = 104, TypeId = 1 }, // Examination Room
                new Cabinet { Id = 65, Building = 3, Floor = 1, Number = 105, TypeId = 1 }, // Examination Room
                new Cabinet { Id = 66, Building = 3, Floor = 1, Number = 106, TypeId = 1 }, // Examination Room
                new Cabinet { Id = 67, Building = 3, Floor = 1, Number = 107, TypeId = 2 }, // Consultation Room
                new Cabinet { Id = 68, Building = 3, Floor = 1, Number = 108, TypeId = 2 }, // Consultation Room
                new Cabinet { Id = 69, Building = 3, Floor = 1, Number = 109, TypeId = 9 }, // Waiting Room
                new Cabinet { Id = 70, Building = 3, Floor = 1, Number = 110, TypeId = 11 }, // Pharmacy
                
                // Building 3, Floor 2
                new Cabinet { Id = 71, Building = 3, Floor = 2, Number = 201, TypeId = 3 }, // Procedure Room
                new Cabinet { Id = 72, Building = 3, Floor = 2, Number = 202, TypeId = 3 }, // Procedure Room
                new Cabinet { Id = 73, Building = 3, Floor = 2, Number = 203, TypeId = 5 }, // Laboratory
                new Cabinet { Id = 74, Building = 3, Floor = 2, Number = 204, TypeId = 6 }, // X-Ray Room
                new Cabinet { Id = 75, Building = 3, Floor = 2, Number = 205, TypeId = 7 }, // Ultrasound Room
                new Cabinet { Id = 76, Building = 3, Floor = 2, Number = 206, TypeId = 12 }, // Physical Therapy Room
                new Cabinet { Id = 77, Building = 3, Floor = 2, Number = 207, TypeId = 1 }, // Examination Room
                new Cabinet { Id = 78, Building = 3, Floor = 2, Number = 208, TypeId = 1 }, // Examination Room
                new Cabinet { Id = 79, Building = 3, Floor = 2, Number = 209, TypeId = 2 }, // Consultation Room
                new Cabinet { Id = 80, Building = 3, Floor = 2, Number = 210, TypeId = 20 } // Medical Records
            };
        }
    }
}
