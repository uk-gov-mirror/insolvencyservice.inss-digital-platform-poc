using INSS.Forms.Components.Helpers;
using INSS.Forms.Components.Models;
using INSS.Forms.Domain.Models.Enums;
using Microsoft.AspNetCore.Components;

namespace INSS.Forms.Components.Abstract
{
    /// <summary>
    /// Abstract base class for list components in Blazor forms.
    /// </summary>
    /// <typeparam name="T">The type of the form data model.</typeparam>
    public abstract class ListBase<T> : InssComponentBase where T : class
    {
        /// <summary>
        /// Gets or sets the form data to list.
        /// </summary>
        [Parameter]
        [EditorRequired]
        public T FormData { get; set; }

        [Parameter]
        [EditorRequired]
        public string AddNewText { get; set; }

        [Parameter]
        [EditorRequired]
        public string AddNewHint { get; set; }

        /// <summary>
        /// Event callback triggered when a new item is added to the list.
        /// </summary>
        [Parameter]
        [EditorRequired]
        public EventCallback<string?> OnAddNewChanged { get; set; }

        /// <summary>
        /// Event callback triggered when the user wants to change an item in the list.
        /// </summary>
        [Parameter]
        [EditorRequired]
        public EventCallback<int> OnChange { get; set; }

        /// <summary>
        /// Event callback triggered when the user wants to remove an item from the list.
        /// </summary>
        [Parameter]
        [EditorRequired]
        public EventCallback<int> OnRemove { get; set; }

        /// <summary>
        /// Gets or sets the value associated with the current selection or input.
        /// </summary>
        [Parameter]
        public string? Value { get; set; }

        /// <summary>
        /// Event callback triggered when the <see cref="Value"/> property changes.
        /// </summary>
        [Parameter]
        public EventCallback<string?> ValueChanged { get; set; }

        /// <summary>
        /// Provides Yes/No radio options for use in list components.
        /// </summary>
        protected List<RadioOption<string>> confirmOptions = EnumHelper.ToRadioOptions<ConfirmType>();

        /// <summary>
        /// Handles the addition of a new item to the list, updating the <see cref="Value"/> property and invoking related callbacks.
        /// </summary>
        /// <param name="value">The value to add as a new item.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected async Task OnAddNew(string? value)
        {
            Value = value;
            await ValueChanged.InvokeAsync(value);
            await OnAddNewChanged.InvokeAsync(value);
        }
    }
}
