using INSS.Forms.RCL.Models;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// A view component for rendering an input field in a form.
/// </summary>
public class InputViewComponent : ViewComponent
{
    /// <summary>
    /// Invokes the input view component with the specified parameters.
    /// </summary>
    /// <param name="name">The name of the input field.</param>
    /// <param name="label">The label displayed for the input field.</param>
    /// <param name="value">The value of the input field.</param>
    /// <param name="smallText">Indicates whether the input uses small text styling.</param>
    /// <param name="inputType">The type of the input component.</param>
    /// <param name="placeHolder">The placeholder text for the input field.</param>
    /// <returns>
    /// An <see cref="IViewComponentResult"/> that renders the input field.
    /// </returns>
    public IViewComponentResult Invoke(
        string name,
        string label,
        string value,
        bool smallText = false,
        InputComponentType inputType = InputComponentType.Text,
        string placeHolder = "")
    {
        var model = new InputModel
        {
            Name = name,
            Label = label,
            InputType = inputType,
            PlaceHolder = placeHolder,
            Value = value,
            SmallText = smallText
        };
        return View(model);
    }
}
