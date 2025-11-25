using ClinicManagement.Helpers;
using ClinicManagement.Models.Auth;
using ClinicManagement.Services;
using ClinicManagement.Services.Auth;
using ClinicManagement.Validators.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ClinicManagement.Controllers.Auth
{
    [PasswordChangeRequestModelValidator]
    public class PasswordChangeRequestController : CommonController<PasswordChangeRequest>
    {
        private readonly IPasswordChangeRequestService _passwordChangeRequestService;

        public PasswordChangeRequestController(IService<PasswordChangeRequest> service, IPasswordChangeRequestService passwordChangeRequestService, ILogger<PasswordChangeRequestController> logger)
            : base(service, logger)
        {
            _passwordChangeRequestService = passwordChangeRequestService;
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
                _logger.LogError(ex, "Error fetching PasswordChangeRequest list");
                return StatusCode(500, "An error occurred while fetching data.");
            }
        }

        [HttpPost]
        [Authorize(RoleType.Admin)]
        public override async Task<IActionResult> Update(int id, PasswordChangeRequest entity)
        {
            if (entity == null)
                return BadRequest("Entity cannot be null.");

            var existingEntity = await _service.GetByIdAsync(id);
            if (existingEntity == null)
                return NotFound($"PasswordChangeRequest with id {id} not found.");

            if (entity.UserId == 0)
                ModelState.AddModelError("UserId", "User is required.");

            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                return View("Entity", existingEntity);
            }

            try
            {
                
                if (existingEntity.Status == PasswordChangeStatus.Pending && entity.Status != PasswordChangeStatus.Pending)
                {
                    var currentUserId = GetCurrentUserId();
                    if (currentUserId.HasValue)
                    {
                        entity.ProcessedByAdminId = currentUserId.Value;
                    }
                    entity.ProcessedAt = DateTimeOffset.UtcNow;
                }
                else
                {
                    
                    entity.ProcessedByAdminId = existingEntity.ProcessedByAdminId;
                    entity.ProcessedAt = existingEntity.ProcessedAt;
                }

                await _service.UpdateAsync(id, entity);
                return RedirectToAction(nameof(Entity), new { id });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "PasswordChangeRequest with id {Id} not found for update", id);
                await LoadDropdownsAsync();
                var currentEntity = await _service.GetByIdAsync(id) ?? entity;
                ModelState.AddModelError("", ex.Message);
                return View("Entity", currentEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating PasswordChangeRequest with id {Id}", id);
                await LoadDropdownsAsync();
                var currentEntity = await _service.GetByIdAsync(id) ?? entity;
                ModelState.AddModelError("", "An error occurred while updating the password change request.");
                return View("Entity", currentEntity);
            }
        }

        protected override async Task LoadDropdownsAsync()
        {
            var users = await _passwordChangeRequestService.GetAllUsersAsync();
            ViewBag.Users = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(users.Select(u => new
            {
                Id = u.Id,
                DisplayName = $"{u.LastName ?? ""}, {u.FirstName}" + (string.IsNullOrEmpty(u.MiddleName) ? "" : $" {u.MiddleName}") + $" ({u.Email})"
            }), "Id", "DisplayName");
        }

        private int? GetCurrentUserId()
        {
            try
            {
                var userIdClaim = User?.FindFirst("id") ?? User?.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    return userId;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting current user ID");
            }
            return null;
        }
    }
}

