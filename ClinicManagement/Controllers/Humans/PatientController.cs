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
    [PatientModelValidator]
    public class PatientController : CommonController<Patient>
    {
        private readonly IPatientService _patientService;

        public PatientController(IService<Patient> service, IPatientService patientService, ILogger<PatientController> logger)
            : base(service, logger)
        {
            _patientService = patientService;
        }

        [HttpPost]
        [Authorize(RoleType.Authorized, RoleType.Operator, RoleType.Admin)]
        public override async Task<IActionResult> Create(Patient entity)
        {
            if (entity == null)
                return BadRequest("Entity cannot be null.");

            if (entity.PersonId == 0)
                ModelState.AddModelError("PersonId", "Person is required.");

            if (entity.AddressId == 0)
                ModelState.AddModelError("AddressId", "Address is required.");

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
                _logger.LogError(ex, "Error creating Patient");
                await LoadDropdownsAsync();
                ModelState.AddModelError("", "An error occurred while creating the patient.");
                return View(entity);
            }
        }

        [HttpPost]
        [Authorize(RoleType.Authorized, RoleType.Operator, RoleType.Admin)]
        public override async Task<IActionResult> Update(int id, Patient entity)
        {
            if (entity == null)
                return BadRequest("Entity cannot be null.");

            if (entity.PersonId == 0)
                ModelState.AddModelError("PersonId", "Person is required.");

            if (entity.AddressId == 0)
                ModelState.AddModelError("AddressId", "Address is required.");

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
                _logger.LogWarning(ex, "Patient with id {Id} not found for update", id);
                await LoadDropdownsAsync();
                var currentEntity = await _service.GetByIdAsync(id) ?? entity;
                ModelState.AddModelError("", ex.Message);
                return View("Entity", currentEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Patient with id {Id}", id);
                await LoadDropdownsAsync();
                var currentEntity = await _service.GetByIdAsync(id) ?? entity;
                ModelState.AddModelError("", "An error occurred while updating the patient.");
                return View("Entity", currentEntity);
            }
        }

        protected override async Task LoadDropdownsAsync()
        {
            var people = await _patientService.GetAllPeopleAsync();
            ViewBag.People = new SelectList(people.Select(p => new { 
                Id = p.Id, 
                DisplayName = $"{p.LastName}, {p.FirstName}" + (string.IsNullOrEmpty(p.Patronymic) ? "" : $" {p.Patronymic}")
            }), "Id", "DisplayName");

            var addresses = await _patientService.GetAllAddressesAsync();
            ViewBag.Addresses = new SelectList(addresses.Select(a => new { 
                Id = a.Id, 
                DisplayName = $"{a.StreetName} {a.StreetNumber}, {a.Locality}, {a.State}, {a.Country}"
            }), "Id", "DisplayName");
        }
    }
}
