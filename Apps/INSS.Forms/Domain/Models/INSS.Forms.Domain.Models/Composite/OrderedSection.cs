namespace INSS.Forms.Domain.Models.Composite
{
    /// <summary>
    /// DTO that represents a section with an explicit order, name, and description.
    /// </summary>
    public class OrderedSection
    {
        /// <summary>
        /// Gets or sets the unique identifier for the section.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the section.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the section.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the sort order of the section.
        /// </summary>
        public int SortOrder { get; set; }
    }
}
