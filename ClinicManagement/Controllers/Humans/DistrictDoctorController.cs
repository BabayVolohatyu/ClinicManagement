using ClinicManagement.Helpers;
using ClinicManagement.Models.Auth;
using ClinicManagement.Models.Humans;
using ClinicManagement.Services;
using ClinicManagement.Services.Humans;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicManagement.Controllers.Humans
{
    [Authorize]
    public class DistrictDoctorController : CommonController<DistrictDoctor>
    {
        private readonly IDistrictDoctorService _districtDoctorService;

        public DistrictDoctorController(IService<DistrictDoctor> service, IDistrictDoctorService districtDoctorService, ILogger<DistrictDoctorController> logger)
            : base(service, logger)
        {
            _districtDoctorService = districtDoctorService;
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
                _logger.LogError(ex, "Error loading create form for DistrictDoctor");
                return StatusCode(500, "An error occurred while loading the create form.");
            }
        }

        [HttpPost]
        [Authorize(RoleType.Authorized, RoleType.Operator, RoleType.Admin)]
        public override async Task<IActionResult> Create(DistrictDoctor entity)
        {
            if (entity == null)
                return BadRequest("Entity cannot be null.");

            if (entity.DoctorId == 0)
                ModelState.AddModelError("DoctorId", "Doctor is required.");

            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                return View(entity);
            }

            try
            {
                await _service.AddAsync(entity);
                return RedirectToAction(nameof(Entity), new { doctorId = entity.DoctorId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating DistrictDoctor");
                await LoadDropdownsAsync();
                ModelState.AddModelError("", ex.Message);
                return View(entity);
            }
        }

        [HttpGet]
        [Authorize(RoleType.Authorized, RoleType.Operator, RoleType.Admin)]
        public new async Task<IActionResult> Entity(int doctorId)
        {
            try
            {
                var entity = await _districtDoctorService.GetByIdAsync(doctorId);
                if (entity == null)
                    return NotFound($"DistrictDoctor not found.");

                await LoadDropdownsAsync();
                return View(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching DistrictDoctor");
                return StatusCode(500, "An error occurred while fetching entity.");
            }
        }

        [HttpPost]
        [Authorize(RoleType.Authorized, RoleType.Operator, RoleType.Admin)]
        public new async Task<IActionResult> Delete(int doctorId)
        {
            try
            {
                await _districtDoctorService.RemoveAsync(doctorId);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting DistrictDoctor");
                return StatusCode(500, "An error occurred while deleting entity.");
            }
        }

        private async Task LoadDropdownsAsync()
        {
            var doctors = await _districtDoctorService.GetAllDoctorsAsync();
            ViewBag.Doctors = new SelectList(doctors.Select(d => new { 
                Id = d.Id, 
                DisplayName = d.Person != null ? $"{d.Person.LastName}, {d.Person.FirstName}" + (string.IsNullOrEmpty(d.Person.Patronymic) ? "" : $" {d.Person.Patronymic}") : $"Doctor {d.Id}"
            }), "Id", "DisplayName");
        }
    }
}
