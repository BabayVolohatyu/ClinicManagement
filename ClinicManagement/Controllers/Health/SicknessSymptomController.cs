using ClinicManagement.Helpers;
using ClinicManagement.Models.Auth;
using ClinicManagement.Models.Health;
using ClinicManagement.Services.Health;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicManagement.Controllers.Health
{
    [Authorize]
    public class SicknessSymptomController : Controller
    {
        private readonly ISicknessSymptomService _service;
        private readonly ILogger<SicknessSymptomController> _logger;

        public SicknessSymptomController(ISicknessSymptomService service, ILogger<SicknessSymptomController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null,
            string? sortBy = null,
            bool sortAscending = true)
        {
            try
            {
                ViewData["Entity"] = RouteData.Values["controller"]?.ToString().ToLower();
                var result = await _service.GetAllAsync(pageNumber, pageSize, searchTerm, sortBy, sortAscending);
                return View(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching SicknessSymptom list");
                return StatusCode(500, "An error occurred while fetching data.");
            }
        }

        [HttpGet]
        [Authorize(RoleType.Authorized, RoleType.Operator, RoleType.Admin)]
        public async Task<IActionResult> Create()
        {
            try
            {
                await LoadDropdownsAsync();
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading create form for SicknessSymptom");
                return StatusCode(500, "An error occurred while loading the create form.");
            }
        }

        [HttpPost]
        [Authorize(RoleType.Authorized, RoleType.Operator, RoleType.Admin)]
        public async Task<IActionResult> Create(SicknessSymptom entity)
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
                return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Entity(int sicknessId, int symptomId)
        {
            try
            {
                var entity = await _service.GetByIdAsync(sicknessId, symptomId);
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
        public async Task<IActionResult> Delete(int sicknessId, int symptomId)
        {
            try
            {
                await _service.RemoveAsync(sicknessId, symptomId);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting SicknessSymptom");
                return StatusCode(500, "An error occurred while deleting entity.");
            }
        }

        private async Task LoadDropdownsAsync()
        {
            var sicknesses = await _service.GetAllSicknessesAsync();
            ViewBag.Sicknesses = new SelectList(sicknesses, "Id", "Name");

            var symptoms = await _service.GetAllSymptomsAsync();
            ViewBag.Symptoms = new SelectList(symptoms, "Id", "Name");
        }
    }
}
