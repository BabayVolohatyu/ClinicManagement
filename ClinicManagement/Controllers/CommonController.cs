using Microsoft.AspNetCore.Mvc;
using ClinicManagement.Services;

namespace ClinicManagement.Controllers.Base
{
    public abstract class CommonController<T> : Controller where T : class
    {
        protected readonly IService<T> _service;
        protected readonly ILogger<CommonController<T>> _logger;

        protected CommonController(IService<T> service, ILogger<CommonController<T>> logger)
        {
            _service = service;
            _logger = logger;
        }

        // GET: /[controller]
        [HttpGet]
        public virtual async Task<IActionResult> Index(
           int pageNumber = 1,
           int pageSize = 10,
           string? searchTerm = null,
           string? sortBy = null,
           bool sortAscending = true)
        {
            try
            {
                var result = await _service.GetAllAsync(pageNumber, pageSize, searchTerm, sortBy, sortAscending);
                return View(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching {EntityName} list", typeof(T).Name);
                return StatusCode(500, "An error occurred while fetching data.");
            }
        }


        // GET: /[controller]/entity/{id}
        [HttpGet]
        public virtual async Task<IActionResult> Entity(int id)
        {
            try
            {
                var entity = await _service.GetByIdAsync(id);
                if (entity == null)
                    return NotFound($"Entity with id {id} not found.");

                return View(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching {EntityName} with id {Id}", typeof(T).Name, id);
                return StatusCode(500, "An error occurred while fetching entity.");
            }
        }

        // GET: /[controller]/create
        [HttpGet]
        public virtual IActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading create form for {EntityName}", typeof(T).Name);
                return StatusCode(500, "An error occurred while loading the create form.");
            }
        }


        // POST: /[controller]/create
        [HttpPost]
        public virtual async Task<IActionResult> Create(T entity)
        {
            if (entity == null)
                return BadRequest("Entity cannot be null.");

            try
            {
                await _service.AddAsync(entity);
                return RedirectToAction(nameof(Entity), new { id = entity?.GetType().GetProperty("Id")?.GetValue(entity) });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating {EntityName}", typeof(T).Name);
                return StatusCode(500, "An error occurred while creating entity.");
            }
        }

        // POST: /[controller]/update/{id}
        [HttpPost]
        public virtual async Task<IActionResult> Update(int id, T entity)
        {
            if (entity == null)
                return BadRequest("Entity cannot be null.");

            try
            {
                await _service.UpdateAsync(id, entity);
                return RedirectToAction(nameof(Entity), new { id = id });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Entity with id {Id} not found for update", id);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating {EntityName} with id {Id}", typeof(T).Name, id);
                return StatusCode(500, "An error occurred while updating entity.");
            }
        }

        // POST: /[controller]/delete/{id}
        [HttpPost]
        public virtual async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Entity with id {Id} not found for delete", id);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting {EntityName} with id {Id}", typeof(T).Name, id);
                return StatusCode(500, "An error occurred while deleting entity.");
            }
        }
    }
}