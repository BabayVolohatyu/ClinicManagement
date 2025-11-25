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

        [HttpPost]
        public override async Task<IActionResult> Update(int id, User entity)
        {
            if (entity == null)
                return BadRequest("Entity cannot be null.");

            
            var password = Request.Form["Password"].ToString();
            if (!string.IsNullOrWhiteSpace(password))
            {
                entity.PasswordHash = password; 
            }
            else
            {
                
                ModelState.Remove("PasswordHash");
                entity.PasswordHash = string.Empty; 
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

        protected override async Task LoadDropdownsAsync()
        {
            var roles = await _userService.GetAllRolesAsync();
            ViewBag.Roles = new SelectList(roles, "Id", "Name");
        }
    }
}

