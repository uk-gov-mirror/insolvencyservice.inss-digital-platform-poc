namespace INSS.Forms.RCL.Models
{
    /// <summary>
    /// Represents a model for a button element with configurable properties.
    /// </summary>
    public class ButtonModel
    {
        /// <summary>
        /// Gets or sets the display text of the button.
        /// </summary>
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the type attribute of the button (e.g., "submit", "button").
        /// </summary>
        public string Type { get; set; } = "submit";

        /// <summary>
        /// Gets or sets the unique identifier for the button.
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the button is disabled.
        /// </summary>
        public bool Disabled { get; set; } = false;
    }
}
