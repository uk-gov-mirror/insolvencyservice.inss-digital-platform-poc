namespace INSS.Forms.RCL.Models
{
    /// <summary>
    /// Represents the model for a previous button in a multi-page form.
    /// </summary>
    public class PreviousButtonModel
    {
        /// <summary>
        /// Gets or sets the index of the current page.
        /// </summary>
        public int CurrentPageIndex { get; set; }

        /// <summary>
        /// Gets or sets the name of the page.
        /// </summary>
        public string PageName { get; set; } = string.Empty;
    }
}
