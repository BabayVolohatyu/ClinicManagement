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
    [DoctorOnCallStatusModelValidator]
    public class DoctorOnCallStatusController : CommonController<DoctorOnCallStatus>
    {
        private readonly IDoctorOnCallStatusService _doctorOnCallStatusService;

        public DoctorOnCallStatusController(IService<DoctorOnCallStatus> service, IDoctorOnCallStatusService doctorOnCallStatusService, ILogger<DoctorOnCallStatusController> logger)
            : base(service, logger)
        {
            _doctorOnCallStatusService = doctorOnCallStatusService;
        }

        [HttpPost]
        [Authorize(RoleType.Authorized, RoleType.Operator, RoleType.Admin)]
        public override async Task<IActionResult> Create(DoctorOnCallStatus entity)
        {
            if (entity == null)
                return BadRequest("Entity cannot be null.");

            if (entity.DoctorId == 0)
                ModelState.AddModelError("DoctorId", "Doctor is required.");

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
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Validation error creating DoctorOnCallStatus");
                await LoadDropdownsAsync();
                ModelState.AddModelError("DoctorId", ex.Message);
                return View(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating DoctorOnCallStatus");
                await LoadDropdownsAsync();
                ModelState.AddModelError("", "An error occurred while creating the doctor on call status.");
                return View(entity);
            }
        }

        [HttpPost]
        [Authorize(RoleType.Authorized, RoleType.Operator, RoleType.Admin)]
        public override async Task<IActionResult> Update(int id, DoctorOnCallStatus entity)
        {
            if (entity == null)
                return BadRequest("Entity cannot be null.");

            if (entity.DoctorId == 0)
                ModelState.AddModelError("DoctorId", "Doctor is required.");

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
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Validation error updating DoctorOnCallStatus with id {Id}", id);
                await LoadDropdownsAsync();
                var currentEntity = await _service.GetByIdAsync(id) ?? entity;
                ModelState.AddModelError("DoctorId", ex.Message);
                return View("Entity", currentEntity);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "DoctorOnCallStatus with id {Id} not found for update", id);
                await LoadDropdownsAsync();
                var currentEntity = await _service.GetByIdAsync(id) ?? entity;
                ModelState.AddModelError("", ex.Message);
                return View("Entity", currentEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating DoctorOnCallStatus with id {Id}", id);
                await LoadDropdownsAsync();
                var currentEntity = await _service.GetByIdAsync(id) ?? entity;
                ModelState.AddModelError("", "An error occurred while updating the doctor on call status.");
                return View("Entity", currentEntity);
            }
        }

        protected override async Task LoadDropdownsAsync()
        {
            var doctors = await _doctorOnCallStatusService.GetAllDoctorsAsync();
            ViewBag.Doctors = new SelectList(doctors.Select(d => new { 
                Id = d.Id, 
                DisplayName = d.Person != null ? $"{d.Person.LastName}, {d.Person.FirstName}" + (string.IsNullOrEmpty(d.Person.Patronymic) ? "" : $" {d.Person.Patronymic}") : $"Doctor {d.Id}"
            }), "Id", "DisplayName");

            var addresses = await _doctorOnCallStatusService.GetAllAddressesAsync();
            ViewBag.Addresses = new SelectList(addresses.Select(a => new { 
                Id = a.Id, 
                DisplayName = $"{a.StreetName} {a.StreetNumber}, {a.Locality}, {a.State}, {a.Country}"
            }), "Id", "DisplayName");
        }
    }
}
