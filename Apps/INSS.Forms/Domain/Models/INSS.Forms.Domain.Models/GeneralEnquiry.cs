using System.ComponentModel.DataAnnotations;

namespace INSS.Forms.Domain.Models
{
    /// <summary>
    /// Represents a general enquiry form, including user, name, and address details.
    /// </summary>
    public class GeneralEnquiry : Form
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

        /// <summary>
        /// Gets or sets the name details for the general enquiry.
        /// </summary>
        public Name Name { get; set; } = new Name();

        /// <summary>
        /// Gets or sets the address details for the general enquiry.
        /// </summary>
        public Address Address { get; set; } = new Address();
    }
}
