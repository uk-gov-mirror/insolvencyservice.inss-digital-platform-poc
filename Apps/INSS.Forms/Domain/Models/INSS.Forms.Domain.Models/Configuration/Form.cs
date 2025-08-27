using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INSS.Forms.Domain.Models.Configuration
{
    /// <summary>
    /// Represents the base class for all forms.
    /// </summary>
    [Table("form")]
    public class Form
    {
        /// <summary>
        /// Gets or sets the unique identifier for the form.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the form.
        /// </summary>
        [MaxLength(255)]
        [Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the URI path to the form.
        /// </summary>
        [MaxLength(2048)]
        [Required]
        [Column("uri-path")]
        public string UriPath { get; set; } = string.Empty;
    }
}
