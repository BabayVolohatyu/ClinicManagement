using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using ClinicManagement.Services;

namespace ClinicManagement.Controllers.Base
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class CommonController<T> : ControllerBase where T : class
    {
        protected readonly IService<T> _service;
        protected readonly ILogger<CommonController<T>> _logger;

        protected CommonController(IService<T> service, ILogger<CommonController<T>> logger)
        {
            _service = service;
            _logger = logger;
        }

        // GET: api/[controller]
        [HttpGet]
        public virtual async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var entities = await _service.GetAllAsync(pageNumber, pageSize);
                return Ok(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching {EntityName} list", typeof(T).Name);
                return StatusCode(500, "An error occurred while fetching data.");
            }
        }

        // GET: api/[controller]/{id}
        [HttpGet("{id:int}")]
        public virtual async Task<IActionResult> Entity(int id)
        {
            try
            {
                var entity = await _service.GetByIdAsync(id);
                if (entity == null)
                    return NotFound($"Entity with id {id} not found.");

                return Ok(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching {EntityName} with id {Id}", typeof(T).Name, id);
                return StatusCode(500, "An error occurred while fetching entity.");
            }
        }

        // POST: api/[controller]
        [HttpPost]
        public virtual async Task<IActionResult> Create([FromBody] T entity)
        {
            if (entity == null)
                return BadRequest("Entity cannot be null.");

            try
            {
                await _service.AddAsync(entity);
                return CreatedAtAction(nameof(Entity), new { id = entity?.GetType().GetProperty("Id")?.GetValue(entity) }, entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating {EntityName}", typeof(T).Name);
                return StatusCode(500, "An error occurred while creating entity.");
            }
        }

        // PUT: api/[controller]/{id}
        [HttpPut("{id:int}")]
        public virtual async Task<IActionResult> Update(int id, [FromBody] T entity)
        {
            if (entity == null)
                return BadRequest("Entity cannot be null.");

            try
            {
                await _service.UpdateAsync(id, entity);
                return NoContent();
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

        // DELETE: api/[controller]/{id}
        [HttpDelete("{id:int}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.RemoveAsync(id);
                return NoContent();
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
