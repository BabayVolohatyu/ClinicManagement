using ClinicManagement.Models.Info;

namespace ClinicManagement.Models.Health
{
    public class Procedure
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public float Price { get; set; }

        
        public ICollection<SicknessProcedure> SicknessProcedures { get; set; } = new List<SicknessProcedure>();

        
        public ICollection<DoctorProcedure> DoctorProcedures { get; set; } = new List<DoctorProcedure>();
    }
}
