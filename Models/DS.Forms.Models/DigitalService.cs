using System.ComponentModel.DataAnnotations;

namespace DS.Forms.Models
{
    /// <summary>
    /// Represents a digital service with a unique identifier and a name.
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
        public string Name { get; set; } = string.Empty;
    }
}
