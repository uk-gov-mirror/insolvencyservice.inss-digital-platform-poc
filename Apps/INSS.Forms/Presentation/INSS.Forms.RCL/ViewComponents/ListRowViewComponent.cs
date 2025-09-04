using INSS.Forms.RCL.Models;
using Microsoft.AspNetCore.Mvc;

namespace INSS.Forms.RCL.ViewComponents
{
    public class ListRowViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string name, string text, string ariaLabel, int itemIndex)
        {
            return View(new RowModel { Name = name, Text = text , AriaLabel = ariaLabel, ItemIndex = itemIndex, Values = { }, PageIndex = 0 });
        }
    }
}
