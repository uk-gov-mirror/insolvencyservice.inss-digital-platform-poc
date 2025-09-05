using Microsoft.AspNetCore.Mvc;

namespace INSS.Forms.RCL.ViewComponents
{
    /// <summary>
    /// A view component for rendering a heading with the specified text.
    /// </summary>
    public class HeadingViewComponent : ViewComponent
    {
        /// <summary>
        /// Invokes the view component to render the heading.
        /// </summary>
        /// <param name="text">The text to display in the heading.</param>
        /// <returns>
        /// An <see cref="IViewComponentResult"/> that renders the "Default" view with the provided text.
        /// </returns>
        public IViewComponentResult Invoke(string text)
        {
            return View("Default", text);
        }
    }
}

