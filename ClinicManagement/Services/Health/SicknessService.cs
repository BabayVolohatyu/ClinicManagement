using ClinicManagement.Data;
using ClinicManagement.Models.Health;

namespace ClinicManagement.Services.Health
{
    public class SicknessService : Service<Sickness>
    {
        public SicknessService(ClinicDbContext context, ILogger<SicknessService> logger)
        : base(context, logger) { }
    }
}

