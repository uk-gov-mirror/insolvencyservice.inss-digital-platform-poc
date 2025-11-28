using System.Text.Json;

namespace INSS.Forms.Analytics.Models
{
    public static class ModelPersistence
    {
        public static T? GetFromSession<T>(ISession session, string sessionKey) where T : class, new()
        {
            var modelJson = session.GetString(sessionKey);
            return modelJson != null
                ? JsonSerializer.Deserialize<T>(modelJson)
                : new T();
        }

        public static void SetToSession<T>(ISession session, string sessionKey, T incoming) where T : class, new()
        {
            var existing = GetFromSession<T>(session, sessionKey) ?? new T();
            var merged = Merge(existing, incoming);
            var modelJson = JsonSerializer.Serialize(merged);
            session.SetString(sessionKey, modelJson);
        }

        public static void Reset(ISession session, string sessionKey)
        {
            session.Remove(sessionKey);
        }

        private static T Merge<T>(T existing, T incoming) where T : class, new()
        {
            if (existing == null) existing = new T();
            if (incoming == null) return existing;

            var type = typeof(T);
            foreach (var prop in type.GetProperties())
            {
                if (!prop.CanRead || !prop.CanWrite) continue;

                var newValue = prop.GetValue(incoming);
                if (newValue != null)
                {
                    if (prop.PropertyType.IsValueType)
                    {
                        var defaultValue = Activator.CreateInstance(prop.PropertyType);
                        if (!newValue.Equals(defaultValue))
                        {
                            prop.SetValue(existing, newValue);
                        }
                    }
                    else
                    {
                        prop.SetValue(existing, newValue);
                    }
                }
            }

            return existing;
        }
    }
}