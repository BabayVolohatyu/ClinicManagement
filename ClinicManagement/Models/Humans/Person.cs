namespace ClinicManagement.Models.Humans
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Patronymic { get; set; }

        //One-to-one
        public Patient? Patient { get; set; }
        public Doctor? Doctor { get; set; }
    }
}
