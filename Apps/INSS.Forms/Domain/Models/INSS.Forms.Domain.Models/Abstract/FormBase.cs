using System.ComponentModel.DataAnnotations;

namespace INSS.Forms.Domain.Models.Abstract
{

    /// <summary>
    /// Provides a base class for form models, including common properties such as instance identifier and user.
    /// </summary>
    public abstract class FormBase
    {
        /// <summary>
        /// Gets or sets the unique instance identifier for this general enquiry.
        /// </summary>
        [Required]
        public Guid InstanceId { get; set; }

        /// <summary>
        /// Gets or sets the user associated with this general enquiry.
        /// </summary>
        [Required]
        public User User { get; set; } = new User();
    }
}
