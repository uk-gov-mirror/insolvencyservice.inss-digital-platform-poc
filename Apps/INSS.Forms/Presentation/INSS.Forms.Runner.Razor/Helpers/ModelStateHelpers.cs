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

    public static void OnlyValidateCollectionProperty(ModelStateDictionary modelState, string collectionPropertyName, int collectionIndex, params string[] propertyNames)
    {
        if (string.IsNullOrEmpty(collectionPropertyName) || propertyNames == null || propertyNames.Length == 0)
            return;

        // Example key: "CollectionPropertyName[0].PropertyName"
        var keysToKeep = propertyNames
            .Select(p => $"{collectionPropertyName}[{collectionIndex}].{p}")
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        var keysToRemove = modelState.Keys
            .Where(key => !keysToKeep.Any(keepKey => key.EndsWith(keepKey, StringComparison.OrdinalIgnoreCase)))
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

    public static void SetCollectionPropertyValueByName(object target, string collectionPropertyName, int collectionIndex, string propertyName, object? value)
    {
        var collectionProperty = target.GetType().GetProperty(collectionPropertyName);
        if (collectionProperty != null && collectionProperty.CanRead)
        {
            var collection = collectionProperty.GetValue(target) as System.Collections.IList;
            if (collection != null && collectionIndex >= 0 && collectionIndex < collection.Count)
            {
                var item = collection[collectionIndex];
                var itemProperty = item?.GetType()?.GetProperty(propertyName);
                if (itemProperty != null && itemProperty.CanWrite)
                {
                    itemProperty.SetValue(item, value);
                }
            }
        }
    }

}
