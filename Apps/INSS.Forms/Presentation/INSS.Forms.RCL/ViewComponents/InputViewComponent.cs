using INSS.Forms.RCL.Models;
using Microsoft.AspNetCore.Mvc;

public class InputViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(string name, string label, string value, bool smallText = false, InputComponentType inputType = InputComponentType.Text, string placeHolder = "")
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
