using ClinicManagement.Helpers;
using ClinicManagement.Models.Auth;
using ClinicManagement.Data;
using ClinicManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Controllers
{
    [Authorize(RoleType.Operator, RoleType.Admin)]
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

        
        [HttpGet]
        [Route("GetTables")]
        public async Task<IActionResult> GetTables()
        {
            try
            {
                
                var roleIdClaim = User.FindFirst("roleId");
                var isAdmin = roleIdClaim != null && 
                             int.TryParse(roleIdClaim.Value, out int userRoleId) && 
                             (RoleType)userRoleId == RoleType.Admin;

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
                
                
                var restrictedTables = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                {
                    "Roles",
                    "migrations",
                    "__EFMigrationsHistory"
                };

                
                var usersTableName = "Users";
                
                while (await reader.ReadAsync())
                {
                    var tableName = reader.GetString(0);
                    
                    
                    if (restrictedTables.Contains(tableName))
                    {
                        continue;
                    }
                    
                    
                    if (!isAdmin && string.Equals(tableName, usersTableName, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }
                    
                    tables.Add(tableName);
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

                
                var roleIdClaim = User.FindFirst("roleId");
                var isAdmin = roleIdClaim != null && 
                             int.TryParse(roleIdClaim.Value, out int userRoleId) && 
                             (RoleType)userRoleId == RoleType.Admin;

                
                var validationError = ValidateQueryAccess(query, isAdmin);
                if (validationError != null)
                {
                    return Json(new { success = false, error = validationError });
                }

                
                var isSelectQuery = query.TrimStart().StartsWith("SELECT", StringComparison.OrdinalIgnoreCase) ||
                                   query.TrimStart().StartsWith("WITH", StringComparison.OrdinalIgnoreCase);

                if (isSelectQuery)
                {
                    
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

    public class ExecuteQueryRequest
    {
        public string Query { get; set; } = string.Empty;
    }
}

