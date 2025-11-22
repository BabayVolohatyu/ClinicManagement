using ClinicManagement.Controllers;
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
    [Authorize]
    [PromotionRequestModelValidator]
    public class PromotionRequestController : CommonController<PromotionRequest>
    {
        private readonly IPromotionRequestService _promotionRequestService;

        public PromotionRequestController(
            IService<PromotionRequest> service,
            IPromotionRequestService promotionRequestService,
            ILogger<PromotionRequestController> logger)
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
                ViewData["Entity"] = "promotionrequest";
                var result = await _promotionRequestService.GetAllAsync(pageNumber, pageSize, searchTerm, sortBy, sortAscending);
                return View(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching PromotionRequest list");
                return StatusCode(500, "An error occurred while fetching promotion requests.");
            }
        }

        [HttpGet]
        [Authorize(RoleType.Admin)]
        public override async Task<IActionResult> Entity(int id)
        {
            try
            {
                var request = await _promotionRequestService.GetByIdAsync(id);
                if (request == null)
                    return NotFound($"Promotion request with id {id} not found.");

                return View(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching PromotionRequest with id {Id}", id);
                return StatusCode(500, "An error occurred while fetching promotion request.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> AskPromotion()
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == null)
                    return Unauthorized("User not authenticated.");

                await LoadAvailableRolesAsync(userId.Value);
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading AskPromotion form");
                return StatusCode(500, "An error occurred while loading the promotion request form.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AskPromotion(PromotionRequest request)
        {
            if (request == null)
                return BadRequest("Promotion request cannot be null.");

            var userId = GetCurrentUserId();
            if (userId == null)
                return Unauthorized("User not authenticated.");

            request.UserId = userId.Value;

            if (!ModelState.IsValid)
            {
                await LoadAvailableRolesAsync(userId.Value);
                return View(request);
            }

            try
            {
                await _promotionRequestService.AddAsync(request);
                return RedirectToAction(nameof(MyRequests));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
                await LoadAvailableRolesAsync(userId.Value);
                return View(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating PromotionRequest");
                ModelState.AddModelError("", "An error occurred while creating the promotion request.");
                await LoadAvailableRolesAsync(userId.Value);
                return View(request);
            }
        }

        [HttpGet]
        public async Task<IActionResult> MyRequests(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null,
            string? sortBy = null,
            bool sortAscending = true)
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == null)
                    return Unauthorized("User not authenticated.");

                var result = await _promotionRequestService.GetByUserIdAsync(
                    userId.Value,
                    pageNumber,
                    pageSize,
                    searchTerm,
                    sortBy,
                    sortAscending);

                ViewData["Entity"] = "promotionrequest";
                return View("Index", result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching user's PromotionRequests");
                return StatusCode(500, "An error occurred while fetching your promotion requests.");
            }
        }

        [HttpPost]
        [Authorize(RoleType.Admin)]
        public async Task<IActionResult> Approve(int id)
        {
            try
            {
                var adminId = GetCurrentUserId();
                if (adminId == null)
                    return Unauthorized("Admin not authenticated.");

                await _promotionRequestService.ApprovePromotionRequestAsync(id, adminId.Value);
                return RedirectToAction(nameof(Entity), new { id = id });
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "PromotionRequest with id {Id} not found for approval", id);
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Invalid operation while approving PromotionRequest {Id}", id);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error approving PromotionRequest with id {Id}", id);
                return StatusCode(500, "An error occurred while approving the promotion request.");
            }
        }

        [HttpPost]
        [Authorize(RoleType.Admin)]
        public async Task<IActionResult> Reject(int id)
        {
            try
            {
                var adminId = GetCurrentUserId();
                if (adminId == null)
                    return Unauthorized("Admin not authenticated.");

                await _promotionRequestService.RejectPromotionRequestAsync(id, adminId.Value);
                return RedirectToAction(nameof(Entity), new { id = id });
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "PromotionRequest with id {Id} not found for rejection", id);
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Invalid operation while rejecting PromotionRequest {Id}", id);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error rejecting PromotionRequest with id {Id}", id);
                return StatusCode(500, "An error occurred while rejecting the promotion request.");
            }
        }

        private async Task LoadAvailableRolesAsync(int userId)
        {
            try
            {
                var roles = await _promotionRequestService.GetAvailableRolesAsync(userId);
                ViewBag.AvailableRoles = new SelectList(roles, "Id", "Name");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading available roles for user {UserId}", userId);
                ViewBag.AvailableRoles = new SelectList(Enumerable.Empty<Role>(), "Id", "Name");
            }
        }

        private int? GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("userId");
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
            return null;
        }
    }
}

