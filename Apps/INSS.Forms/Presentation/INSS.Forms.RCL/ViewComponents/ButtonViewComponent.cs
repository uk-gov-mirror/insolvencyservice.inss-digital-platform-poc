using INSS.Forms.RCL.Models;
using Microsoft.AspNetCore.Mvc;

namespace INSS.Forms.RCL.ViewComponents
{
    public class ButtonViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ButtonModel model)
        {
            return View(model);
        }
    }
}

