using ClinicManagement.Data;
using ClinicManagement.Models.Info;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.Controllers
{
    [Route("{modelName}")]
    public class TestController : Controller
    {
        protected readonly ClinicDbContext _context;

        public TestController(ClinicDbContext context) 
        {
            _context = context;
        }
        [HttpGet("")]
        [HttpGet("index")]
        public IActionResult Index(string modelName)
        {
            var dbSetProperty = _context
                .GetType()
                .GetProperties()
                .FirstOrDefault(p => p.Name.Equals(modelName, StringComparison.OrdinalIgnoreCase));
        

            if (dbSetProperty == null) 
            {
                return NotFound($"Haven't found {modelName}");
            }

            var items = ((IQueryable<object>)dbSetProperty.GetValue(_context)).ToList();

            var filteredItems = items.Select(item =>
            {
                var dict = new Dictionary<string, object?>();
                foreach (var prop in item.GetType().GetProperties())
                {
                    if (prop.PropertyType.IsPrimitive
                        || prop.PropertyType == typeof(string)
                        || prop.PropertyType == typeof(DateTimeOffset))
                    {
                        dict[prop.Name] = prop.GetValue(item);
                    }
                }
                return dict;
            }).ToList();

            return View("TestIndex", filteredItems);
        }
    }
}
