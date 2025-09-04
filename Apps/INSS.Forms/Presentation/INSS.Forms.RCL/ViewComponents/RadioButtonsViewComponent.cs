using INSS.Forms.RCL.Models;
using Microsoft.AspNetCore.Mvc;

namespace INSS.Forms.RCL.ViewComponents
{
    public class RadioButtonsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string name, string text, string value, List<RadioOption<string>> items, string hint = "", bool inline = false)
        {
            var model = new RadioButtonModel
            {
                Name = name,
                Text = text,
                Hint = hint,
                Inline = inline,
                Value = value,
                Items = items
            };

            return View(model);
        }
    }
}