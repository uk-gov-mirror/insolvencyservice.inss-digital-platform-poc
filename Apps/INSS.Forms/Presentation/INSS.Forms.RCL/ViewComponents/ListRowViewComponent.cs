using INSS.Forms.RCL.Models;
using Microsoft.AspNetCore.Mvc;

namespace INSS.Forms.RCL.ViewComponents
{
    /// <summary>
    /// A view component that renders a single row in a list, providing display and accessibility information.
    /// </summary>
    public class ListRowViewComponent : ViewComponent
    {
        /// <summary>
        /// Invokes the view component to render a row with the specified parameters.
        /// </summary>
        /// <param name="name">The name of the row.</param>
        /// <param name="text">The display text for the row.</param>
        /// <param name="ariaLabel">The ARIA label for accessibility purposes.</param>
        /// <param name="itemIndex">The index of the item within the row.</param>
        /// <returns>
        /// An <see cref="IViewComponentResult"/> that renders the row view with the provided model.
        /// </returns>
        public IViewComponentResult Invoke(string name, string text, string ariaLabel, int itemIndex)
        {
            return View(new RowModel { Name = name, Text = text, AriaLabel = ariaLabel, ItemIndex = itemIndex, Values = { }, PageIndex = 0 });
        }
    }
}
