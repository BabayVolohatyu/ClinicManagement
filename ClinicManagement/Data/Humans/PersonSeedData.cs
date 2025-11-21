using ClinicManagement.Models.Humans;

namespace ClinicManagement.Data.Humans
{
    public static class PersonSeedData
    {
        public static List<Person> GetSeedData()
        {
            return new List<Person>
            {
                // Doctors
                new Person { Id = 1, FirstName = "Ivan", LastName = "Petrov", Patronymic = "Sergeevich" },
                new Person { Id = 2, FirstName = "Maria", LastName = "Ivanova", Patronymic = "Viktorovna" },
                new Person { Id = 3, FirstName = "Alexander", LastName = "Sidorov", Patronymic = "Petrovich" },
                new Person { Id = 4, FirstName = "Elena", LastName = "Kozlova", Patronymic = "Ivanovna" },
                new Person { Id = 5, FirstName = "Dmitry", LastName = "Volkov", Patronymic = "Alexandrovich" },
                new Person { Id = 6, FirstName = "Olga", LastName = "Novikova", Patronymic = "Dmitrievna" },
                new Person { Id = 7, FirstName = "Sergey", LastName = "Morozov", Patronymic = "Vladimirovich" },
                new Person { Id = 8, FirstName = "Anna", LastName = "Pavlova", Patronymic = "Sergeevna" },
                
                // Patients
                new Person { Id = 9, FirstName = "Nikolay", LastName = "Fedorov", Patronymic = "Ivanovich" },
                new Person { Id = 10, FirstName = "Tatiana", LastName = "Sokolova", Patronymic = "Petrovna" },
                new Person { Id = 11, FirstName = "Vladimir", LastName = "Lebedev", Patronymic = "Nikolaevich" },
                new Person { Id = 12, FirstName = "Svetlana", LastName = "Orlova", Patronymic = "Vladimirovna" },
                new Person { Id = 13, FirstName = "Andrey", LastName = "Popov", Patronymic = "Andreevich" },
                new Person { Id = 14, FirstName = "Natalia", LastName = "Kuznetsova", Patronymic = "Andreevna" },
                new Person { Id = 15, FirstName = "Mikhail", LastName = "Semenov", Patronymic = "Mikhailovich" },
                new Person { Id = 16, FirstName = "Yulia", LastName = "Vasilyeva", Patronymic = "Mikhailovna" },
                new Person { Id = 17, FirstName = "Pavel", LastName = "Zakharov", Patronymic = "Pavlovich" },
                new Person { Id = 18, FirstName = "Ekaterina", LastName = "Stepanova", Patronymic = "Pavlovna" },
                new Person { Id = 19, FirstName = "Igor", LastName = "Makarov", Patronymic = "Igorevich" },
                new Person { Id = 20, FirstName = "Larisa", LastName = "Fomina", Patronymic = "Igorevna" },
                new Person { Id = 21, FirstName = "Roman", LastName = "Romanov", Patronymic = "Romanovich" },
                new Person { Id = 22, FirstName = "Galina", LastName = "Voronova", Patronymic = "Romanovna" },
                new Person { Id = 23, FirstName = "Viktor", LastName = "Alekseev", Patronymic = "Viktorovich" },
                new Person { Id = 24, FirstName = "Raisa", LastName = "Grigorieva", Patronymic = "Viktorovna" }
            };
        }
    }
}

