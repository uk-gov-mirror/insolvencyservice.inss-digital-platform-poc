namespace INSS.Forms.Domain.Models.Composite
{
    /// <summary>
    /// DTO that represents a form with an explicit order, name, and URI path.
    /// </summary>
    public class OrderedForm
    {
        /// <summary>
        /// Gets or sets the unique identifier for the form.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the display name of the form.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the URI path associated with the form.
        /// </summary>
        public string UriPath { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the sort order of the form.
        /// </summary>
        public int SortOrder { get; set; }
    }
}
