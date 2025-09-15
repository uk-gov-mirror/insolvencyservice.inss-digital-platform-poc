using INSS.Forms.Domain.Models.Abstract;
using Microsoft.AspNetCore.Components;

public abstract class InssFormListItemBase<TForm, TFormListItem> : InssFormBase<TForm>
    where TForm : FormBase, new()
    where TFormListItem : class, new()
{
    [SupplyParameterFromForm(FormName = FormName)]
    protected TFormListItem FormListItem { get; set; } = new();

    protected override string FieldName(string propertyName)
    {
        return string.Concat(nameof(FormListItem), ".", propertyName);
    }
}