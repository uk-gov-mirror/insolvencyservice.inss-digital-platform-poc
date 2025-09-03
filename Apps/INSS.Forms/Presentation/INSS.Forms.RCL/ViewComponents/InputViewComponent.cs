using INSS.Forms.RCL.Models;
using Microsoft.AspNetCore.Mvc;

public class InputViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(string name, string label, string value, bool smallText = false, string type = "", string placeHolder = "")
    {
        var model = new InputModel
        {
            Name = name,
            Label = label,
            Type = type,
            PlaceHolder = placeHolder,
            Value = value,
            SmallText = smallText
        };
        return View(model);
    }
}
