using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ClinicManagement.Validators
{
    public abstract class ModelValidator : ActionFilterAttribute
    {
        protected abstract Dictionary<string, string> ForeignKeyErrorMappings { get; }
        protected abstract Dictionary<string, string> DisplayNameMappings { get; }
        protected abstract string[] NavigationPropertyIndicators { get; }

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
                        if (IsNavigationPropertyError(key, context.ModelState))
                        {
                            propertiesToRemove.Add(key);
                            continue;
                        }

                        if (IsForeignKeyProperty(key) && entry.Errors.Any(e => e.ErrorMessage.Contains("required")))
                        {
                            var cleanErrorMessage = GetForeignKeyErrorMessage(key);
                            modelErrors.Add(cleanErrorMessage);
                        }
                        else
                        {
                            foreach (var error in entry.Errors)
                            {
                                modelErrors.Add($"{GetDisplayName(key)}: {error.ErrorMessage}");
                            }
                        }
                    }
                }

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
                    context.ModelState.Clear();
                }
            }
        }

        protected virtual bool IsNavigationPropertyError(string propertyName, ModelStateDictionary modelState)
        {
            if (!propertyName.EndsWith("Id") && modelState.Keys.Any(k => k == propertyName + "Id"))
            {
                return true;
            }

            return NavigationPropertyIndicators.Any(indicator =>
                propertyName.Equals(indicator, StringComparison.OrdinalIgnoreCase) ||
                propertyName.EndsWith("." + indicator, StringComparison.OrdinalIgnoreCase));
        }

        protected virtual bool IsForeignKeyProperty(string propertyName)
        {
            var foreignKeyProperties = ForeignKeyErrorMappings.Keys.ToArray();
            return foreignKeyProperties.Any(fk => propertyName.Equals(fk, StringComparison.OrdinalIgnoreCase) ||
                                                 propertyName.EndsWith("." + fk, StringComparison.OrdinalIgnoreCase));
        }

        protected virtual string GetForeignKeyErrorMessage(string propertyName)
        {
            var basePropertyName = propertyName.Split('.').Last();

            return ForeignKeyErrorMappings.TryGetValue(basePropertyName, out var message)
                ? message
                : $"{basePropertyName} is required";
        }

        protected virtual string GetDisplayName(string propertyName)
        {
            var basePropertyName = propertyName.Split('.').Last();

            return DisplayNameMappings.TryGetValue(basePropertyName, out var displayName)
                ? displayName
                : basePropertyName;
        }
    }
}
