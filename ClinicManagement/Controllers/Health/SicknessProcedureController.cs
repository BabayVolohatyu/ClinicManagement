using ClinicManagement.Helpers;
using ClinicManagement.Models.Auth;
using ClinicManagement.Models.Health;
using ClinicManagement.Services;
using ClinicManagement.Services.Health;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicManagement.Controllers.Health
{
    [Authorize]
    public class SicknessProcedureController : CommonController<SicknessProcedure>
    {
        private readonly ISicknessProcedureService _sicknessProcedureService;

        public SicknessProcedureController(IService<SicknessProcedure> service, ISicknessProcedureService sicknessProcedureService, ILogger<SicknessProcedureController> logger)
            : base(service, logger)
        {
            _sicknessProcedureService = sicknessProcedureService;
        }

        [HttpPost]
        [Authorize(RoleType.Authorized, RoleType.Operator, RoleType.Admin)]
        public override async Task<IActionResult> Create(SicknessProcedure entity)
        {
            if (entity == null)
                return BadRequest("Entity cannot be null.");

            if (entity.SicknessId == 0)
                ModelState.AddModelError("SicknessId", "Sickness is required.");

            if (entity.ProcedureId == 0)
                ModelState.AddModelError("ProcedureId", "Procedure is required.");

            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                return View(entity);
            }

            try
            {
                await _service.AddAsync(entity);
                return RedirectToAction(nameof(Entity), new { sicknessId = entity.SicknessId, procedureId = entity.ProcedureId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating SicknessProcedure");
                await LoadDropdownsAsync();
                ModelState.AddModelError("", ex.Message);
                return View(entity);
            }
        }

        [HttpGet]
        [Authorize(RoleType.Authorized, RoleType.Operator, RoleType.Admin)]
        public new async Task<IActionResult> Entity(int sicknessId, int procedureId)
        {
            try
            {
                var entity = await _sicknessProcedureService.GetByIdAsync(sicknessId, procedureId);
                if (entity == null)
                    return NotFound($"SicknessProcedure not found.");

                await LoadDropdownsAsync();
                return View(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching SicknessProcedure");
                return StatusCode(500, "An error occurred while fetching entity.");
            }
        }

        [HttpPost]
        [Authorize(RoleType.Authorized, RoleType.Operator, RoleType.Admin)]
        public new async Task<IActionResult> Delete(int sicknessId, int procedureId)
        {
            try
            {
                await _sicknessProcedureService.RemoveAsync(sicknessId, procedureId);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting SicknessProcedure");
                return StatusCode(500, "An error occurred while deleting entity.");
            }
        }

        protected override async Task LoadDropdownsAsync()
        {
            var sicknesses = await _sicknessProcedureService.GetAllSicknessesAsync();
            ViewBag.Sicknesses = new SelectList(sicknesses, "Id", "Name");

            var procedures = await _sicknessProcedureService.GetAllProceduresAsync();
            ViewBag.Procedures = new SelectList(procedures, "Id", "Name");
        }
    }
}
