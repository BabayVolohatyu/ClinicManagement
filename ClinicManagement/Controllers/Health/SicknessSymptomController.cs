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
    public class SicknessSymptomController : CommonController<SicknessSymptom>
    {
        private readonly ISicknessSymptomService _sicknessSymptomService;

        public SicknessSymptomController(IService<SicknessSymptom> service, ISicknessSymptomService sicknessSymptomService, ILogger<SicknessSymptomController> logger)
            : base(service, logger)
        {
            _sicknessSymptomService = sicknessSymptomService;
        }

        [HttpPost]
        [Authorize(RoleType.Authorized, RoleType.Operator, RoleType.Admin)]
        public override async Task<IActionResult> Create(SicknessSymptom entity)
        {
            if (entity == null)
                return BadRequest("Entity cannot be null.");

            if (entity.SicknessId == 0)
                ModelState.AddModelError("SicknessId", "Sickness is required.");

            if (entity.SymptomId == 0)
                ModelState.AddModelError("SymptomId", "Symptom is required.");

            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                return View(entity);
            }

            try
            {
                await _service.AddAsync(entity);
                return RedirectToAction(nameof(Entity), new { sicknessId = entity.SicknessId, symptomId = entity.SymptomId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating SicknessSymptom");
                await LoadDropdownsAsync();
                ModelState.AddModelError("", ex.Message);
                return View(entity);
            }
        }

        [HttpGet]
        [Authorize(RoleType.Authorized, RoleType.Operator, RoleType.Admin)]
        public new async Task<IActionResult> Entity(int sicknessId, int symptomId)
        {
            try
            {
                var entity = await _sicknessSymptomService.GetByIdAsync(sicknessId, symptomId);
                if (entity == null)
                    return NotFound($"SicknessSymptom not found.");

                await LoadDropdownsAsync();
                return View(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching SicknessSymptom");
                return StatusCode(500, "An error occurred while fetching entity.");
            }
        }

        [HttpPost]
        [Authorize(RoleType.Authorized, RoleType.Operator, RoleType.Admin)]
        public new async Task<IActionResult> Delete(int sicknessId, int symptomId)
        {
            try
            {
                await _sicknessSymptomService.RemoveAsync(sicknessId, symptomId);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting SicknessSymptom");
                return StatusCode(500, "An error occurred while deleting entity.");
            }
        }

        protected override async Task LoadDropdownsAsync()
        {
            var sicknesses = await _sicknessSymptomService.GetAllSicknessesAsync();
            ViewBag.Sicknesses = new SelectList(sicknesses, "Id", "Name");

            var symptoms = await _sicknessSymptomService.GetAllSymptomsAsync();
            ViewBag.Symptoms = new SelectList(symptoms, "Id", "Name");
        }
    }
}
