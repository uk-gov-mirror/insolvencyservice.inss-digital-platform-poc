using INSS.Forms.BCL.Services;
using INSS.Forms.Domain.Models.Abstract;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

/// <summary>
/// Provides a base Blazor component for handling list items within a form, including property validation and assignment.
/// </summary>
/// <typeparam name="TForm">The type of the main form model, which must inherit from <see cref="FormBase"/> and have a parameterless constructor.</typeparam>
/// <typeparam name="TFormListItem">The type of the list item model, which must be a reference type with a parameterless constructor.</typeparam>
public abstract class InssFormListItemBase<TForm, TFormListItem> : InssFormBase<TForm>
    where TForm : FormBase, new()
    where TFormListItem : class, new()
{
    /// <summary>
    /// Gets or sets the list item model bound to the form.
    /// </summary>
    [SupplyParameterFromForm(FormName = FormName)]
    protected TFormListItem FormListItem { get; set; } = new();

    /// <summary>
    /// Validates a single property of a list item and, if valid, assigns its value to the persisted list item instance.
    /// Updates the <see cref="ValidationMessageStore"/> for the current <see cref="EditContext"/>.
    /// </summary>
    /// <param name="itemToValidate">The list item instance containing the property to validate.</param>
    /// <param name="itemToPersist">The list item instance to which the property value will be assigned if valid.</param>
    /// <param name="property">The name of the property to validate and assign.</param>
    /// <returns>
    /// <c>true</c> if the property is valid and assigned; otherwise, <c>false</c>.
    /// </returns>
    protected bool ValidateAndAssignProperty(TFormListItem itemToValidate, TFormListItem itemToPersist, string property)
    {
        var errors = PropertyValidator.ValidateProperties(itemToValidate, [property]);
        if (errors.Any())
        {
            var validationMessageStore = new ValidationMessageStore(CurrentEditContext);

            foreach (var error in errors)
            {
                var fieldIdentifier = new FieldIdentifier(FormListItem, error.MemberNames.First());
                validationMessageStore.Add(fieldIdentifier, error.ErrorMessage!);
            }

            CurrentEditContext.NotifyValidationStateChanged();
            FormListItem = itemToValidate;
            return false;
        }

        var value = itemToValidate.GetType().GetProperty(property)?.GetValue(itemToValidate);
        SetPropertyValueByName(itemToPersist, property, value);

        return true;
    }

    /// <summary>
    /// Returns the full field name for a property of the <see cref="FormListItem"/>.
    /// </summary>
    /// <param name="propertyName">The name of the property for which to generate the field name.</param>
    /// <returns>
    /// A string representing the full field name in the format "FormListItem.PropertyName".
    /// </returns>
    protected override string FieldName(string propertyName)
    {
        return string.Concat(nameof(FormListItem), ".", propertyName);
    }
}