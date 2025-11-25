using ClinicManagement.Helpers;
using ClinicManagement.Models.Auth;
using ClinicManagement.Services;
using ClinicManagement.Services.Auth;
using ClinicManagement.Validators.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

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
        [Authorize(RoleType.Authorized, RoleType.Operator, RoleType.Admin)]
        public override async Task<IActionResult> Create()
        {
            try
            {
                await LoadDropdownsAsync();
                var currentUserId = GetCurrentUserId();
                if (currentUserId.HasValue)
                {
                    ViewBag.CurrentUserId = currentUserId.Value;
                }
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading create form for PromotionRequest");
                return StatusCode(500, "An error occurred while loading the create form.");
            }
        }

        [HttpPost]
        [Authorize(RoleType.Authorized, RoleType.Operator, RoleType.Admin)]
        public override async Task<IActionResult> Create(PromotionRequest entity)
        {
            if (entity == null)
                return BadRequest("Entity cannot be null.");

            var currentUserId = GetCurrentUserId();
            if (currentUserId.HasValue)
            {
                entity.UserId = currentUserId.Value;
            }
            else
            {
                ModelState.AddModelError("UserId", "Unable to determine current user.");
            }

            if (entity.RequestedRoleId == 0)
                ModelState.AddModelError("RequestedRoleId", "Requested Role is required.");

            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                if (currentUserId.HasValue)
                {
                    ViewBag.CurrentUserId = currentUserId.Value;
                }
                return View(entity);
            }

            try
            {
                
                entity.RequestedAt = DateTimeOffset.UtcNow;
                entity.Status = PromotionStatus.Pending;
                
                
                entity.RequestedRole = null!;
                entity.User = null!;
                entity.ProcessedByAdmin = null;

                _logger.LogInformation("Creating PromotionRequest: UserId={UserId}, RequestedRoleId={RequestedRoleId}, RequestedAt={RequestedAt}, Status={Status}",
                    entity.UserId, entity.RequestedRoleId, entity.RequestedAt, entity.Status);

                await _service.AddAsync(entity);
                
                _logger.LogInformation("PromotionRequest created successfully with Id={Id}", entity.Id);
                return RedirectToAction("Index", "Home");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Invalid operation while creating PromotionRequest: UserId={UserId}, RequestedRoleId={RequestedRoleId}",
                    entity?.UserId, entity?.RequestedRoleId);
                await LoadDropdownsAsync();
                if (currentUserId.HasValue)
                {
                    ViewBag.CurrentUserId = currentUserId.Value;
                }
                ModelState.AddModelError("", ex.Message);
                return View(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating PromotionRequest: UserId={UserId}, RequestedRoleId={RequestedRoleId}, Exception={Exception}",
                    entity?.UserId, entity?.RequestedRoleId, ex.ToString());
                await LoadDropdownsAsync();
                if (currentUserId.HasValue)
                {
                    ViewBag.CurrentUserId = currentUserId.Value;
                }
                ModelState.AddModelError("", $"An error occurred while creating the promotion request: {ex.Message}");
                return View(entity);
            }
        }

        [HttpPost]
        [Authorize(RoleType.Admin)]
        public override async Task<IActionResult> Update(int id, PromotionRequest entity)
        {
            if (entity == null)
                return BadRequest("Entity cannot be null.");

            var existingEntity = await _service.GetByIdAsync(id);
            if (existingEntity == null)
                return NotFound($"PromotionRequest with id {id} not found.");

            if (entity.UserId == 0)
                ModelState.AddModelError("UserId", "User is required.");

            if (entity.RequestedRoleId == 0)
                ModelState.AddModelError("RequestedRoleId", "Requested Role is required.");

            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                return View("Entity", existingEntity);
            }

            try
            {
                
                if (existingEntity.Status == PromotionStatus.Pending && entity.Status != PromotionStatus.Pending)
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

        protected override async Task LoadDropdownsAsync()
        {
            var users = await _promotionRequestService.GetAllUsersAsync();
            ViewBag.Users = new SelectList(users.Select(u => new
            {
                Id = u.Id,
                DisplayName = $"{u.LastName ?? ""}, {u.FirstName}" + (string.IsNullOrEmpty(u.MiddleName) ? "" : $" {u.MiddleName}") + $" ({u.Email})"
            }), "Id", "DisplayName");

            var roles = await _promotionRequestService.GetAllRolesAsync();
            ViewBag.Roles = new SelectList(roles, "Id", "Name");
        }

        private int? GetCurrentUserId()
        {
            try
            {
                _logger.LogInformation("Getting current user ID. IsAuthenticated={IsAuthenticated}, ClaimsCount={ClaimsCount}",
                    User?.Identity?.IsAuthenticated, User?.Claims?.Count() ?? 0);

                var allClaims = User?.Claims?.Select(c => $"{c.Type}={c.Value}").ToList() ?? new List<string>();
                _logger.LogInformation("All claims: {Claims}", string.Join(", ", allClaims));

                var userIdClaim = User?.FindFirst("id") ?? User?.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null)
                {
                    _logger.LogInformation("Found userId claim: {ClaimType}={ClaimValue}", userIdClaim.Type, userIdClaim.Value);
                    if (int.TryParse(userIdClaim.Value, out int userId))
                    {
                        return userId;
                    }
                    else
                    {
                        _logger.LogWarning("Failed to parse userId claim value: {Value}", userIdClaim.Value);
                    }
                }
                else
                {
                    _logger.LogWarning("No userId claim found. Available claim types: {Types}",
                        string.Join(", ", User?.Claims?.Select(c => c.Type) ?? Enumerable.Empty<string>()));
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

