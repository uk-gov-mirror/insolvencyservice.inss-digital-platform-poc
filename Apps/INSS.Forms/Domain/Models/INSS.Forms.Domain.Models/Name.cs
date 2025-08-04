using System.ComponentModel.DataAnnotations;

namespace INSS.Forms.Domain.Models
{
    /// <summary>
    /// Represents a person's name with first and last name properties.
    /// </summary>
    public class Name
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [MaxLength(255)]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [MaxLength(255)]
        public string LastName { get; set; } = string.Empty;
    }
}
