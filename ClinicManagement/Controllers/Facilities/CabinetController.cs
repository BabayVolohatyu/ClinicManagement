using ClinicManagement.Controllers.Base;
using ClinicManagement.Helpers;
using ClinicManagement.Models.Auth;
using ClinicManagement.Models.Facilities;
using ClinicManagement.Services;
using ClinicManagement.Services.Facilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicManagement.Controllers.Facilities
{
    public class CabinetController : CommonController<Cabinet>
    {
        private readonly ICabinetService _cabinetService;

        public CabinetController(IService<Cabinet> service, ICabinetService cabinetService, ILogger<CabinetController> logger)
            : base(service, logger)
        {
            _cabinetService = cabinetService;
        }

        // GET: /Cabinet/Create
        [HttpGet]
        [Authorize(RoleType.Authorized, RoleType.Operator, RoleType.Admin)]
        public override async Task<IActionResult> Create()
        {
            try
            {
                await LoadCabinetTypesAsync();
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading create form for Cabinet");
                return StatusCode(500, "An error occurred while loading the create form.");
            }
        }

        // POST: /Cabinet/Create
        [HttpPost]
        [Authorize(RoleType.Authorized, RoleType.Operator, RoleType.Admin)]
        public override async Task<IActionResult> Create(Cabinet entity)
        {
            if (entity == null)
                return BadRequest("Entity cannot be null.");

            // Validate TypeId directly from the model binding
            if (entity.TypeId == 0)
            {
                ModelState.AddModelError("TypeId", "Cabinet Type is required.");
            }

            if (!ModelState.IsValid)
            {
                await LoadCabinetTypesAsync();
                return View(entity);
            }

            try
            {
                await _service.AddAsync(entity);
                return RedirectToAction(nameof(Entity), new { id = entity.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Cabinet");
                await LoadCabinetTypesAsync();
                ModelState.AddModelError("", "An error occurred while creating the cabinet.");
                return View(entity);
            }
        }

        // GET: /Cabinet/Entity/{id}
        public override async Task<IActionResult> Entity(int id)
        {
            try
            {
                var entity = await _service.GetByIdAsync(id);
                if (entity == null)
                    return NotFound($"Cabinet with id {id} not found.");

                await LoadCabinetTypesAsync();
                return View(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching Cabinet with id {Id}", id);
                return StatusCode(500, "An error occurred while fetching entity.");
            }
        }

        // POST: /Cabinet/Update/{id}
        [HttpPost]
        [Authorize(RoleType.Authorized, RoleType.Operator, RoleType.Admin)]
        public override async Task<IActionResult> Update(int id, Cabinet entity)
        {
            if (entity == null)
                return BadRequest("Entity cannot be null.");

            // Validate TypeId directly from the model binding
            if (entity.TypeId == 0)
            {
                ModelState.AddModelError("TypeId", "Cabinet Type is required.");
            }

            if (!ModelState.IsValid)
            {
                await LoadCabinetTypesAsync();
                // Reload entity from DB to ensure latest values in view
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
                _logger.LogWarning(ex, "Cabinet with id {Id} not found for update", id);
                await LoadCabinetTypesAsync();
                var currentEntity = await _service.GetByIdAsync(id) ?? entity;
                ModelState.AddModelError("", ex.Message);
                return View("Entity", currentEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Cabinet with id {Id}", id);
                await LoadCabinetTypesAsync();
                var currentEntity = await _service.GetByIdAsync(id) ?? entity;
                ModelState.AddModelError("", "An error occurred while updating the cabinet.");
                return View("Entity", currentEntity);
            }
        }

        private async Task LoadCabinetTypesAsync()
        {
            var cabinetTypes = await _cabinetService.GetAllTypesAsync();
            ViewBag.CabinetTypes = new SelectList(cabinetTypes, "Id", "Type");
        }
    }
}