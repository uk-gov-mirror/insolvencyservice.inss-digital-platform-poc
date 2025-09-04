using INSS.Forms.RCL.Models;
using Microsoft.AspNetCore.Mvc;

namespace INSS.Forms.RCL.ViewComponents
{
    public class SummaryRowViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string name, string text, string ariaLabel, string[] values, int itemIndex, int pageIndex)
        {
            return View(new RowModel { Name = name, Text = text , AriaLabel = ariaLabel, Values = values, ItemIndex = itemIndex, PageIndex = pageIndex });
        }
    }
}
