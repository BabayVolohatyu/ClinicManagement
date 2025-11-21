using ClinicManagement.Models.Humans;

namespace ClinicManagement.Data.Humans
{
    public static class PatientSeedData
    {
        public static List<Patient> GetSeedData()
        {
            return new List<Patient>
            {
                // Patients (PersonId 9-24, AddressId 4-16)
                new Patient { Id = 1, PersonId = 9, AddressId = 4 },
                new Patient { Id = 2, PersonId = 10, AddressId = 5 },
                new Patient { Id = 3, PersonId = 11, AddressId = 6 },
                new Patient { Id = 4, PersonId = 12, AddressId = 7 },
                new Patient { Id = 5, PersonId = 13, AddressId = 8 },
                new Patient { Id = 6, PersonId = 14, AddressId = 9 },
                new Patient { Id = 7, PersonId = 15, AddressId = 10 },
                new Patient { Id = 8, PersonId = 16, AddressId = 11 },
                new Patient { Id = 9, PersonId = 17, AddressId = 12 },
                new Patient { Id = 10, PersonId = 18, AddressId = 13 },
                new Patient { Id = 11, PersonId = 19, AddressId = 14 },
                new Patient { Id = 12, PersonId = 20, AddressId = 15 },
                new Patient { Id = 13, PersonId = 21, AddressId = 16 },
                new Patient { Id = 14, PersonId = 22, AddressId = 4 },
                new Patient { Id = 15, PersonId = 23, AddressId = 5 },
                new Patient { Id = 16, PersonId = 24, AddressId = 6 }
            };
        }
    }
}

