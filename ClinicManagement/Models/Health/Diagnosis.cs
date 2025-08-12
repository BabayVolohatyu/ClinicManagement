namespace ClinicManagement.Models.Health
{
    public class Diagnosis
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }

        //One-to-one
        public Appointment Appointment { get; set; } = null!;
    }
}
