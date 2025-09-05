namespace INSS.Forms.RCL.Models
{
    /// <summary>
    /// Represents the model for an input component in a form.
    /// </summary>
    public class InputModel
    {
        /// <summary>
        /// Gets or sets the name of the input field.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the label displayed for the input field.
        /// </summary>
        public string Label { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the value of the input field.
        /// </summary>
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the placeholder text for the input field.
        /// </summary>
        public string PlaceHolder { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the type of the input component.
        /// </summary>
        public InputComponentType InputType { get; set; } = InputComponentType.Text;

        /// <summary>
        /// Gets or sets a value indicating whether the input uses small text styling.
        /// </summary>
        public bool SmallText { get; set; } = false;
    }
}
