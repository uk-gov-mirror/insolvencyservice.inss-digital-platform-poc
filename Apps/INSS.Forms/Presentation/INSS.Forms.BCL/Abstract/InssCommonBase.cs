using INSS.Forms.Domain.Models.Configuration;
using INSS.Forms.Domain.Models.Constants;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;

namespace INSS.Forms.BCL.Abstract
{
    /// <summary>
    /// Provides a base class for Blazor components with common functionality,
    /// including session management, unique element ID/name generation, and
    /// utility methods for value handling.
    /// </summary>
    public abstract class InssCommonBase : ComponentBase
    {
        /// <summary>
        /// Gets or sets the HTTP context accessor for accessing session and request data.
        /// </summary>
        [Inject] protected IHttpContextAccessor HttpContextAccessor { get; set; } = default!;

        /// <summary>
        /// A unique GUID string used to ensure element IDs are unique per component instance.
        /// </summary>
        private readonly string _guid = Guid.NewGuid().ToString("N");

        /// <summary>
        /// Gets or sets the list index of the current list item stored in the session.
        /// </summary>
        public int ListIndex
        {
            get
            {
                var value = HttpContextAccessor.HttpContext!.Session.GetInt32($"{Config.SessionKey}_ListIndex");
                return value ?? 0;
            }
            set
            {
                HttpContextAccessor.HttpContext!.Session.SetInt32($"{Config.SessionKey}_ListIndex", value);
            }
        }

        /// <summary>
        /// Gets or sets the index of the current page stored in the session.
        /// </summary>
        public int PageIndex
        {
            get
            {
                var value = HttpContextAccessor.HttpContext!.Session.GetInt32($"{Config.SessionKey}_PageIndex");
                return value ?? 0;
            }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }

                HttpContextAccessor.HttpContext!.Session.SetInt32($"{Config.SessionKey}_PageIndex", value);
            }
        }

        /// <summary>
        /// Gets or sets the unique name for the component.
        /// This name is used as part of the generated element IDs and names.
        /// </summary>
        [Parameter]
        [EditorRequired] // This makes the parameter mandatory
        public string Name { get; set; } = string.Empty;

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
        protected string GenerateElementId(string? context = null, string? key = null)
        {
            return BuildElementString(context, key, true);
        }

        /// <summary>
        /// Generates an element name using the specified context and key.
        /// </summary>
        /// <param name="context">An optional context string to further qualify the name.</param>
        /// <param name="key">An optional key to further qualify the name.</param>
        /// <returns>An element name string.</returns>
        protected string GenerateElementName(string? context = null, string? key = null)
        {
            return BuildElementString(context, key, false);
        }

        /// <summary>
        /// Returns an array containing the specified value, or an empty array if the value is null or empty.
        /// </summary>
        /// <param name="value">The string value to wrap in an array.</param>
        /// <returns>
        /// An array containing the value if it is not null or empty; otherwise, an empty array.
        /// </returns>
        protected string[] GetValue(string? value)
        {
            return string.IsNullOrEmpty(value) ? [] : [value];
        }

        /// <summary>
        /// Generates an indexed name for an entity by appending the specified index.
        /// </summary>
        /// <param name="entityName">The base name of the entity.</param>
        /// <param name="index">The index to append to the entity name.</param>
        /// <returns>
        /// A string in the format "{entityName}-{index}" representing the indexed entity name.
        /// </returns>
        protected string IndexedName(string entityName, int index)
        {
            return $"{entityName}-{index.ToString()}";
        }

        /// <summary>
        /// Returns the full field name for a property of the <see cref="Form"/>.
        /// </summary>
        /// <param name="propertyName">The name of the property for which to generate the field name.</param>
        /// <returns>
        /// A string representing the full field name in the format "PropertyName".
        /// </returns>
        protected virtual string FieldName(string propertyName)
        {
            return propertyName;
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
            var sb = new System.Text.StringBuilder(Config.AppIdPrefix);
            sb.Append(Name.ToLowerInvariant());

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
