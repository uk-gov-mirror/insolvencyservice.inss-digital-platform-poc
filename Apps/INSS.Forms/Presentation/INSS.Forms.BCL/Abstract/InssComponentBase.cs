using Microsoft.AspNetCore.Components;

namespace INSS.Forms.BCL.Abstract
{
    /// <summary>
    /// Provides a base class for components that require a unique name and generate element IDs and names.
    /// </summary>
    public abstract class InssComponentBase : ComponentBase
    {
        private const string idPrefix = "inss-form-element-id-";

        /// <summary>
        /// Gets or sets the unique name for the component.
        /// This name is used as part of the generated element IDs and names.
        /// </summary>
        [Parameter]
        [EditorRequired] // This makes the parameter mandatory
        public string Name { get; set; } = string.Empty;

        private readonly string _guid = Guid.NewGuid().ToString("N");

        /// <summary>
        /// Generates a unique element ID using the specified context and key.
        /// </summary>
        /// <param name="context">An optional context string to further qualify the ID.</param>
        /// <param name="key">An optional key to further qualify the ID.</param>
        /// <returns>A unique element ID string.</returns>
        /// <remarks>
        /// Example usages of GenerateElementId and their outputs:

        /// Assume:
        /// Name = "form"
        /// _guid = "a1b2c3d4e5f6g7h8i9j0" (example GUID, actual value will differ per instance)

        /// 1. No context or key
        /// GenerateElementId()
        /// Output: "inss-form-element-id-form-a1b2c3d4e5f6g7h8i9j0"

        /// 2. With context only
        /// GenerateElementId("section1")
        /// Output: "inss-form-element-id-form-section1-a1b2c3d4e5f6g7h8i9j0"

        /// 3. With key only
        /// GenerateElementId(null, "row5")
        /// Output: "inss-form-element-id-form-row5-a1b2c3d4e5f6g7h8i9j0"

        /// 4. With both context and key
        /// GenerateElementId("section1", "row5")
        /// Output: "inss-form-element-id-form-section1-row5-a1b2c3d4e5f6g7h8i9j0"

        /// </remarks>
        /// <param name="context">Optional context string to further qualify the ID.</param>
        /// <param name="key">Optional key string to further qualify the ID.</param>
        public string GenerateElementId(string? context = null, string? key = null)
        {
            return BuildElementString(context, key, true);
        }

        /// <summary>
        /// Generates an element name using the specified context and key.
        /// </summary>
        /// <param name="context">An optional context string to further qualify the name.</param>
        /// <param name="key">An optional key to further qualify the name.</param>
        /// <returns>An element name string.</returns>
        public string GenerateElementName(string? context = null, string? key = null)
        {
            return BuildElementString(context, key, false);
        }

        /// <summary>
        /// Builds the element string for ID or name generation.
        /// </summary>
        /// <param name="context">An optional context string.</param>
        /// <param name="key">An optional key string.</param>
        /// <param name="includeGuid">Whether to include a unique GUID in the result.</param>
        /// <returns>The constructed element string.</returns>
        private string BuildElementString(string? context, string? key, bool includeGuid)
        {
            var sb = new System.Text.StringBuilder(idPrefix);
            sb.Append(Name);

            if (!string.IsNullOrEmpty(context))
            {
                sb.Append('-').Append(context);
            }

            if (!string.IsNullOrEmpty(key))
            {
                sb.Append('-').Append(key);
            }

            if (includeGuid)
            {
                sb.Append('-').Append(_guid);
            }

            return sb.ToString();
        }
    }
}
