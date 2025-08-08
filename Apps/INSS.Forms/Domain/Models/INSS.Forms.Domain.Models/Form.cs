using System.ComponentModel.DataAnnotations;

namespace INSS.Forms.Domain.Models
{
    /// <summary>
    /// Represents the base class for all forms.
    /// </summary>
    public class Form
    {
        /// <summary>
        /// Gets or sets the unique identifier for the form.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the form.
        /// </summary>
        [MaxLength(255)]
        [Required]
        public string FormName { get; set; } = string.Empty;
    }
}
