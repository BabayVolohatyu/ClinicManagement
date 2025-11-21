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
    [ScheduleModelValidator]
    public class ScheduleController : CommonController<Schedule>
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IService<Schedule> service, IScheduleService scheduleService, ILogger<ScheduleController> logger)
            : base(service, logger)
        {
            _scheduleService = scheduleService;
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
                _logger.LogError(ex, "Error loading create form for Schedule");
                return StatusCode(500, "An error occurred while loading the create form.");
            }
        }

        [HttpPost]
        [Authorize(RoleType.Authorized, RoleType.Operator, RoleType.Admin)]
        public override async Task<IActionResult> Create(Schedule entity)
        {
            if (entity == null)
                return BadRequest("Entity cannot be null.");

            if (entity.DoctorId == 0)
                ModelState.AddModelError("DoctorId", "Doctor is required.");

            if (entity.CabinetId == 0)
                ModelState.AddModelError("CabinetId", "Cabinet is required.");

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
                _logger.LogError(ex, "Error creating Schedule");
                await LoadDropdownsAsync();
                ModelState.AddModelError("", "An error occurred while creating the schedule.");
                return View(entity);
            }
        }

        public override async Task<IActionResult> Entity(int id)
        {
            try
            {
                var entity = await _service.GetByIdAsync(id);
                if (entity == null)
                    return NotFound($"Schedule with id {id} not found.");

                await LoadDropdownsAsync();
                return View(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching Schedule with id {Id}", id);
                return StatusCode(500, "An error occurred while fetching entity.");
            }
        }

        [HttpPost]
        [Authorize(RoleType.Authorized, RoleType.Operator, RoleType.Admin)]
        public override async Task<IActionResult> Update(int id, Schedule entity)
        {
            if (entity == null)
                return BadRequest("Entity cannot be null.");

            if (entity.DoctorId == 0)
                ModelState.AddModelError("DoctorId", "Doctor is required.");

            if (entity.CabinetId == 0)
                ModelState.AddModelError("CabinetId", "Cabinet is required.");

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
                _logger.LogWarning(ex, "Schedule with id {Id} not found for update", id);
                await LoadDropdownsAsync();
                var currentEntity = await _service.GetByIdAsync(id) ?? entity;
                ModelState.AddModelError("", ex.Message);
                return View("Entity", currentEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Schedule with id {Id}", id);
                await LoadDropdownsAsync();
                var currentEntity = await _service.GetByIdAsync(id) ?? entity;
                ModelState.AddModelError("", "An error occurred while updating the schedule.");
                return View("Entity", currentEntity);
            }
        }

        private async Task LoadDropdownsAsync()
        {
            var doctors = await _scheduleService.GetAllDoctorsAsync();
            ViewBag.Doctors = new SelectList(doctors.Select(d => new { 
                Id = d.Id, 
                DisplayName = d.Person != null ? $"{d.Person.LastName}, {d.Person.FirstName}" + (string.IsNullOrEmpty(d.Person.Patronymic) ? "" : $" {d.Person.Patronymic}") : $"Doctor {d.Id}"
            }), "Id", "DisplayName");

            var cabinets = await _scheduleService.GetAllCabinetsAsync();
            ViewBag.Cabinets = new SelectList(cabinets.Select(c => new { 
                Id = c.Id, 
                DisplayName = $"Building {c.Building}, Floor {c.Floor}, Room {c.Number}" + (c.Type != null ? $" ({c.Type.Type})" : "")
            }), "Id", "DisplayName");
        }
    }
}
