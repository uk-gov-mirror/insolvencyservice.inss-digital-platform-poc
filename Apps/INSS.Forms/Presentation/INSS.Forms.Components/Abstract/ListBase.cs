using INSS.Forms.Domain.Models;
using Microsoft.AspNetCore.Components;

namespace INSS.Forms.Components.Abstract
{
    /// <summary>
    /// Abstract base class for list components in Blazor forms.
    /// </summary>
    /// <typeparam name="T">The type of the form data model.</typeparam>
    public abstract class ListBase<T> : ComponentBase where T : class
    {
        /// <summary>
        /// Gets or sets the form data to list.
        /// </summary>
        [Parameter]
        [EditorRequired]
        public T FormData { get; set; }

        /// <summary>
        /// Event callback triggered when the user wants to change an item in the list.
        /// </summary>
        [Parameter]
        [EditorRequired]
        public EventCallback<string> OnChange { get; set; }

        /// <summary>
        /// Event callback triggered when the user wants to remove an item from the list.
        /// </summary>
        [Parameter]
        [EditorRequired]
        public EventCallback<string> OnRemove { get; set; }
    }
}
