using Microsoft.AspNetCore.Mvc.ModelBinding;

public static class ModelStateHelpers
{
    /// <summary>
    /// Removes all ModelState entries except those matching the provided property names.
    /// </summary>
    /// <param name="modelState">The ModelStateDictionary to modify.</param>
    /// <param name="propertyNames">The property names to keep (case-insensitive, matches end of key).</param>
    public static void OnlyValidateProperty(ModelStateDictionary modelState, params string[] propertyNames)
    {
        if (propertyNames == null || propertyNames.Length == 0)
            return;

        var keysToRemove = modelState.Keys
            .Where(key => !propertyNames.Any(p => key.EndsWith(p, StringComparison.OrdinalIgnoreCase)))
            .ToList();

        foreach (var key in keysToRemove)
        {
            modelState.Remove(key);
        }
    }

    public static void SetPropertyValueByName(object target, string propertyName, object? value)
    {
        var property = target.GetType().GetProperty(propertyName);
        if (property != null && property.CanWrite)
        {
            property.SetValue(target, value);
        }
    }

}
