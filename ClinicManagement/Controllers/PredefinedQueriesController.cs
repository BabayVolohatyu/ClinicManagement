using ClinicManagement.Helpers;
using ClinicManagement.Models.Auth;
using ClinicManagement.Data;
using ClinicManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Controllers
{
    [Authorize(RoleType.Authorized, RoleType.Operator, RoleType.Admin)]
    [Route("[controller]")]
    public class PredefinedQueriesController : Controller
    {
        private readonly ClinicDbContext _dbContext;
        private readonly PredefinedQueriesService _queriesService;
        private readonly ILogger<PredefinedQueriesController> _logger;

        public PredefinedQueriesController(
            ClinicDbContext dbContext,
            PredefinedQueriesService queriesService,
            ILogger<PredefinedQueriesController> logger)
        {
            _dbContext = dbContext;
            _queriesService = queriesService;
            _logger = logger;
        }

        
        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index(string queryKey = null, [FromQuery] string[] parameters = null)
        {
            var queries = _queriesService.GetQueries();
            ViewBag.QueryKey = queryKey;
            ViewBag.Parameters = parameters ?? Array.Empty<string>();
            return View(queries);
        }

        
        [HttpGet]
        [Route("List")]
        public IActionResult List()
        {
            var queries = _queriesService.GetQueries();
            var queryList = queries.Select(kvp => new
            {
                key = kvp.Key,
                name = kvp.Value.Name,
                description = kvp.Value.Description,
                requiresParameters = kvp.Value.RequiresParameters,
                parameterLabels = kvp.Value.ParameterLabels ?? Array.Empty<string>()
            }).ToList();

            return Json(new { success = true, queries = queryList });
        }

        
        [HttpGet]
        [Route("GetQueryDefinition")]
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
                        key = queryKey,
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
        [Route("Execute")]
        public async Task<IActionResult> Execute([FromBody] ExecutePredefinedQueryRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.QueryKey))
                {
                    return Json(new { success = false, error = "Query key cannot be empty" });
                }

                
                string query;
                try
                {
                    query = _queriesService.BuildQuery(request.QueryKey, request.Parameters ?? Array.Empty<string>());
                }
                catch (ArgumentException ex)
                {
                    return Json(new { success = false, error = ex.Message });
                }

                if (string.IsNullOrWhiteSpace(query))
                {
                    return Json(new { success = false, error = "Generated query is empty" });
                }

                
                var roleIdClaim = User.FindFirst("roleId");
                var isAdmin = roleIdClaim != null && 
                             int.TryParse(roleIdClaim.Value, out int userRoleId) && 
                             (RoleType)userRoleId == RoleType.Admin;

                
                var validationError = ValidateQueryAccess(query, isAdmin);
                if (validationError != null)
                {
                    return Json(new { success = false, error = validationError });
                }

                
                var trimmedQuery = query.Trim();
                var isSelectQuery = trimmedQuery.StartsWith("SELECT", StringComparison.OrdinalIgnoreCase) ||
                                   trimmedQuery.StartsWith("WITH", StringComparison.OrdinalIgnoreCase);

                if (!isSelectQuery)
                {
                    return Json(new { success = false, error = "Predefined queries can only execute SELECT statements for security reasons." });
                }

                
                var results = new List<Dictionary<string, object>>();
                
                try
                {
                    var connection = _dbContext.Database.GetDbConnection();
                    if (connection.State != System.Data.ConnectionState.Open)
                    {
                        await connection.OpenAsync();
                    }

                    using var command = connection.CreateCommand();
                    command.CommandText = query;

                    using var reader = await command.ExecuteReaderAsync();
                    
                    var columns = new List<string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        columns.Add(reader.GetName(i));
                    }

                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            var row = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                var value = reader.IsDBNull(i) ? null : reader.GetValue(i);
                                row[columns[i]] = value ?? "NULL";
                            }
                            results.Add(row);
                        }
                    }

                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        await connection.CloseAsync();
                    }

                    return Json(new 
                    { 
                        success = true, 
                        results = results, 
                        columns = columns,
                        queryKey = request.QueryKey
                    });
                }
                catch (Exception ex)
                {
                    try
                    {
                        var connection = _dbContext.Database.GetDbConnection();
                        if (connection.State == System.Data.ConnectionState.Open)
                        {
                            await connection.CloseAsync();
                        }
                    }
                    catch { }
                    _logger.LogError(ex, "Error executing predefined query: {QueryKey}", request.QueryKey);
                    return Json(new { success = false, error = $"Error executing query: {ex.Message}" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExecutePredefinedQuery");
                return Json(new { success = false, error = ex.Message });
            }
        }

        
        
        
        private string? ValidateQueryAccess(string query, bool isAdmin)
        {
            
            var restrictedTables = new[] { "Roles", "migrations", "__EFMigrationsHistory" };
            
            
            foreach (var table in restrictedTables)
            {
                var tableUpper = table.ToUpperInvariant();
                var queryUpper = query.ToUpperInvariant();
                
                var patterns = new[]
                {
                    $"\"{table}\"",
                    $"\"{tableUpper}\"",
                    $"\"{table.ToLowerInvariant()}\"",
                    $" {table} ",
                    $" {tableUpper} ",
                    $" {table.ToLowerInvariant()} ",
                    $".{table}",
                    $".{tableUpper}",
                    $"{table}.",
                    $"{tableUpper}.",
                    $"FROM {table}",
                    $"FROM \"{table}\"",
                    $"JOIN {table}",
                    $"JOIN \"{table}\"",
                    $"INTO {table}",
                    $"INTO \"{table}\"",
                    $"UPDATE {table}",
                    $"UPDATE \"{table}\"",
                    $"DELETE FROM {table}",
                    $"DELETE FROM \"{table}\"",
                    $"TABLE {table}",
                    $"TABLE \"{table}\""
                };

                foreach (var pattern in patterns)
                {
                    if (queryUpper.Contains(pattern.ToUpperInvariant(), StringComparison.Ordinal))
                    {
                        return $"Access to table '{table}' is restricted and cannot be used in queries.";
                    }
                }
            }

            
            if (!isAdmin)
            {
                var usersTable = "Users";
                var usersTableUpper = usersTable.ToUpperInvariant();
                var queryUpper = query.ToUpperInvariant();
                
                var usersTablePatterns = new[]
                {
                    $"\"{usersTable}\"",
                    $"\"{usersTableUpper}\"",
                    $"\"users\"",
                    $" {usersTable} ",
                    $" {usersTableUpper} ",
                    $" users ",
                    $".{usersTable}",
                    $".{usersTableUpper}",
                    $"{usersTable}.",
                    $"{usersTableUpper}.",
                    $"FROM {usersTable}",
                    $"FROM \"{usersTable}\"",
                    $"JOIN {usersTable}",
                    $"JOIN \"{usersTable}\"",
                    $"INTO {usersTable}",
                    $"INTO \"{usersTable}\"",
                    $"UPDATE {usersTable}",
                    $"UPDATE \"{usersTable}\"",
                    $"DELETE FROM {usersTable}",
                    $"DELETE FROM \"{usersTable}\"",
                    $"TABLE {usersTable}",
                    $"TABLE \"{usersTable}\""
                };

                foreach (var pattern in usersTablePatterns)
                {
                    if (queryUpper.Contains(pattern.ToUpperInvariant(), StringComparison.Ordinal))
                    {
                        return "Access to table 'Users' is restricted to administrators only.";
                    }
                }
            }

            return null;
        }
    }

    public class ExecutePredefinedQueryRequest
    {
        public string QueryKey { get; set; } = string.Empty;
        public string[]? Parameters { get; set; }
    }
}

