using INSS.Forms.RCL.Models;
using Microsoft.AspNetCore.Mvc;

namespace INSS.Forms.RCL.ViewComponents
{
    public class PreviousButtonViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int currentPageIndex, string pageName = "")
        {
            return View(new PreviousButtonModel { CurrentPageIndex = currentPageIndex, PageName = pageName });
        }
    }
}

