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
    public class QueryManagerController : Controller
    {
        private readonly ClinicDbContext _dbContext;
        private readonly ILogger<QueryManagerController> _logger;

        public QueryManagerController(
            ClinicDbContext dbContext,
            ILogger<QueryManagerController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        // GET: /QueryManager
        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index(string queryKey = null, [FromQuery] string[] parameters = null)
        {
            string preFilledQuery = string.Empty;
            
            if (!string.IsNullOrEmpty(queryKey))
            {
                try
                {
                    var queriesService = HttpContext.RequestServices.GetRequiredService<PredefinedQueriesService>();
                    preFilledQuery = queriesService.BuildQuery(queryKey, parameters ?? Array.Empty<string>());
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error building query for key: {QueryKey}", queryKey);
                    ViewBag.QueryError = $"Error building query: {ex.Message}";
                }
            }
            
            ViewBag.PreFilledQuery = preFilledQuery;
            return View();
        }

        // GET: /QueryManager/GetTables
        [HttpGet]
        [Route("GetTables")]
        public async Task<IActionResult> GetTables()
        {
            try
            {
                var tables = new List<string>();
                var connection = _dbContext.Database.GetDbConnection();
                
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    await connection.OpenAsync();
                }
                
                using var command = connection.CreateCommand();
                command.CommandText = @"
                    SELECT table_name 
                    FROM information_schema.tables 
                    WHERE table_schema = 'public' 
                    AND table_type = 'BASE TABLE'
                    ORDER BY table_name;";
                
                using var reader = await command.ExecuteReaderAsync();
                
                while (await reader.ReadAsync())
                {
                    tables.Add(reader.GetString(0));
                }
                
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    await connection.CloseAsync();
                }
                
                return Json(new { tables });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting table list");
                return Json(new { tables = new List<string>(), error = ex.Message });
            }
        }

        // POST: /QueryManager/Execute
        [HttpPost]
        [Route("Execute")]
        public async Task<IActionResult> Execute([FromBody] ExecuteQueryRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Query))
                {
                    return Json(new { success = false, error = "Query cannot be empty" });
                }

                var query = request.Query.Trim();

                // Check if it's a SELECT query
                var isSelectQuery = query.TrimStart().StartsWith("SELECT", StringComparison.OrdinalIgnoreCase) ||
                                   query.TrimStart().StartsWith("WITH", StringComparison.OrdinalIgnoreCase);

                if (isSelectQuery)
                {
                    // Execute SELECT query and return results
                    var results = new List<Dictionary<string, object>>();
                    
                    try
                    {
                        using var command = _dbContext.Database.GetDbConnection().CreateCommand();
                        command.CommandText = query;
                        await _dbContext.Database.OpenConnectionAsync();

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

                        await _dbContext.Database.CloseConnectionAsync();

                        return Json(new 
                        { 
                            success = true, 
                            results = results, 
                            columns = columns
                        });
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            await _dbContext.Database.CloseConnectionAsync();
                        }
                        catch { }
                        _logger.LogError(ex, "Error executing SELECT query: {Query}", query);
                        return Json(new { success = false, error = ex.Message });
                    }
                }
                else
                {
                    // Execute non-SELECT queries (INSERT, UPDATE, DELETE, etc.)
                    try
                    {
                        var affectedRows = await _dbContext.Database.ExecuteSqlRawAsync(query);
                        return Json(new 
                        { 
                            success = true, 
                            message = $"Query executed successfully. Affected rows: {affectedRows}",
                            affectedRows = affectedRows
                        });
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error executing non-SELECT query: {Query}", query);
                        return Json(new { success = false, error = ex.Message });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ExecuteQuery");
                return Json(new { success = false, error = ex.Message });
            }
        }
    }

    public class ExecuteQueryRequest
    {
        public string Query { get; set; } = string.Empty;
    }
}

