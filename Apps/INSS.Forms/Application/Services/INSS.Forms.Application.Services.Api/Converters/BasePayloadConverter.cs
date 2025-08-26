using INSS.Forms.Domain.Models.Abstract;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace INSS.Forms.Application.Services.Api.Converters
{
    /// <summary>
    /// Provides a custom JSON converter for <see cref="FormBase"/> types, supporting polymorphic deserialization
    /// based on a discriminator property ("formType") in the JSON payload.
    /// </summary>
    public class BasePayloadConverter : JsonConverter<FormBase>
    {
        /// <summary>
        /// A mapping of discriminator strings to their corresponding <see cref="Type"/> objects for derived <see cref="FormBase"/> types.
        /// </summary>
        private static readonly Dictionary<string, Type> DiscriminatorTypeMap = GetDiscriminatorTypeMap();

        /// <summary>
        /// Builds a dictionary mapping discriminator strings to derived <see cref="FormBase"/> types.
        /// </summary>
        /// <returns>
        /// A dictionary where the key is the discriminator string and the value is the corresponding <see cref="Type"/>.
        /// </returns>
        private static Dictionary<string, Type> GetDiscriminatorTypeMap()
        {
            var baseType = typeof(FormBase);
            var assembly = baseType.Assembly;

            // Find all non-abstract types that inherit from FormBase
            var derivedTypes = assembly.GetTypes()
                .Where(t => baseType.IsAssignableFrom(t) && t != baseType && !t.IsAbstract);

            var map = new Dictionary<string, Type>();

            foreach (var type in derivedTypes)
            {
                // Use the full type name as the discriminator (adjust if you use a different convention)
                var discriminator = type.FullName + ", " + type.Assembly.GetName().Name + ", Version=" +
                                    type.Assembly.GetName().Version + ", Culture=neutral, PublicKeyToken=null";

                map[discriminator] = type;
            }

            return map;
        }

        /// <summary>
        /// Reads and deserializes a <see cref="FormBase"/> object from JSON, using the "formType" discriminator property
        /// to determine the concrete type.
        /// </summary>
        /// <param name="reader">The <see cref="Utf8JsonReader"/> to read from.</param>
        /// <param name="typeToConvert">The type to convert (ignored).</param>
        /// <param name="options">The serializer options.</param>
        /// <returns>
        /// The deserialized <see cref="FormBase"/> instance, or <c>null</c> if deserialization fails.
        /// </returns>
        /// <exception cref="JsonException">
        /// Thrown if the discriminator property is missing or the type is unknown.
        /// </exception>
        public override FormBase? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var doc = JsonDocument.ParseValue(ref reader);
            var root = doc.RootElement;

            if (!root.TryGetProperty("formType", out var typeProp))
                throw new JsonException("Missing discriminator: formType");

            var typeString = typeProp.GetString();

            if (typeString == null || !DiscriminatorTypeMap.TryGetValue(typeString, out var targetType))
                throw new JsonException($"Unknown Type {typeString}");

            return (FormBase?)root.Deserialize(targetType, options);
        }

        /// <summary>
        /// Serializes a <see cref="FormBase"/> object to JSON, using its runtime type.
        /// </summary>
        /// <param name="writer">The <see cref="Utf8JsonWriter"/> to write to.</param>
        /// <param name="value">The <see cref="FormBase"/> value to serialize.</param>
        /// <param name="options">The serializer options.</param>
        public override void Write(Utf8JsonWriter writer, FormBase value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}
