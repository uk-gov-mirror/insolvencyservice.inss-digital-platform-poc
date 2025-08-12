using System.ComponentModel.DataAnnotations;

namespace INSS.Forms.Domain.Models.Configuration
{
    /// <summary>
    /// Represents a section in a form workflow.
    /// </summary>
    public class Section
    {
        /// <summary>
        /// Gets or sets the unique identifier for the section.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the section.
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the section.
        /// </summary>
        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the collection of forms associated with this section.
        /// </summary>
        public ICollection<SectionForm> SectionForms { get; set; } = [];
    }
}
