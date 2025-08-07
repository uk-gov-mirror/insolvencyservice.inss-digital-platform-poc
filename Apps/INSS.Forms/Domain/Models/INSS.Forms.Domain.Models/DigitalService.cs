using System.ComponentModel.DataAnnotations;

namespace INSS.Forms.Domain.Models
{
    /// <summary>
    /// Represents a digital service with a unique identifier, name, title, and associated steps.
    /// </summary>
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
        /// Gets or sets the collection of steps associated with this digital service.
        /// </summary>
        public List<DigitalServiceStep> Steps { get; set; } = new();
    }
}
