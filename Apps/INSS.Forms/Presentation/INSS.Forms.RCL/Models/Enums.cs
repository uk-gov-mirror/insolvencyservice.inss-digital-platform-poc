namespace INSS.Forms.RCL.Models
{
    /// <summary>
    /// Specifies the type of input component to be rendered.
    /// </summary>
    public enum InputComponentType
    {
        /// <summary>
        /// A standard text input field.
        /// </summary>
        Text,

        /// <summary>
        /// An input field for email addresses.
        /// </summary>
        Email,

        /// <summary>
        /// An input field for telephone numbers.
        /// </summary>
        Telephone,

        /// <summary>
        /// An input field for numeric values.
        /// </summary>
        Number,

        /// <summary>
        /// An input field for currency values.
        /// </summary>
        Currency
    }
}
