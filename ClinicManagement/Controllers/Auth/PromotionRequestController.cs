using ClinicManagement.Helpers;
using ClinicManagement.Models.Auth;
using ClinicManagement.Services;
using ClinicManagement.Services.Auth;
using ClinicManagement.Validators.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicManagement.Controllers.Auth
{
    [PromotionRequestModelValidator]
    public class PromotionRequestController : CommonController<PromotionRequest>
    {
        private readonly IPromotionRequestService _promotionRequestService;

        public PromotionRequestController(IService<PromotionRequest> service, IPromotionRequestService promotionRequestService, ILogger<PromotionRequestController> logger)
            : base(service, logger)
        {
            _promotionRequestService = promotionRequestService;
        }

        [HttpGet]
        [Authorize]
        public override async Task<IActionResult> Create()
        {
            try
            {
                await LoadDropdownsAsync();
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading create form for PromotionRequest");
                return StatusCode(500, "An error occurred while loading the create form.");
            }
        }

        [HttpPost]
        [Authorize]
        public override async Task<IActionResult> Create(PromotionRequest entity)
        {
            if (entity == null)
                return BadRequest("Entity cannot be null.");

            if (entity.RequestedRoleId == 0)
                ModelState.AddModelError("RequestedRoleId", "Requested Role is required.");

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
                _logger.LogWarning(ex, "Invalid operation while creating PromotionRequest");
                await LoadDropdownsAsync();
                ModelState.AddModelError("", ex.Message);
                return View(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating PromotionRequest");
                await LoadDropdownsAsync();
                ModelState.AddModelError("", "An error occurred while creating the promotion request.");
                return View(entity);
            }
        }

        [HttpGet]
        [Authorize(RoleType.Admin)]
        public override async Task<IActionResult> Index(
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
                _logger.LogError(ex, "Error fetching PromotionRequest list");
                return StatusCode(500, "An error occurred while fetching data.");
            }
        }

        [HttpGet]
        [Authorize(RoleType.Admin)]
        public override async Task<IActionResult> Entity(int id)
        {
            try
            {
                var entity = await _service.GetByIdAsync(id);
                if (entity == null)
                    return NotFound($"PromotionRequest with id {id} not found.");

                await LoadDropdownsAsync();
                return View(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching PromotionRequest with id {Id}", id);
                return StatusCode(500, "An error occurred while fetching entity.");
            }
        }

        [HttpPost]
        [Authorize(RoleType.Admin)]
        public override async Task<IActionResult> Update(int id, PromotionRequest entity)
        {
            if (entity == null)
                return BadRequest("Entity cannot be null.");

            if (entity.RequestedRoleId == 0)
                ModelState.AddModelError("RequestedRoleId", "Requested Role is required.");

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
                _logger.LogWarning(ex, "PromotionRequest with id {Id} not found for update", id);
                await LoadDropdownsAsync();
                var currentEntity = await _service.GetByIdAsync(id) ?? entity;
                ModelState.AddModelError("", ex.Message);
                return View("Entity", currentEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating PromotionRequest with id {Id}", id);
                await LoadDropdownsAsync();
                var currentEntity = await _service.GetByIdAsync(id) ?? entity;
                ModelState.AddModelError("", "An error occurred while updating the promotion request.");
                return View("Entity", currentEntity);
            }
        }

        private async Task LoadDropdownsAsync()
        {
            var users = await _promotionRequestService.GetAllUsersAsync();
            ViewBag.Users = new SelectList(users.Select(u => new { 
                Id = u.Id, 
                DisplayName = $"{u.LastName ?? ""}, {u.FirstName}" + (string.IsNullOrEmpty(u.MiddleName) ? "" : $" {u.MiddleName}") + $" ({u.Email})"
            }), "Id", "DisplayName");

            var roles = await _promotionRequestService.GetAllRolesAsync();
            ViewBag.Roles = new SelectList(roles, "Id", "Name");
        }
    }
}

