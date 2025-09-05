using INSS.Forms.RCL.Models;
using Microsoft.AspNetCore.Mvc;

namespace INSS.Forms.RCL.ViewComponents
{
    /// <summary>
    /// A view component for rendering a group of radio buttons.
    /// </summary>
    public class RadioButtonsViewComponent : ViewComponent
    {
        /// <summary>
        /// Invokes the radio buttons view component with the specified parameters.
        /// </summary>
        /// <param name="name">The name attribute for the radio button group.</param>
        /// <param name="text">The display text for the radio button group.</param>
        /// <param name="value">The selected value for the radio button group.</param>
        /// <param name="items">The collection of radio button options.</param>
        /// <param name="hint">Optional hint text to provide additional information about the radio button group.</param>
        /// <param name="inline">Indicates whether the radio buttons should be displayed inline.</param>
        /// <returns>
        /// An <see cref="IViewComponentResult"/> that renders the radio buttons.
        /// </returns>
        public IViewComponentResult Invoke(
            string name,
            string text,
            string value,
            List<RadioOption<string>> items,
            string hint = "",
            bool inline = false)
        {
            var model = new RadioButtonModel
            {
                Name = name,
                Text = text,
                Hint = hint,
                Inline = inline,
                Value = value,
                Items = items
            };

            return View(model);
        }
    }
}