using INSS.Forms.Domain.Models.Constants;
using INSS.Forms.Domain.Models.Enums;

namespace INSS.Forms.Components.Abstract
{
    /// <summary>
    /// Provides base functionality for managing a list of form items in a paginated Blazor form.
    /// Supports adding, changing, and removing items, as well as navigation between list-related pages.
    /// </summary>
    /// <typeparam name="TItem">The type of items in the form list.</typeparam>
    public abstract class FormListBase<TItem> : FormPagerBase where TItem : class, new()
    {
        /// <summary>
        /// Gets or sets the current index in the form data list.
        /// </summary>
        protected int ListIndex { get; set; } = 0;

        /// <summary>
        /// Gets or sets the value indicating whether a new item should be added.
        /// </summary>
        protected string? AddNewItemValue { get; set; }

        /// <summary>
        /// Gets or sets the value indicating whether an item removal is confirmed.
        /// </summary>
        protected string? ConfirmValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether an item is currently being changed.
        /// </summary>
        protected bool ChangingItem { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether an item is currently being removed.
        /// </summary>
        protected bool RemovingItem { get; set; } = false;

        /// <summary>
        /// Gets or sets the key value of the entity being acted upon.
        /// </summary>
        protected string KeyEntityValue { get; set; } = string.Empty;

        /// <summary>
        /// Gets a value indicating whether the "Next" button should be disabled.
        /// </summary>
        protected bool IsNextButtonDisabled => AddNewItemValue is null && IsCurrentPage(EntityNames.List);

        /// <summary>
        /// Gets or sets the form data that will be used in the list.
        /// </summary>
        required public IList<TItem> FormData { get; set; }

        /// <summary>
        /// Returns a unique name for an entity based on its index.
        /// </summary>
        /// <param name="entityName">The base name of the entity.</param>
        /// <param name="index">The index of the entity in the list.</param>
        /// <returns>A string representing the indexed entity name.</returns>
        protected string IndexedName(string entityName, int index)
        {
            return $"{entityName}-{index.ToString()}";
        }

        /// <summary>
        /// Skips to the specified index in the form data list and navigates to the given page.
        /// </summary>
        /// <param name="index">The index to skip to.</param>
        /// <param name="page">The name of the page to navigate to.</param>
        protected async Task SkipTo(int index, string page)
        {
            if (index >= 0 && index < FormData.Count)
            {
                ListIndex = index;
                await Next(page).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Handles changes to the confirmation value for removing an item.
        /// </summary>
        /// <param name="value">The new confirmation value.</param>
        protected void ConfirmChanged(string? value)
        {
            ConfirmValue = value;
        }

        /// <summary>
        /// Handles changes to the value indicating whether a new item should be added.
        /// </summary>
        /// <param name="value">The new value for adding a new item.</param>
        protected void AddNewChanged(string? value)
        {
            AddNewItemValue = value;
        }

        /// <summary>
        /// Initiates the process to change an item at the specified index.
        /// Navigates to the change page if the index is valid.
        /// </summary>
        /// <param name="index">The index of the item to change.</param>
        protected async Task ChangeItem(int index)
        {
            if (index >= 0 && index < FormData.Count)
            {
                ChangingItem = true;
                ListIndex = index;
                await Next(EntityNames.ListChange).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Initiates the process to confirm removal of an item at the specified index.
        /// Navigates to the confirmation page.
        /// </summary>
        /// <param name="index">The index of the item to remove.</param>
        /// <param name="entityName">The name of the entity being removed.</param>
        protected async Task ConfirmRemoveItem(int index, string entityName)
        {
            RemovingItem = true;
            ListIndex = index;
            KeyEntityValue = entityName;

            await Next(EntityNames.ConfirmAction).ConfigureAwait(false);
        }

        /// <summary>
        /// Handles navigation and state changes based on the current page and user actions.
        /// Supports adding, changing, and removing items, as well as navigation to summary or other pages.
        /// </summary>
        /// <param name="keyPropertyName">The key property name used for navigation after adding or removing items.</param>
        protected async Task NextHandler(string keyPropertyName)
        {
            ChangingItem = false;

            if (IsCurrentPage(EntityNames.List))
            {
                if (AddNewItemValue == ConfirmType.Yes.ToString())
                {
                    AddNewItemValue = null;
                    FormData.Add(new TItem());
                    ListIndex = FormData.Count - 1;
                    await Next(keyPropertyName).ConfigureAwait(false);
                    return;
                }
                else if (AddNewItemValue == ConfirmType.No.ToString())
                {
                    await Next(EntityNames.Summary).ConfigureAwait(false);
                }

                AddNewItemValue = null;
                return;
            }
            else if (IsCurrentPage(EntityNames.ListChange))
            {
                await Next(EntityNames.List).ConfigureAwait(false);
            }
            else if (IsCurrentPage(EntityNames.ConfirmAction))
            {
                if (ConfirmValue == ConfirmType.Yes.ToString())
                {
                    if (ListIndex >= 0 && ListIndex < FormData.Count)
                    {
                        FormData.RemoveAt(ListIndex);
                        if (ListIndex >= FormData.Count)
                        {
                            ListIndex = FormData.Count - 1;
                        }
                    }

                    if (!FormData.Any())
                    {
                        FormData.Add(new TItem());
                        ListIndex = 0;
                        await Next(keyPropertyName).ConfigureAwait(false);
                    }
                    else
                    {
                        await Next(EntityNames.List).ConfigureAwait(false);
                    }
                }
                else
                {
                    await Next(EntityNames.List).ConfigureAwait(false);
                }

                ConfirmValue = null;
                RemovingItem = false;
            }
            else
            {
                await Next().ConfigureAwait(false);
            }
        }
    }
}
