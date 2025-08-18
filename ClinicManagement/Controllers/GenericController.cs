using ClinicManagement.Data;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.Controllers
{
    [Route("[controller]")]
    public class GenericController<T> : Controller where T : class
    {
        protected readonly ClinicDbContext _context;

        public GenericController(ClinicDbContext context) 
        {
            _context = context;
        }

        [HttpGet("")]
        [HttpGet("index")]
        public IActionResult Index()
        {
            var items = _context.Set<T>().ToList();

            var filteredItems = items.Select(item => {
                var dict = new Dictionary<string, object?>();

                foreach (var prop in typeof(T).GetProperties())
                {
                    if (
                    prop.PropertyType.IsPrimitive ||
                    prop.PropertyType == typeof(string) ||
                    Nullable.GetUnderlyingType(prop.PropertyType) != null ||
                    prop.PropertyType == typeof(DateTimeOffset))
                    {
                        dict[prop.Name] = prop.GetValue(item);
                    }
                }

                return dict;
            }).ToList();

            return View(filteredItems);
        }
    }
}
