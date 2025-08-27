using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INSS.Forms.Domain.Models.Configuration
{
    /// <summary>
    /// Represents a digital service with a unique identifier, name, title, and associated sections.
    /// </summary>
    [Table("digital_service")]
    public class DigitalService
    {
        /// <summary>
        /// Gets or sets the unique identifier for the digital service.
        /// </summary>
        [Key]
        public required Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the digital service.
        /// </summary>
        [MaxLength(255)]
        [Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the title of the digital service.
        /// </summary>
        [MaxLength(255)]
        [Required]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the collection of form sections associated with this digital service.
        /// </summary>
        public List<DigitalServiceSection> DigitalServiceSections { get; set; } = new();
    }
}
