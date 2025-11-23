using ClinicManagement.Data;
using ClinicManagement.Models.Health;

namespace ClinicManagement.Services.Health
{
    public class ProcedureService : Service<Procedure>
    {
        public ProcedureService(ClinicDbContext context, ILogger<ProcedureService> logger)
        : base(context, logger) { }
    }
}

