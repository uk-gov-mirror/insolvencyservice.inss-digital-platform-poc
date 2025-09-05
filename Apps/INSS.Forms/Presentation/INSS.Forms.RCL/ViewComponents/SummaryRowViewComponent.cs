using INSS.Forms.RCL.Models;
using Microsoft.AspNetCore.Mvc;

namespace INSS.Forms.RCL.ViewComponents
{
    /// <summary>
    /// ViewComponent for rendering a summary row in a form.
    /// </summary>
    public class SummaryRowViewComponent : ViewComponent
    {
        /// <summary>
        /// Invokes the view component to render a summary row.
        /// </summary>
        /// <param name="name">The name of the row.</param>
        /// <param name="text">The display text for the row.</param>
        /// <param name="ariaLabel">The ARIA label for accessibility.</param>
        /// <param name="values">The values associated with the row.</param>
        /// <param name="itemIndex">The index of the item within the row.</param>
        /// <param name="pageIndex">The index of the page containing the row.</param>
        /// <returns>An <see cref="IViewComponentResult"/> that renders the summary row.</returns>
        public IViewComponentResult Invoke(string name, string text, string ariaLabel, string[] values, int itemIndex, int pageIndex)
        {
            return View(new RowModel { Name = name, Text = text, AriaLabel = ariaLabel, Values = values, ItemIndex = itemIndex, PageIndex = pageIndex });
        }
    }
}
