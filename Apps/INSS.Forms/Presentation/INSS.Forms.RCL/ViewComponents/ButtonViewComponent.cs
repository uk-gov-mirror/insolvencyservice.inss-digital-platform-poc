using INSS.Forms.RCL.Models;
using Microsoft.AspNetCore.Mvc;

namespace INSS.Forms.RCL.ViewComponents
{
    /// <summary>
    /// A view component for rendering a button using the <see cref="ButtonModel"/>.
    /// </summary>
    public class ButtonViewComponent : ViewComponent
    {
        /// <summary>
        /// Invokes the view component to render a button with the specified model.
        /// </summary>
        /// <param name="model">The <see cref="ButtonModel"/> containing button properties.</param>
        /// <returns>
        /// An <see cref="IViewComponentResult"/> that renders the button view.
        /// </returns>
        public IViewComponentResult Invoke(ButtonModel model)
        {
            return View(model);
        }
    }
}

