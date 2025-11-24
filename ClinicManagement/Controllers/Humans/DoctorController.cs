using ClinicManagement.Helpers;
using ClinicManagement.Models.Auth;
using ClinicManagement.Models.Humans;
using ClinicManagement.Services;
using ClinicManagement.Services.Humans;
using ClinicManagement.Validators.Humans;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicManagement.Controllers.Humans
{
    [DoctorModelValidator]
    public class DoctorController : CommonController<Doctor>
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IService<Doctor> service, IDoctorService doctorService, ILogger<DoctorController> logger)
            : base(service, logger)
        {
            _doctorService = doctorService;
        }

        [HttpPost]
        [Authorize(RoleType.Authorized, RoleType.Operator, RoleType.Admin)]
        public override async Task<IActionResult> Create(Doctor entity)
        {
            if (entity == null)
                return BadRequest("Entity cannot be null.");

            if (entity.PersonId == 0)
                ModelState.AddModelError("PersonId", "Person is required.");

            if (entity.SpecialtyId == 0)
                ModelState.AddModelError("SpecialtyId", "Specialty is required.");

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
                _logger.LogError(ex, "Error creating Doctor");
                await LoadDropdownsAsync();
                ModelState.AddModelError("", "An error occurred while creating the doctor.");
                return View(entity);
            }
        }

        [HttpPost]
        [Authorize(RoleType.Authorized, RoleType.Operator, RoleType.Admin)]
        public override async Task<IActionResult> Update(int id, Doctor entity)
        {
            if (entity == null)
                return BadRequest("Entity cannot be null.");

            if (entity.PersonId == 0)
                ModelState.AddModelError("PersonId", "Person is required.");

            if (entity.SpecialtyId == 0)
                ModelState.AddModelError("SpecialtyId", "Specialty is required.");

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
                _logger.LogWarning(ex, "Doctor with id {Id} not found for update", id);
                await LoadDropdownsAsync();
                var currentEntity = await _service.GetByIdAsync(id) ?? entity;
                ModelState.AddModelError("", ex.Message);
                return View("Entity", currentEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Doctor with id {Id}", id);
                await LoadDropdownsAsync();
                var currentEntity = await _service.GetByIdAsync(id) ?? entity;
                ModelState.AddModelError("", "An error occurred while updating the doctor.");
                return View("Entity", currentEntity);
            }
        }

        protected override async Task LoadDropdownsAsync()
        {
            var people = await _doctorService.GetAllPeopleAsync();
            ViewBag.People = new SelectList(people.Select(p => new { 
                Id = p.Id, 
                DisplayName = $"{p.LastName}, {p.FirstName}" + (string.IsNullOrEmpty(p.Patronymic) ? "" : $" {p.Patronymic}")
            }), "Id", "DisplayName");

            var specialties = await _doctorService.GetAllSpecialtiesAsync();
            ViewBag.Specialties = new SelectList(specialties, "Id", "Name");
        }
    }
}
