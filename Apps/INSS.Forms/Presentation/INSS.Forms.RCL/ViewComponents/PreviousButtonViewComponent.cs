using Microsoft.AspNetCore.Mvc;

namespace INSS.Forms.RCL.ViewComponents
{
    public class PreviousButtonViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int currentPageIndex)
        {
            return View("Default", currentPageIndex);
        }
    }
}

