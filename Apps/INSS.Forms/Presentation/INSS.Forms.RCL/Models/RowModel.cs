namespace INSS.Forms.RCL.Models
{
    /// <summary>
    /// Represents a row model containing display and accessibility information for a form row.
    /// </summary>
    public class RowModel
    {
        /// <summary>
        /// Gets or sets the name of the row.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the display text for the row.
        /// </summary>
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ARIA label for accessibility purposes.
        /// </summary>
        public string AriaLabel { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the values associated with the row.
        /// </summary>
        public string[] Values { get; set; } = { };

        /// <summary>
        /// Gets or sets the index of the item within the row.
        /// </summary>
        public int ItemIndex { get; set; } = 0;

        /// <summary>
        /// Gets or sets the index of the page containing the row.
        /// </summary>
        public int PageIndex { get; set; } = 0;
    }
}
