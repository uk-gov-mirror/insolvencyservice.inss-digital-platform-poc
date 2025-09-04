using Microsoft.AspNetCore.Mvc;

namespace INSS.Forms.RCL.ViewComponents
{
    public class HeadingViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string text)
        {
            return View("Default", text);
        }
    }
}

