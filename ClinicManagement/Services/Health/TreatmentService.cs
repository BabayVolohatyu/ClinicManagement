using ClinicManagement.Data;
using ClinicManagement.Models.Health;

namespace ClinicManagement.Services.Health
{
    public class TreatmentService : Service<Treatment>
    {
        public TreatmentService(ClinicDbContext context, ILogger<TreatmentService> logger)
        : base(context, logger) { }
    }
}

