using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Reflection;

public class ValidateModelFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var modelErrors = new List<string>();
            var propertiesToRemove = new List<string>();

            foreach (var key in context.ModelState.Keys.ToList())
            {
                var entry = context.ModelState[key];

                if (entry.ValidationState == ModelValidationState.Invalid)
                {
                    // Check if this is a navigation property that has a corresponding foreign key
                    if (IsNavigationPropertyError(key, context.ModelState))
                    {
                        propertiesToRemove.Add(key);
                        continue;
                    }

                    // For foreign key properties, ensure they have proper error messages
                    if (IsForeignKeyProperty(key) && entry.Errors.Any(e => e.ErrorMessage.Contains("required")))
                    {
                        var cleanErrorMessage = GetForeignKeyErrorMessage(key);
                        modelErrors.Add(cleanErrorMessage);
                    }
                    else
                    {
                        // For other errors, use the original messages
                        foreach (var error in entry.Errors)
                        {
                            modelErrors.Add($"{GetDisplayName(key)}: {error.ErrorMessage}");
                        }
                    }
                }
            }

            // Remove navigation property errors from ModelState
            foreach (var key in propertiesToRemove)
            {
                context.ModelState.Remove(key);
            }

            if (modelErrors.Any())
            {
                context.Result = new ContentResult
                {
                    Content = "Model is invalid: " + string.Join("; ", modelErrors),
                    StatusCode = 400
                };
            }
            else if (propertiesToRemove.Any() && !modelErrors.Any())
            {
                // If we only had navigation property errors and they've been handled,
                // consider the model valid for controller processing
                context.ModelState.Clear();
            }
        }
    }

    private bool IsNavigationPropertyError(string propertyName, ModelStateDictionary modelState)
    {
        // Navigation properties typically don't have "Id" suffix
        // Check if this might be a navigation property and has a corresponding foreign key
        if (!propertyName.EndsWith("Id") && modelState.Keys.Any(k => k == propertyName + "Id"))
        {
            return true;
        }

        // Common navigation property patterns
        var navigationIndicators = new[] { "Type", "Cabinet", "Patient", "DoctorProcedure", "Diagnosis" };
        return navigationIndicators.Any(indicator =>
            propertyName.Equals(indicator, StringComparison.OrdinalIgnoreCase) ||
            propertyName.EndsWith("." + indicator, StringComparison.OrdinalIgnoreCase));
    }

    private bool IsForeignKeyProperty(string propertyName)
    {
        var foreignKeyProperties = new[] { "TypeId", "CabinetId", "PatientId", "DoctorProcedureId" };
        return foreignKeyProperties.Any(fk => propertyName.Equals(fk, StringComparison.OrdinalIgnoreCase) ||
                                             propertyName.EndsWith("." + fk, StringComparison.OrdinalIgnoreCase));
    }

    private string GetForeignKeyErrorMessage(string propertyName)
    {
        var errorMessages = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["TypeId"] = "Cabinet Type is required",
            ["CabinetId"] = "Cabinet is required",
            ["PatientId"] = "Patient is required",
            ["DoctorProcedureId"] = "Doctor Procedure is required"
        };

        // Extract the base property name if it's nested
        var basePropertyName = propertyName.Split('.').Last();

        return errorMessages.TryGetValue(basePropertyName, out var message)
            ? message
            : $"{basePropertyName} is required";
    }

    private string GetDisplayName(string propertyName)
    {
        var displayNames = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["TypeId"] = "Cabinet Type",
            ["CabinetId"] = "Cabinet",
            ["PatientId"] = "Patient",
            ["DoctorProcedureId"] = "Doctor Procedure",
            ["Building"] = "Building",
            ["Floor"] = "Floor",
            ["Number"] = "Number",
            ["StartTime"] = "Start Time",
            ["EndTime"] = "End Time",
            ["DidItHappen"] = "Did It Happen"
        };

        // Extract the base property name if it's nested
        var basePropertyName = propertyName.Split('.').Last();

        return displayNames.TryGetValue(basePropertyName, out var displayName)
            ? displayName
            : basePropertyName;
    }
}