using INSS.Forms.Domain.Models;
using Microsoft.AspNetCore.Components;

namespace INSS.Forms.Components.Abstract
{
    /// <summary>
    /// Abstract base class for summary components in Blazor forms.
    /// </summary>
    /// <typeparam name="T">The type of the form data model.</typeparam>
    public abstract class SummaryBase<T> : InssComponentBase where T : class
    {
        /// <summary>
        /// Gets or sets the form data to be summarized.
        /// </summary>
        [Parameter]
        [EditorRequired]
        public T FormData { get; set; }

        /// <summary>
        /// Event callback triggered when the user proceeds to the next step.
        /// </summary>
        [Parameter]
        public EventCallback<string> OnNext { get; set; }

        /// <summary>
        /// Returns an array containing the specified value, or an empty array if the value is null or empty.
        /// </summary>
        /// <param name="value">The string value to wrap in an array.</param>
        /// <returns>
        /// An array containing the value if it is not null or empty; otherwise, an empty array.
        /// </returns>
        public string[] GetValue(string? value)
        {
            return string.IsNullOrEmpty(value) ? [] : new[] { value };
        }

        public bool IsVisible(Func<bool> isVisible)
        {
            return isVisible != null && isVisible();
        }
    }
}
