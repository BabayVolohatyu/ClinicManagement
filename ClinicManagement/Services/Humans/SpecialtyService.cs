using ClinicManagement.Data;
using ClinicManagement.Models.Humans;

namespace ClinicManagement.Services.Humans
{
    public class SpecialtyService : Service<Specialty>
    {
        public SpecialtyService(ClinicDbContext context, ILogger<SpecialtyService> logger)
        : base(context, logger) { }
    }
}
