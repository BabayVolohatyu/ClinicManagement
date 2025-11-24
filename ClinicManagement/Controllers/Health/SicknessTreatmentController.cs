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
    public class SicknessTreatmentController : CommonController<SicknessTreatment>
    {
        private readonly ISicknessTreatmentService _sicknessTreatmentService;

        public SicknessTreatmentController(IService<SicknessTreatment> service, ISicknessTreatmentService sicknessTreatmentService, ILogger<SicknessTreatmentController> logger)
            : base(service, logger)
        {
            _sicknessTreatmentService = sicknessTreatmentService;
        }

        [HttpPost]
        [Authorize(RoleType.Authorized, RoleType.Operator, RoleType.Admin)]
        public override async Task<IActionResult> Create(SicknessTreatment entity)
        {
            if (entity == null)
                return BadRequest("Entity cannot be null.");

            if (entity.SicknessId == 0)
                ModelState.AddModelError("SicknessId", "Sickness is required.");

            if (entity.TreatmentId == 0)
                ModelState.AddModelError("TreatmentId", "Treatment is required.");

            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                return View(entity);
            }

            try
            {
                await _service.AddAsync(entity);
                return RedirectToAction(nameof(Entity), new { sicknessId = entity.SicknessId, treatmentId = entity.TreatmentId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating SicknessTreatment");
                await LoadDropdownsAsync();
                ModelState.AddModelError("", ex.Message);
                return View(entity);
            }
        }

        [HttpGet]
        [Authorize(RoleType.Authorized, RoleType.Operator, RoleType.Admin)]
        public new async Task<IActionResult> Entity(int sicknessId, int treatmentId)
        {
            try
            {
                var entity = await _sicknessTreatmentService.GetByIdAsync(sicknessId, treatmentId);
                if (entity == null)
                    return NotFound($"SicknessTreatment not found.");

                await LoadDropdownsAsync();
                return View(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching SicknessTreatment");
                return StatusCode(500, "An error occurred while fetching entity.");
            }
        }

        [HttpPost]
        [Authorize(RoleType.Authorized, RoleType.Operator, RoleType.Admin)]
        public new async Task<IActionResult> Delete(int sicknessId, int treatmentId)
        {
            try
            {
                await _sicknessTreatmentService.RemoveAsync(sicknessId, treatmentId);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting SicknessTreatment");
                return StatusCode(500, "An error occurred while deleting entity.");
            }
        }

        protected override async Task LoadDropdownsAsync()
        {
            var sicknesses = await _sicknessTreatmentService.GetAllSicknessesAsync();
            ViewBag.Sicknesses = new SelectList(sicknesses, "Id", "Name");

            var treatments = await _sicknessTreatmentService.GetAllTreatmentsAsync();
            ViewBag.Treatments = new SelectList(treatments, "Id", "Name");
        }
    }
}
