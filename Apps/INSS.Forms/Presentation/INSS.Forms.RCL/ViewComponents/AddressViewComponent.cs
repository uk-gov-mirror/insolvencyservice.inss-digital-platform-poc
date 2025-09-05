using INSS.Forms.Domain.Models.Composite;
using Microsoft.AspNetCore.Mvc;

namespace INSS.Forms.RCL.ViewComponents
{
    /// <summary>
    /// View component for rendering an <see cref="Address"/> model.
    /// </summary>
    public class AddressViewComponent : ViewComponent
    {
        /// <summary>
        /// Invokes the view component to render the specified <see cref="Address"/> model.
        /// </summary>
        /// <param name="model">The <see cref="Address"/> model to render.</param>
        /// <returns>
        /// An <see cref="IViewComponentResult"/> that renders the view for the address.
        /// </returns>
        public IViewComponentResult Invoke(Address model)
        {
            return View(model);
        }
    }
}


