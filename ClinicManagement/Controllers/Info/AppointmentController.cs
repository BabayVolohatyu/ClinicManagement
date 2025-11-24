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
    [AppointmentModelValidator]
    public class AppointmentController : CommonController<Appointment>
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IService<Appointment> service, IAppointmentService appointmentService, ILogger<AppointmentController> logger)
            : base(service, logger)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost]
        [Authorize(RoleType.Authorized, RoleType.Operator, RoleType.Admin)]
        public override async Task<IActionResult> Create(Appointment entity)
        {
            if (entity == null)
                return BadRequest("Entity cannot be null.");

            if (entity.DoctorProcedureId == 0)
                ModelState.AddModelError("DoctorProcedureId", "Doctor Procedure is required.");

            if (entity.CabinetId == 0)
                ModelState.AddModelError("CabinetId", "Cabinet is required.");

            if (entity.PatientId == 0)
                ModelState.AddModelError("PatientId", "Patient is required.");

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
                _logger.LogError(ex, "Error creating Appointment");
                await LoadDropdownsAsync();
                ModelState.AddModelError("", "An error occurred while creating the appointment.");
                return View(entity);
            }
        }

        [HttpPost]
        [Authorize(RoleType.Authorized, RoleType.Operator, RoleType.Admin)]
        public override async Task<IActionResult> Update(int id, Appointment entity)
        {
            if (entity == null)
                return BadRequest("Entity cannot be null.");

            if (entity.DoctorProcedureId == 0)
                ModelState.AddModelError("DoctorProcedureId", "Doctor Procedure is required.");

            if (entity.CabinetId == 0)
                ModelState.AddModelError("CabinetId", "Cabinet is required.");

            if (entity.PatientId == 0)
                ModelState.AddModelError("PatientId", "Patient is required.");

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
                _logger.LogWarning(ex, "Appointment with id {Id} not found for update", id);
                await LoadDropdownsAsync();
                var currentEntity = await _service.GetByIdAsync(id) ?? entity;
                ModelState.AddModelError("", ex.Message);
                return View("Entity", currentEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Appointment with id {Id}", id);
                await LoadDropdownsAsync();
                var currentEntity = await _service.GetByIdAsync(id) ?? entity;
                ModelState.AddModelError("", "An error occurred while updating the appointment.");
                return View("Entity", currentEntity);
            }
        }

        protected override async Task LoadDropdownsAsync()
        {
            var doctorProcedures = await _appointmentService.GetAllDoctorProceduresAsync();
            ViewBag.DoctorProcedures = new SelectList(doctorProcedures.Select(dp => new { 
                Id = dp.Id, 
                DisplayName = (dp.Doctor != null && dp.Doctor.Person != null ? $"{dp.Doctor.Person.LastName}, {dp.Doctor.Person.FirstName}" : $"Doctor {dp.DoctorId}") + 
                              " - " + (dp.Procedure != null ? dp.Procedure.Name : $"Procedure {dp.ProcedureId}")
            }), "Id", "DisplayName");

            var cabinets = await _appointmentService.GetAllCabinetsAsync();
            ViewBag.Cabinets = new SelectList(cabinets.Select(c => new { 
                Id = c.Id, 
                DisplayName = $"Building {c.Building}, Floor {c.Floor}, Room {c.Number}" + (c.Type != null ? $" ({c.Type.Type})" : "")
            }), "Id", "DisplayName");

            var patients = await _appointmentService.GetAllPatientsAsync();
            ViewBag.Patients = new SelectList(patients.Select(p => new { 
                Id = p.Id, 
                DisplayName = p.Person != null ? $"{p.Person.LastName}, {p.Person.FirstName}" + (string.IsNullOrEmpty(p.Person.Patronymic) ? "" : $" {p.Person.Patronymic}") : $"Patient {p.Id}"
            }), "Id", "DisplayName");
        }
    }
}
