using ClinicManagement.Models.Info;

namespace ClinicManagement.Data.Info
{
    public static class AddressSeedData
    {
        public static List<Address> GetSeedData()
        {
            return new List<Address>
            {
                // Clinic addresses
                new Address { Id = 1, Country = "Russia", State = "Moscow Oblast", Locality = "Moscow", StreetName = "Lenin Avenue", StreetNumber = 15 },
                new Address { Id = 2, Country = "Russia", State = "Moscow Oblast", Locality = "Moscow", StreetName = "Pushkin Street", StreetNumber = 42 },
                new Address { Id = 3, Country = "Russia", State = "Moscow Oblast", Locality = "Moscow", StreetName = "Gorky Street", StreetNumber = 28 },
                
                // Patient addresses
                new Address { Id = 4, Country = "Russia", State = "Moscow Oblast", Locality = "Moscow", StreetName = "Tverskaya Street", StreetNumber = 10 },
                new Address { Id = 5, Country = "Russia", State = "Moscow Oblast", Locality = "Moscow", StreetName = "Arbat Street", StreetNumber = 25 },
                new Address { Id = 6, Country = "Russia", State = "Moscow Oblast", Locality = "Moscow", StreetName = "Kutuzovsky Avenue", StreetNumber = 8 },
                new Address { Id = 7, Country = "Russia", State = "Moscow Oblast", Locality = "Moscow", StreetName = "Leningradsky Avenue", StreetNumber = 33 },
                new Address { Id = 8, Country = "Russia", State = "Moscow Oblast", Locality = "Moscow", StreetName = "Spartakovskaya Street", StreetNumber = 17 },
                new Address { Id = 9, Country = "Russia", State = "Moscow Oblast", Locality = "Moscow", StreetName = "Novy Arbat", StreetNumber = 12 },
                new Address { Id = 10, Country = "Russia", State = "Moscow Oblast", Locality = "Moscow", StreetName = "Sadovaya Street", StreetNumber = 5 },
                new Address { Id = 11, Country = "Russia", State = "Moscow Oblast", Locality = "Moscow", StreetName = "Prechistenka Street", StreetNumber = 20 },
                new Address { Id = 12, Country = "Russia", State = "Moscow Oblast", Locality = "Moscow", StreetName = "Ostozhenka Street", StreetNumber = 14 },
                new Address { Id = 13, Country = "Russia", State = "Moscow Oblast", Locality = "Moscow", StreetName = "Znamenka Street", StreetNumber = 7 },
                new Address { Id = 14, Country = "Russia", State = "Moscow Oblast", Locality = "Moscow", StreetName = "Volkhonka Street", StreetNumber = 19 },
                new Address { Id = 15, Country = "Russia", State = "Moscow Oblast", Locality = "Moscow", StreetName = "Nikitsky Boulevard", StreetNumber = 11 },
                new Address { Id = 16, Country = "Russia", State = "Moscow Oblast", Locality = "Moscow", StreetName = "Tverskoy Boulevard", StreetNumber = 22 }
            };
        }
    }
}

