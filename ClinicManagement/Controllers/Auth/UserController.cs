using ClinicManagement.Helpers;
using ClinicManagement.Models.Auth;
using ClinicManagement.Services;
using ClinicManagement.Services.Auth;
using ClinicManagement.Validators.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicManagement.Controllers.Auth
{
    [UserModelValidator]
    [Authorize(RoleType.Admin)]
    public class UserController : CommonController<User>
    {
        private readonly IUserService _userService;

        public UserController(IService<User> service, IUserService userService, ILogger<UserController> logger)
            : base(service, logger)
        {
            _userService = userService;
        }

        [HttpGet]
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
                _logger.LogError(ex, "Error fetching User list");
                return StatusCode(500, "An error occurred while fetching data.");
            }
        }

        [HttpGet]
        public override async Task<IActionResult> Create()
        {
            try
            {
                await LoadDropdownsAsync();
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading create form for User");
                return StatusCode(500, "An error occurred while loading the create form.");
            }
        }

        [HttpPost]
        public override async Task<IActionResult> Create(User entity)
        {
            if (entity == null)
                return BadRequest("Entity cannot be null.");

            if (entity.RoleId == 0)
                ModelState.AddModelError("RoleId", "Role is required.");

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
                _logger.LogError(ex, "Error creating User");
                await LoadDropdownsAsync();
                ModelState.AddModelError("", "An error occurred while creating the user.");
                return View(entity);
            }
        }

        [HttpGet]
        public override async Task<IActionResult> Entity(int id)
        {
            try
            {
                var entity = await _service.GetByIdAsync(id);
                if (entity == null)
                    return NotFound($"User with id {id} not found.");

                await LoadDropdownsAsync();
                return View(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching User with id {Id}", id);
                return StatusCode(500, "An error occurred while fetching entity.");
            }
        }

        [HttpPost]
        public override async Task<IActionResult> Update(int id, User entity)
        {
            if (entity == null)
                return BadRequest("Entity cannot be null.");

            // Handle password from form - if Password field is provided, use it; otherwise keep existing
            var password = Request.Form["Password"].ToString();
            if (!string.IsNullOrWhiteSpace(password))
            {
                entity.PasswordHash = password; // Will be hashed in service
            }
            else
            {
                // Remove PasswordHash from model binding to avoid validation error
                ModelState.Remove("PasswordHash");
                entity.PasswordHash = string.Empty; // Will be preserved in service
            }

            if (entity.RoleId == 0)
                ModelState.AddModelError("RoleId", "Role is required.");

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
                _logger.LogWarning(ex, "User with id {Id} not found for update", id);
                await LoadDropdownsAsync();
                var currentEntity = await _service.GetByIdAsync(id) ?? entity;
                ModelState.AddModelError("", ex.Message);
                return View("Entity", currentEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating User with id {Id}", id);
                await LoadDropdownsAsync();
                var currentEntity = await _service.GetByIdAsync(id) ?? entity;
                ModelState.AddModelError("", "An error occurred while updating the user.");
                return View("Entity", currentEntity);
            }
        }

        [HttpPost]
        public override async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "User with id {Id} not found for delete", id);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting User with id {Id}", id);
                return StatusCode(500, "An error occurred while deleting user.");
            }
        }

        private async Task LoadDropdownsAsync()
        {
            var roles = await _userService.GetAllRolesAsync();
            ViewBag.Roles = new SelectList(roles, "Id", "Name");
        }
    }
}

