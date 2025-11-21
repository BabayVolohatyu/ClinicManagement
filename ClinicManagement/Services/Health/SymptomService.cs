using ClinicManagement.Data;
using ClinicManagement.Models.Health;

namespace ClinicManagement.Services.Health
{
    public class SymptomService : Service<Symptom>
    {
        public SymptomService(ClinicDbContext context, ILogger<SymptomService> logger)
        : base(context, logger) { }
    }
}

