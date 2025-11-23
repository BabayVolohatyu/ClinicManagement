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
                // Get current user's role
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
                
                // Restricted tables that nobody can access
                var restrictedTables = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                {
                    "Roles",
                    "migrations",
                    "__EFMigrationsHistory"
                };

                // Users table is only available to admin
                var usersTableName = "Users";
                
                while (await reader.ReadAsync())
                {
                    var tableName = reader.GetString(0);
                    
                    // Skip restricted tables for everyone
                    if (restrictedTables.Contains(tableName))
                    {
                        continue;
                    }
                    
                    // Skip users table for non-admin users
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

                // Get current user's role
                var roleIdClaim = User.FindFirst("roleId");
                var isAdmin = roleIdClaim != null && 
                             int.TryParse(roleIdClaim.Value, out int userRoleId) && 
                             (RoleType)userRoleId == RoleType.Admin;

                // Validate query doesn't reference restricted tables
                var validationError = ValidateQueryAccess(query, isAdmin);
                if (validationError != null)
                {
                    return Json(new { success = false, error = validationError });
                }

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

        /// <summary>
        /// Validates that the query doesn't reference restricted tables
        /// </summary>
        private string? ValidateQueryAccess(string query, bool isAdmin)
        {
            // Tables that are restricted for everyone (case-insensitive matching)
            var restrictedTables = new[] { "Roles", "migrations", "__EFMigrationsHistory" };
            
            // Check for restricted tables (case-insensitive)
            foreach (var table in restrictedTables)
            {
                // Check for table references in various SQL contexts
                // PostgreSQL uses double quotes for identifiers, so we check both quoted and unquoted forms
                var tableUpper = table.ToUpperInvariant();
                var queryUpper = query.ToUpperInvariant();
                
                // Patterns to match table references
                var patterns = new[]
                {
                    $"\"{table}\"",           // "Roles"
                    $"\"{tableUpper}\"",       // "ROLES"
                    $"\"{table.ToLowerInvariant()}\"", // "roles"
                    $" {table} ",              //  Roles  (with spaces)
                    $" {tableUpper} ",         //  ROLES  (with spaces)
                    $" {table.ToLowerInvariant()} ", //  roles  (with spaces)
                    $".{table}",              // .Roles
                    $".{tableUpper}",         // .ROLES
                    $"{table}.",              // Roles.
                    $"{tableUpper}.",         // ROLES.
                    $"FROM {table}",          // FROM Roles
                    $"FROM \"{table}\"",      // FROM "Roles"
                    $"JOIN {table}",          // JOIN Roles
                    $"JOIN \"{table}\"",      // JOIN "Roles"
                    $"INTO {table}",          // INTO Roles
                    $"INTO \"{table}\"",      // INTO "Roles"
                    $"UPDATE {table}",        // UPDATE Roles
                    $"UPDATE \"{table}\"",    // UPDATE "Roles"
                    $"DELETE FROM {table}",   // DELETE FROM Roles
                    $"DELETE FROM \"{table}\"", // DELETE FROM "Roles"
                    $"TABLE {table}",         // TABLE Roles
                    $"TABLE \"{table}\""      // TABLE "Roles"
                };

                foreach (var pattern in patterns)
                {
                    if (queryUpper.Contains(pattern.ToUpperInvariant(), StringComparison.Ordinal))
                    {
                        return $"Access to table '{table}' is restricted and cannot be used in queries.";
                    }
                }
            }

            // Users table is only available to admin
            if (!isAdmin)
            {
                var usersTable = "Users";
                var usersTableUpper = usersTable.ToUpperInvariant();
                var queryUpper = query.ToUpperInvariant();
                
                var usersTablePatterns = new[]
                {
                    $"\"{usersTable}\"",           // "Users"
                    $"\"{usersTableUpper}\"",      // "USERS"
                    $"\"users\"",                  // "users"
                    $" {usersTable} ",              //  Users  (with spaces)
                    $" {usersTableUpper} ",        //  USERS  (with spaces)
                    $" users ",                    //  users  (with spaces)
                    $".{usersTable}",              // .Users
                    $".{usersTableUpper}",         // .USERS
                    $"{usersTable}.",              // Users.
                    $"{usersTableUpper}.",         // USERS.
                    $"FROM {usersTable}",          // FROM Users
                    $"FROM \"{usersTable}\"",      // FROM "Users"
                    $"JOIN {usersTable}",          // JOIN Users
                    $"JOIN \"{usersTable}\"",      // JOIN "Users"
                    $"INTO {usersTable}",          // INTO Users
                    $"INTO \"{usersTable}\"",      // INTO "Users"
                    $"UPDATE {usersTable}",        // UPDATE Users
                    $"UPDATE \"{usersTable}\"",    // UPDATE "Users"
                    $"DELETE FROM {usersTable}",   // DELETE FROM Users
                    $"DELETE FROM \"{usersTable}\"", // DELETE FROM "Users"
                    $"TABLE {usersTable}",         // TABLE Users
                    $"TABLE \"{usersTable}\""      // TABLE "Users"
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

