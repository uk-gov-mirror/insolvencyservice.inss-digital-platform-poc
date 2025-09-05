namespace INSS.Forms.RCL.Models
{
    /// <summary>
    /// Represents a model for a group of radio buttons, including options and display properties.
    /// </summary>
    public class RadioButtonModel
    {
        /// <summary>
        /// Gets or sets the name attribute for the radio button group.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the display text for the radio button group.
        /// </summary>
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the hint text to provide additional information about the radio button group.
        /// </summary>
        public string Hint { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the radio buttons should be displayed inline.
        /// </summary>
        public bool Inline { get; set; } = false;

        /// <summary>
        /// Gets or sets the selected value for the radio button group.
        /// </summary>
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the collection of radio button options.
        /// </summary>
        public List<RadioOption<string>> Items { get; set; } = new List<RadioOption<string>>();
    }
}
