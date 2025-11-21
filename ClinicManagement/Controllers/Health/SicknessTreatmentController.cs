using ClinicManagement.Helpers;
using ClinicManagement.Models.Auth;
using ClinicManagement.Models.Health;
using ClinicManagement.Services.Health;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicManagement.Controllers.Health
{
    [Authorize]
    public class SicknessTreatmentController : Controller
    {
        private readonly ISicknessTreatmentService _service;
        private readonly ILogger<SicknessTreatmentController> _logger;

        public SicknessTreatmentController(ISicknessTreatmentService service, ILogger<SicknessTreatmentController> logger)
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
                _logger.LogError(ex, "Error fetching SicknessTreatment list");
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
                _logger.LogError(ex, "Error loading create form for SicknessTreatment");
                return StatusCode(500, "An error occurred while loading the create form.");
            }
        }

        [HttpPost]
        [Authorize(RoleType.Authorized, RoleType.Operator, RoleType.Admin)]
        public async Task<IActionResult> Create(SicknessTreatment entity)
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
                return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Entity(int sicknessId, int treatmentId)
        {
            try
            {
                var entity = await _service.GetByIdAsync(sicknessId, treatmentId);
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
        public async Task<IActionResult> Delete(int sicknessId, int treatmentId)
        {
            try
            {
                await _service.RemoveAsync(sicknessId, treatmentId);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting SicknessTreatment");
                return StatusCode(500, "An error occurred while deleting entity.");
            }
        }

        private async Task LoadDropdownsAsync()
        {
            var sicknesses = await _service.GetAllSicknessesAsync();
            ViewBag.Sicknesses = new SelectList(sicknesses, "Id", "Name");

            var treatments = await _service.GetAllTreatmentsAsync();
            ViewBag.Treatments = new SelectList(treatments, "Id", "Name");
        }
    }
}
