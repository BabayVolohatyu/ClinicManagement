using ClinicManagement.Helpers;
using ClinicManagement.Models.Auth;
using ClinicManagement.Models.Health;
using ClinicManagement.Services;
using ClinicManagement.Services.Health;
using ClinicManagement.Validators.Health;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicManagement.Controllers.Health
{
    [DiagnosisModelValidator]
    public class DiagnosisController : CommonController<Diagnosis>
    {
        private readonly IDiagnosisService _diagnosisService;

        public DiagnosisController(IService<Diagnosis> service, IDiagnosisService diagnosisService, ILogger<DiagnosisController> logger)
            : base(service, logger)
        {
            _diagnosisService = diagnosisService;
        }

        [HttpGet]
        [Authorize(RoleType.Authorized, RoleType.Operator, RoleType.Admin)]
        public override async Task<IActionResult> Create()
        {
            try
            {
                await LoadDropdownsAsync();
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading create form for Diagnosis");
                return StatusCode(500, "An error occurred while loading the create form.");
            }
        }

        [HttpPost]
        [Authorize(RoleType.Authorized, RoleType.Operator, RoleType.Admin)]
        public override async Task<IActionResult> Create(Diagnosis entity)
        {
            if (entity == null)
                return BadRequest("Entity cannot be null.");

            if (entity.AppointmentId == 0)
                ModelState.AddModelError("AppointmentId", "Appointment is required.");

            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                return View(entity);
            }

            try
            {
                await _service.AddAsync(entity);
                return RedirectToAction(nameof(Entity), new { id = entity.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Diagnosis");
                await LoadDropdownsAsync();
                ModelState.AddModelError("", "An error occurred while creating the diagnosis.");
                return View(entity);
            }
        }

        public override async Task<IActionResult> Entity(int id)
        {
            try
            {
                var entity = await _service.GetByIdAsync(id);
                if (entity == null)
                    return NotFound($"Diagnosis with id {id} not found.");

                await LoadDropdownsAsync();
                return View(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching Diagnosis with id {Id}", id);
                return StatusCode(500, "An error occurred while fetching entity.");
            }
        }

        [HttpPost]
        [Authorize(RoleType.Authorized, RoleType.Operator, RoleType.Admin)]
        public override async Task<IActionResult> Update(int id, Diagnosis entity)
        {
            if (entity == null)
                return BadRequest("Entity cannot be null.");

            if (entity.AppointmentId == 0)
                ModelState.AddModelError("AppointmentId", "Appointment is required.");

            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                var currentEntity = await _service.GetByIdAsync(id) ?? entity;
                return View("Entity", currentEntity);
            }

            try
            {
                await _service.UpdateAsync(id, entity);
                return RedirectToAction(nameof(Entity), new { id });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Diagnosis with id {Id} not found for update", id);
                await LoadDropdownsAsync();
                var currentEntity = await _service.GetByIdAsync(id) ?? entity;
                ModelState.AddModelError("", ex.Message);
                return View("Entity", currentEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Diagnosis with id {Id}", id);
                await LoadDropdownsAsync();
                var currentEntity = await _service.GetByIdAsync(id) ?? entity;
                ModelState.AddModelError("", "An error occurred while updating the diagnosis.");
                return View("Entity", currentEntity);
            }
        }

        private async Task LoadDropdownsAsync()
        {
            var appointments = await _diagnosisService.GetAllAppointmentsAsync();
            ViewBag.Appointments = new SelectList(appointments.Select(a => new { 
                Id = a.Id, 
                DisplayName = $"Appointment {a.Id} - " + 
                              (a.Patient != null && a.Patient.Person != null ? $"{a.Patient.Person.LastName}, {a.Patient.Person.FirstName}" : $"Patient {a.PatientId}") +
                              " - " + (a.StartTime.ToString("g"))
            }), "Id", "DisplayName");
        }
    }
}
