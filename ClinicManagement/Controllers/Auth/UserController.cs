using ClinicManagement.Controllers;
using ClinicManagement.Helpers;
using ClinicManagement.Models.Auth;
using ClinicManagement.Services;
using ClinicManagement.Services.Auth;
using ClinicManagement.Validators.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicManagement.Controllers.Auth
{
    [Authorize(RoleType.Admin)]
    [UserModelValidator]
    public class UserController : CommonController<User>
    {
        private readonly IUserService _userService;

        public UserController(IService<User> service, IUserService userService, ILogger<UserController> logger)
            : base(service, logger)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(RoleType.Admin)]
        public override async Task<IActionResult> Entity(int id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);
                if (user == null)
                    return NotFound($"User with id {id} not found.");

                await LoadRolesAsync();
                return View(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching User with id {Id}", id);
                return StatusCode(500, "An error occurred while fetching user.");
            }
        }

        [HttpGet]
        [Authorize(RoleType.Admin)]
        public override async Task<IActionResult> Create()
        {
            try
            {
                await LoadRolesAsync();
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading create form for User");
                return StatusCode(500, "An error occurred while loading the create form.");
            }
        }

        [HttpPost]
        [Authorize(RoleType.Admin)]
        public async Task<IActionResult> Create(User user, string password)
        {
            if (user == null)
                return BadRequest("User cannot be null.");

            if (string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError("Password", "Password is required.");
            }

            if (!ModelState.IsValid)
            {
                await LoadRolesAsync();
                return View(user);
            }

            try
            {
                user.PasswordHash = password;
                await _userService.AddAsync(user);
                return RedirectToAction(nameof(Entity), new { id = user.Id });
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
                await LoadRolesAsync();
                return View(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating User");
                ModelState.AddModelError("", "An error occurred while creating the user.");
                await LoadRolesAsync();
                return View(user);
            }
        }

        [HttpPost]
        [Authorize(RoleType.Admin)]
        public override async Task<IActionResult> Update(int id, User user)
        {
            if (user == null)
                return BadRequest("User cannot be null.");

            try
            {
                var existingUser = await _userService.GetByIdAsync(id);
                if (existingUser == null)
                    return NotFound($"User with id {id} not found.");

                // Preserve password hash if not changed
                user.PasswordHash = existingUser.PasswordHash;
                user.CreatedAt = existingUser.CreatedAt;

                await _userService.UpdateAsync(id, user);
                return RedirectToAction(nameof(Entity), new { id = id });
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "User with id {Id} not found for update", id);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating User with id {Id}", id);
                ModelState.AddModelError("", "An error occurred while updating user.");
                await LoadRolesAsync();
                return View("Entity", user);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(int id, int roleId)
        {
            try
            {
                await _userService.UpdateUserRoleAsync(id, roleId);
                return RedirectToAction(nameof(Entity), new { id = id });
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "User or Role not found for role update");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating role for User with id {Id}", id);
                return StatusCode(500, "An error occurred while updating user role.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePassword(int id, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword))
            {
                return BadRequest("Password cannot be empty.");
            }

            try
            {
                await _userService.UpdateUserPasswordAsync(id, newPassword);
                return RedirectToAction(nameof(Entity), new { id = id });
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "User not found for password update");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating password for User with id {Id}", id);
                return StatusCode(500, "An error occurred while updating password.");
            }
        }

        [HttpPost]
        public override async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _userService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException ex)
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

        private async Task LoadRolesAsync()
        {
            var roles = await _userService.GetAllRolesAsync();
            ViewBag.Roles = new SelectList(roles, "Id", "Name");
        }
    }
}

