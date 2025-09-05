using INSS.Forms.RCL.Models;
using Microsoft.AspNetCore.Mvc;

namespace INSS.Forms.RCL.ViewComponents
{
    /// <summary>
    /// View component for rendering a previous button in a multi-page form.
    /// </summary>
    public class PreviousButtonViewComponent : ViewComponent
    {
        /// <summary>
        /// Invokes the view component to render the previous button.
        /// </summary>
        /// <param name="currentPageIndex">The index of the current page.</param>
        /// <param name="pageName">The name of the page (optional).</param>
        /// <returns>
        /// An <see cref="IViewComponentResult"/> that renders the previous button view.
        /// </returns>
        public IViewComponentResult Invoke(int currentPageIndex, string pageName = "")
        {
            return View(new PreviousButtonModel { CurrentPageIndex = currentPageIndex, PageName = pageName });
        }
    }
}

