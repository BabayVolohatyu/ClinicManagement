using ClinicManagement.Models;
using ClinicManagement.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ClinicManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PredefinedQueriesService _queriesService;

        public HomeController(ILogger<HomeController> logger, PredefinedQueriesService queriesService)
        {
            _logger = logger;
            _queriesService = queriesService;
        }

        public IActionResult Index()
        {
            var queries = _queriesService.GetQueries();
            return View(queries);
        }

        [HttpGet]
        public IActionResult GetQueryDefinition(string queryKey)
        {
            try
            {
                var queries = _queriesService.GetQueries();
                if (!queries.ContainsKey(queryKey))
                {
                    return Json(new { success = false, error = $"Query key '{queryKey}' not found" });
                }

                var queryDef = queries[queryKey];
                return Json(new 
                { 
                    success = true, 
                    queryDef = new 
                    { 
                        name = queryDef.Name,
                        description = queryDef.Description,
                        requiresParameters = queryDef.RequiresParameters,
                        parameterLabels = queryDef.ParameterLabels ?? Array.Empty<string>()
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting query definition for key: {QueryKey}", queryKey);
                return Json(new { success = false, error = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult RedirectToQueryManager(string queryKey, [FromForm] string[] parameters)
        {
            // Build the URL manually to ensure it works
            var urlHelper = Url;
            var url = urlHelper.Action("Index", "QueryManager", new { queryKey = queryKey });
            
            if (parameters != null && parameters.Length > 0)
            {
                var queryString = string.Join("&", parameters.Select(p => $"parameters={Uri.EscapeDataString(p ?? string.Empty)}"));
                url += "&" + queryString;
            }
            
            return Redirect(url);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
