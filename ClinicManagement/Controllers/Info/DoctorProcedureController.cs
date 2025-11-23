using ClinicManagement.Helpers;
using ClinicManagement.Models.Auth;
using ClinicManagement.Models.Info;
using ClinicManagement.Services;
using ClinicManagement.Services.Info;
using ClinicManagement.Validators.Info;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicManagement.Controllers.Info
{
    [DoctorProcedureModelValidator]
    public class DoctorProcedureController : CommonController<DoctorProcedure>
    {
        private readonly IDoctorProcedureService _doctorProcedureService;

        public DoctorProcedureController(IService<DoctorProcedure> service, IDoctorProcedureService doctorProcedureService, ILogger<DoctorProcedureController> logger)
            : base(service, logger)
        {
            _doctorProcedureService = doctorProcedureService;
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
                _logger.LogError(ex, "Error loading create form for DoctorProcedure");
                return StatusCode(500, "An error occurred while loading the create form.");
            }
        }

        [HttpPost]
        [Authorize(RoleType.Authorized, RoleType.Operator, RoleType.Admin)]
        public override async Task<IActionResult> Create(DoctorProcedure entity)
        {
            if (entity == null)
                return BadRequest("Entity cannot be null.");

            if (entity.DoctorId == 0)
                ModelState.AddModelError("DoctorId", "Doctor is required.");

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
                return RedirectToAction(nameof(Entity), new { id = entity.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating DoctorProcedure");
                await LoadDropdownsAsync();
                ModelState.AddModelError("", "An error occurred while creating the doctor procedure.");
                return View(entity);
            }
        }

        public override async Task<IActionResult> Entity(int id)
        {
            try
            {
                var entity = await _service.GetByIdAsync(id);
                if (entity == null)
                    return NotFound($"DoctorProcedure with id {id} not found.");

                await LoadDropdownsAsync();
                return View(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching DoctorProcedure with id {Id}", id);
                return StatusCode(500, "An error occurred while fetching entity.");
            }
        }

        [HttpPost]
        [Authorize(RoleType.Authorized, RoleType.Operator, RoleType.Admin)]
        public override async Task<IActionResult> Update(int id, DoctorProcedure entity)
        {
            if (entity == null)
                return BadRequest("Entity cannot be null.");

            if (entity.DoctorId == 0)
                ModelState.AddModelError("DoctorId", "Doctor is required.");

            if (entity.ProcedureId == 0)
                ModelState.AddModelError("ProcedureId", "Procedure is required.");

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
                _logger.LogWarning(ex, "DoctorProcedure with id {Id} not found for update", id);
                await LoadDropdownsAsync();
                var currentEntity = await _service.GetByIdAsync(id) ?? entity;
                ModelState.AddModelError("", ex.Message);
                return View("Entity", currentEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating DoctorProcedure with id {Id}", id);
                await LoadDropdownsAsync();
                var currentEntity = await _service.GetByIdAsync(id) ?? entity;
                ModelState.AddModelError("", "An error occurred while updating the doctor procedure.");
                return View("Entity", currentEntity);
            }
        }

        private async Task LoadDropdownsAsync()
        {
            var doctors = await _doctorProcedureService.GetAllDoctorsAsync();
            ViewBag.Doctors = new SelectList(doctors.Select(d => new { 
                Id = d.Id, 
                DisplayName = d.Person != null ? $"{d.Person.LastName}, {d.Person.FirstName}" + (string.IsNullOrEmpty(d.Person.Patronymic) ? "" : $" {d.Person.Patronymic}") : $"Doctor {d.Id}"
            }), "Id", "DisplayName");

            var procedures = await _doctorProcedureService.GetAllProceduresAsync();
            ViewBag.Procedures = new SelectList(procedures, "Id", "Name");
        }
    }
}
